using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobPortal.User
{
    public partial class Profile : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        string str = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("login.aspx");
            }

            if (!IsPostBack)
            {
                showUserProfile();
            }
        }

        private void showUserProfile()
        {
            using (con = new SqlConnection(str))
            {
                string query = @"SELECT UserId, UserName, Name, Address, Mobile, Email, Country, Resume 
                                 FROM [User] WHERE UserName = @username";

                using (cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@username", Session["user"].ToString());
                    sda = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        dlprofile.DataSource = dt;
                        dlprofile.DataBind();
                    }
                    else
                    {
                        Response.Write("<script>alert('Please log in again with your latest username.');</script>");
                    }
                }
            }
        }

        protected void dlprofile_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "EditUserProfile")
            {
                Response.Redirect("ResumeBuild.aspx?id=" + e.CommandArgument.ToString());
            }
        }
    }
}
