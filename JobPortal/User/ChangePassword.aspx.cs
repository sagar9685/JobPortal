using System;
using System.Data.SqlClient; // Add this if you are using SQL Server
using System.Configuration; // For connection string

namespace JobPortal.User
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text; // Get the username input
            string currentPassword = txtCurrentPassword.Text;
            string newPassword = txtNewPassword.Text;
            string confirmNewPassword = txtConfirmNewPassword.Text;

            // Check if new password matches confirmation
            if (newPassword != confirmNewPassword)
            {
                lblChangePasswordMsg.Visible = true;
                lblChangePasswordMsg.Text = "New password and confirmation do not match.";
                lblChangePasswordMsg.CssClass = "alert alert-danger";
                return;
            }

            // Check if the current username and password are valid
            if (IsCurrentPasswordValid(username, currentPassword))
            {
                // Change the password in the database
                if (ChangeUserPassword(username, newPassword))
                {
                    lblChangePasswordMsg.Visible = true;
                    lblChangePasswordMsg.Text = "Password changed successfully.";
                    lblChangePasswordMsg.CssClass = "alert alert-success";
                }
                else
                {
                    lblChangePasswordMsg.Visible = true;
                    lblChangePasswordMsg.Text = "Failed to change password. Please try again.";
                    lblChangePasswordMsg.CssClass = "alert alert-danger";
                }
            }
            else
            {
                lblChangePasswordMsg.Visible = true;
                lblChangePasswordMsg.Text = "Current username and password do not match.";
                lblChangePasswordMsg.CssClass = "alert alert-danger";
            }
        }

        // Method to check if the current password is valid
        private bool IsCurrentPasswordValid(string username, string password)
        {
            bool isValid = false;

            // Replace with your actual connection string
            string connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM [User] WHERE Username = @Username AND Password = @Password"; // Use hashed passwords in production!
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password); // Hash this password in production
                    int count = (int)cmd.ExecuteScalar();
                    isValid = (count > 0);
                }
            }

            return isValid;
        }

        // Method to change the user's password in the database
        private bool ChangeUserPassword(string username, string newPassword)
        {
            bool isChanged = false;

            // Replace with your actual connection string
            string connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE [User] SET Password = @Password WHERE Username = @Username"; // Use hashed passwords in production!
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", newPassword); // Hash this password in production
                    int rowsAffected = cmd.ExecuteNonQuery();
                    isChanged = (rowsAffected > 0);
                }
            }

            return isChanged;
        }
    }
}
