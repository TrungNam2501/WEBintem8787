using System.Data;
using System.Text.RegularExpressions;
using IntemBB.Web.Data;
using IntemBB.Web.Models;
using IntemBB.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IntemBB.Web.Controllers;

[Authorize]
public class IntemKDController : Controller
{
    private readonly SqlServerHelper _sqlHelper;
    private readonly NetworkService _networkService;
    private readonly ExcelService _excelService;

    public IntemKDController(
        SqlServerHelper sqlHelper,
        NetworkService networkService,
        ExcelService excelService)
    {
        _sqlHelper = sqlHelper;
        _networkService = networkService;
        _excelService = excelService;
    }

    private string GetUserId()
    {
        return User.FindFirst("UserId")?.Value ?? "";
    }

    [HttpGet]
    public IActionResult Index()
    {
        var model = new IntemKDViewModel
        {
            NguoiThaoTac = GetUserId()
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult SelectMachine(IntemKDViewModel model)
    {
        model.NguoiThaoTac = GetUserId();

        if (string.IsNullOrEmpty(model.SelectedMachine))
        {
            model.ThongBao = "Chưa chọn máy!!!";
            return View("Index", model);
        }

        var (success, message) = _networkService.CheckMachineConnection(model.SelectedMachine);
        model.ConnectionStatus = message;

        string equipCode = model.SelectedMachine switch
        {
            "rdMay1" => "01",
            "rdMay2" => "03",
            "rdMay02" => "02",
            "rdMay04" => "04",
            _ => ""
        };

        model.LabelType = model.SelectedMachine switch
        {
            "rdMay1" or "rdMay02" => "Chất phối hợp",
            "rdMay2" or "rdMay04" => "Chất xúc tiến",
            _ => "Chất phối hợp"
        };

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
                recipes.Rows[i]["Recipe_Name"].ToString(),
                recipes.Rows[i]["RowNumber"].ToString()));
        }

        model.ThoiGianKT = recipes.Rows[0][2].ToString()?.Substring(11, 8) ?? "";
        model.ThoiGianSX = recipes.Rows[0][1].ToString()?.Substring(11, 8) + " | " + model.ThoiGianKT;

        var validDays = GetValidDays(recipes.Rows[0][0].ToString() ?? "", equipCode);
        if (validDays > 0)
        {
            model.NgayHieuLuc = Convert.ToDateTime(recipes.Rows[0][2].ToString())
                .AddDays(validDays).ToString("yyyy-MM-dd");
        }

        var planInfo = GetPlanInfo(model.SoLo, equipCode, recipes.Rows[0][0].ToString() ?? "", model.ThoiGianKT);
        model.PlanId = planInfo.PlanId;
        model.PlanNum = planInfo.PlanNum;
        model.SoMeSX = planInfo.SoMeSX;

        return View("Index", model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult PrintLabel(IntemKDViewModel model)
    {
        model.NguoiThaoTac = GetUserId();

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

        if (dt.Rows.Count == 0) return ("", "0", "0");

        string planId, planNum, soMeSX;

        if (dt.Rows.Count > 1)
        {
            planId = ""; planNum = "0"; soMeSX = "0";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var endDate = dt.Rows[i][2].ToString()?.Trim();
                if (endDate != null && endDate.Length >= 19 && endDate.Substring(11, 8) == thoiGianKT)
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
