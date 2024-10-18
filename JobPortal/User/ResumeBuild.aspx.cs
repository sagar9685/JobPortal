using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;

namespace JobPortal.User
{
    public partial class ResumeBuild : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd;
        SqlDataReader sdr;
        string str = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("login.aspx");
            }

            if (!IsPostBack) // Use !IsPostBack to load user info only once
            {
                if (Request.QueryString["id"] != null)
                {
                    showUserInfo();
                }
                else
                {
                    Response.Redirect("login.aspx");
                }
            }
        }

        private void showUserInfo()
        {
            try
            {
                con = new SqlConnection(str);
                string query = "SELECT * FROM [User] WHERE UserId=@UserId";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UserId", Request.QueryString["id"]);
                con.Open();
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    if (sdr.Read())
                    {
                        txtUserName.Text = sdr["UserName"].ToString();
                        TxtFullName.Text = sdr["Name"].ToString();
                        TxtEmail.Text = sdr["Email"].ToString();
                        txtMobile.Text = sdr["Mobile"].ToString();
                        TxtTenth.Text = sdr["TenthGrade"].ToString();
                        txtTwelth.Text = sdr["TwelthGrade"].ToString();
                        txtGraduation.Text = sdr["GraduationGrade"].ToString();
                        txtPostGraduation.Text = sdr["PostGraduationGrade"].ToString();
                        txtPhd.Text = sdr["PHD"].ToString();
                        txtWork.Text = sdr["WorksOn"].ToString(); // Use the correct column name
                        txtExperience.Text = sdr["Experience"].ToString(); // Check spelling
                        TxtAddress.Text = sdr["Address"].ToString();
                        ddlCountry.SelectedValue = sdr["Country"].ToString();
                    }
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "User not found.";
                    lblMsg.CssClass = "alert alert-danger";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Error: " + ex.Message; // Display the error message in lblMsg
                lblMsg.CssClass = "alert alert-danger";
            }
            finally
            {
                con.Close();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["id"] != null)
                {
                    string concatQuery = string.Empty;
                    string filePath = string.Empty;
                    bool isValid = true; // Assume valid until proven otherwise

                    con = new SqlConnection(str);

                    // Ensure the Resumes folder exists before saving the file
                    string resumeFolderPath = Server.MapPath("~/Resumes/");
                    if (!Directory.Exists(resumeFolderPath))
                    {
                        Directory.CreateDirectory(resumeFolderPath); // Create folder if it doesn't exist
                    }

                    if (fuResume.HasFile)
                    {
                        if (utils.IsValidExtension4Resume(fuResume.FileName))
                        {
                            concatQuery = "Resume=@resume,";
                        }
                        else
                        {
                            isValid = false;
                            lblMsg.Visible = true;
                            lblMsg.Text = "Resume should be in .doc, .docx, or .pdf format.";
                            lblMsg.CssClass = "alert alert-danger";
                        }
                    }

                    string query = @"UPDATE [User] SET 
                                    UserName=@Username,
                                    Name=@Name,
                                    Email=@Email,
                                    Mobile=@Mobile,
                                    TenthGrade=@TenthGrade,
                                    TwelthGrade=@TwelthGrade,
                                    GraduationGrade=@GraduationGrade,
                                    PostGraduationGrade=@PostGraduationGrade,
                                    Phd=@Phd,
                                    WorksOn=@WorksOn,
                                    Experience=@Experience," + concatQuery + @"
                                    Address=@Address,
                                    Country=@Country 
                                    WHERE UserId=@UserId";

                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Username", txtUserName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Name", TxtFullName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", TxtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text.Trim());
                    cmd.Parameters.AddWithValue("@TenthGrade", TxtTenth.Text.Trim());
                    cmd.Parameters.AddWithValue("@TwelthGrade", txtTwelth.Text.Trim());
                    cmd.Parameters.AddWithValue("@GraduationGrade", txtGraduation.Text.Trim());
                    cmd.Parameters.AddWithValue("@PostGraduationGrade", txtPostGraduation.Text.Trim());
                    cmd.Parameters.AddWithValue("@Phd", txtPhd.Text.Trim());
                    cmd.Parameters.AddWithValue("@WorksOn", txtWork.Text.Trim());
                    cmd.Parameters.AddWithValue("@Experience", txtExperience.Text.Trim());
                    cmd.Parameters.AddWithValue("@Address", TxtAddress.Text.Trim());
                    cmd.Parameters.AddWithValue("@Country", ddlCountry.SelectedValue);
                    cmd.Parameters.AddWithValue("@UserId", Request.QueryString["id"]);

                    if (isValid)
                    {
                        if (fuResume.HasFile)
                        {
                            string fileName = Path.GetFileName(fuResume.FileName);
                            string extension = Path.GetExtension(fuResume.FileName);
                            Guid obj = Guid.NewGuid();
                            filePath = $"Resumes/{obj.ToString()}{extension}"; // Keep filename shorter

                            // Save the file to the folder
                            fuResume.PostedFile.SaveAs(Path.Combine(resumeFolderPath, obj.ToString() + extension));
                            cmd.Parameters.AddWithValue("@resume", filePath);
                        }

                        con.Open();
                        int r = cmd.ExecuteNonQuery();
                        if (r > 0)
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = "Resume details updated successfully!";
                            lblMsg.CssClass = "alert alert-success";
                        }
                        else
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = "Cannot update the record, please try again later.";
                            lblMsg.CssClass = "alert alert-danger";
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("Violation of UNIQUE KEY constraint"))
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "<b>" + txtUserName.Text.Trim() + "</b> username already exists! Try a different one.";
                    lblMsg.CssClass = "alert alert-danger";
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "SQL Error: " + ex.Message; // Display SQL error messages
                    lblMsg.CssClass = "alert alert-danger";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Error: " + ex.Message; // Display generic error messages
                lblMsg.CssClass = "alert alert-danger";
            }
            finally
            {
                con.Close();
            }
        }
    }
}
