using Google.Protobuf.Reflection;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobPortal.Admin
{
    public partial class JobList : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        DataTable dt;
        string str = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (Session["admin"] == null)
            {
                Response.Redirect("../User/Login.aspx");
            }

            if (!IsPostBack)
            {
                showJob();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // No need to call showJob() here as it's already called in Page_PreRender
        }

        private void showJob()
        {
            string query = @"SELECT Row_Number() OVER (ORDER BY (SELECT 1)) AS SrNo, 
                            JobId, Title, NoOfPost, Qualification, Experience, 
                            LastDateToApply, CompanyName, Country, State, CreateDate FROM Jobs";
            using (con = new SqlConnection(str))
            {
                using (cmd = new SqlCommand(query, con))
                {
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    sda.Fill(dt);
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    if (Request.QueryString["id"] != null)
                    {
                        linkBack.Visible = true;
                    }
                }
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            showJob();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int jobId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
                using (con = new SqlConnection(str))
                {
                    using (cmd = new SqlCommand("DELETE FROM Jobs WHERE JobId = @id", con))
                    {
                        cmd.Parameters.AddWithValue("@id", jobId);
                        con.Open();
                        int r = cmd.ExecuteNonQuery();
                        if (r > 0)
                        {
                            lblMsg.Text = "Job Deleted Successfully";
                            lblMsg.CssClass = "alert alert-success";
                        }
                        else
                        {
                            lblMsg.Text = "Something went wrong..!";
                            lblMsg.CssClass = "alert alert-danger";
                        }
                    }
                }

                showJob(); // Refresh the GridView
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Error: " + ex.Message;
                lblMsg.CssClass = "alert alert-danger";
            }
            finally
            {
                con.Close();
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if(e.CommandName == "EditJob")
                {
                    Response.Redirect("NewJob.aspx?id=" +e.CommandArgument.ToString());

                }

            }
            catch(Exception ex)
            {

            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // Check if the current row is a data row
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Set the ID of the row to its index
                e.Row.ID = e.Row.RowIndex.ToString();

                // Check if the "id" parameter is in the query string
                if (Request.QueryString["id"] != null)
                {
                    // Retrieve the job ID from the data key
                    int jobId = Convert.ToInt32(GridView1.DataKeys[e.Row.RowIndex].Value);

                    // Check if it matches the query string "id"
                    if (jobId == Convert.ToInt32(Request.QueryString["id"]))
                    {
                        // Change the background color to highlight the row
                        e.Row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                    }
                }
            }
        }
    }
}
