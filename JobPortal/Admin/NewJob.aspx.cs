using System;
using System.Configuration;
using System.Data.SqlClient;

namespace JobPortal.Admin
{
    public partial class NewJob : System.Web.UI.Page
    {
        // Retrieve the connection string from the Web.config file
        private string str = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the session is valid, if not redirect to login
            if (Session["admin"] == null)
            {
                Response.Redirect("../User/Login.aspx");
            }

            Session["title"] = "Add Job";

            if (!IsPostBack)
            {
                filldata();
            }
        }

        private void filldata()
        {
            if (Request.QueryString["id"] != null)
            {
                using (SqlConnection con = new SqlConnection(str))
                {
                    string query = "SELECT * FROM Jobs WHERE JobId = @JobId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@JobId", Request.QueryString["id"]);
                        con.Open();

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if (sdr.HasRows)
                            {
                                while (sdr.Read())
                                {
                                    txtJobTitle.Text = sdr["Title"].ToString();
                                    txtNoOfPost.Text = sdr["NoOfPost"].ToString();
                                    txtDescription.Text = sdr["Description"].ToString();
                                    TxtQualification.Text = sdr["Qualification"].ToString();
                                    txtExperience.Text = sdr["Experience"].ToString();
                                    txtSpecialization.Text = sdr["Specialization"].ToString();
                                    txtLastDate.Text = Convert.ToDateTime(sdr["LastDateToApply"]).ToString("yyyy-MM-dd");
                                    txtSalary.Text = sdr["Salary"].ToString();
                                    ddlJobType.SelectedValue = sdr["JobType"].ToString();
                                    txtCompany.Text = sdr["CompanyName"].ToString();
                                    txtWebSite.Text = sdr["WebSite"].ToString();
                                    txtEmail.Text = sdr["Email"].ToString();
                                    txtAddress.Text = sdr["Address"].ToString();
                                    ddlCountry.SelectedValue = sdr["Country"].ToString();
                                    TxtState.Text = sdr["State"].ToString();
                                    btnAdd.Text = "Update";
                                    linkBack.Visible = true;
                                    Session["title"] = "Edit Job";
                                }
                            }
                            else
                            {
                                lblMsg.Text = "Job not found...!";
                                lblMsg.CssClass = "alert alert-danger";
                            }
                        }
                    }
                }
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string query = string.Empty, concatQuery = string.Empty, imagePath = string.Empty;
                string type;

                using (SqlConnection con = new SqlConnection(str))
                {
                    if (Request.QueryString["id"] != null)
                    {
                        if (fuCompanyLogo.HasFile)
                        {
                            if (utils.IsValidExtension(fuCompanyLogo.FileName))
                            {
                                concatQuery = "CompanyImage = @CompanyImage, ";
                            }
                            else
                            {
                                lblMsg.Text = "Please select a .jpeg, .jpg, or .png file for the logo";
                                lblMsg.CssClass = "alert alert-danger";
                                return;
                            }
                        }

                        // Update Query
                        query = $"UPDATE Jobs SET Title = @Title, NoOfPost = @NoOfPost, Description = @Description, Qualification = @Qualification, " +
                                $"Experience = @Experience, Specialization = @Specialization, LastDateToApply = @LastDateToApply, Salary = @Salary, " +
                                $"JobType = @JobType, CompanyName = @CompanyName, {concatQuery} " +
                                $"WebSite = @WebSite, Email = @Email, Address = @Address, Country = @Country, State = @State WHERE JobId = @id";
                        type = "updated";
                    }
                    else
                    {
                        // Insert Query
                        query = "INSERT INTO Jobs (Title, NoOfPost, Description, Qualification, Experience, Specialization, LastDateToApply, Salary, JobType, " +
                                "CompanyName, CompanyImage, WebSite, Email, Address, Country, State, CreateDate) " +
                                "VALUES (@Title, @NoOfPost, @Description, @Qualification, @Experience, @Specialization, @LastDateToApply, @Salary, @JobType, " +
                                "@CompanyName, @CompanyImage, @WebSite, @Email, @Address, @Country, @State, @CreateDate)";
                        type = "saved";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Title", txtJobTitle.Text.Trim());
                        cmd.Parameters.AddWithValue("@NoOfPost", txtNoOfPost.Text.Trim());
                        cmd.Parameters.AddWithValue("@Description", txtDescription.Text.Trim());
                        cmd.Parameters.AddWithValue("@Qualification", TxtQualification.Text.Trim());
                        cmd.Parameters.AddWithValue("@Experience", txtExperience.Text.Trim());
                        cmd.Parameters.AddWithValue("@Specialization", txtSpecialization.Text.Trim());
                        cmd.Parameters.AddWithValue("@LastDateToApply", txtLastDate.Text.Trim());
                        cmd.Parameters.AddWithValue("@Salary", txtSalary.Text.Trim());
                        cmd.Parameters.AddWithValue("@JobType", ddlJobType.SelectedValue);
                        cmd.Parameters.AddWithValue("@CompanyName", txtCompany.Text.Trim());
                        cmd.Parameters.AddWithValue("@WebSite", txtWebSite.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                        cmd.Parameters.AddWithValue("@Country", ddlCountry.SelectedValue);
                        cmd.Parameters.AddWithValue("@State", TxtState.Text.Trim());

                        if (Request.QueryString["id"] != null)
                        {
                            cmd.Parameters.AddWithValue("@id", Request.QueryString["id"].ToString());
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                        }

                        // Check if the company logo file is uploaded
                        if (fuCompanyLogo.HasFile && utils.IsValidExtension(fuCompanyLogo.FileName))
                        {
                            Guid obj = Guid.NewGuid();
                            imagePath = obj.ToString() + "_" + fuCompanyLogo.FileName; // Ensure unique name
                            fuCompanyLogo.PostedFile.SaveAs(Server.MapPath("~/Images/") + imagePath);
                            cmd.Parameters.AddWithValue("@CompanyImage", imagePath);
                        }
                        else
                        {
                            if (Request.QueryString["id"] == null)
                            {
                                cmd.Parameters.AddWithValue("@CompanyImage", DBNull.Value);
                            }
                        }

                        con.Open();
                        int res = cmd.ExecuteNonQuery();

                        if (res > 0)
                        {
                            lblMsg.Text = "Job " + type + " successfully!";
                            lblMsg.CssClass = "alert alert-success";
                            clear();
                        }
                        else
                        {
                            lblMsg.Text = "Unable to save the job. Please try again later.";
                            lblMsg.CssClass = "alert alert-danger";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Display any exception messages in an alert
                lblMsg.Text = "Error: " + ex.Message; // Display error message in label instead of JavaScript alert
                lblMsg.CssClass = "alert alert-danger";
            }
        }

        private void clear()
        {
            txtJobTitle.Text = string.Empty;
            txtNoOfPost.Text = string.Empty;
            txtDescription.Text = string.Empty;
            TxtQualification.Text = string.Empty;
            txtExperience.Text = string.Empty;
            txtSpecialization.Text = string.Empty;
            txtLastDate.Text = string.Empty;
            txtSalary.Text = string.Empty;
            txtCompany.Text = string.Empty;
            txtWebSite.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtAddress.Text = string.Empty;
            TxtState.Text = string.Empty;
            ddlJobType.SelectedIndex = -1;
            ddlCountry.SelectedIndex = -1;
        }

        // Helper method to validate file extensions for the logo upload
        //private bool IsValidExtension(string fileName)
        //{
        //    string[] fileExtensions = { ".jpg", ".jpeg", ".png" };
        //    foreach (string extension in fileExtensions)
        //    {
        //        if (fileName.EndsWith(extension, StringComparison.OrdinalIgnoreCase))
        //        {
        //            return true; // Return true if valid
        //        }
        //    }
        //    return false; // Return false if no valid extension found
        //}
    }
}
