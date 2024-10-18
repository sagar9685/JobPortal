using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobPortal.User
{
    public partial class Login : System.Web.UI.Page
    {
        string str = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        string username, password = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlLoginType.SelectedValue == "Admin")
                {
                    // Fetch admin credentials from AppSettings
                    username = ConfigurationManager.AppSettings["username"];
                    password = ConfigurationManager.AppSettings["password"];

                    // Compare input with admin credentials
                    if (username == txtUserName.Text.Trim() && password == TxtPassword.Text.Trim())
                    {
                        Session["admin"] = username;
                        Response.Redirect("../Admin/Dashboard.aspx", false);
                    }
                    else
                    {
                        showErrorMsg("Admin");
                    }
                }
                else
                {
                    // For User Login
                    using (SqlConnection con = new SqlConnection(str))
                    {
                        string query = @"SELECT * FROM [User] WHERE Username = @UserName AND Password = @Password";

                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            // Use proper parameter names
                            cmd.Parameters.AddWithValue("@UserName", txtUserName.Text.Trim());
                            cmd.Parameters.AddWithValue("@Password", TxtPassword.Text.Trim());

                            con.Open();
                            SqlDataReader sdr = cmd.ExecuteReader();

                            if (sdr.Read())
                            {
                                // Save session for user
                                Session["user"] = sdr["UserName"].ToString();
                                Session["userId"] = sdr["UserId"].ToString();
                                Response.Redirect("Default.aspx", false);
                            }
                            else
                            {
                                showErrorMsg("User");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception and display error message
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        // Method to display error message
        private void showErrorMsg(string userType)
        {
            lblMsg.Visible = true;
            lblMsg.Text = "<b>" + userType + "</b> credentials are incorrect....!";
            lblMsg.CssClass = "alert alert-danger";
        }
        protected void ValidateCaptcha(object sender, ServerValidateEventArgs e)
        {
            string captchaResponse = Request.Form["g-recaptcha-response"];
            bool isValidCaptcha = ValidateRecaptcha(captchaResponse);
            e.IsValid = isValidCaptcha;
        }

        private bool ValidateRecaptcha(string captchaResponse)
        {
            var secretKey = "6Lc58mMqAAAAAHUF9L_Sfc8xbsDdf2Ore1jyDymL"; // Use your reCAPTCHA secret key here
            var apiUrl = $"https://www.google.com/recaptcha/api/siteverify?secret={secretKey}&response={captchaResponse}";

            using (WebClient client = new WebClient())
            {
                string jsonResult = client.DownloadString(apiUrl);
                JavaScriptSerializer js = new JavaScriptSerializer();
                dynamic result = js.Deserialize<dynamic>(jsonResult);

                // Return true if reCAPTCHA verification is successful
                return Convert.ToBoolean(result["success"]);
            }
        }



    }
}
