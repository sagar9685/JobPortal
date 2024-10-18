using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobPortal.Admin
{
    public partial class UserList : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        DataTable dt;
        string str = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["admin"]==null)
            {
                Response.Redirect("../User/Login.aspx");
            }
            if (!IsPostBack)
            {
                showUsers();
            }
        }
        private void showUsers()
        {
            string query = @"SELECT Row_Number() OVER (ORDER BY (SELECT 1)) AS SrNo, 
                            UserId, Name, Email, Mobile,Country  FROM [User]";
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
            showUsers();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int UserId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
                using (con = new SqlConnection(str))
                {
                    using (cmd = new SqlCommand("DELETE FROM [User] WHERE UserId = @UserId", con)) // Corrected parameter name
                    {
                        cmd.Parameters.AddWithValue("@UserId", UserId);
                        con.Open();
                        int r = cmd.ExecuteNonQuery();
                        if (r > 0)
                        {
                            lblMsg.Text = "User Deleted Successfully";
                            lblMsg.CssClass = "alert alert-success";
                        }
                        else
                        {
                            lblMsg.Text = "Something went wrong..!";
                            lblMsg.CssClass = "alert alert-danger";
                        }
                    }
                }

                showUsers(); // Refresh the GridView
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
    }
}