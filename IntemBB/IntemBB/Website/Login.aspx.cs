using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntemBB.Website
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string user = txtSothe.Text.Trim();
            string pass =txtPassword.Text.Trim();
            string name = "";
            if (user == "014185")
            {
                name = "Tùng";
            }
            if (user == "005571")
            {
                name = "Thuần";
            }
            if(user=="020569")
            {
                name = "Chương";
            }
            if (user == "213785")
            {
                name = "Jen Hao";
            }
            if (user == "018892")
            {
                name = "Hạ";
            }
            if (user == "023999")
            {
                name = "Minh Đăng";
            }
            if (user == "025839")
            {
                name = "Đức";
            }
            string sql_rerult = "SELECT * FROM[BB].[dbo].[kmt_oper]where role = '" + 2 + "' and [active]='1'";
            DataTable dt_resule = SQL.ExecuteQuery33(sql_rerult);
            if (dt_resule.Rows.Count == 0)
            {
                lblthongbao.Text= "Chương trình bị quá hạn vui lòng thử lại sau";
                return;
            }
            else
            {
                if ((user == "1" && pass == "321") || (user == "025839" && pass == "025839")  || (user == "014185" && pass == "thinghiemp8700") || (user == "005571" && pass == "thinghiemp8700") || (user == "020569" && pass == "thinghiemp8700") || (user == "213785" && pass == "thinghiemp8700") || (user == "018892" && pass == "ha19950305") || (user == "023999" && pass == "123456"))
                {
                    SQL.ueser = txtSothe.Text.Trim();
                    Session["username"] = name.Trim();
                    Session.Timeout = 600;
                    Response.Redirect("/Website/IntemBB.aspx");
                }
                else
                {
                    lblthongbao.Text = "Tài khoản hoặc mật khẩu không đúng!";
                }
            }
           
        }
    }
}