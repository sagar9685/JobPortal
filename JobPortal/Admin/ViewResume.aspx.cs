using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobPortal.Admin
{
    public partial class NewResume : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        string str = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["admin"] == null)
            {
                Response.Redirect("../User/Login.aspx");
            }

            if (!IsPostBack)
            {
                showAppliedJob(); // Load job applications
            }
        }

        private void showAppliedJob()
        {
            string query = @"SELECT Row_Number() OVER (ORDER BY (SELECT 1)) AS SrNo, 
                             aj.AppliedJobId, j.CompanyName, aj.JobId, j.Title, 
                             u.Mobile, u.Name, u.Email, u.Resume 
                             FROM AppliedJobs aj 
                             INNER JOIN [User] u ON aj.UserId = u.UserId
                             INNER JOIN Jobs j ON aj.JobId = j.JobId";

            try
            {
                using (con = new SqlConnection(str))
                {
                    using (cmd = new SqlCommand(query, con))
                    {
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        dt = new DataTable();
                        sda.Fill(dt);  // This is where the error may occur
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                lblMsg.Text = "SQL Error: " + sqlEx.Message;
                lblMsg.CssClass = "alert alert-danger";
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Error: " + ex.Message;
                lblMsg.CssClass = "alert alert-danger";
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            showAppliedJob();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int appliedJobId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);

                using (con = new SqlConnection(str))
                {
                    using (cmd = new SqlCommand("DELETE FROM AppliedJobs WHERE AppliedJobId = @id", con))
                    {
                        cmd.Parameters.AddWithValue("@id", appliedJobId);
                        con.Open();
                        int r = cmd.ExecuteNonQuery();

                        if (r > 0)
                        {
                            lblMsg.Text = "Resume Deleted Successfully";
                            lblMsg.CssClass = "alert alert-success";
                        }
                        else
                        {
                            lblMsg.Text = "Something went wrong!";
                            lblMsg.CssClass = "alert alert-danger";
                        }
                    }
                }

                showAppliedJob();
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Error: " + ex.Message;
                lblMsg.CssClass = "alert alert-danger";
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes["onclick"]=Page.ClientScript.GetPostBackClientHyperlink(GridView1,"select$"+e.Row.RowIndex);
            e.Row.ToolTip = "click to view job Details";

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach(GridViewRow row in GridView1.Rows)
            {
                if(row.RowIndex == GridView1.SelectedIndex)
                {
                    HiddenField jobId = (HiddenField)row.FindControl("hdnJobId");
                    Response.Redirect("JobList.aspx?id="+jobId.Value);
                }
                else
                {
                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                    row.ToolTip = "click to select this row";
                }
            }
        }
    }
}
