using Microsoft.AspNetCore.Mvc.Rendering;

namespace IntemBB.Web.Models;

public class IntemBBViewModel
{
    public string SelectedMachine { get; set; } = string.Empty;
    public string SoLo { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");
    public string ThoiGianSX { get; set; } = string.Empty;
    public string NgayHieuLuc { get; set; } = string.Empty;
    public string NguoiThaoTac { get; set; } = string.Empty;
    public string SoMeSX { get; set; } = string.Empty;
    public string PlanNum { get; set; } = "0";
    public string PlanId { get; set; } = string.Empty;
    public string ThoiGianKT { get; set; } = string.Empty;
    public string SelectedRecipe { get; set; } = string.Empty;
    public string SelectedMayBB { get; set; } = "BB 01";
    public string SelectedMayIn { get; set; } = "Hoa chat 198.1.8.111";
    public string ConnectionStatus { get; set; } = "Chưa kết nối";
    public string LabelType { get; set; } = "Chất phối hợp";
    public string? ThongBao { get; set; }

    public List<SelectListItem> RecipeList { get; set; } = new();

    public List<SelectListItem> MayBBList { get; set; } = new()
    {
        new("BB 01", "BB 01"),
        new("BB 02", "BB 02"),
        new("BB 03", "BB 03"),
        new("BB 04", "BB 04"),
        new("BB 05", "BB 05"),
        new("BB 06", "BB 06"),
        new("BB 07", "BB 07"),
        new("BB 08", "BB 08")
    };

    public List<SelectListItem> MayInList { get; set; } = new()
    {
        new("Hoa chat 198.1.8.111", "Hoa chat 198.1.8.111"),
        new("Hoa Chat New 8.112", "Hoa Chat New 8.112")
    };
}
