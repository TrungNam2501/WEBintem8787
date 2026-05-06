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
    public IActionResult CheckConnection(IntemBBViewModel model)
    {
        ModelState.Clear();
        if (!string.IsNullOrEmpty(model.SelectedMachine))
        {
            var (success, message) = _networkService.CheckMachineConnection(model.SelectedMachine);
            model.ConnectionStatus = message;
        }

        model.NguoiThaoTac = GetUserId();
        return View("Index", model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult SelectMachine(IntemBBViewModel model)
    {
        ModelState.Clear();
        model.NguoiThaoTac = GetUserId();

        if (string.IsNullOrEmpty(model.SelectedMachine))
        {
            model.ThongBao = "Chưa chọn máy!!!";
            return View("Index", model);
        }

        // Check connection
        var (success, message) = _networkService.CheckMachineConnection(model.SelectedMachine);
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
    public IActionResult SelectRecipe(IntemBBViewModel model)
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
        var (success, message) = _networkService.CheckMachineConnection(model.SelectedMachine);
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
        if (string.IsNullOrEmpty(model.SelectedMachine) || model.SelectedMachine == "")
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

        model.ThongBao = "In tem thành công!";
        return View("Index", model);
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
