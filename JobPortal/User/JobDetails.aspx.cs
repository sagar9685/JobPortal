using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;
using System;

//job details,,
//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Data;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;

namespace JobPortal.User
{
    public partial class JobDetails : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt, dt1;
        string str = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        public string JobTitle = string.Empty;
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                showJobDetail();
                DataBind();
            }
            else
            {
                Response.Redirect("JobListing.aspx");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        private void showJobDetail()
        {

            con = new SqlConnection(str);
            string query = @"select * from Jobs where JobId=@id ";
            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            DataList1.DataSource = dt;
            DataList1.DataBind();
            JobTitle = dt.Rows[0]["title"].ToString();

        }

        protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "ApplyJob")
            {
                if (Session["user"] != null)
                {
                    try
                    {
                        con = new SqlConnection(str);
                        con.Open();

                        // Check if user profile is complete by ensuring Email, Mobile, and Resume are not empty
                        string checkProfileQuery = @"SELECT COUNT(1) FROM [User] 
                                             WHERE UserId = @UserId 
                                             AND Email IS NOT NULL AND Email <> ''
                                             AND Mobile IS NOT NULL AND Mobile <> ''
                                             AND Resume IS NOT NULL AND Resume <> ''";
                        SqlCommand checkProfileCmd = new SqlCommand(checkProfileQuery, con);
                        checkProfileCmd.Parameters.AddWithValue("@UserId", Session["userId"]);
                        int profileComplete = (int)checkProfileCmd.ExecuteScalar();

                        if (profileComplete == 0)
                        {
                            // If profile is not complete, redirect to profile page
                            lblMsg.Visible = true;
                            lblMsg.Text = "Please complete your profile before applying for jobs.";
                            lblMsg.CssClass = "alert alert-warning";
                            Response.Redirect("profile.aspx");
                            return;
                        }

                        // If profile is complete, proceed to apply for the job
                        string query = "INSERT INTO AppliedJobs (JobId, UserId) VALUES (@JobId, @UserId)";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@JobId", Request.QueryString["id"]);
                        cmd.Parameters.AddWithValue("@UserId", Session["userId"]);

                        int r = cmd.ExecuteNonQuery();
                        if (r > 0)
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = "Job applied successfully!";
                            lblMsg.CssClass = "alert alert-success";
                            showJobDetail();
                        }
                        else
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = "Failed to apply, please try again later.";
                            lblMsg.CssClass = "alert alert-danger";
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('" + ex.Message + "');</script>");
                    }
                    finally
                    {
                        con.Close();
                    }
                }
                else
                {
                    // Redirect to login page if the user is not logged in
                    Response.Redirect("Login.aspx");
                }
            }
        }


        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (Session["user"] != null)
            {
                LinkButton btnApplyJob = e.Item.FindControl("lbApplyJob") as LinkButton;
                if (isApplied())
                {
                    btnApplyJob.Enabled = false;
                    btnApplyJob.Text = "Applied";
                }
                else
                {
                    btnApplyJob.Enabled = true;
                    btnApplyJob.Text = "Apply Now";
                }
            }
        }

        bool isApplied()
        {
            con = new SqlConnection(str);
            string query = @"select * from AppliedJobs where UserId=@UserId and JobId=@JobId ";
            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@UserId", Session["UserId"]);
            cmd.Parameters.AddWithValue("@JobId", Request.QueryString["id"]);
            sda = new SqlDataAdapter(cmd);
            dt1 = new DataTable();
            sda.Fill(dt1);
            return dt1.Rows.Count > 0;
        }
        protected string GetImageUrl(object url)
        {
            return url == DBNull.Value || string.IsNullOrEmpty(url.ToString())
                ? ResolveUrl("~/Images/No_image.png")
                : ResolveUrl("~/Images/" + url.ToString().Trim());
        }
    }
}