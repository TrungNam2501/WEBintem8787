using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntemBB.Website
{
    public partial class IntemKD : System.Web.UI.Page
    {

        Dictionary<string, string> cnnstr = new Dictionary<string, string>
        {
            { "V-BB3701", @"Data Source=198.1.8.21;Initial Catalog=mfns;User ID=kendakv2;Password=kenda123" },
            { "V-BB3702", @"Data Source=198.1.8.22;Initial Catalog=mfns;User ID=kendakv2;Password=kenda123" },
            { "V-BB3703", @"Data Source=198.1.8.23;Initial Catalog=mfns;User ID=kendakv2;Password=kenda123" },
            { "V-BB3704", @"Data Source=198.1.8.24;Initial Catalog=mfns;User ID=kendakv2;Password=kenda123" },
            { "V-BB3705", @"Data Source=198.1.8.35;Initial Catalog=mfns;User ID=kendakv2;Password=kenda123" },
            { "V-BB3706", @"Data Source=198.1.8.36;Initial Catalog=mfns;User ID=kendakv2;Password=kenda123" },
            { "V-BB3707", @"Data Source=198.1.8.37;Initial Catalog=mfns;User ID=kendakv2;Password=kenda123" },
            { "V-BB3708", @"Data Source=198.1.8.38;Initial Catalog=mfns;User ID=kendakv2;Password=kenda123" },
            { "01", @"Data Source=198.1.8.21;Initial Catalog=mfns;User ID=kendakv2;Password=kenda123" },
            { "02", @"Data Source=198.1.8.22;Initial Catalog=mfns;User ID=kendakv2;Password=kenda123" },
            { "03", @"Data Source=198.1.8.23;Initial Catalog=mfns;User ID=kendakv2;Password=kenda123" },
            { "04", @"Data Source=198.1.8.24;Initial Catalog=mfns;User ID=kendakv2;Password=kenda123" },
            { "05", @"Data Source=198.1.8.35;Initial Catalog=mfns;User ID=kendakv2;Password=kenda123" },
            { "06", @"Data Source=198.1.8.36;Initial Catalog=mfns;User ID=kendakv2;Password=kenda123" },
            { "07", @"Data Source=198.1.8.37;Initial Catalog=mfns;User ID=kendakv2;Password=kenda123" },
            { "08", @"Data Source=198.1.8.38;Initial Catalog=mfns;User ID=kendakv2;Password=kenda123" },
            { "33", @"Data Source=198.1.10.33;Initial Catalog=erp;User ID=kendakv2;Password=kenda123" },
            { "186", @"Data Source=198.1.9.186;Initial Catalog=InTem;User ID=kendakv2;Password=kenda123" },
            { "maytest", @"Data Source=198.1.10.133;Initial Catalog=LotBB;User ID=kenda;Password=kenda123" },
            { "34", @"Data Source=198.1.10.34;Initial Catalog=P8400;User ID=kendakv2;Password=kenda123" },
            { "V11", @"Data Source=198.1.8.16;Initial Catalog=CWSS_S7;User ID=kendakv2;Password=kenda123" },
            { "V12", @"Data Source=198.1.8.17;Initial Catalog=CWSS_S7;User ID=kendakv2;Password=kenda123" },
            { "V13", @"Data Source=198.1.8.15;Initial Catalog=CWSS_S7;User ID=kendakv2;Password=kenda123" },
            { "V14", @"Data Source=198.1.8.18;Initial Catalog=CWSS_S7;User ID=kendakv2;Password=kenda123" },
            { "4", @"Data Source=198.1.10.4;Initial Catalog=LOT;User ID=kendakv2;Password=kenda123" },


        };
        public string cainlai; // Ca in lai sử dụng trong trường hợp chọn ngày in lai
        public int ingay; // ingay biến tạm, khác 0 khi pday lùi lại 1(ca đêm sau 12h) 
        int itam;
        DataTable dtFilter = new DataTable();
        string tenbieu1 = "";
        string tenbieu2 = "";
        string tenbieu3 = "";
        decimal weight = 0;
        decimal sokgxuat = 0;
        decimal total_weight;
        private Ping ping = new Ping();
        WebReference.test websv = new WebReference.test();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtSolo.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
            checkconnect();
            txtNguoiTT.Text = SQL.ueser;

        }
        private void ThongBao(string content)
        {
            lblThongbao.Text = content;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "Showmess();", true);
        }
        private void checkconnect()
        {

            if (rdMay2.Checked == true)
            {
                try
                {
                    Ping p1 = new Ping();
                    PingReply PR = p1.Send("198.1.8.15");
                    if (PR.Status == IPStatus.Success)
                    {

                        lblKetnoi1.Text = "Kết nối thành công máy -9";
                        //ThongBao("Kết nối thành công máy -9");

                    }
                    if (PR.Status != IPStatus.Success)
                    {

                        lblKetnoi1.Text = "Máy tính -9 đang tẳt";

                    }
                }
                catch (Exception ex)
                {
                    string a = ex.ToString();
                    ThongBao(a);
                }
            }
            if (rdMay1.Checked == true)
            {
                try
                {
                    Ping p1 = new Ping();
                    PingReply PR = p1.Send("198.1.8.16");
                    if (PR.Status == IPStatus.Success)
                    {

                        lblKetnoi1.Text = "Kết nối thành công máy -1";

                    }
                    if (PR.Status != IPStatus.Success)
                    {

                        lblKetnoi1.Text = "Máy tính -1 đang tẳt";

                    }
                }
                catch (Exception ex)
                {
                    string a = ex.ToString();
                    ThongBao(a);
                }

            }
            if (rdMay02.Checked == true)
            {
                try
                {
                    Ping p1 = new Ping();
                    PingReply PR = p1.Send("198.1.8.17");
                    if (PR.Status == IPStatus.Success)
                    {

                        lblKetnoi1.Text = "Kết nối thành công máy -1 mới";

                    }
                    if (PR.Status != IPStatus.Success)
                    {

                        lblKetnoi1.Text = "Máy tính -1 mới đang tẳt";

                    }
                }
                catch (Exception ex)
                {
                    string a = ex.ToString();
                    ThongBao(a);
                }
            }
            if (rdMay04.Checked == true)
            {
                try
                {
                    Ping p1 = new Ping();
                    PingReply PR = p1.Send("198.1.8.18");
                    if (PR.Status == IPStatus.Success)
                    {

                        lblKetnoi1.Text = "Kết nối thành công máy -9 mới";

                    }
                    if (PR.Status != IPStatus.Success)
                    {

                        lblKetnoi1.Text = "Máy tính -9 mới đang tẳt";

                    }
                }
                catch (Exception ex)
                {
                    string a = ex.ToString();
                    ThongBao(a);
                }

            }

        }

        protected void btnIntem_Click(object sender, EventArgs e)
        {
            string somethutesanxuat = txtSoMeSX.Text.ToString().Trim();
            string somecaidatsanxuat = txtPlanNum.Text.ToString().Trim();


            if (Regex.IsMatch(somethutesanxuat, @"^\d+$") && Regex.IsMatch(somecaidatsanxuat, @"^\d+$"))
            {

                int soMeThucTe = int.Parse(somethutesanxuat);
                int soMeCaiDat = int.Parse(somecaidatsanxuat);


                if (soMeThucTe > soMeCaiDat)
                {

                    ThongBao("Số mẻ thực tế không thể nhập lớn hơn số mẻ cài đặt vui lòng kiểm tra");
                    return;
                }
                else
                {

                }
            }
            else
            {
                ThongBao("Chuỗi chứa ký tự không hợp lệ, chỉ được nhập chữ số.");

                return;
            }



            if (rdMay1.Checked == false && rdMay2.Checked == false && rdMay02.Checked == false && rdMay04.Checked == false)
            {
                ThongBao("Chưa chọn máy!!!");
                return;
            }
            //string drMayBBchon = drMaybb.Text.Trim();
            //string maybb = drMayBBchon.Substring(drMayBBchon.Length - 2);


            #region V2
            string classs = "";
            string shift_id = Get_shift();
            string pday = setPday(shift_id);
            DateTime myPday = DateTime.ParseExact(pday, "yyyyMMdd", CultureInfo.InvariantCulture);
            pday = myPday.ToString("yyyyMMdd");
            string _chekday = DateTime.Now.ToString("yyyyMMdd");
            // kiểm tra ngày đã chọn và ngày hiện tại có bằng nhau không ?
            if (pday != _chekday) // Không bằng nhau
            {
                if (cainlai == "2")
                {
                    classs = cainlai;
                    //ingay = 1;
                }
                else
                {
                    classs = cainlai;
                }

            }
            else /// Nếu ngày đã chọn và ngày hiện tại bằng nhau kiểm tra xem ca 1 hay ca 2.
            {
                /// nếu là ca 2 kiểm tra thời gian sản xuất trên combobox thời gian sản xuất a|b 
                /// nếu a vượt quá 0h phải lùi pday lại 1
                if (cainlai == "2")
                {
                    if (txtTGSX.Text != "")
                    {
                        string[] arrTG = new string[2];
                        arrTG = txtTGSX.Text.Split('|'); // tách thời gian bắt đầu và kết thúc
                        //TimeSpan tsMocgio = new TimeSpan(0, 00, 00);
                        TimeSpan tspan1 = TimeSpan.Parse(arrTG[0]);
                        TimeSpan tspan2 = TimeSpan.Parse(arrTG[1]);
                        TimeSpan tspanMocgio1 = TimeSpan.Parse("00:00:00");
                        TimeSpan tspanMocgio2 = TimeSpan.Parse("06:30:00"); // 6h30 kết thúc ca tối

                        if (tspan1 <= tspanMocgio2) // kiểm giờ bắt đầu có vượt qua 6h30h chưa thì tăng ingày=1 để giảm pday
                        {
                            ingay = 1;
                        }
                    }
                    classs = cainlai;
                    //ingay = 1;
                }
                else
                    classs = shift_id;
            }

            DateTime pday22 = (DateTime.ParseExact(pday, "yyyyMMdd", CultureInfo.InvariantCulture));
            pday = (pday22.AddDays(-ingay)).ToString("yyyyMMdd");

            string may1 = "";
            string slipno = "";


            if (rdMay1.Checked == false && rdMay2.Checked == false && rdMay02.Checked == false && rdMay04.Checked == false)
            {

                return;
            }
            if (txtNguoiTT.Text == "")
            {
                ThongBao("Không được để trống nguời thao tác !!!");

                return;
            }
            if (txtSoMeSX.Text == "")
            {
                ThongBao("Vui lòng nhập số bao hỏng ");
                return;
            }
            if (txtTGSX.Text == "" || drChonmakeo.SelectedItem.Text == "" || txtSoMeSX.Text == "")
            {
                ThongBao("Vui lòng chọn đầy đủ dữ liệu rồi in ");
                return;
            }


            string plan_id = "";
            string real_num = "";
            string tenbieu = "";



            if (rdMay2.Checked == true)
            {

                may1 = "03";

                slipno = classs + may1 + "-" + pday.Substring(4, 4);
                tenbieu = "Thẻ biểu thị Chất xúc tiến thuốc";
                tenbieu1 = "藥品促進劑標示卡";

                tenbieu2 = "規格";


                tenbieu3 = "Quy Cách";
                if (txtTGSX.Text == "")
                {
                    string sqltotalweightcantay = "SELECT Total_Weight FROM [CWSS_S7].[dbo].[Pmt_recipe] where recipe_name ='" + drChonmakeo.SelectedItem.Text + "'";
                    DataTable dtcantay = SQL.ExecuteQuery15(sqltotalweightcantay);

                    total_weight = Convert.ToDecimal(dtcantay.Rows[0]["Total_Weight"].ToString());
                    int somecantay = Convert.ToInt32(txtSoMeSX.Text);
                    weight = total_weight * somecantay;

                    string mes = "V10" + DateTime.Now.ToString("yyMMdd") + "0001001";

                    plan_id = "V10" + DateTime.Now.ToString("yyMMdd") + "0001001";
                    if (drChonmakeo.SelectedItem.Text == "")
                    {
                        return;
                    }

                    string somecon = (int.Parse(txtPlanNum.Text.Trim()) - int.Parse(txtSoMeSX.Text.Trim())).ToString();




                  
                   
                    real_num = "Cân Tay";

                }
                else
                {
                    string recipe = "SELECT plan_id, plan_num,End_Date,Real_Num,IF_FLAG FROM [dbo].[LR_plan] where CONVERT(varchar(10),Start_Date,25) ='" + txtSolo.Text.ToString().Trim() + "' and recipe_name ='" + drChonmakeo.SelectedItem.Text + "' and  End_Date !='' order by Recipe_Name";
                    DataTable dtrecipe = SQL.ExecuteQuery33(recipe);
                    if (dtrecipe.Rows.Count > 0)
                    {
                        int plan_num = 0;
                        cnnstr.TryGetValue("V13", out string cnn33sql);
                        string sqltotalweight = "SELECT Total_Weight FROM [dbo].[Pmt_recipe] where recipe_name ='" + drChonmakeo.SelectedItem.Text + "' and [Equip_Code] ='03'";
                        DataTable dt = SQLpro.ExecuteQuery(sqltotalweight, cnn33sql);
                        total_weight = Convert.ToDecimal(dt.Rows[0]["Total_Weight"].ToString());

                        if (dtrecipe.Rows.Count > 1)
                        {
                            for (int k = 0; k < dtrecipe.Rows.Count; k++)
                            {
                                if (dtrecipe.Rows[k][2].ToString().Trim().Substring(11, 8) == lblThoiGianKT.Text)
                                {
                                    plan_id = dtrecipe.Rows[k][0].ToString();
                                }
                            }
                        }
                        else
                            plan_id = dtrecipe.Rows[0][0].ToString();


                        plan_id = plan_id + "901";



                        plan_num = Convert.ToInt32(dtrecipe.Rows[0][1].ToString());
                        int some = Convert.ToInt32(txtSoMeSX.Text);


                        if (dtFilter.Rows.Count == 1)
                        {
                            real_num = dtrecipe.Rows[drChonmakeo.SelectedIndex][1].ToString();
                        }
                        else
                        {
                            for (int j = 0; j < dtFilter.Rows.Count; j++)
                            {
                                for (int i = 0; i < dtrecipe.Rows.Count; i++)
                                {
                                    if (dtFilter.Rows[j][2].ToString().Trim().Substring(11, 8) == lblThoiGianKT.Text)
                                    {
                                        real_num = dtrecipe.Rows[i][3].ToString().Trim();
                                        break;
                                    }
                                }
                                if (real_num != "")
                                    break;
                            }
                        }
                      
                        real_num = txtSoMeSX.Text;
                        int some9 = Convert.ToInt32(txtSoMeSX.Text);
                        weight = total_weight * some9 + sokgxuat;
                        if (some9 > (int.Parse(real_num)))
                        {

                            return;
                        }
                        //nam mod (27/07/2022)
                        string Ifflag = dtrecipe.Rows[0][4].ToString();
                        if (Ifflag == "4")
                        {
                            real_num = txtSoMeSX.Text.ToString() + "(cân tay)";
                        }

                        string somecon = (int.Parse(txtPlanNum.Text.Trim()) - int.Parse(txtSoMeSX.Text.Trim())).ToString();

                     

                      

                    }
                }

            }
            if (rdMay1.Checked == true)
            {
                may1 = "01";

                slipno = classs + may1 + "-" + pday.Substring(4, 4);
                tenbieu = "Thẻ biểu thị Chất phối hợp thuốc";
                tenbieu1 = "藥品配合劑標示卡";

                tenbieu2 = "規格";
                tenbieu3 = "Quy Cách";


                if (txtTGSX.Text == "")
                {
                    string sqltotalweightcantay = "SELECT Total_Weight FROM [CWSS_S7].[dbo].[Pmt_recipe] where recipe_name ='" + drChonmakeo.SelectedItem.Text + "'";
                    DataTable dtcantay = SQL.ExecuteQuery16(sqltotalweightcantay);

                    total_weight = Convert.ToDecimal(dtcantay.Rows[0]["Total_Weight"].ToString());
                    int somecantay = Convert.ToInt32(txtSoMeSX.Text);
                    weight = total_weight * somecantay;

                    string mes = "V10" + DateTime.Now.ToString("yyMMdd") + "0001001";
                    plan_id = "V10" + DateTime.Now.ToString("yyMMdd") + "0001001";


                    if (drChonmakeo.SelectedItem.Text == "")
                    {

                        return;
                    }

                    string somecon = (int.Parse(txtPlanNum.Text.Trim()) - int.Parse(txtSoMeSX.Text.Trim())).ToString();

                 
                    
                    real_num = "Cân Tay";

                }
                else
                {
                    string recipe = "SELECT plan_id, plan_num, End_Date, Real_Num,IF_FLAG FROM [dbo].[LR_plan] where CONVERT(varchar(10),Start_Date,25) ='" + txtSolo.Text.ToString().Trim() + "' and recipe_name ='" + drChonmakeo.SelectedItem.Text + "' and  End_Date != '' order by Recipe_Name";
                    DataTable dtrecipe = SQL.ExecuteQuery33(recipe);
                    if (dtrecipe.Rows.Count > 0)
                    {
                        if (dtrecipe.Rows.Count > 1)
                        {
                            for (int k = 0; k < dtrecipe.Rows.Count; k++)
                            {
                                if (dtrecipe.Rows[k][2].ToString().Trim().Substring(11, 8) == lblThoiGianKT.Text)
                                {
                                    plan_id = dtrecipe.Rows[k][0].ToString();
                                }
                            }
                        }
                        else
                            plan_id = dtrecipe.Rows[0][0].ToString();
                        int plan_num = 0;

                        cnnstr.TryGetValue("V11", out string cnn33sql);
                        string sqltotalweight = "SELECT Total_Weight FROM [CWSS_S7].[dbo].[Pmt_recipe] where recipe_name ='" + drChonmakeo.SelectedItem.Text + "'";
                        DataTable dt = SQLpro.ExecuteQuery(sqltotalweight, cnn33sql);

                        total_weight = Convert.ToDecimal(dt.Rows[0]["Total_Weight"].ToString());

                        plan_id = plan_id + "901";


                        //plan_id = dtrecipe.Rows[0][0].ToString() + "901";
                        plan_num = Convert.ToInt32(dtrecipe.Rows[0][1].ToString());
                        int some = Convert.ToInt32(txtSoMeSX.Text);
                        //real_num = dtrecipe.Rows[0][1].ToString();
                        if (dtFilter.Rows.Count == 1)
                        {
                            real_num = dtrecipe.Rows[drChonmakeo.SelectedIndex][1].ToString();
                        }
                        else
                        {
                            for (int j = 0; j < dtFilter.Rows.Count; j++)
                            {
                                for (int i = 0; i < dtrecipe.Rows.Count; i++)
                                {
                                    string a = dtFilter.Rows[j][2].ToString().Trim().Substring(11, 8);
                                    if (dtFilter.Rows[j][2].ToString().Trim().Substring(11, 8) == lblThoiGianKT.Text)
                                    {
                                        if (dtrecipe.Rows[i][2].ToString().Trim().Substring(11, 8) == lblThoiGianKT.Text)
                                        {
                                            real_num = dtrecipe.Rows[i][3].ToString().Trim();
                                            break;

                                        }
                                    }
                                }
                                if (real_num != "")
                                    break;
                            }


                        }
                     
                        real_num = txtSoMeSX.Text;
                        int some9 = Convert.ToInt32(txtSoMeSX.Text);
                        weight = total_weight * some9 + sokgxuat;
                        if (some9 > (int.Parse(real_num)))
                        {
                            return;
                        }
                        //nam mod (27/07/2022)
                        string Ifflag = dtrecipe.Rows[0][4].ToString();
                        if (Ifflag == "4")
                        {
                            real_num = txtSoMeSX.Text.ToString() + "(cân tay)";
                        }
                        string somecon = (int.Parse(txtPlanNum.Text.Trim()) - int.Parse(txtSoMeSX.Text.Trim())).ToString();

                     
                        
                    }
                }
            }
            if (rdMay02.Checked == true)
            {
                may1 = "02";

                slipno = classs + may1 + "-" + pday.Substring(4, 4);
                tenbieu = "Thẻ biểu thị Chất phối hợp thuốc";
                tenbieu1 = "藥品配合劑標示卡";

                tenbieu2 = "規格";
                tenbieu3 = "Quy Cách";


                if (txtTGSX.Text == "")
                {
                    string sqltotalweightcantay = "SELECT Total_Weight FROM [CWSS_S7].[dbo].[Pmt_recipe] where recipe_name ='" + drChonmakeo.SelectedItem.Text + "'";
                    DataTable dtcantay = SQL.ExecuteQuery17(sqltotalweightcantay);

                    total_weight = Convert.ToDecimal(dtcantay.Rows[0]["Total_Weight"].ToString());
                    int somecantay = Convert.ToInt32(txtSoMeSX.Text);
                    weight = total_weight * somecantay;

                    string mes = "V10" + DateTime.Now.ToString("yyMMdd") + "0001001";
                    plan_id = "V10" + DateTime.Now.ToString("yyMMdd") + "0001001";


                    if (drChonmakeo.SelectedItem.Text == "")
                    {
                        //Cursor.Current = Cursors.Default;
                        return;
                    }
                    string somecon = (int.Parse(txtPlanNum.Text.Trim()) - int.Parse(txtSoMeSX.Text.Trim())).ToString();


                  

                  
                   

                    real_num = "Cân Tay";

                }
                else
                {
                    string recipe = "SELECT plan_id, plan_num, End_Date, Real_Num,IF_FLAG FROM [dbo].[LR_plan] where CONVERT(varchar(10),Start_Date,25) ='" + txtSolo.Text.ToString().Trim() + "' and Equip_Code ='02'  and recipe_name ='" + drChonmakeo.SelectedItem.Text + "' and  End_Date != '' order by Recipe_Name";
                    DataTable dtrecipe = SQL.ExecuteQuery33(recipe);
                    if (dtrecipe.Rows.Count > 0)
                    {
                        if (dtrecipe.Rows.Count > 1)
                        {
                            for (int k = 0; k < dtrecipe.Rows.Count; k++)
                            {
                                if (dtrecipe.Rows[k][2].ToString().Trim().Substring(11, 8) == lblThoiGianKT.Text)
                                {
                                    plan_id = dtrecipe.Rows[k][0].ToString();
                                }
                            }
                        }
                        else
                            plan_id = dtrecipe.Rows[0][0].ToString();
                        int plan_num = 0;

                        cnnstr.TryGetValue("V12", out string cnn33sql);
                      
                        string sqltotalweight = "SELECT Total_Weight FROM [dbo].[Pmt_recipe] where  recipe_name ='" + drChonmakeo.SelectedItem.Text + "'" + "and Equip_Code ='02'";
                        DataTable dt = SQLpro.ExecuteQuery(sqltotalweight, cnn33sql);

                        total_weight = Convert.ToDecimal(dt.Rows[0]["Total_Weight"].ToString());

                        plan_id = plan_id + "901";



                        //plan_id = dtrecipe.Rows[0][0].ToString() + "901";
                        plan_num = Convert.ToInt32(dtrecipe.Rows[0][1].ToString());
                        int some = Convert.ToInt32(txtSoMeSX.Text);
                        //real_num = dtrecipe.Rows[0][1].ToString();
                        if (dtFilter.Rows.Count == 1)
                        {
                            real_num = dtrecipe.Rows[drChonmakeo.SelectedIndex][1].ToString();
                        }
                        else
                        {
                            for (int j = 0; j < dtFilter.Rows.Count; j++)
                            {
                                for (int i = 0; i < dtrecipe.Rows.Count; i++)
                                {
                                    string a = dtFilter.Rows[j][2].ToString().Trim().Substring(11, 8);
                                    if (dtFilter.Rows[j][2].ToString().Trim().Substring(11, 8) == lblThoiGianKT.Text)
                                    {
                                        if (dtrecipe.Rows[i][2].ToString().Trim().Substring(11, 8) == lblThoiGianKT.Text)
                                        {
                                            real_num = dtrecipe.Rows[i][3].ToString().Trim();
                                            break;

                                        }
                                    }
                                }
                                if (real_num != "")
                                    break;
                            }


                        }
                      
                        real_num = txtSoMeSX.Text;
                        int some9 = Convert.ToInt32(txtSoMeSX.Text);
                        weight = total_weight * some9 + sokgxuat;
                        if (some9 > (int.Parse(real_num)))
                        {
                            //MessageBox.Show("Vượt quá số mẻ thực tế");
                            //txtsome.Text = "";
                            return;
                        }
                        //nam mod (27/07/2022)
                        string Ifflag = dtrecipe.Rows[0][4].ToString();
                        if (Ifflag == "4")
                        {
                            real_num = txtSoMeSX.Text.ToString() + "(cân tay)";
                        }
                        string somecon = (int.Parse(txtPlanNum.Text.Trim()) - int.Parse(txtSoMeSX.Text.Trim())).ToString();



                        


                    }
                }

            }
            if (rdMay04.Checked == true)
            {
                may1 = "04";

                slipno = classs + may1 + "-" + pday.Substring(4, 4);

                tenbieu = "Thẻ biểu thị Chất xúc tiến thuốc";
                tenbieu1 = "藥品促進劑標示卡";

                tenbieu2 = "規格";


                tenbieu3 = "Quy Cách";
                if (txtTGSX.Text == "")
                {
                    string sqltotalweightcantay = "SELECT Total_Weight FROM [CWSS_S7].[dbo].[Pmt_recipe] where recipe_name ='" + drChonmakeo.SelectedItem.Text + "'";
                    DataTable dtcantay = SQL.ExecuteQuery18(sqltotalweightcantay);

                    total_weight = Convert.ToDecimal(dtcantay.Rows[0]["Total_Weight"].ToString());
                    int somecantay = Convert.ToInt32(txtSoMeSX.Text);
                    weight = total_weight * somecantay;

                    string mes = "V10" + DateTime.Now.ToString("yyMMdd") + "0001001";

                    plan_id = "V10" + DateTime.Now.ToString("yyMMdd") + "0001001";


                    if (drChonmakeo.SelectedItem.Text == "")
                    {
                        return;
                    }
                    string somecon = (int.Parse(txtPlanNum.Text.Trim()) - int.Parse(txtSoMeSX.Text.Trim())).ToString();

                   
                    

                    real_num = "Cân Tay";

                }
                else
                {
                    string recipe = "SELECT plan_id, plan_num,End_Date,Real_Num,IF_FLAG FROM [dbo].[LR_plan] where CONVERT(varchar(10),Start_Date,25) ='" + txtSolo.Text.ToString().Trim() + "' and Equip_Code ='04' and recipe_name ='" + drChonmakeo.SelectedItem.Text + "' and  End_Date != '' order by Recipe_Name";
                    DataTable dtrecipe = SQL.ExecuteQuery33(recipe);
                    if (dtrecipe.Rows.Count > 0)
                    {
                        int plan_num = 0;
                          cnnstr.TryGetValue("V14", out string cnn33sql);
                        string sqltotalweight = "SELECT Total_Weight FROM [dbo].[Pmt_recipe] where recipe_name ='" + drChonmakeo.SelectedItem.Text + "' and [Equip_Code] ='04'";
                        DataTable dt = SQLpro.ExecuteQuery(sqltotalweight, cnn33sql);
                        if (dt.Rows.Count > 0)
                        {
                            total_weight = Convert.ToDecimal(dt.Rows[0]["Total_Weight"].ToString());
                        }
                        else

                        { total_weight = 0; }

                        if (dtrecipe.Rows.Count > 1)
                        {
                            for (int k = 0; k < dtrecipe.Rows.Count; k++)
                            {
                                if (dtrecipe.Rows[k][2].ToString().Trim().Substring(11, 8) == lblThoiGianKT.Text)
                                {
                                    plan_id = dtrecipe.Rows[k][0].ToString();
                                }
                            }
                        }
                        else
                            plan_id = dtrecipe.Rows[0][0].ToString();

                        plan_id = plan_id + "901";

                        //plan_id = dtrecipe.Rows[0][0].ToString() + "901";
                        plan_num = Convert.ToInt32(dtrecipe.Rows[0][1].ToString());
                        int some = Convert.ToInt32(txtSoMeSX.Text);
                        //real_num = dtrecipe.Rows[0][1].ToString();

                        if (dtFilter.Rows.Count == 1)
                        {
                            real_num = dtrecipe.Rows[drChonmakeo.SelectedIndex][1].ToString();
                        }
                        else
                        {
                            for (int j = 0; j < dtFilter.Rows.Count; j++)
                            {
                                for (int i = 0; i < dtrecipe.Rows.Count; i++)
                                {
                                    if (dtFilter.Rows[j][2].ToString().Trim().Substring(11, 8) == lblThoiGianKT.Text)
                                    {
                                        real_num = dtrecipe.Rows[i][3].ToString().Trim();
                                        break;
                                    }
                                }
                                if (real_num != "")
                                    break;
                            }
                        }
                    
                        real_num = txtSoMeSX.Text;
                        int some9 = Convert.ToInt32(txtSoMeSX.Text);
                        weight = total_weight * some9 + sokgxuat;
                        if (some9 > (int.Parse(real_num)))
                        {
                            //MessageBox.Show("Vượt quá số mẻ thực tế");
                            //txtsome.Text = "";
                            //Cursor.Current = Cursors.Default;
                            return;
                        }
                        //weight = total_weight * plan_num;

                        //nam mod (27/07/2022)
                        string Ifflag = dtrecipe.Rows[0][4].ToString();
                        if (Ifflag == "4")
                        {
                            real_num = txtSoMeSX.Text.ToString() + "(cân tay)";
                        }
                        string somecon = (int.Parse(txtPlanNum.Text.Trim()) - int.Parse(txtSoMeSX.Text.Trim())).ToString();


                       
                       
                    }
                }

            }
            //string filename = "_" +txtSolo.Text+ ".xlsx";
            string filename = "_" + drMayin.Text.Trim() + ".xlsx";
            string pathfolder = System.AppDomain.CurrentDomain.BaseDirectory + @"Data_HC\";
            string pathfile = System.AppDomain.CurrentDomain.BaseDirectory + @"Data_HC\" + filename; //tạo file và folder cho file Excel
            if (!Directory.Exists(pathfolder)) // Nếu chưa có folder thì tạo cái mới
            {
                Directory.CreateDirectory(pathfolder);
            }
            //string may = (rdMay1.Checked == true) && (rdMay2.Checked == false) ? "-1" : "-9"; //Mod 2021-05-07
            string may = "";
            string equipcode = "";
            if (rdMay1.Checked == true)
            {
                may = "01";
                equipcode = "01";
            }
            if (rdMay2.Checked == true)
            {
                may = "03";
                equipcode = "03";

            }
            if (rdMay02.Checked == true)
            {
                may = "02";
                equipcode = "02";

            }
            if (rdMay04.Checked == true)
            {
                may = "04";
                equipcode = "04";
            }


            //string sql186 = "INSERT INTO [BB].[dbo].[IntemHC] VALUES ('" + plan_id + "','" + equipcode + "','" + drChonmakeo.SelectedItem.Text + "','" + weight + "','" + txtSoMeSX.Text + "','','" + real_num + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + txtNguoiTT.Text.Trim() + "')";


            //bool bflag = SQL.ExecuteNonQuery33BB(sql186);
            //if (!bflag)
            //{

            //    bool flag = Send_Email_Without_Attachment(sql186, "Log insert tem hóa chất " + DateTime.Now.ToString("yyyyMMdd HH:mm:ss"));
            //    try
            //    {
            //        Create_txtlog(sql186 + "-" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), pday);
            //    }
            //    catch { /*Cursor.Current = Cursors.Default;*/ }

            //}


            decimal weigtpgamdin = total_weight * decimal.Parse(txtPlanNum.Text.Trim());
            string kietrapg = "SELECT * FROM kvmes.material_resource WHERE id = @ID";
            var parameters = new Dictionary<string, object>
                            {
                                { "ID", plan_id } // Tên key phải khớp với tên biến trong câu SQL
                            };

            DataTable result = SQLproPg.ExecuteQueryPg(kietrapg, CommandType.Text, parameters);

            if (result.Rows.Count == 0)
            {

                string product_type = "";
                string tenmay = "";
                switch (plan_id.Substring(0, 3))
                {
                    case "V11":
                        product_type = "COMPOUNDING_INGREDIENT";
                        tenmay = "KV-P8700-DRUG-CMPAUTO-1";
                        break;
                    case "V12":
                        product_type = "COMPOUNDING_INGREDIENT";
                        tenmay = "KV-P8700-DRUG-CMPAUTO-2";
                        break;
                    case "V13":
                        product_type = "ACCELERATOR_INGREDIENT";
                        tenmay = "KV-P8700-DRUG-ACLAUTO-3";
                        break;
                    case "V14":
                        product_type = "ACCELERATOR_INGREDIENT";
                        tenmay = "KV-P8700-DRUG-ACLAUTO-4";
                        break;
                    default:
                        product_type = "";
                        break;
                }


                DateTime expiryTime1 = DateTime.ParseExact(txtNHL.Text.Trim(), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                long expiryTime = new DateTimeOffset(expiryTime1).ToUnixTimeSeconds() * 1_000_000_000;
                DateTime updateby1 = DateTime.ParseExact(txtSolo.Text.Trim(), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                long updateby = new DateTimeOffset(updateby1).ToUnixTimeSeconds() * 1_000_000_000;
                bool pgadm = MaterialResourceInserter.InsertMaterialResource(plan_id, drChonmakeo.SelectedItem.Text.Trim(), product_type, weight, 1, expiryTime, updateby, txtNguoiTT.Text.Trim(), updateby, txtNguoiTT.Text.Trim(), tenmay, updateby);

            }






            string result1 = "";
            string partno = "";

            partno = drChonmakeo.SelectedItem.Text.Trim();

            string oem = "";


            string sqloem = "SELECT  [OEM]  FROM [BB].[dbo].[IF_RtPlan2CWSS] where Plan_Id ='" + plan_id.Substring(0, plan_id.Length - 3) + "'";
            DataTable dtoem = SQL.ExecuteQuery33(sqloem);
            if (dtoem.Rows.Count == 0)
            {
                ThongBao("Không có dữ liệu oem");
                return;
            }
            oem = dtoem.Rows[0][0].ToString().Trim();

            result1 = GetExcel.Create_Excel(may.Trim(), partno, txtSoMeSX.Text.Trim(), slipno, txtTGSX.Text.Trim(), txtNHL.Text.Trim(), txtNguoiTT.Text.Trim(), lblThoiGianKT.Text.Trim(), real_num, plan_id, tenbieu, tenbieu1, tenbieu2, tenbieu3, pathfile, oem);


            string s = Print_Excel(filename, pathfile);
            ThongBao(s + " - " + result1 + "In thành công\n");


            #endregion
        }
        private static string GetIpAddressFromMachineCode(string machineCode)
        {
            switch (machineCode)
            {
                case "01":
                    return "198.1.8.21";
                case "02":
                    return "198.1.8.22";
                case "03":
                    return "198.1.8.23";
                case "04":
                    return "198.1.8.24";
                case "05":
                    return "198.1.8.35";
                case "06":
                    return "198.1.8.36";
                case "07":
                    return "198.1.8.37";
                default:
                    return null; // Trả về null nếu mã máy không hợp lệ
            }
        }
        public static bool PingMachine(string machineCode)
        {

            string ipAddress = GetIpAddressFromMachineCode(machineCode);

            if (!string.IsNullOrEmpty(ipAddress))
            {
                try
                {
                    Ping ping = new Ping();
                    PingReply pingReply = ping.Send(ipAddress);

                    // Trả về true nếu ping thành công
                    return pingReply.Status == IPStatus.Success;
                }
                catch
                {
                    // Nếu có lỗi xảy ra trong quá trình ping, trả về false
                    return false;
                }
            }
            else
            {
                // Nếu mã máy không hợp lệ, trả về false
                return false;
            }
        }

        protected void rdMay1_CheckedChanged(object sender, EventArgs e)
        {
            txtNguoiTT.Focus();
            loadkeo();
        }

        protected void rdMay2_CheckedChanged(object sender, EventArgs e)
        {
            txtNguoiTT.Focus();
            loadkeo();
        }

        protected void rdMay02_CheckedChanged(object sender, EventArgs e)
        {
            txtNguoiTT.Focus();
            loadkeo();

        }

        protected void rdMay04_CheckedChanged(object sender, EventArgs e)
        {
            txtNguoiTT.Focus();
            loadkeo();
        }
        private void resettext()
        {

            txtSolo.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtPlanid.Text = "";
            txtPlanNum.Text = "";
            txtNHL.Text = "";
            txtSoMeSX.Text = "";
            txtTGSX.Text = "";
            DataTable dataTable = new DataTable();
            drChonmakeo.DataTextField = "";
            drChonmakeo.DataSource = dataTable;
            drChonmakeo.DataBind();
        }
        private void loadkeo()
        {

            string planNum = "", soMeSX = "", planID = "";

            if (rdMay1.Checked == true)
            {
                lblTB.Text = "Chất phối hợp";
                string recipe = "SELECT Recipe_Name, Start_Date,End_date as time, plan_id , ROW_NUMBER() OVER (ORDER BY Start_Date) AS RowNumber FROM [dbo].[LR_plan] where CONVERT(varchar(10),Start_Date,25) ='" + txtSolo.Text.ToString().Trim() + "' and [Equip_Code] ='01'  and plan_id like 'V%' and [End_Date] != ''  order by Start_Date";
                DataTable dtrecipe = SQL.ExecuteQuery33(recipe);
                if (dtrecipe.Rows.Count == 0)
                {
                    resettext();
                    ThongBao("Ngày hiện tại chưa có dữ liệu ");
                    return;
                }
                dtFilter = dtrecipe;
                drChonmakeo.DataValueField = "RowNumber";

                drChonmakeo.DataTextField = "Recipe_Name";
                drChonmakeo.DataTextField = "Recipe_Name";
                drChonmakeo.DataSource = dtrecipe;
                drChonmakeo.DataBind();
                lblThoiGianKT.Text = dtrecipe.Rows[0][2].ToString().Substring(11, 8);
                txtTGSX.Text = dtrecipe.Rows[0][1].ToString().Substring(11, 8) + " | " + lblThoiGianKT.Text;

                string ngayhieuluc = "SELECT [ValidDays] FROM Pmt_recipe where Recipe_Code ='" + dtrecipe.Rows[0][0].ToString() + "' and Equip_Code ='01'";
                DataTable dtNgayhieuluc = SQL.ExecuteQuery33(ngayhieuluc);
                if (dtNgayhieuluc.Rows.Count > 0)
                {
                    int a = int.Parse(dtNgayhieuluc.Rows[0][0].ToString().Trim());

                    txtNHL.Text = Convert.ToDateTime(dtrecipe.Rows[0][2].ToString()).AddDays(a).ToString("yyyy-MM-dd");
                }



                txtPlanid.Text = dtrecipe.Rows[0][3].ToString().Trim();
                //cbbpartno_SelectedIndexChanged(sender, e);
                string recipe16 = "SELECT plan_id, plan_num,End_Date,Real_Num FROM [dbo].[LR_plan] where CONVERT(varchar(10),Start_Date,25) ='" + txtSolo.Text.ToString().Trim() + "' and [Equip_Code] ='01' and recipe_name ='" + drChonmakeo.SelectedItem.Text + "' and [End_Date] != '' order by Start_Date";
                DataTable dtrecipe16 = SQL.ExecuteQuery33(recipe16);
                if (dtrecipe16.Rows.Count > 0)
                {
                    if (dtrecipe16.Rows.Count > 1)
                    {
                        for (int h = 0; h < dtrecipe16.Rows.Count; h++)
                        {
                            if (dtrecipe16.Rows[h][2].ToString().Trim().Substring(11, 8) == lblThoiGianKT.Text)
                            {
                                planNum = dtrecipe16.Rows[h][1].ToString().Trim();
                                soMeSX = dtrecipe16.Rows[h][3].ToString().Trim();
                                planID = dtrecipe16.Rows[h][0].ToString().Trim();
                                string checkTem = " SELECT  [Plan_Id],[print_num],[Somecon] FROM [BB].[dbo].[TemBarcodeHC] where Plan_Id like '" + planID + "%' order by Print_dat desc ";
                                DataTable checkTemBar = SQL.ExecuteQuery33(checkTem);
                                if (checkTemBar.Rows.Count > 0)
                                {
                                    txtPlanNum.Text = planNum;
                                    txtSoMeSX.Text = soMeSX;
                                    txtPlanid.Text = planID;
                                }
                                else
                                {
                                    txtPlanNum.Text = planNum;
                                    txtSoMeSX.Text = soMeSX;
                                    txtPlanid.Text = planID;
                                }
                            }
                        }
                    }
                    else
                    {
                        planNum = dtrecipe16.Rows[0][1].ToString().Trim();
                        soMeSX = dtrecipe16.Rows[0][3].ToString().Trim();
                        planID = dtrecipe16.Rows[0][0].ToString().Trim();
                        string checkTem = " SELECT  [Plan_Id],[print_num],[Somecon] FROM [BB].[dbo].[TemBarcodeHC] where Plan_Id like '" + planID + "%' order by Print_dat desc ";
                        DataTable checkTemBar = SQL.ExecuteQuery33(checkTem);
                        if (checkTemBar.Rows.Count > 0)
                        {
                            txtPlanNum.Text = planNum;
                            txtSoMeSX.Text = soMeSX;
                            txtPlanid.Text = planID;
                        }
                        else
                        {
                            txtPlanNum.Text = planNum;
                            txtSoMeSX.Text = soMeSX;
                            txtPlanid.Text = planID;
                        }
                        return;
                    }

                }

                if (dtrecipe.Rows.Count > 0)
                {
                    drChonmakeo.DataValueField = "RowNumber";
                    drChonmakeo.DataTextField = "Recipe_Name";
                    drChonmakeo.DataTextField = "Recipe_Name";
                    drChonmakeo.DataSource = dtrecipe;
                    drChonmakeo.DataBind();
                }
                else
                {
                    txtSoMeSX.Text = "";
                    txtTGSX.Text = "";
                    txtNHL.Text = "";
                    lblThoiGianKT.Text = "";
                    txtPlanid.Text = "";

                }



            }
            if (rdMay2.Checked == true)
            {
                lblTB.Text = "Chất xúc tiến";
                string recipe = "SELECT Recipe_Name, Start_Date,End_date as time, plan_id, ROW_NUMBER() OVER (ORDER BY Start_Date) AS RowNumber FROM [dbo].[LR_plan] where CONVERT(varchar(10),Start_Date,25) ='" + txtSolo.Text.ToString().Trim() + "' and [Equip_Code]='03' and plan_id like 'V%' and [End_Date] != '' order by Start_Date";
                DataTable dtrecipe = SQL.ExecuteQuery33(recipe);
                if (dtrecipe.Rows.Count == 0)
                {
                    resettext();
                    ThongBao("Ngày hiện tại chưa có dữ liệu ");
                    return;
                }

                DataRow toInsert = dtrecipe.NewRow();
                dtFilter = dtrecipe;

                drChonmakeo.DataValueField = "RowNumber";
                drChonmakeo.DataTextField = "Recipe_Name";
                drChonmakeo.DataTextField = "Recipe_Name";
                drChonmakeo.DataSource = dtrecipe;
                drChonmakeo.DataBind();
                lblThoiGianKT.Text = dtrecipe.Rows[0][2].ToString().Substring(11, 8);
                txtTGSX.Text = dtrecipe.Rows[0][1].ToString().Substring(11, 8) + " | " + lblThoiGianKT.Text;


                string ngayhieuluc = "SELECT [ValidDays] FROM Pmt_recipe where Recipe_Code ='" + dtrecipe.Rows[0][0].ToString() + "' and Equip_Code ='03'";
                DataTable dtNgayhieuluc = SQL.ExecuteQuery33(ngayhieuluc);
                if (dtNgayhieuluc.Rows.Count > 0)
                {
                    int a = int.Parse(dtNgayhieuluc.Rows[0][0].ToString().Trim());

                    txtNHL.Text = Convert.ToDateTime(dtrecipe.Rows[0][2].ToString()).AddDays(a).ToString("yyyy-MM-dd");
                }


                string recipe15 = "SELECT plan_id, plan_num,End_Date,Real_Num FROM [dbo].[LR_plan] where CONVERT(varchar(10),Start_Date,25) ='" + txtSolo.Text.ToString().Trim() + "' and [Equip_Code] ='03' and recipe_name ='" + drChonmakeo.SelectedItem.Text + "' and [End_Date] != '' order by Start_Date";
                DataTable dtrecipe15 = SQL.ExecuteQuery33(recipe15);
                if (dtrecipe15.Rows.Count > 0)
                {
                    if (dtrecipe15.Rows.Count > 1)
                    {
                        for (int g = 0; g < dtrecipe15.Rows.Count; g++)
                        {
                            if (dtrecipe15.Rows[g][2].ToString().Trim().Substring(11, 8) == lblThoiGianKT.Text)
                            {
                                planNum = dtrecipe15.Rows[g][1].ToString().Trim();
                                soMeSX = dtrecipe15.Rows[g][3].ToString().Trim();
                                planID = dtrecipe15.Rows[g][0].ToString().Trim();
                                string checkTem = " SELECT  [Plan_Id],[print_num],[Somecon] FROM [BB].[dbo].[TemBarcodeHC] where Plan_Id like '" + planID + "%' order by Print_dat desc ";
                                DataTable checkTemBar = SQL.ExecuteQuery33(checkTem);
                                if (checkTemBar.Rows.Count > 0)
                                {
                                    txtPlanNum.Text = planNum;
                                    txtSoMeSX.Text = soMeSX;
                                    txtPlanid.Text = planID;
                                }
                                else
                                {
                                    txtPlanNum.Text = planNum;
                                    txtSoMeSX.Text = soMeSX;
                                    txtPlanid.Text = planID;
                                }
                            }
                        }
                    }
                    else
                    {
                        planNum = dtrecipe15.Rows[0][1].ToString().Trim();
                        soMeSX = dtrecipe15.Rows[0][3].ToString().Trim();
                        planID = dtrecipe15.Rows[0][0].ToString().Trim();
                        string checkTem = " SELECT  [Plan_Id],[print_num],[Somecon] FROM [BB].[dbo].[TemBarcodeHC] where Plan_Id like '" + planID + "%' order by Print_dat desc ";
                        DataTable checkTemBar = SQL.ExecuteQuery33(checkTem);
                        if (checkTemBar.Rows.Count > 0)
                        {
                            txtPlanNum.Text = planNum;
                            txtSoMeSX.Text = soMeSX;
                            txtPlanid.Text = planID;
                        }
                        else
                        {
                            txtPlanNum.Text = planNum;
                            txtSoMeSX.Text = soMeSX;
                            txtPlanid.Text = planID;
                        }
                        return;
                    }

                }

                if (dtrecipe.Rows.Count > 0)
                {
                    drChonmakeo.DataValueField = "RowNumber";
                    drChonmakeo.DataTextField = "Recipe_Name";
                    drChonmakeo.DataTextField = "Recipe_Name";
                    drChonmakeo.DataSource = dtrecipe;
                    drChonmakeo.DataBind();


                }
                else
                {
                    txtSoMeSX.Text = "";
                    txtTGSX.Text = "";
                    txtNHL.Text = "";
                    lblThoiGianKT.Text = "";

                }
            }
            if (rdMay02.Checked == true)
            {
                lblTB.Text = "Chất phối hợp";
                string recipe = "SELECT Recipe_Name, Start_Date,End_date as time, plan_id, ROW_NUMBER() OVER (ORDER BY Start_Date) AS RowNumber FROM [dbo].[LR_plan] where CONVERT(varchar(10),Start_Date,25) ='" + txtSolo.Text.ToString().Trim() + "' and [Equip_Code] ='02' and [End_Date] != '' and plan_id like 'V%'  order by Start_Date";
                DataTable dtrecipe = SQL.ExecuteQuery33(recipe);
                if (dtrecipe.Rows.Count == 0)
                {
                    resettext();
                    ThongBao("Ngày hiện tại chưa có dữ liệu ");
                    return;
                }
                // Tạo cột mới để chứa thông tin kết hợp


                dtFilter = dtrecipe;
                drChonmakeo.DataValueField = "RowNumber";
                drChonmakeo.DataTextField = "Recipe_Name";
                drChonmakeo.DataTextField = "Recipe_Name";
                drChonmakeo.DataSource = dtrecipe;
                drChonmakeo.DataBind();

                lblThoiGianKT.Text = dtrecipe.Rows[0][2].ToString().Substring(11, 8);
                txtTGSX.Text = dtrecipe.Rows[0][1].ToString().Substring(11, 8) + " | " + lblThoiGianKT.Text;


                string ngayhieuluc = "SELECT [ValidDays] FROM Pmt_recipe where Recipe_Code ='" + dtrecipe.Rows[0][0].ToString() + "' and Equip_Code ='02'";
                DataTable dtNgayhieuluc = SQL.ExecuteQuery33(ngayhieuluc);
                if (dtNgayhieuluc.Rows.Count > 0)
                {
                    int a = int.Parse(dtNgayhieuluc.Rows[0][0].ToString().Trim());

                    txtNHL.Text = Convert.ToDateTime(dtrecipe.Rows[0][2].ToString()).AddDays(a).ToString("yyyy-MM-dd");
                }


                txtPlanid.Text = dtrecipe.Rows[0][3].ToString().Trim();
                //cbbpartno_SelectedIndexChanged(sender, e);
                string recipe17 = "SELECT plan_id, plan_num,End_Date,Real_Num FROM [dbo].[LR_plan] where CONVERT(varchar(10),Start_Date,25) ='" + txtSolo.Text.ToString().Trim() + "' and recipe_name ='" + drChonmakeo.SelectedItem.Text + "'   AND End_Date != ''  and [Equip_Code] ='02' order by Start_Date";
                DataTable dtrecipe17 = SQL.ExecuteQuery33(recipe17);
                if (dtrecipe17.Rows.Count > 0)
                {
                    if (dtrecipe17.Rows.Count > 1)
                    {
                        for (int h = 0; h < dtrecipe17.Rows.Count; h++)
                        {
                            if (dtrecipe17.Rows[h][2].ToString().Trim().Substring(11, 8) == lblThoiGianKT.Text)
                            {
                                planNum = dtrecipe17.Rows[h][1].ToString().Trim();
                                soMeSX = dtrecipe17.Rows[h][3].ToString().Trim();
                                planID = dtrecipe17.Rows[h][0].ToString().Trim();
                                string checkTem = " SELECT  [Plan_Id],[print_num],[Somecon] FROM [BB].[dbo].[TemBarcodeHC] where Plan_Id like '" + planID + "%' order by Print_dat desc ";
                                DataTable checkTemBar = SQL.ExecuteQuery33(checkTem);
                                if (checkTemBar.Rows.Count > 0)
                                {
                                    txtPlanNum.Text = planNum;
                                    txtSoMeSX.Text = soMeSX;
                                    txtPlanid.Text = planID;
                                }
                                else
                                {
                                    txtPlanNum.Text = planNum;
                                    txtSoMeSX.Text = soMeSX;
                                    txtPlanid.Text = planID;
                                }
                            }
                        }
                    }
                    else
                    {
                        planNum = dtrecipe17.Rows[0][1].ToString().Trim();
                        soMeSX = dtrecipe17.Rows[0][3].ToString().Trim();
                        planID = dtrecipe17.Rows[0][0].ToString().Trim();
                        string checkTem = " SELECT  [Plan_Id],[print_num],[Somecon] FROM [BB].[dbo].[TemBarcodeHC] where Plan_Id like '" + planID + "%' order by Print_dat desc ";
                        DataTable checkTemBar = SQL.ExecuteQuery33(checkTem);
                        if (checkTemBar.Rows.Count > 0)
                        {
                            txtPlanNum.Text = planNum;
                            txtSoMeSX.Text = soMeSX;
                            txtPlanid.Text = planID;
                        }
                        else
                        {
                            txtPlanNum.Text = planNum;
                            txtSoMeSX.Text = soMeSX;
                            txtPlanid.Text = planID;
                        }
                        return;
                    }

                }

                if (dtrecipe.Rows.Count > 0)
                {
                    drChonmakeo.DataValueField = "RowNumber";
                    drChonmakeo.DataTextField = "Recipe_Name";
                    drChonmakeo.DataTextField = "Recipe_Name";
                    drChonmakeo.DataSource = dtrecipe;
                    drChonmakeo.DataBind();
                }
                else
                {
                    txtSoMeSX.Text = "";
                    txtTGSX.Text = "";
                    txtNHL.Text = "";
                    lblThoiGianKT.Text = "";
                    txtPlanid.Text = "";
                    //txtempno.Enabled = false;
                    //button1.Enabled = false;
                }


            }
            if (rdMay04.Checked == true)
            {
                lblTB.Text = "Chất xúc tiến";
                string recipe = "SELECT Recipe_Name,  Start_Date,End_date as time, ROW_NUMBER() OVER (ORDER BY Start_Date) AS RowNumber FROM [dbo].[LR_plan] where CONVERT(varchar(10),Start_Date,25) ='" + txtSolo.Text.ToString().Trim() + "' and [Equip_Code]='04' and plan_id like 'V%' and [End_Date] != '' order by Start_Date";
                DataTable dtrecipe = SQL.ExecuteQuery33(recipe);
                if (dtrecipe.Rows.Count == 0)
                {
                    resettext();
                    ThongBao("Ngày hiện tại chưa có dữ liệu ");
                    return;
                }
                DataRow toInsert = dtrecipe.NewRow();
                dtFilter = dtrecipe;
                drChonmakeo.DataValueField = "RowNumber";
                drChonmakeo.DataTextField = "Recipe_Name";
                drChonmakeo.DataTextField = "Recipe_Name";
                drChonmakeo.DataSource = dtrecipe;
                drChonmakeo.DataBind();
                lblThoiGianKT.Text = dtrecipe.Rows[0][2].ToString().Substring(11, 8);
                txtTGSX.Text = dtrecipe.Rows[0][1].ToString().Substring(11, 8) + " | " + lblThoiGianKT.Text;



                string ngayhieuluc = "SELECT [ValidDays] FROM Pmt_recipe where Recipe_Code ='" + dtrecipe.Rows[0][0].ToString() + "' and Equip_Code ='04'";
                DataTable dtNgayhieuluc = SQL.ExecuteQuery33(ngayhieuluc);
                if (dtNgayhieuluc.Rows.Count > 0)
                {
                    int a = int.Parse(dtNgayhieuluc.Rows[0][0].ToString().Trim());

                    txtNHL.Text = Convert.ToDateTime(dtrecipe.Rows[0][2].ToString()).AddDays(a).ToString("yyyy-MM-dd");
                }

                string recipe18 = "SELECT plan_id, plan_num,End_Date,Real_Num FROM [dbo].[LR_plan] where CONVERT(varchar(10),Start_Date,25) ='" + txtSolo.Text.ToString().Trim() + "' and [Equip_Code] ='04' and recipe_name ='" + drChonmakeo.SelectedItem.Text + "' and [End_Date] != '' order by Start_Date";
                DataTable dtrecipe18 = SQL.ExecuteQuery33(recipe18);
                if (dtrecipe18.Rows.Count > 0)
                {
                    if (dtrecipe18.Rows.Count > 1)
                    {
                        for (int g = 0; g < dtrecipe18.Rows.Count; g++)
                        {
                            if (dtrecipe18.Rows[g][2].ToString().Trim().Substring(11, 8) == lblThoiGianKT.Text)
                            {
                                planNum = dtrecipe18.Rows[g][1].ToString().Trim();
                                soMeSX = dtrecipe18.Rows[g][3].ToString().Trim();
                                planID = dtrecipe18.Rows[g][0].ToString().Trim();
                                string checkTem = " SELECT  [Plan_Id],[print_num],[Somecon] FROM [BB].[dbo].[TemBarcodeHC] where Plan_Id like '" + planID + "%' order by Print_dat desc ";
                                DataTable checkTemBar = SQL.ExecuteQuery33(checkTem);
                                if (checkTemBar.Rows.Count > 0)
                                {
                                    txtPlanNum.Text = planNum;
                                    txtSoMeSX.Text = soMeSX;
                                    txtPlanid.Text = planID;
                                }
                                else
                                {
                                    txtPlanNum.Text = planNum;
                                    txtSoMeSX.Text = soMeSX;
                                    txtPlanid.Text = planID;
                                }
                            }
                        }
                    }
                    else
                    {
                        planNum = dtrecipe18.Rows[0][1].ToString().Trim();
                        soMeSX = dtrecipe18.Rows[0][3].ToString().Trim();
                        planID = dtrecipe18.Rows[0][0].ToString().Trim();
                        string checkTem = " SELECT  [Plan_Id],[print_num],[Somecon] FROM [BB].[dbo].[TemBarcodeHC] where Plan_Id like '" + planID + "%' order by Print_dat desc ";
                        DataTable checkTemBar = SQL.ExecuteQuery33(checkTem);
                        if (checkTemBar.Rows.Count > 0)
                        {
                            txtPlanNum.Text = planNum;
                            txtSoMeSX.Text = soMeSX;
                            txtPlanid.Text = planID;
                        }
                        else
                        {
                            txtPlanNum.Text = planNum;
                            txtSoMeSX.Text = soMeSX;
                            txtPlanid.Text = planID;
                        }
                        return;
                    }
                    //lbl_realnum.Text = dtrecipe15.Rows[0][3].ToString().Trim();
                    //return;
                }

                if (dtrecipe.Rows.Count > 0)
                {
                    drChonmakeo.DataValueField = "RowNumber";
                    drChonmakeo.DataTextField = "Recipe_Name";
                    drChonmakeo.DataTextField = "Recipe_Name";
                    drChonmakeo.DataSource = dtrecipe;
                    drChonmakeo.DataBind();

                    //cbbpartno_SelectedIndexChanged(sender, e);
                }
                else
                {
                    txtSoMeSX.Text = "";
                    txtTGSX.Text = "";
                    txtNHL.Text = "";
                    lblThoiGianKT.Text = "";
                    //txtempno.Enabled = false;
                    //button1.Enabled = false;
                }

            }
        }

        protected void txtSolo_TextChanged(object sender, EventArgs e)
        {
            DateTime dtime1 = DateTime.Now;
            DateTime dtime2 = Convert.ToDateTime(txtSolo.Text);
            if (rdMay1.Checked == false && rdMay2.Checked == false && rdMay02.Checked == false && rdMay04.Checked == false)
            {
                ThongBao("Vui lòng chọn máy cần kết nối ");
                return;
            }
            if (dtime2 > dtime1)
            {
                ThongBao("Ngày hiện tại không có dữ liệu");
                return;
            }
            loadkeo();

        }
        private void loadkeo1()
        {
            if (rdMay1.Checked == true)
            {
                lblTB.Text = "Chất phối hợp";

                string recipe = "SELECT Recipe_Name, Start_Date,End_date as time , ROW_NUMBER() OVER (ORDER BY Start_Date) AS RowNumber  FROM [dbo].[LR_plan] where CONVERT(varchar(10),Start_Date,25) ='" + txtSolo.Text.ToString().Trim() + "' and [Equip_Code] ='01' and  plan_id like 'V%' and [End_Date] != ''  order by Start_Date";
                DataTable dtrecipe = SQL.ExecuteQuery33(recipe);
                dtFilter = dtrecipe;



                //dtrecipe.Rows.Add(toInsert);
                drChonmakeo.DataValueField = "RowNumber";
                drChonmakeo.DataTextField = "Recipe_Name";
                drChonmakeo.DataTextField = "Recipe_Name";
                drChonmakeo.DataSource = dtrecipe;
                //drChonmakeo.DataBind();


                //cbbpartno_SelectedIndexChanged(sender, e);

                //if (dtrecipe.Rows.Count > 0)
                //{
                //    drChonmakeo.DataTextField = "Recipe_Name";
                //    drChonmakeo.DataTextField = "Recipe_Name";
                //    drChonmakeo.DataSource = dtrecipe;
                //    drChonmakeo.DataBind();
                //}
                //else
                //{
                //    txtSoMeSX.Text = "";
                //    txtTGSX.Text = "";
                //    txtNHL.Text = "";
                //    txtThoiGianKT.Text = "";
                //    //txtempno.Enabled = false;
                //    //button1.Enabled = false;
                //}

            }
            if (rdMay2.Checked == true)
            {
                lblTB.Text = "Chất xúc tiến";
                string recipe = "SELECT Recipe_Name, Start_Date,End_date as time, ROW_NUMBER() OVER (ORDER BY Start_Date) AS RowNumber FROM [dbo].[LR_plan] where CONVERT(varchar(10),Start_Date,25) ='" + txtSolo.Text.ToString().Trim() + "' and [Equip_Code]='03' and  plan_id like 'V%' and [End_Date] != '' order by Start_Date";
                DataTable dtrecipe = SQL.ExecuteQuery33(recipe);
                DataRow toInsert = dtrecipe.NewRow();
                dtFilter = dtrecipe;
                //dtrecipe.Rows.Add(toInsert);
                drChonmakeo.DataValueField = "RowNumber";
                drChonmakeo.DataTextField = "Recipe_Name";
                drChonmakeo.DataTextField = "Recipe_Name";
                drChonmakeo.DataSource = dtrecipe;

            }
            if (rdMay02.Checked == true)
            {
                lblTB.Text = "Chất phối hợp";
                string recipe = "SELECT Recipe_Name, Start_Date,End_date as time , ROW_NUMBER() OVER (ORDER BY Start_Date) AS RowNumber FROM [dbo].[LR_plan] where CONVERT(varchar(10),Start_Date,25) ='" + txtSolo.Text.ToString().Trim() + "' and [Equip_Code] ='02' and [End_Date] != '' and  plan_id like 'V%'  order by Start_Date";
                DataTable dtrecipe = SQL.ExecuteQuery33(recipe);
                dtFilter = dtrecipe;



                //dtrecipe.Rows.Add(toInsert);
                drChonmakeo.DataValueField = "RowNumber";
                drChonmakeo.DataTextField = "Recipe_Name";
                drChonmakeo.DataTextField = "Recipe_Name";
                drChonmakeo.DataSource = dtrecipe;
            }
            if (rdMay04.Checked == true)
            {
                lblTB.Text = "Chất xúc tiến";
                string recipe = "SELECT Recipe_Name, Start_Date,End_date as time, ROW_NUMBER() OVER (ORDER BY Start_Date) AS RowNumber FROM [dbo].[LR_plan] where CONVERT(varchar(10),Start_Date,25) ='" + txtSolo.Text.ToString().Trim() + "' and [Equip_Code]='04' and  plan_id like 'V%' and [End_Date] != '' order by Start_Date";
                DataTable dtrecipe = SQL.ExecuteQuery33(recipe);
                DataRow toInsert = dtrecipe.NewRow();
                dtFilter = dtrecipe;
                //dtrecipe.Rows.Add(toInsert);
                drChonmakeo.DataValueField = "RowNumber";
                drChonmakeo.DataTextField = "Recipe_Name";
                drChonmakeo.DataTextField = "Recipe_Name";
                drChonmakeo.DataSource = dtrecipe;
            }
        }
        public string Get_shift()
        {


            string shift;
            DateTime dNow = DateTime.Now;

            //tao biến datetime thời gian ca 1
            DateTime dFrom1 = DateTime.Now;
            TimeSpan tsFrom1 = new TimeSpan(6, 30, 0); // 2021-05-19
            dFrom1 = dFrom1.Date + tsFrom1;

            DateTime dTo1 = DateTime.Now;

            TimeSpan tsTo1 = new TimeSpan(18, 30, 0); // Ngày chỉnh sửa 2021-06-17
            //
            dTo1 = dTo1.Date + tsTo1;
            //-------------------------

            if (dNow >= dFrom1 && dNow <= dTo1)
                shift = "1";
            else
                shift = "2";
            return shift;
        }

        private string setPday(string shift)
        {
            //------------------------------------------
            //nếu là ca 2 mà thời gian insert từ 0h sáng hôm sau -> 6h20 sáng thì pday phải là ngày hiện tại - 1
            //------------------------------------------
            //string sPday = solo.Text.Replace("-", "");
            string sSoLo = txtSolo.Text.Replace("-", "");
            string sPday = DateTime.Now.ToString("yyyyMMdd");
            if (sPday != sSoLo)
            {
                if (txtTGSX.Text == "")
                {
                    cainlai = "";
                    return sSoLo;
                }
                else
                {
                    // Tìm ca của ngày đã chọn 
                    //ingay = ((int.Parse(sPday)) - (int.Parse(sSoLo)));

                    string[] arrTG = txtTGSX.Text.Split('|');
                    TimeSpan _dFrom = TimeSpan.Parse(arrTG[0]);

                    TimeSpan tsFrom1 = new TimeSpan(6, 30, 0);
                    TimeSpan tsTo1 = new TimeSpan(18, 30, 0);

                    TimeSpan _tsCheck1 = new TimeSpan(0, 00, 00);
                    TimeSpan _tscheck2 = new TimeSpan(6, 30, 00);

                    if (_dFrom >= tsFrom1 && _dFrom <= tsTo1)
                    {
                        cainlai = "1";
                    }
                    else
                    {
                        cainlai = "2";
                    }
                    if (_dFrom >= _tsCheck1 && _dFrom <= _tscheck2) // Kiểm tra nếu ngày đã chọn có thời gian qua ngày hôm sau thì tăng ingay lên 1 
                    {
                        ingay = 1; /// ingay tăng lên 1, thì pday - 1 giảm 1 ngày nếu thời gian kết thúc qua ngày hôm sau
                    }
                    return sSoLo;
                }

            }

            if (txtTGSX.Text != "")
            {
                string[] arrTG = txtTGSX.Text.Split('|');
                TimeSpan _dFrom = TimeSpan.Parse(arrTG[1]);

                TimeSpan tsFrom1 = new TimeSpan(6, 30, 0);
                TimeSpan tsTo1 = new TimeSpan(18, 30, 0);

                if (_dFrom >= tsFrom1 && _dFrom <= tsTo1)
                {
                    cainlai = "1";
                }
                else
                {
                    cainlai = "2";
                }
                return sSoLo;
            }

            DateTime dFrom = DateTime.Now;  //tao biến datetime thời gian bắt đầu từ 0h sáng hôm sau
            TimeSpan tsFrom = new TimeSpan(0, 0, 0);
            dFrom = dFrom.Date + tsFrom;

            DateTime dTo = DateTime.Now; //tao biến datetime thời gian đến 6h20 sáng hôm sau
            TimeSpan tsTo = new TimeSpan(6, 30, 0); // 2021-05-19
            dTo = dTo.Date + tsTo;

            DateTime dNow = DateTime.Now;
            //DateTime dNow = DateTime.Now;
            //TimeSpan ts = new TimeSpan(3, 20, 0);
            //dNow = dNow.Date + ts;

            if (shift == "2") //Nếu là ca 2 
            {
                if (dNow >= dFrom && dNow <= dTo)
                {
                    DateTime Yesterday = DateTime.Now.AddDays(-1);   //ngày hiện tại - 1
                    sPday = Yesterday.ToString("yyyyMMdd");
                }
            }
            return sPday;
        }
        private string Print_Excel(string filename, string pathfile)
        {
            try
            {
                bool flag = true;
                string messager = "";
                string[] sp = filename.Split('_');
                string printerName = sp[1].Substring(0, sp[1].Length - 5);
                //string printerName = drMayin.Text;
                if (printerName.Substring(0, 3) == "Fax" || printerName.Substring(0, 5) == "Foxit"
                    || printerName.Substring(0, 9) == "Microsoft")
                {
                    File.Delete(pathfile);
                    return "Chon lai may in";
                }

                string s = GetExcel.PrintExcel.Print_xls_file(printerName, pathfile, ref flag, ref messager); //In
                if (flag == true)
                {
                    File.Delete(pathfile);
                }
                else
                {
                    File.Delete(pathfile);
                    return s;
                }
                //MessageBox.Show(s);
                return s;
            }
            catch (Exception ex)
            {
                return "Ket Lệnh in 9.245" + ex.ToString();
                Create_txtlog("Ket Lệnh in 9.245" + ex.ToString(), DateTime.Now.ToString());
            }
        }
        public static bool Send_Email_Without_Attachment(string body, string Subject)
        {
            bool bResult = true;
            try
            {
                string day = DateTime.Now.ToString("yyyyMMdd");

                MailMessage em = new MailMessage();
                //Attachment attach = new Attachment(AttachmentPath);
                em.From = new System.Net.Mail.MailAddress("kenda_kv@kenda.com.tw");
                //em.To.Add("loiit@kenda.com.tw");
                //em.To.Add("tu@kenda.com.tw");
                em.CC.Add("loiit@kenda.com.tw");
                em.Subject = Subject;
                em.IsBodyHtml = true;
                em.Body = body;
                //m.Attachments.Add(attach);
                //        //em.Bcc.Add(from);                    
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("kmail.kenda.com.tw");
                //smtp.Host = "192.1.1.12";
                //smtp.Port = 25;
                smtp.EnableSsl = false;
                //smtp.Credentials = new System.Net.NetworkCredential("tu@kenda.com.tw", "1234567");
                smtp.Credentials = new System.Net.NetworkCredential();

                smtp.Send(em);
                //attach.Dispose();
                em.Dispose();
                smtp.Dispose();
            }
            catch (Exception ex)
            {
                ex.ToString();
                bResult = false;
            }
            return bResult;
        }
        private void Create_txtlog(string txt, string pday)
        {
            string result = "";
            string filename = "_" + pday + ".txt";
            string pathfolder = System.AppDomain.CurrentDomain.BaseDirectory + @"Logs\";
            string pathfile = System.AppDomain.CurrentDomain.BaseDirectory + @"Logs\" + filename; //tạo file và folder cho file Excel
            if (!Directory.Exists(pathfolder)) // Nếu chưa có folder thì tạo cái mới
            {
                Directory.CreateDirectory(pathfolder);
            }
            string path = System.AppDomain.CurrentDomain.BaseDirectory + @"Logs\" + "_" + pday + ".txt";
            if (System.IO.File.Exists(path))
            {
                System.IO.File.AppendAllText(path, txt + Environment.NewLine);
            }
            else
            {
                System.IO.File.Create(path);
                System.IO.File.AppendAllText(path, txt + Environment.NewLine);
            }

        }
        protected void drChonmakeo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                loadkeo1();
                DataRow[] foundRows = dtFilter.Select("Recipe_Name like '%" + drChonmakeo.SelectedItem.Text.Trim() + "%'");
                DataTable dt = new DataTable();
                DataColumn col = new DataColumn();
                DataColumn col1 = new DataColumn();

                col.ColumnName = "End_date";
                col1.ColumnName = "time";
                dt.Columns.Add(col);
                dt.Columns.Add(col1);

                for (int i = 0; i < foundRows.Length; i++)
                {
                    dt.Rows.Add(foundRows[i][2], foundRows[i][1]);
                }


                int ipos = int.Parse(drChonmakeo.SelectedValue) - 1;
                if (dtFilter.Rows[ipos][2].ToString() != "" || dtFilter.Rows[ipos][1].ToString() != "")
                {
                    lblThoiGianKT.Text = dtFilter.Rows[ipos][2].ToString().Substring(11, 8);
                    txtTGSX.Text = dtFilter.Rows[ipos][1].ToString().Substring(11, 8) + " | " + lblThoiGianKT.Text;
                }

                //int day = 7;
                //if (rdMay1.Checked == true)
                //    day = 7;
                //txtNHL.Text = Convert.ToDateTime(dt.Rows[0][0].ToString()).AddDays(day).ToString("yyyy-MM-dd");


                string planNum = "", soMeSX = "", planID = "";

                //txtempno.Focus();
                if (rdMay1.Checked == true)
                {

                    string ngayhieuluc = "SELECT [ValidDays] FROM Pmt_recipe where Recipe_Code ='" + drChonmakeo.SelectedItem.Text + "' and Equip_Code ='01'";
                    DataTable dtNgayhieuluc = SQL.ExecuteQuery33(ngayhieuluc);
                    if (dtNgayhieuluc.Rows.Count > 0)
                    {
                        int a = int.Parse(dtNgayhieuluc.Rows[0][0].ToString().Trim());

                        txtNHL.Text = Convert.ToDateTime(txtSolo.Text).AddDays(a).ToString("yyyy-MM-dd");
                    }

                    string recipe = "SELECT plan_id, plan_num,End_Date,Real_Num FROM [dbo].[LR_plan] where CONVERT(varchar(10),Start_Date,25) ='" + txtSolo.Text.ToString().Trim() + "' and [Equip_Code]='01'  and recipe_name ='" + drChonmakeo.SelectedItem.Text + "' and [End_Date] != '' order by Start_Date";
                    DataTable dtrecipe16 = SQL.ExecuteQuery33(recipe);
                    if (dtrecipe16.Rows.Count > 0)
                    {
                        if (dtrecipe16.Rows.Count > 1)
                        {
                            for (int h = 0; h < dtrecipe16.Rows.Count; h++)
                            {
                                if (dtrecipe16.Rows[h][2].ToString().Trim().Substring(11, 8) == lblThoiGianKT.Text)
                                {
                                    planNum = dtrecipe16.Rows[h][1].ToString().Trim();
                                    soMeSX = dtrecipe16.Rows[h][3].ToString().Trim();
                                    planID = dtrecipe16.Rows[h][0].ToString().Trim();
                                    string checkTem = " SELECT  [Plan_Id],[print_num],[Somecon] FROM [BB].[dbo].[TemBarcodeHC] where Plan_Id like '" + planID + "%' order by Print_dat desc ";
                                    DataTable checkTemBar = SQL.ExecuteQuery33(checkTem);
                                    if (checkTemBar.Rows.Count > 0)
                                    {
                                        txtPlanNum.Text = planNum;
                                        txtSoMeSX.Text = soMeSX;
                                        txtPlanid.Text = planID;
                                    }
                                    else
                                    {
                                        txtPlanNum.Text = planNum;
                                        txtSoMeSX.Text = soMeSX;
                                        txtPlanid.Text = planID;
                                    }
                                }
                            }

                        }
                        else
                        {
                            planNum = dtrecipe16.Rows[0][1].ToString().Trim();
                            soMeSX = dtrecipe16.Rows[0][3].ToString().Trim();
                            planID = dtrecipe16.Rows[0][0].ToString().Trim();
                            string checkTem = " SELECT  [Plan_Id],[print_num],[Somecon] FROM [BB].[dbo].[TemBarcodeHC] where Plan_Id like '" + planID + "%' order by Print_dat desc ";
                            DataTable checkTemBar = SQL.ExecuteQuery33(checkTem);
                            if (checkTemBar.Rows.Count > 0)
                            {
                                txtPlanNum.Text = planNum;
                                txtSoMeSX.Text = soMeSX;
                                txtPlanid.Text = planID;
                            }
                            else
                            {
                                txtPlanNum.Text = planNum;
                                txtSoMeSX.Text = soMeSX;
                                txtPlanid.Text = planID;
                            }
                            return;
                        }

                    }
                }
                if (rdMay2.Checked == true)
                {
                    string ngayhieuluc = "SELECT [ValidDays] FROM Pmt_recipe where Recipe_Code ='" + drChonmakeo.SelectedItem.Text + "' and Equip_Code ='03'";
                    DataTable dtNgayhieuluc = SQL.ExecuteQuery33(ngayhieuluc);
                    if (dtNgayhieuluc.Rows.Count > 0)
                    {
                        int a = int.Parse(dtNgayhieuluc.Rows[0][0].ToString().Trim());

                        txtNHL.Text = Convert.ToDateTime(txtSolo.Text).AddDays(a).ToString("yyyy-MM-dd");
                    }


                    string recipe = "SELECT plan_id, plan_num,End_Date,Real_Num FROM [dbo].[LR_plan] where CONVERT(varchar(10),Start_Date,25) ='" + txtSolo.Text.ToString().Trim() + "' and [Equip_Code]='03' and recipe_name ='" + drChonmakeo.SelectedItem.Text + "' and [End_Date] != '' order by Start_Date";
                    DataTable dtrecipe15 = SQL.ExecuteQuery33(recipe);
                    if (dtrecipe15.Rows.Count > 0)
                    {
                        if (dtrecipe15.Rows.Count > 1)
                        {
                            for (int g = 0; g < dtrecipe15.Rows.Count; g++)
                            {
                                if (dtrecipe15.Rows[g][2].ToString().Trim().Substring(11, 8) == lblThoiGianKT.Text)
                                {
                                    planNum = dtrecipe15.Rows[g][1].ToString().Trim();
                                    soMeSX = dtrecipe15.Rows[g][3].ToString().Trim();
                                    planID = dtrecipe15.Rows[g][0].ToString().Trim();
                                    string checkTem = " SELECT  [Plan_Id],[print_num],[Somecon] FROM [BB].[dbo].[TemBarcodeHC] where Plan_Id like '" + planID + "%' order by Print_dat desc ";
                                    DataTable checkTemBar = SQL.ExecuteQuery33(checkTem);
                                    if (checkTemBar.Rows.Count > 0)
                                    {
                                        txtPlanNum.Text = planNum;
                                        txtSoMeSX.Text = soMeSX;
                                        txtPlanid.Text = planID;
                                    }
                                    else
                                    {
                                        txtPlanNum.Text = planNum;
                                        txtSoMeSX.Text = soMeSX;
                                        txtPlanid.Text = planID;
                                    }
                                }
                            }

                            //txtPlanNum.Text = dtrecipe15.Rows[ipos][1].ToString().Trim();
                            //txtSoMeSX.Text = dtrecipe15.Rows[ipos][3].ToString().Trim();
                            //txtPlanid.Text = dtrecipe15.Rows[ipos][0].ToString().Trim();


                            //planNum = dtrecipe15.Rows[ipos][1].ToString().Trim();
                            //soMeSX = dtrecipe15.Rows[ipos][3].ToString().Trim();
                            //planID = dtrecipe15.Rows[ipos][0].ToString().Trim();
                            //string checkTem = " SELECT  [Plan_Id],[print_num],[Somecon] FROM [BB].[dbo].[TemBarcodeHC] where Plan_Id like '" + planID + "%' order by Print_dat desc ";
                            //DataTable checkTemBar = SQL.ExecuteQuery33(checkTem);
                            //if (checkTemBar.Rows.Count > 0)
                            //{
                            //    txtPlanNum.Text = checkTemBar.Rows[0][2].ToString().Trim();
                            //    txtSoMeSX.Text = checkTemBar.Rows[0][2].ToString().Trim();
                            //    txtPlanid.Text = planID;
                            //}
                            //else
                            //{
                            //    txtPlanNum.Text = planNum;
                            //    txtSoMeSX.Text = soMeSX;
                            //    txtPlanid.Text = planID;
                            //}
                        }
                        else
                        {

                            planNum = dtrecipe15.Rows[0][1].ToString().Trim();
                            soMeSX = dtrecipe15.Rows[0][3].ToString().Trim();
                            planID = dtrecipe15.Rows[0][0].ToString().Trim();
                            string checkTem = " SELECT  [Plan_Id],[print_num],[Somecon] FROM [BB].[dbo].[TemBarcodeHC] where Plan_Id like '" + planID + "%' order by Print_dat desc ";
                            DataTable checkTemBar = SQL.ExecuteQuery33(checkTem);
                            if (checkTemBar.Rows.Count > 0)
                            {
                                txtPlanNum.Text = planNum;
                                txtSoMeSX.Text = soMeSX;
                                txtPlanid.Text = planID;
                            }
                            else
                            {
                                txtPlanNum.Text = planNum;
                                txtSoMeSX.Text = soMeSX;
                                txtPlanid.Text = planID;
                            }
                            return;
                        }
                        //lbl_realnum.Text = dtrecipe15.Rows[0][3].ToString().Trim();
                        //return;
                    }
                }
                if (rdMay02.Checked == true)
                {
                    string ngayhieuluc = "SELECT [ValidDays] FROM Pmt_recipe where Recipe_Code ='" + drChonmakeo.SelectedItem.Text + "' and Equip_Code ='02'";
                    DataTable dtNgayhieuluc = SQL.ExecuteQuery33(ngayhieuluc);
                    if (dtNgayhieuluc.Rows.Count > 0)
                    {
                        int a = int.Parse(dtNgayhieuluc.Rows[0][0].ToString().Trim());

                        txtNHL.Text = Convert.ToDateTime(txtSolo.Text).AddDays(a).ToString("yyyy-MM-dd");
                    }

                    string recipe = "SELECT plan_id, plan_num,End_Date,Real_Num FROM [dbo].[LR_plan] where CONVERT(varchar(10),Start_Date,25) ='" + txtSolo.Text.ToString().Trim() + "' and [Equip_Code]='02'  and recipe_name ='" + drChonmakeo.SelectedItem.Text + "' and [End_Date] != '' order by Start_Date";
                    DataTable dtrecipe17 = SQL.ExecuteQuery33(recipe);
                    if (dtrecipe17.Rows.Count > 0)
                    {
                        if (dtrecipe17.Rows.Count > 1)
                        {
                            for (int h = 0; h < dtrecipe17.Rows.Count; h++)
                            {
                                if (dtrecipe17.Rows[h][2].ToString().Trim().Substring(11, 8) == lblThoiGianKT.Text)
                                {

                                    planNum = dtrecipe17.Rows[h][1].ToString().Trim();
                                    soMeSX = dtrecipe17.Rows[h][3].ToString().Trim();
                                    planID = dtrecipe17.Rows[h][0].ToString().Trim();
                                    string checkTem = " SELECT  [Plan_Id],[print_num],[Somecon] FROM [BB].[dbo].[TemBarcodeHC] where Plan_Id like '" + planID + "%' order by Print_dat desc ";
                                    DataTable checkTemBar = SQL.ExecuteQuery33(checkTem);
                                    if (checkTemBar.Rows.Count > 0)
                                    {
                                        txtPlanNum.Text = planNum;
                                        txtSoMeSX.Text = soMeSX;
                                        txtPlanid.Text = planID;
                                    }
                                    else
                                    {
                                        txtPlanNum.Text = planNum;
                                        txtSoMeSX.Text = soMeSX;
                                        txtPlanid.Text = planID;
                                    }
                                }
                            }
                            //txtPlanNum.Text = dtrecipe17.Rows[ipos][1].ToString().Trim();
                            //txtSoMeSX.Text = dtrecipe17.Rows[ipos][3].ToString().Trim();
                            //txtPlanid.Text = dtrecipe17.Rows[ipos][0].ToString().Trim();


                            //planNum = dtrecipe17.Rows[ipos][1].ToString().Trim();
                            //soMeSX = dtrecipe17.Rows[ipos][3].ToString().Trim();
                            //planID = dtrecipe17.Rows[ipos][0].ToString().Trim();
                            //string checkTem = " SELECT  [Plan_Id],[print_num],[Somecon] FROM [BB].[dbo].[TemBarcodeHC] where Plan_Id like '" + planID + "%' order by Print_dat desc ";
                            //DataTable checkTemBar = SQL.ExecuteQuery33(checkTem);
                            //if (checkTemBar.Rows.Count > 0)
                            //{
                            //    txtPlanNum.Text = checkTemBar.Rows[0][2].ToString().Trim();
                            //    txtSoMeSX.Text = checkTemBar.Rows[0][2].ToString().Trim();
                            //    txtPlanid.Text = planID;
                            //}
                            //else
                            //{
                            //    txtPlanNum.Text = planNum;
                            //    txtSoMeSX.Text = soMeSX;
                            //    txtPlanid.Text = planID;
                            //}
                        }
                        else
                        {
                            planNum = dtrecipe17.Rows[0][1].ToString().Trim();
                            soMeSX = dtrecipe17.Rows[0][3].ToString().Trim();
                            planID = dtrecipe17.Rows[0][0].ToString().Trim();
                            string checkTem = " SELECT  [Plan_Id],[print_num],[Somecon] FROM [BB].[dbo].[TemBarcodeHC] where Plan_Id like '" + planID + "%' order by Print_dat desc ";
                            DataTable checkTemBar = SQL.ExecuteQuery33(checkTem);
                            if (checkTemBar.Rows.Count > 0)
                            {
                                txtPlanNum.Text = planNum;
                                txtSoMeSX.Text = soMeSX;
                                txtPlanid.Text = planID;
                            }
                            else
                            {
                                txtPlanNum.Text = planNum;
                                txtSoMeSX.Text = soMeSX;
                                txtPlanid.Text = planID;
                            }
                            return;
                        }

                    }
                }
                if (rdMay04.Checked == true)
                {
                    string ngayhieuluc = "SELECT [ValidDays] FROM Pmt_recipe where Recipe_Code ='" + drChonmakeo.SelectedItem.Text + "' and Equip_Code ='04'";
                    DataTable dtNgayhieuluc = SQL.ExecuteQuery33(ngayhieuluc);
                    if (dtNgayhieuluc.Rows.Count > 0)
                    {
                        int a = int.Parse(dtNgayhieuluc.Rows[0][0].ToString().Trim());

                        txtNHL.Text = Convert.ToDateTime(txtSolo.Text).AddDays(a).ToString("yyyy-MM-dd");
                    }
                    string recipe = "SELECT plan_id, plan_num,End_Date,Real_Num FROM [dbo].[LR_plan] where CONVERT(varchar(10),Start_Date,25) ='" + txtSolo.Text.ToString().Trim() + "' and [Equip_Code]='04' and recipe_name ='" + drChonmakeo.SelectedItem.Text + "' and [End_Date] != '' order by Start_Date";
                    DataTable dtrecipe18 = SQL.ExecuteQuery33(recipe);
                    if (dtrecipe18.Rows.Count > 0)
                    {
                        if (dtrecipe18.Rows.Count > 1)
                        {
                            for (int g = 0; g < dtrecipe18.Rows.Count; g++)
                            {
                                if (dtrecipe18.Rows[g][2].ToString().Trim().Substring(11, 8) == lblThoiGianKT.Text)
                                {
                                    planNum = dtrecipe18.Rows[g][1].ToString().Trim();
                                    soMeSX = dtrecipe18.Rows[g][3].ToString().Trim();
                                    planID = dtrecipe18.Rows[g][0].ToString().Trim();
                                    string checkTem = " SELECT  [Plan_Id],[print_num],[Somecon] FROM [BB].[dbo].[TemBarcodeHC] where Plan_Id like '" + planID + "%' order by Print_dat desc ";
                                    DataTable checkTemBar = SQL.ExecuteQuery33(checkTem);
                                    if (checkTemBar.Rows.Count > 0)
                                    {
                                        txtPlanNum.Text = planNum;
                                        txtSoMeSX.Text = soMeSX;
                                        txtPlanid.Text = planID;
                                    }
                                    else
                                    {
                                        txtPlanNum.Text = planNum;
                                        txtSoMeSX.Text = soMeSX;
                                        txtPlanid.Text = planID;
                                    }
                                }
                            }
                            //txtPlanNum.Text = dtrecipe18.Rows[ipos][1].ToString().Trim();
                            //txtSoMeSX.Text = dtrecipe18.Rows[ipos][3].ToString().Trim();
                            //txtPlanid.Text = dtrecipe18.Rows[ipos][0].ToString().Trim();


                            //planNum = dtrecipe18.Rows[ipos][1].ToString().Trim();
                            //soMeSX = dtrecipe18.Rows[ipos][3].ToString().Trim();
                            //planID = dtrecipe18.Rows[ipos][0].ToString().Trim();
                            //string checkTem = " SELECT  [Plan_Id],[print_num],[Somecon] FROM [BB].[dbo].[TemBarcodeHC] where Plan_Id like '" + planID + "%' order by Print_dat desc ";
                            //DataTable checkTemBar = SQL.ExecuteQuery33(checkTem);
                            //if (checkTemBar.Rows.Count > 0)
                            //{
                            //    txtPlanNum.Text = checkTemBar.Rows[0][2].ToString().Trim();
                            //    txtSoMeSX.Text = checkTemBar.Rows[0][2].ToString().Trim();
                            //    txtPlanid.Text = planID;
                            //}
                            //else
                            //{
                            //    txtPlanNum.Text = planNum;
                            //    txtSoMeSX.Text = soMeSX;
                            //    txtPlanid.Text = planID;
                            //}
                        }
                        else
                        {
                            planNum = dtrecipe18.Rows[0][1].ToString().Trim();
                            soMeSX = dtrecipe18.Rows[0][3].ToString().Trim();
                            planID = dtrecipe18.Rows[0][0].ToString().Trim();
                            string checkTem = " SELECT  [Plan_Id],[print_num],[Somecon] FROM [BB].[dbo].[TemBarcodeHC] where Plan_Id like '" + planID + "%' order by Print_dat desc ";
                            DataTable checkTemBar = SQL.ExecuteQuery33(checkTem);
                            if (checkTemBar.Rows.Count > 0)
                            {
                                txtPlanNum.Text = planNum;
                                txtSoMeSX.Text = soMeSX;
                                txtPlanid.Text = planID;
                            }
                            else
                            {
                                txtPlanNum.Text = planNum;
                                txtSoMeSX.Text = soMeSX;
                                txtPlanid.Text = planID;
                            }
                            return;
                        }
                        //lbl_realnum.Text = dtrecipe15.Rows[0][3].ToString().Trim();
                        //return;
                    }
                }

            }
            catch (Exception ex)
            {
                IFormatProvider culture = new CultureInfo("en-US", true);
                txtNHL.Text = DateTime.ParseExact(txtSolo.Text, "yyyy-MM-dd", culture).AddDays(7).ToString("MM-dd");

                lblThoiGianKT.Text = "";
                txtTGSX.Text = "";

            }
        }
    }
}