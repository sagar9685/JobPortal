using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobPortal.Admin
{
    public partial class ContactList : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DataTable dt;
        string str = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["admin"] == null)
            {
                Response.Redirect("../User/Login.aspx");
            }

            // Show contacts on initial load and on postbacks triggered by GridView events
            if (!IsPostBack)
            {
                showContact();
            }
        }

        private void showContact()
        {
            string query = @"SELECT Row_Number() OVER (ORDER BY (SELECT 1)) AS SrNo, 
                            ContactId, Name, Email, Subject, Message  FROM contact";
            using (con = new SqlConnection(str))
            {
                using (cmd = new SqlCommand(query, con))
                {
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    sda.Fill(dt);
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            showContact();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int ContactId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
                using (con = new SqlConnection(str))
                {
                    using (cmd = new SqlCommand("DELETE FROM contact WHERE ContactId = @ContactId", con)) // Corrected parameter name
                    {
                        cmd.Parameters.AddWithValue("@ContactId", ContactId);
                        con.Open();
                        int r = cmd.ExecuteNonQuery();
                        if (r > 0)
                        {
                            lblMsg.Text = "Contact Deleted Successfully";
                            lblMsg.CssClass = "alert alert-success";
                        }
                        else
                        {
                            lblMsg.Text = "Something went wrong..!";
                            lblMsg.CssClass = "alert alert-danger";
                        }
                    }
                }

                showContact(); // Refresh the GridView
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Error: " + ex.Message;
                lblMsg.CssClass = "alert alert-danger";
            }
        }
    }
}
