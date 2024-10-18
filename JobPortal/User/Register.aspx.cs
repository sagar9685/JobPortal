using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace JobPortal.User
{
    public partial class Register : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        string str = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                using (con = new SqlConnection(str))
                {
                    string query = @"INSERT INTO [User] (Username,Password,Name,Address,Mobile,Email,Country) 
                                     VALUES (@UserName,@Password,@Name,@Address,@Mobile,@Email,@Country)";
                    using (cmd = new SqlCommand(query, con))
                    {
                        // Assuming name, email, subject, message are server controls (like <input> with runat="server")
                        cmd.Parameters.AddWithValue("@UserName", txtUserName.Text.Trim());
                        cmd.Parameters.AddWithValue("@Password", TxtConfirmPassword.Text.Trim());
                        cmd.Parameters.AddWithValue("@Name", TxtFullName.Text.Trim());
                        cmd.Parameters.AddWithValue("@Address", TxtAddress.Text.Trim());
                        cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email", TxtEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@Country", ddlCountry.SelectedValue);

                        con.Open();
                        int result = cmd.ExecuteNonQuery(); // Execute the query

                        if (result > 0) // If a row was affected, show success
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = "Registered Successfully!";
                            lblMsg.CssClass = "alert alert-success";
                            Clear(); // Clear the input fields
                        }
                        else
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = "Could not save the record. Please try again later.";
                            lblMsg.CssClass = "alert alert-danger";
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627) // Unique Key Violation Error Number
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "<b>" + txtUserName.Text.Trim() + "</b> Username already exists! Try a different one.";
                    lblMsg.CssClass = "alert alert-danger";
                }
                else
                {
                    // For any other SQL exceptions
                    lblMsg.Visible = true;
                    lblMsg.Text = "An error occurred: " + ex.Message;
                    lblMsg.CssClass = "alert alert-danger";
                }
            }

            catch (Exception ex)
            {
                Response.Write("<scipt>alert('" + ex.Message + "');</script>");
            }
            finally
            {
                con.Close();    
            }
        }

        private void Clear()
        {
            
            txtUserName.Text=string.Empty;
            TxtPassword.Text=string.Empty;  
            TxtConfirmPassword.Text=string.Empty;
            TxtFullName.Text=string.Empty;
            TxtAddress.Text=string.Empty;
            txtMobile.Text=string.Empty;        
            TxtEmail.Text=string.Empty;
            ddlCountry.ClearSelection();
        }
    }
}