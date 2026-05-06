using System.Diagnostics;
using System.Runtime.InteropServices;
using OfficeOpenXml;
using OfficeOpenXml.Style;

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

            // === Cell Values ===
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

            // Data values
            worksheet.Cells["C9:E12"].Value = chat.Trim();
            worksheet.Cells["C13:E16"].Value = may.Trim();
            worksheet.Cells["C17:E20"].Value = realNum + "/" + some.Trim();
            worksheet.Cells["I9:K10"].Value = solo.Trim();
            worksheet.Cells["I11:K12"].Value = thoigian.Trim();
            worksheet.Cells["I13:K16"].Value = hansd.Trim();
            worksheet.Cells["I17:K20"].Value = nguoilam.Trim();
            worksheet.Cells["F21:K23"].Value = "*" + planId.Trim() + "*";
            worksheet.Cells["F24:K24"].Value = "*" + planId.Trim() + "*";

            // === Font Name ===
            worksheet.Cells["A1:K20"].Style.Font.Name = "Times New Roman";
            worksheet.Cells["A21:B22"].Style.Font.Name = "Times New Roman";
            worksheet.Cells["F21:K23"].Style.Font.Name = "Code39AzaleaWide2";
            worksheet.Cells["F21:K23"].Style.Font.Size = 30;
            worksheet.Cells["F24:K24"].Style.Font.Name = "Times New Roman";
            worksheet.Cells["F24:K24"].Style.Font.Size = 11;

            // === Bold ===
            worksheet.Cells["J1:K2"].Style.Font.Bold = true;
            worksheet.Cells["C9:E12"].Style.Font.Bold = true;
            worksheet.Cells["C13:E16"].Style.Font.Bold = true;
            worksheet.Cells["C17:E20"].Style.Font.Bold = true;
            worksheet.Cells["I9:K10"].Style.Font.Bold = true;
            worksheet.Cells["I11:K12"].Style.Font.Bold = true;
            worksheet.Cells["I13:K16"].Style.Font.Bold = true;
            worksheet.Cells["I17:K20"].Style.Font.Bold = true;

            // === Font Sizes ===
            worksheet.Cells["A1:K2"].Style.Font.Size = 22;
            worksheet.Cells["A3:K4"].Style.Font.Size = 22;
            worksheet.Cells["A4:K6"].Style.Font.Size = 18;
            worksheet.Cells["A7:K8"].Style.Font.Size = 18;
            worksheet.Cells["A9:B10"].Style.Font.Size = 16;
            worksheet.Cells["A11:B12"].Style.Font.Size = 16;
            worksheet.Cells["A13:B14"].Style.Font.Size = 16;
            worksheet.Cells["A15:B16"].Style.Font.Size = 16;
            worksheet.Cells["A17:B18"].Style.Font.Size = 16;
            worksheet.Cells["A19:B20"].Style.Font.Size = 16;
            worksheet.Cells["A21:B22"].Style.Font.Size = 11;
            worksheet.Cells["F9:H9"].Style.Font.Size = 16;
            worksheet.Cells["F10:H10"].Style.Font.Size = 16;
            worksheet.Cells["F11:H11"].Style.Font.Size = 16;
            worksheet.Cells["F12:H12"].Style.Font.Size = 16;
            worksheet.Cells["F13:H14"].Style.Font.Size = 16;
            worksheet.Cells["F15:H16"].Style.Font.Size = 16;
            worksheet.Cells["F17:H18"].Style.Font.Size = 16;
            worksheet.Cells["F19:H20"].Style.Font.Size = 16;

            worksheet.Cells["C9:E12"].Style.Font.Size = 20;
            worksheet.Cells["C13:E16"].Style.Font.Size = 26;
            if (realNum.Length > 6)
                worksheet.Cells["C17:E20"].Style.Font.Size = 20;
            else
                worksheet.Cells["C17:E20"].Style.Font.Size = 26;

            worksheet.Cells["I9:K10"].Style.Font.Size = 26;
            worksheet.Cells["I11:K12"].Style.Font.Size = 16;
            worksheet.Cells["I13:K16"].Style.Font.Size = 26;
            worksheet.Cells["I17:K20"].Style.Font.Size = 26;

            // === Merge Cells ===
            worksheet.Cells["A1:B2"].Merge = true;
            worksheet.Cells["C1:I2"].Merge = true;
            worksheet.Cells["J1:K2"].Merge = true;
            worksheet.Cells["A3:K4"].Merge = true;
            worksheet.Cells["A5:K6"].Merge = true;
            worksheet.Cells["A7:K8"].Merge = true;
            worksheet.Cells["A9:B10"].Merge = true;
            worksheet.Cells["A11:B12"].Merge = true;
            worksheet.Cells["A13:B14"].Merge = true;
            worksheet.Cells["A15:B16"].Merge = true;
            worksheet.Cells["A17:B18"].Merge = true;
            worksheet.Cells["A19:B20"].Merge = true;
            worksheet.Cells["A21:B22"].Merge = true;
            worksheet.Cells["C9:E12"].Merge = true;
            worksheet.Cells["C13:E16"].Merge = true;
            worksheet.Cells["C17:E20"].Merge = true;
            worksheet.Cells["F9:H9"].Merge = true;
            worksheet.Cells["F10:H10"].Merge = true;
            worksheet.Cells["F11:H11"].Merge = true;
            worksheet.Cells["F12:H12"].Merge = true;
            worksheet.Cells["F13:H14"].Merge = true;
            worksheet.Cells["F15:H16"].Merge = true;
            worksheet.Cells["F17:H18"].Merge = true;
            worksheet.Cells["F19:H20"].Merge = true;
            worksheet.Cells["I9:K10"].Merge = true;
            worksheet.Cells["I11:K12"].Merge = true;
            worksheet.Cells["I13:K16"].Merge = true;
            worksheet.Cells["I17:K20"].Merge = true;
            worksheet.Cells["F21:K23"].Merge = true;
            worksheet.Cells["F24:K24"].Merge = true;

            // === Column Width / Row Height ===
            worksheet.Column(2).Width = 12.86;
            worksheet.Column(8).Width = 3.86;
            worksheet.Row(9).Height = 19;
            worksheet.Row(10).Height = 19;
            worksheet.Row(11).Height = 19;
            worksheet.Row(12).Height = 19;
            worksheet.Row(15).Height = 19;

            // === Alignment ===
            worksheet.Cells["A1:K24"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Cells["A1:K24"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            worksheet.Cells["C9:E12"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Distributed;
            worksheet.Cells["A5:K6"].Style.VerticalAlignment = ExcelVerticalAlignment.Bottom;
            worksheet.Cells["A7:K8"].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
            worksheet.Cells["A9:B10"].Style.VerticalAlignment = ExcelVerticalAlignment.Bottom;
            worksheet.Cells["A11:B12"].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
            worksheet.Cells["A13:B14"].Style.VerticalAlignment = ExcelVerticalAlignment.Bottom;
            worksheet.Cells["A15:B16"].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
            worksheet.Cells["A17:B18"].Style.VerticalAlignment = ExcelVerticalAlignment.Bottom;
            worksheet.Cells["A19:B20"].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
            worksheet.Cells["F9:H9"].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
            worksheet.Cells["F10:H10"].Style.VerticalAlignment = ExcelVerticalAlignment.Bottom;
            worksheet.Cells["F11:H11"].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
            worksheet.Cells["F12:H12"].Style.VerticalAlignment = ExcelVerticalAlignment.Bottom;
            worksheet.Cells["F13:H14"].Style.VerticalAlignment = ExcelVerticalAlignment.Bottom;
            worksheet.Cells["F15:H16"].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
            worksheet.Cells["F17:H18"].Style.VerticalAlignment = ExcelVerticalAlignment.Bottom;
            worksheet.Cells["F19:H20"].Style.VerticalAlignment = ExcelVerticalAlignment.Top;

            // === Borders ===
            worksheet.Cells["A5:K20"].Style.Border.BorderAround(ExcelBorderStyle.Thin);

            var cell = worksheet.Cells["A5:K20"];
            var border = cell.Style.Border;
            border.Top.Style = border.Left.Style = border.Right.Style = border.Bottom.Style =
                ExcelBorderStyle.Thin;

            cell = worksheet.Cells["A5:K6"];
            border = cell.Style.Border;
            border.Bottom.Style = ExcelBorderStyle.None;
            cell = worksheet.Cells["A7:K8"];
            border = cell.Style.Border;
            border.Top.Style = ExcelBorderStyle.None;

            cell = worksheet.Cells["A9:B10"];
            border = cell.Style.Border;
            border.Bottom.Style = ExcelBorderStyle.None;
            cell = worksheet.Cells["A11:B12"];
            border = cell.Style.Border;
            border.Top.Style = ExcelBorderStyle.None;

            cell = worksheet.Cells["A13:B14"];
            border = cell.Style.Border;
            border.Bottom.Style = ExcelBorderStyle.None;
            cell = worksheet.Cells["A15:B16"];
            border = cell.Style.Border;
            border.Top.Style = ExcelBorderStyle.None;

            cell = worksheet.Cells["A17:B18"];
            border = cell.Style.Border;
            border.Bottom.Style = ExcelBorderStyle.None;
            cell = worksheet.Cells["A19:B20"];
            border = cell.Style.Border;
            border.Top.Style = ExcelBorderStyle.None;

            cell = worksheet.Cells["F9:H9"];
            border = cell.Style.Border;
            border.Bottom.Style = ExcelBorderStyle.None;
            cell = worksheet.Cells["F10:H10"];
            border = cell.Style.Border;
            border.Top.Style = ExcelBorderStyle.None;

            cell = worksheet.Cells["F11:H11"];
            border = cell.Style.Border;
            border.Bottom.Style = ExcelBorderStyle.None;
            cell = worksheet.Cells["F12:H12"];
            border = cell.Style.Border;
            border.Top.Style = ExcelBorderStyle.None;

            cell = worksheet.Cells["F13:H14"];
            border = cell.Style.Border;
            border.Bottom.Style = ExcelBorderStyle.None;
            cell = worksheet.Cells["F15:H16"];
            border = cell.Style.Border;
            border.Top.Style = ExcelBorderStyle.None;

            cell = worksheet.Cells["F17:H18"];
            border = cell.Style.Border;
            border.Bottom.Style = ExcelBorderStyle.None;
            cell = worksheet.Cells["F19:H20"];
            border = cell.Style.Border;
            border.Top.Style = ExcelBorderStyle.None;

            cell = worksheet.Cells["A5:K20"];
            cell.Style.Border.BorderAround(ExcelBorderStyle.Medium);

            // === Print Settings ===
            worksheet.PrinterSettings.TopMargin = 0m;
            worksheet.PrinterSettings.LeftMargin = 0m;
            worksheet.PrinterSettings.RightMargin = 0m;
            worksheet.PrinterSettings.PaperSize = ePaperSize.A5;
            worksheet.PrinterSettings.Orientation = eOrientation.Landscape;
            worksheet.PrinterSettings.HorizontalCentered = true;
            worksheet.PrinterSettings.FitToPage = true;

            package.Save();
            return pathFile;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Excel Error: {ex.Message}");
            return string.Empty;
        }
    }

    public (bool Success, string Message) PrintExcel(string printerName, string pathFile)
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return (false, "In trực tiếp chỉ hỗ trợ trên Windows Server");
        }

        try
        {
            if (printerName.StartsWith("Fax") || printerName.StartsWith("Foxit")
                || printerName.StartsWith("Microsoft"))
            {
                try { File.Delete(pathFile); } catch { }
                return (false, "Chọn lại máy in");
            }

            dynamic? excelApp = null;
            dynamic? wb = null;
            try
            {
                var excelType = Type.GetTypeFromProgID("Excel.Application");
                if (excelType == null)
                    return (false, "Không tìm thấy Microsoft Excel trên server");

                excelApp = Activator.CreateInstance(excelType);
                if (excelApp == null)
                    return (false, "Không thể khởi tạo Excel Application");

                excelApp.Visible = false;
                excelApp.DisplayAlerts = false;

                wb = excelApp.Workbooks.Open(pathFile);
                wb.Worksheets[1].PrintOut(
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    printerName, Type.Missing, Type.Missing, Type.Missing);

                wb.Close(false);
                Marshal.ReleaseComObject(wb);
                wb = null;

                excelApp.Quit();
                Marshal.ReleaseComObject(excelApp);
                excelApp = null;

                GC.Collect();
                GC.WaitForPendingFinalizers();

                try { File.Delete(pathFile); } catch { }
                return (true, printerName + " - In thành công");
            }
            catch (Exception ex)
            {
                if (wb != null) { try { wb.Close(false); Marshal.ReleaseComObject(wb); } catch { } }
                if (excelApp != null) { try { excelApp.Quit(); Marshal.ReleaseComObject(excelApp); } catch { } }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                try { File.Delete(pathFile); } catch { }
                return (false, "Lỗi in: " + ex.Message);
            }
        }
        catch (Exception ex)
        {
            try { File.Delete(pathFile); } catch { }
            return (false, "Lỗi: " + ex.Message);
        }
    }
}
