using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using IntemBB.Web.Data;
using IntemBB.Web.Models;
using IntemBB.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IntemBB.Web.Controllers;

[Authorize]
public class IntemBBController : Controller
{
    private readonly SqlServerHelper _sqlHelper;
    private readonly NetworkService _networkService;
    private readonly ExcelService _excelService;
    private readonly MaterialResourceService _materialService;
    private readonly IConfiguration _configuration;

    public IntemBBController(
        SqlServerHelper sqlHelper,
        NetworkService networkService,
        ExcelService excelService,
        MaterialResourceService materialService,
        IConfiguration configuration)
    {
        _sqlHelper = sqlHelper;
        _networkService = networkService;
        _excelService = excelService;
        _materialService = materialService;
        _configuration = configuration;
    }

    private string GetUserId()
    {
        return User.FindFirst("UserId")?.Value ?? "";
    }

    private string FormatTime(object? value)
    {
        if (value == null || value == DBNull.Value) return "";
        if (value is DateTime dt) return dt.ToString("HH:mm:ss");
        if (DateTime.TryParse(value.ToString(), out var parsed)) return parsed.ToString("HH:mm:ss");
        return "";
    }

    private DateTime? ParseDateTime(object? value)
    {
        if (value == null || value == DBNull.Value) return null;
        if (value is DateTime dt) return dt;
        if (DateTime.TryParse(value.ToString(), out var parsed)) return parsed;
        return null;
    }

    private string FormatRecipeDisplay(DataRow row)
    {
        string name = row["Recipe_Name"].ToString() ?? "";
        string startTime = FormatTime(row[1]);
        string endTime = FormatTime(row[2]);
        if (!string.IsNullOrEmpty(startTime) && !string.IsNullOrEmpty(endTime))
            return $"{name} ({startTime} - {endTime})";
        return name;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var model = new IntemBBViewModel
        {
            NguoiThaoTac = GetUserId()
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CheckConnection(IntemBBViewModel model)
    {
        ModelState.Clear();
        if (!string.IsNullOrEmpty(model.SelectedMachine))
        {
            var (success, message) = await _networkService.CheckMachineConnectionAsync(model.SelectedMachine);
            model.ConnectionStatus = message;
        }

        model.NguoiThaoTac = GetUserId();
        return View("Index", model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SelectMachine(IntemBBViewModel model)
    {
        ModelState.Clear();
        model.NguoiThaoTac = GetUserId();

        if (string.IsNullOrEmpty(model.SelectedMachine))
        {
            model.ThongBao = "Chưa chọn máy!!!";
            return View("Index", model);
        }

        // Check connection
        var (success, message) = await _networkService.CheckMachineConnectionAsync(model.SelectedMachine);
        model.ConnectionStatus = message;

        // Determine equip code and label type
        string equipCode;
        switch (model.SelectedMachine)
        {
            case "rdMay1":
                equipCode = "01";
                model.LabelType = "Chất phối hợp";
                break;
            case "rdMay2":
                equipCode = "03";
                model.LabelType = "Chất xúc tiến";
                break;
            case "rdMay02":
                equipCode = "02";
                model.LabelType = "Chất phối hợp";
                break;
            case "rdMay04":
                equipCode = "04";
                model.LabelType = "Chất xúc tiến";
                break;
            default:
                model.ThongBao = "Máy không hợp lệ";
                return View("Index", model);
        }

        // Load recipes
        var recipes = LoadRecipes(model.SoLo, equipCode);
        if (recipes.Rows.Count == 0)
        {
            model.SoLo = DateTime.Now.ToString("yyyy-MM-dd");
            model.ThongBao = "Ngày hiện tại chưa có dữ liệu";
            return View("Index", model);
        }

        model.RecipeList = new List<SelectListItem>();
        for (int i = 0; i < recipes.Rows.Count; i++)
        {
            model.RecipeList.Add(new SelectListItem(
                FormatRecipeDisplay(recipes.Rows[i]),
                recipes.Rows[i]["RowNumber"].ToString()));
        }

        // Set time info from first recipe
        model.ThoiGianKT = FormatTime(recipes.Rows[0][2]);
        model.ThoiGianSX = FormatTime(recipes.Rows[0][1]) + " | " + model.ThoiGianKT;

        // Get valid days
        var validDays = GetValidDays(recipes.Rows[0][0].ToString() ?? "", equipCode);
        var endDate = ParseDateTime(recipes.Rows[0][2]);
        if (validDays > 0 && endDate.HasValue)
        {
            model.NgayHieuLuc = endDate.Value.AddDays(validDays).ToString("yyyy-MM-dd");
        }

        // Get plan info
        var planInfo = GetPlanInfo(model.SoLo, equipCode, recipes.Rows[0][0].ToString() ?? "", model.ThoiGianKT);
        model.PlanId = planInfo.PlanId;
        model.PlanNum = planInfo.PlanNum;
        model.SoMeSX = planInfo.SoMeSX;

        return View("Index", model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SelectRecipe(IntemBBViewModel model)
    {
        ModelState.Clear();
        model.NguoiThaoTac = GetUserId();

        if (string.IsNullOrEmpty(model.SelectedMachine))
        {
            model.ThongBao = "Chưa chọn máy!!!";
            return View("Index", model);
        }

        string equipCode = model.SelectedMachine switch
        {
            "rdMay1" => "01",
            "rdMay2" => "03",
            "rdMay02" => "02",
            "rdMay04" => "04",
            _ => ""
        };

        if (string.IsNullOrEmpty(equipCode)) return View("Index", model);

        // Set label type based on machine
        model.LabelType = (equipCode == "01" || equipCode == "02") ? "Chất phối hợp" : "Chất xúc tiến";

        // Check connection
        var (success, message) = await _networkService.CheckMachineConnectionAsync(model.SelectedMachine);
        model.ConnectionStatus = message;

        // Reload recipes
        var recipes = LoadRecipes(model.SoLo, equipCode);
        if (recipes.Rows.Count == 0)
        {
            model.ThongBao = "Không có dữ liệu";
            return View("Index", model);
        }

        model.RecipeList = new List<SelectListItem>();
        for (int i = 0; i < recipes.Rows.Count; i++)
        {
            var rowNum = recipes.Rows[i]["RowNumber"].ToString();
            model.RecipeList.Add(new SelectListItem(
                FormatRecipeDisplay(recipes.Rows[i]),
                rowNum)
            { Selected = rowNum == model.SelectedRecipe });
        }

        // Find selected recipe row
        int selectedIdx = 0;
        if (int.TryParse(model.SelectedRecipe, out int rowNumber))
        {
            selectedIdx = rowNumber - 1;
            if (selectedIdx < 0 || selectedIdx >= recipes.Rows.Count)
                selectedIdx = 0;
        }

        var selectedRow = recipes.Rows[selectedIdx];
        string recipeName = selectedRow["Recipe_Name"].ToString() ?? "";
        model.ThoiGianKT = FormatTime(selectedRow[2]);
        model.ThoiGianSX = FormatTime(selectedRow[1]) + " | " + model.ThoiGianKT;

        // NgayHieuLuc: original code uses SoLo date when changing recipe
        var validDays = GetValidDays(recipeName, equipCode);
        if (validDays > 0 && DateTime.TryParse(model.SoLo, out var soLoDate))
        {
            model.NgayHieuLuc = soLoDate.AddDays(validDays).ToString("yyyy-MM-dd");
        }

        var planInfo = GetPlanInfo(model.SoLo, equipCode, recipeName, model.ThoiGianKT);
        model.PlanId = planInfo.PlanId;
        model.PlanNum = planInfo.PlanNum;
        model.SoMeSX = planInfo.SoMeSX;

        return View("Index", model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult PrintLabel(IntemBBViewModel model)
    {
        ModelState.Clear();
        model.NguoiThaoTac = GetUserId();

        // Validate inputs
        if (string.IsNullOrEmpty(model.SelectedMachine))
        {
            model.ThongBao = "Chưa chọn máy!!!";
            return View("Index", model);
        }

        if (string.IsNullOrEmpty(model.NguoiThaoTac))
        {
            model.ThongBao = "Không được để trống người thao tác!!!";
            return View("Index", model);
        }

        if (string.IsNullOrEmpty(model.SoMeSX))
        {
            model.ThongBao = "Vui lòng nhập số bao hỏng";
            return View("Index", model);
        }

        if (string.IsNullOrEmpty(model.ThoiGianSX) || string.IsNullOrEmpty(model.SelectedRecipe))
        {
            model.ThongBao = "Vui lòng chọn đầy đủ dữ liệu rồi in";
            return View("Index", model);
        }

        if (!Regex.IsMatch(model.SoMeSX, @"^\d+$") || !Regex.IsMatch(model.PlanNum, @"^\d+$"))
        {
            model.ThongBao = "Chuỗi chứa ký tự không hợp lệ, chỉ được nhập chữ số.";
            return View("Index", model);
        }

        int soMeThucTe = int.Parse(model.SoMeSX);
        int soMeCaiDat = int.Parse(model.PlanNum);
        if (soMeThucTe > soMeCaiDat)
        {
            model.ThongBao = "Số mẻ thực tế không thể nhập lớn hơn số mẻ cài đặt vui lòng kiểm tra";
            return View("Index", model);
        }

        string equipCode = GetEquipCode(model.SelectedMachine);
        if (string.IsNullOrEmpty(equipCode)) return View("Index", model);

        string maybb = model.SelectedMayBB.Length >= 2
            ? model.SelectedMayBB.Substring(model.SelectedMayBB.Length - 2)
            : "01";

        // Determine shift and pday
        string shiftId = GetShift();
        string cainlai = "";
        int ingay = 0;
        string pday = CalculatePday(shiftId, model.SoLo, model.ThoiGianSX, ref cainlai, ref ingay);
        string classs = "";

        DateTime myPday = DateTime.ParseExact(pday, "yyyyMMdd", CultureInfo.InvariantCulture);
        pday = myPday.ToString("yyyyMMdd");
        string checkday = DateTime.Now.ToString("yyyyMMdd");

        if (pday != checkday)
        {
            classs = cainlai;
        }
        else
        {
            if (cainlai == "2")
            {
                if (!string.IsNullOrEmpty(model.ThoiGianSX))
                {
                    string[] arrTG = model.ThoiGianSX.Split('|');
                    if (arrTG.Length >= 1 && TimeSpan.TryParse(arrTG[0].Trim(), out var tspan1))
                    {
                        TimeSpan tspanMocgio2 = new TimeSpan(6, 30, 0);
                        if (tspan1 <= tspanMocgio2) ingay = 1;
                    }
                }
                classs = cainlai;
            }
            else
            {
                classs = shiftId;
            }
        }

        DateTime pday22 = DateTime.ParseExact(pday, "yyyyMMdd", CultureInfo.InvariantCulture);
        pday = pday22.AddDays(-ingay).ToString("yyyyMMdd");

        string slipno = classs + equipCode + "-" + pday.Substring(4, 4);

        // Get recipe name from dropdown
        var recipes = LoadRecipes(model.SoLo, equipCode);
        string recipeName = "";
        if (int.TryParse(model.SelectedRecipe, out int rowNum) && rowNum > 0 && rowNum <= recipes.Rows.Count)
            recipeName = recipes.Rows[rowNum - 1]["Recipe_Name"].ToString() ?? "";

        if (string.IsNullOrEmpty(recipeName))
        {
            model.ThongBao = "Không tìm thấy chất đã chọn";
            return View("Index", model);
        }

        // Determine label names
        string tenbieu, tenbieu1;
        if (equipCode == "01" || equipCode == "02")
        {
            tenbieu = "Thẻ biểu thị Chất phối hợp thuốc";
            tenbieu1 = "藥品配合劑標示卡";
        }
        else
        {
            tenbieu = "Thẻ biểu thị Chất xúc tiến thuốc";
            tenbieu1 = "藥品促進劑標示卡";
        }
        string tenbieu2 = "規格";
        string tenbieu3 = "Quy Cách";

        string planId = "";
        string realNum = "";
        decimal totalWeight = 0;
        decimal weight = 0;
        decimal sokgxuat = 0;

        string totalWeightServer = GetTotalWeightServer(equipCode);

        if (string.IsNullOrEmpty(model.ThoiGianSX))
        {
            // "Cân tay" - manual weighing case
            totalWeight = GetTotalWeight(totalWeightServer, recipeName, "", false);
            int somecantay = int.Parse(model.SoMeSX);
            weight = totalWeight * somecantay;

            planId = "V10" + DateTime.Now.ToString("yyMMdd") + "0001001";
            string somecon = (int.Parse(model.PlanNum) - int.Parse(model.SoMeSX)).ToString();

            InsertTemBarcodeHC(planId, model.SoLo, model.NgayHieuLuc, recipeName,
                maybb, weight, model.NguoiThaoTac, model.SoMeSX, somecon);

            if (_networkService.PingMachine(maybb))
            {
                InsertAutoSmallScanCode(maybb, planId,
                    DateTime.Now.ToString("yyyyMMdd"),
                    DateTime.Now.AddDays(7).ToString("yyyyMMdd"),
                    recipeName, weight);
            }

            model.SoMeSX = somecon;
            model.PlanNum = somecon;
            realNum = "Cân Tay";
        }
        else
        {
            // Normal flow with production data
            string recipeQuery = @"SELECT plan_id, plan_num, End_Date, Real_Num, IF_FLAG
                                  FROM [dbo].[LR_plan]
                                  WHERE CONVERT(varchar(10), Start_Date, 25) = @soLo
                                  AND [Equip_Code] = @equipCode
                                  AND recipe_name = @recipeName
                                  AND [End_Date] != ''
                                  ORDER BY Start_Date";

            var dtrecipe = _sqlHelper.ExecuteQuery("Server33", recipeQuery,
                new Dictionary<string, object>
                {
                    { "soLo", model.SoLo }, { "equipCode", equipCode }, { "recipeName", recipeName }
                });

            if (dtrecipe.Rows.Count > 0)
            {
                totalWeight = GetTotalWeight(totalWeightServer, recipeName, equipCode, true);

                if (dtrecipe.Rows.Count > 1)
                {
                    for (int k = 0; k < dtrecipe.Rows.Count; k++)
                    {
                        if (FormatTime(dtrecipe.Rows[k][2]) == model.ThoiGianKT)
                        {
                            planId = dtrecipe.Rows[k][0].ToString() ?? "";
                        }
                    }
                }
                else
                {
                    planId = dtrecipe.Rows[0][0].ToString() ?? "";
                }

                planId = planId + "901";

                realNum = model.SoMeSX;

                // Check existing weight
                string checkWeightQuery = @"SELECT Weight AS sokg FROM [BB].[dbo].[TemBarcodeHC]
                                           WHERE Plan_Id = @planId AND BB_machno = @maybb
                                           ORDER BY Print_dat DESC";
                var dtSkgx = _sqlHelper.ExecuteQuery("Server33", checkWeightQuery,
                    new Dictionary<string, object> { { "planId", planId }, { "maybb", maybb } });
                if (dtSkgx.Rows.Count > 0)
                {
                    decimal.TryParse(dtSkgx.Rows[0][0]?.ToString()?.Trim(), out sokgxuat);
                }

                int some9 = int.Parse(model.SoMeSX);
                weight = totalWeight * some9 + sokgxuat;

                // Check IF_FLAG for "cân tay" label
                string ifFlag = dtrecipe.Rows[0][4]?.ToString() ?? "";
                if (ifFlag == "4")
                {
                    realNum = model.SoMeSX + "(cân tay)";
                }

                string somecon = (int.Parse(model.PlanNum) - int.Parse(model.SoMeSX)).ToString();

                InsertTemBarcodeHC(planId, model.SoLo, model.NgayHieuLuc, recipeName,
                    maybb, weight, model.NguoiThaoTac, model.SoMeSX, somecon);

                model.SoMeSX = somecon;
                model.PlanNum = somecon;

                if (_networkService.PingMachine(maybb))
                {
                    InsertAutoSmallScanCode(maybb, planId,
                        model.SoLo.Replace("-", ""),
                        model.NgayHieuLuc.Replace("-", ""),
                        recipeName, weight);
                }
            }
        }

        // Insert into IntemHC log
        InsertIntemHC(planId, equipCode, recipeName, weight, model.SoMeSX, realNum, model.NguoiThaoTac);

        // Get OEM
        string oem = GetOEM(planId);

        // Create Excel file
        string dataFolder = Path.Combine(Directory.GetCurrentDirectory(), "Data_HC");
        if (!Directory.Exists(dataFolder)) Directory.CreateDirectory(dataFolder);

        string filename = "_" + model.SelectedMayIn.Trim() + ".xlsx";
        string pathFile = Path.Combine(dataFolder, filename);

        string excelResult = _excelService.CreateExcel(
            equipCode, recipeName, model.SoMeSX, slipno,
            model.ThoiGianSX, model.NgayHieuLuc, model.NguoiThaoTac,
            model.ThoiGianKT, realNum, planId,
            tenbieu, tenbieu1, tenbieu2, tenbieu3, pathFile, oem);

        if (string.IsNullOrEmpty(excelResult))
        {
            model.ThongBao = "Lỗi tạo file Excel";
            ReloadRecipeList(model, equipCode);
            return View("Index", model);
        }

        // Print directly to printer on server
        string printerName = model.SelectedMayIn.Trim();
        var (printSuccess, printMessage) = _excelService.PrintExcel(printerName, pathFile);

        model.ThongBao = printMessage;
        ReloadRecipeList(model, equipCode);
        return View("Index", model);
    }

    private void ReloadRecipeList(IntemBBViewModel model, string equipCode)
    {
        var recipes = LoadRecipes(model.SoLo, equipCode);
        model.RecipeList = new List<SelectListItem>();
        for (int i = 0; i < recipes.Rows.Count; i++)
        {
            model.RecipeList.Add(new SelectListItem(
                FormatRecipeDisplay(recipes.Rows[i]),
                recipes.Rows[i]["RowNumber"].ToString())
            { Selected = recipes.Rows[i]["RowNumber"].ToString() == model.SelectedRecipe });
        }
    }

    private string GetEquipCode(string selectedMachine)
    {
        return selectedMachine switch
        {
            "rdMay1" => "01",
            "rdMay2" => "03",
            "rdMay02" => "02",
            "rdMay04" => "04",
            _ => ""
        };
    }

    private string GetTotalWeightServer(string equipCode)
    {
        return equipCode switch
        {
            "01" => "Server16",
            "02" => "Server17",
            "03" => "Server15",
            "04" => "Server18",
            _ => "Server16"
        };
    }

    private string GetShift()
    {
        DateTime dNow = DateTime.Now;
        DateTime dFrom1 = dNow.Date.Add(new TimeSpan(6, 30, 0));
        DateTime dTo1 = dNow.Date.Add(new TimeSpan(18, 30, 0));
        return (dNow >= dFrom1 && dNow <= dTo1) ? "1" : "2";
    }

    private string CalculatePday(string shift, string soLo, string thoiGianSX,
        ref string cainlai, ref int ingay)
    {
        string sSoLo = soLo.Replace("-", "");
        string sPday = DateTime.Now.ToString("yyyyMMdd");

        if (sPday != sSoLo)
        {
            if (string.IsNullOrEmpty(thoiGianSX))
            {
                cainlai = "";
                return sSoLo;
            }

            string[] arrTG = thoiGianSX.Split('|');
            if (arrTG.Length >= 1 && TimeSpan.TryParse(arrTG[0].Trim(), out var dFrom))
            {
                TimeSpan tsFrom1 = new TimeSpan(6, 30, 0);
                TimeSpan tsTo1 = new TimeSpan(18, 30, 0);
                TimeSpan tsCheck2 = new TimeSpan(6, 30, 0);

                cainlai = (dFrom >= tsFrom1 && dFrom <= tsTo1) ? "1" : "2";

                if (dFrom >= TimeSpan.Zero && dFrom <= tsCheck2) ingay = 1;
            }
            return sSoLo;
        }

        if (!string.IsNullOrEmpty(thoiGianSX))
        {
            string[] arrTG = thoiGianSX.Split('|');
            if (arrTG.Length >= 2 && TimeSpan.TryParse(arrTG[1].Trim(), out var dFrom))
            {
                TimeSpan tsFrom1 = new TimeSpan(6, 30, 0);
                TimeSpan tsTo1 = new TimeSpan(18, 30, 0);
                cainlai = (dFrom >= tsFrom1 && dFrom <= tsTo1) ? "1" : "2";
            }
            return sSoLo;
        }

        if (shift == "2")
        {
            DateTime dFrom = DateTime.Now.Date;
            DateTime dTo = DateTime.Now.Date.Add(new TimeSpan(6, 30, 0));
            if (DateTime.Now >= dFrom && DateTime.Now <= dTo)
            {
                sPday = DateTime.Now.AddDays(-1).ToString("yyyyMMdd");
            }
        }
        return sPday;
    }

    private decimal GetTotalWeight(string serverKey, string recipeName, string equipCode, bool useEquipFilter)
    {
        string query;
        Dictionary<string, object> parameters;

        if (useEquipFilter && !string.IsNullOrEmpty(equipCode))
        {
            query = "SELECT Total_Weight FROM [dbo].[Pmt_recipe] WHERE recipe_name = @recipeName AND [Equip_Code] = @equipCode";
            parameters = new Dictionary<string, object> { { "recipeName", recipeName }, { "equipCode", equipCode } };
        }
        else
        {
            query = "SELECT Total_Weight FROM [dbo].[Pmt_recipe] WHERE recipe_name = @recipeName";
            parameters = new Dictionary<string, object> { { "recipeName", recipeName } };
        }

        var dt = _sqlHelper.ExecuteQuery(serverKey, query, parameters);
        if (dt.Rows.Count > 0 && decimal.TryParse(dt.Rows[0]["Total_Weight"]?.ToString(), out decimal tw))
            return tw;
        return 0;
    }

    private string GetOEM(string planId)
    {
        if (string.IsNullOrEmpty(planId) || planId.Length < 3) return "";
        string basePlanId = planId.Substring(0, planId.Length - 3);

        string query = "SELECT [OEM] FROM [BB].[dbo].[IF_RtPlan2CWSS] WHERE Plan_Id = @planId";
        var dt = _sqlHelper.ExecuteQuery("Server33", query,
            new Dictionary<string, object> { { "planId", basePlanId } });

        if (dt.Rows.Count > 0)
            return dt.Rows[0][0]?.ToString()?.Trim() ?? "";
        return "";
    }

    private void InsertTemBarcodeHC(string planId, string soLo, string ngayHieuLuc,
        string recipeName, string maybb, decimal weight, string username,
        string soMeSX, string somecon)
    {
        string query = @"INSERT INTO [BB].[dbo].[TemBarcodeHC]
            (Plan_Id, Prd_Date, End_Date, Recipe_ID, Equip_Code, Weight, BB_machno, Ursno, Print_dat, print_num, Somecon)
            VALUES (@planId, @prdDate, @endDate, @recipeId, @equipCode, @weight, @bbMachno, @ursno, @printDat, @printNum, @somecon)";

        _sqlHelper.ExecuteNonQuery("Server33", query,
            new Dictionary<string, object>
            {
                { "planId", planId },
                { "prdDate", soLo.Replace("-", "") },
                { "endDate", ngayHieuLuc.Replace("-", "") },
                { "recipeId", recipeName },
                { "equipCode", maybb },
                { "weight", weight.ToString() },
                { "bbMachno", maybb },
                { "ursno", username },
                { "printDat", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") },
                { "printNum", soMeSX },
                { "somecon", somecon }
            });
    }

    private void InsertAutoSmallScanCode(string maybb, string planId,
        string prdDate, string endDate, string recipeName, decimal weight)
    {
        try
        {
            string connStr = _sqlHelper.GetMachineConnectionString(maybb);
            string query = @"INSERT INTO [AutoSmall_ScanCode]
                VALUES (@planId, @prdDate, @endDate, @recipeId, @equipCode, @printDat, @weight, 0)";

            _sqlHelper.ExecuteNonQueryWithConnectionString(connStr, query,
                new Dictionary<string, object>
                {
                    { "planId", planId },
                    { "prdDate", prdDate },
                    { "endDate", endDate },
                    { "recipeId", recipeName },
                    { "equipCode", maybb },
                    { "printDat", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") },
                    { "weight", weight.ToString() }
                });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"AutoSmall_ScanCode insert error: {ex.Message}");
        }
    }

    private void InsertIntemHC(string planId, string equipCode, string recipeName,
        decimal weight, string soMeSX, string realNum, string nguoiThaoTac)
    {
        string query = @"INSERT INTO [BB].[dbo].[IntemHC]
            VALUES (@planId, @equipCode, @recipeName, @weight, @soMeSX, '', @realNum, @printDat, @nguoiTT)";

        _sqlHelper.ExecuteNonQuery("Server33", query,
            new Dictionary<string, object>
            {
                { "planId", planId },
                { "equipCode", equipCode },
                { "recipeName", recipeName },
                { "weight", weight.ToString() },
                { "soMeSX", soMeSX },
                { "realNum", realNum },
                { "printDat", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") },
                { "nguoiTT", nguoiThaoTac }
            });
    }

    private DataTable LoadRecipes(string soLo, string equipCode)
    {
        string query = @"SELECT Recipe_Name, Start_Date, End_date as time, plan_id,
                        ROW_NUMBER() OVER (ORDER BY Start_Date) AS RowNumber
                        FROM [dbo].[LR_plan]
                        WHERE CONVERT(varchar(10), Start_Date, 25) = @soLo
                        AND [Equip_Code] = @equipCode
                        AND plan_id LIKE 'V%'
                        AND [End_Date] != ''
                        ORDER BY Start_Date";

        return _sqlHelper.ExecuteQuery("Server33", query,
            new Dictionary<string, object> { { "soLo", soLo }, { "equipCode", equipCode } });
    }

    private int GetValidDays(string recipeCode, string equipCode)
    {
        string query = @"SELECT [ValidDays] FROM Pmt_recipe
                        WHERE Recipe_Code = @recipeCode AND Equip_Code = @equipCode";

        var dt = _sqlHelper.ExecuteQuery("Server33", query,
            new Dictionary<string, object> { { "recipeCode", recipeCode }, { "equipCode", equipCode } });

        if (dt.Rows.Count > 0 && int.TryParse(dt.Rows[0][0]?.ToString()?.Trim(), out int days))
            return days;
        return 0;
    }

    private (string PlanId, string PlanNum, string SoMeSX) GetPlanInfo(
        string soLo, string equipCode, string recipeName, string thoiGianKT)
    {
        string query = @"SELECT plan_id, plan_num, End_Date, Real_Num
                        FROM [dbo].[LR_plan]
                        WHERE CONVERT(varchar(10), Start_Date, 25) = @soLo
                        AND [Equip_Code] = @equipCode
                        AND recipe_name = @recipeName
                        AND [End_Date] != ''
                        ORDER BY Start_Date";

        var dt = _sqlHelper.ExecuteQuery("Server33", query,
            new Dictionary<string, object>
            {
                { "soLo", soLo }, { "equipCode", equipCode }, { "recipeName", recipeName }
            });

        if (dt.Rows.Count == 0)
            return ("", "0", "0");

        string planId, planNum, soMeSX;

        if (dt.Rows.Count > 1)
        {
            planId = ""; planNum = "0"; soMeSX = "0";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var endTime = FormatTime(dt.Rows[i][2]);
                if (endTime == thoiGianKT)
                {
                    planId = dt.Rows[i][0].ToString()?.Trim() ?? "";
                    planNum = dt.Rows[i][1].ToString()?.Trim() ?? "0";
                    soMeSX = dt.Rows[i][3].ToString()?.Trim() ?? "0";
                    break;
                }
            }
        }
        else
        {
            planId = dt.Rows[0][0].ToString()?.Trim() ?? "";
            planNum = dt.Rows[0][1].ToString()?.Trim() ?? "0";
            soMeSX = dt.Rows[0][3].ToString()?.Trim() ?? "0";
        }

        // Check existing barcode
        if (!string.IsNullOrEmpty(planId))
        {
            string checkQuery = @"SELECT [Plan_Id], [print_num], [Somecon]
                                FROM [BB].[dbo].[TemBarcodeHC]
                                WHERE Plan_Id LIKE @planIdPattern
                                ORDER BY Print_dat DESC";

            var checkDt = _sqlHelper.ExecuteQuery("Server33", checkQuery,
                new Dictionary<string, object> { { "planIdPattern", planId + "%" } });

            if (checkDt.Rows.Count > 0)
            {
                planNum = checkDt.Rows[0][2].ToString()?.Trim() ?? planNum;
                soMeSX = checkDt.Rows[0][2].ToString()?.Trim() ?? soMeSX;
            }
        }

        return (planId, planNum, soMeSX);
    }
}
