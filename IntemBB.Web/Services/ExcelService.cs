using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace IntemBB.Web.Services;

public class ExcelService
{
    public ExcelService()
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
    }

    public string CreateExcel(string may, string chat, string some, string solo,
        string thoigian, string hansd, string nguoilam, string tgkt, string realNum,
        string planId, string tenbieu, string tenbieu1, string tenbieu2,
        string tenbieu3, string pathFile, string oem)
    {
        try
        {
            var newFile = new FileInfo(pathFile);
            if (newFile.Exists)
            {
                newFile.Delete();
                newFile = new FileInfo(pathFile);
            }

            string updateKvs = "KVS3JIC001. 10 Rev. 4";
            if (may == "01" || may == "02")
                updateKvs = "KVS3JIC001. 11 Rev. 4";

            using var package = new ExcelPackage(newFile);
            var worksheet = package.Workbook.Worksheets.Add("1");

            worksheet.Cells["C1:I2"].Value = "建大橡膠（越南）有限公司";
            if (oem == "OEM")
            {
                worksheet.Cells["J1:K2"].Value = "QC-OEM";
            }

            worksheet.Cells["A3:K4"].Value = "Công ty Cao su Kenda(Việt Nam)";
            worksheet.Cells["A4:K6"].Value = tenbieu1;
            worksheet.Cells["A7:K8"].Value = tenbieu;
            worksheet.Cells["A9:B10"].Value = tenbieu2;
            worksheet.Cells["A11:B12"].Value = tenbieu3;
            worksheet.Cells["A13:B14"].Value = "機台";
            worksheet.Cells["A15:B16"].Value = "Máy";
            worksheet.Cells["A17:B18"].Value = "生產首數";
            worksheet.Cells["A19:B20"].Value = "Số mẻ sản xuất";
            worksheet.Cells["A21:B22"].Value = updateKvs;
            worksheet.Cells["F9:H9"].Value = "批號";
            worksheet.Cells["F10:H10"].Value = "Số lô";
            worksheet.Cells["F11:H11"].Value = "生產時間";
            worksheet.Cells["F12:H12"].Value = "Thời gian sản xuất";
            worksheet.Cells["F13:H14"].Value = "有效日";
            worksheet.Cells["F15:H16"].Value = "Ngày hiệu lực";
            worksheet.Cells["F17:H18"].Value = "作業員";
            worksheet.Cells["F19:H20"].Value = "Người thao tác";

            worksheet.Cells["C9:E12"].Value = chat.Trim();
            worksheet.Cells["C13:E16"].Value = may.Trim();
            worksheet.Cells["C17:E20"].Value = realNum + "/" + some.Trim();
            worksheet.Cells["I9:K10"].Value = solo.Trim();
            worksheet.Cells["I11:K12"].Value = thoigian.Trim();
            worksheet.Cells["I13:K16"].Value = hansd.Trim();
            worksheet.Cells["I17:K20"].Value = nguoilam.Trim();
            worksheet.Cells["F21:K23"].Value = "*" + planId.Trim() + "*";

            package.Save();
            return pathFile;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Excel Error: {ex.Message}");
            return string.Empty;
        }
    }
}
