using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace JobPortal.User
{
    public partial class Contact : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        string str = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMsg.Visible = false; // Ensure that the label is hidden initially
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                using (con = new SqlConnection(str))
                {
                    string query = @"INSERT INTO Contact (Name, Email, Subject, Message) 
                                     VALUES (@Name, @Email, @Subject, @Message)";
                    using (cmd = new SqlCommand(query, con))
                    {
                        // Assuming name, email, subject, message are server controls (like <input> with runat="server")
                        cmd.Parameters.AddWithValue("@Name", name.Value.Trim());
                        cmd.Parameters.AddWithValue("@Email", email.Value.Trim());
                        cmd.Parameters.AddWithValue("@Subject", subject.Value.Trim());
                        cmd.Parameters.AddWithValue("@Message", message.Value.Trim());

                        con.Open();
                        int result = cmd.ExecuteNonQuery(); // Execute the query

                        if (result > 0) // If a row was affected, show success
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = "Thanks For Reaching Out. We Will Look Into Your Query!";
                            lblMsg.CssClass = "alert alert-success";
                            ClearFields(); // Clear the input fields
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
            catch (Exception ex)
            {
                // Displaying a friendly message, while logging can be done for detailed error reporting
                lblMsg.Visible = true;
                lblMsg.Text = "An error occurred: " + ex.Message;
                lblMsg.CssClass = "alert alert-danger";
            }
        }

        // Method to clear all the input fields
        private void ClearFields()
        {
            name.Value = string.Empty;
            email.Value = string.Empty;
            subject.Value = string.Empty;
            message.Value = string.Empty;
        }
    }
}
