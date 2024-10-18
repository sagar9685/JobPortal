using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobPortal.User
{
    public partial class JobListing : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter sda;
        DataTable dt;
        string str = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        public int JobCount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                showJobList(); // Load all jobs initially
                RBSelectedColorChange(); // Adjust radio button style
            }
        }

        private void RBSelectedColorChange()
        {
            if (RadioButtonList1.SelectedItem != null)
            {
                RadioButtonList1.SelectedItem.Attributes.Add("class", "selectedradio");
            }
        }
        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Initialize the connection string from web.config
            string connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

            // Check if the connection string is null or empty
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("The connection string is not configured.");
            }

            // Using statement to manage connection object
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Open the connection before executing commands
                con.Open();

                if (RadioButtonList1.SelectedValue != "0")
                {
                    string postDate = selectRadioButton(); // Ensure this method returns a valid string

                    // Ensure postDate is valid before executing the query
                    if (!string.IsNullOrEmpty(postDate))
                    {
                        string query = @"SELECT JobId, Title, Salary, JobType, CompanyName, CompanyImage, Country, State, CreateDate 
                                 FROM Jobs WHERE Convert(Date, CreateDate) " + postDate;

                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                            {
                                DataTable dt = new DataTable();
                                sda.Fill(dt);
                                // Ensure you handle the DataTable (dt) as needed
                            }
                        }
                    }
                    else
                    {
                        // Handle case where postDate is invalid (e.g., log error or show message)
                    }

                    showJobList();
                    RBSelectedColorChange();
                }
                else
                {
                    showJobList();
                    RBSelectedColorChange();
                }

                // Additional filtering logic
                List<string> jobTypes = GetSelectedJobTypes();
                string country = ddlCountry.SelectedValue != "0" ? ddlCountry.SelectedValue : null;
                string postedWithin = RadioButtonList1.SelectedValue;

                showJobList(country, jobTypes, postedWithin); // Apply filtering
            } // Automatically closes the connection here
        }



        private string selectRadioButton()
        {
            string postedDate= string.Empty;
            DateTime date = DateTime.Today;
            if (RadioButtonList1.SelectedValue == "1")
            {
                postedDate = " =Convert(Date,'" + date.ToString("yyyy/MM/dd") + "')";
            }
            else if(RadioButtonList1.SelectedValue == "2")
            {
                postedDate ="between Convert (Date,'"+DateTime.Now.AddDays(-2).ToString("yyyy/MM/dd")+"')and Convert(Date,'"+date.ToString("yyyy/MM/dd")+"')";
            }
            else if (RadioButtonList1.SelectedValue == "3")
            {
                postedDate = "between Convert (Date,'" + DateTime.Now.AddDays(-3).ToString("yyyy/MM/dd") + "')and Convert(Date,'" + date.ToString("yyyy/MM/dd") + "')";
            }
            else if (RadioButtonList1.SelectedValue == "4")
            {
                postedDate = "between Convert (Date,'" + DateTime.Now.AddDays(-5).ToString("yyyy/MM/dd") + "')and Convert(Date,'" + date.ToString("yyyy/MM/dd") + "')";
            }
            else 
            {
                postedDate = "between Convert (Date,'" + DateTime.Now.AddDays(-10).ToString("yyyy/MM/dd") + "')and Convert(Date,'" + date.ToString("yyyy/MM/dd") + "')";
            }
            return postedDate;
        }


        // Main function to display job list with optional filters for country and job types
        private void showJobList(string country = null, List<string> jobTypes = null, string postedWithin = null)
        {
            using (SqlConnection con = new SqlConnection(str))
            {
                string query = @"SELECT JobId, Title, Salary, JobType, CompanyName, CompanyImage, Country, State, CreateDate 
                         FROM Jobs WHERE 1=1";

                // Add filtering conditions based on parameters
                if (!string.IsNullOrEmpty(country))
                {
                    query += " AND Country = @Country";
                }

                if (jobTypes != null && jobTypes.Count > 0)
                {
                    // Construct IN clause for job types with proper parameter placeholders
                    string jobTypePlaceholders = string.Join(",", jobTypes.Select((jt, index) => $"@JobType{index}"));
                    query += $" AND JobType IN ({jobTypePlaceholders})";
                }

                // Add filter for postedWithin (days)
                if (!string.IsNullOrEmpty(postedWithin) && postedWithin != "0")
                {
                    query += " AND DATEDIFF(day, CreateDate, GETDATE()) <= @PostedWithin";
                }

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameters for country
                    if (!string.IsNullOrEmpty(country))
                    {
                        cmd.Parameters.AddWithValue("@Country", country);
                    }

                    // Add parameters for job types
                    if (jobTypes != null && jobTypes.Count > 0)
                    {
                        for (int i = 0; i < jobTypes.Count; i++)
                        {
                            cmd.Parameters.AddWithValue($"@JobType{i}", jobTypes[i].Trim());
                        }
                    }

                    // Add parameter for postedWithin
                    if (!string.IsNullOrEmpty(postedWithin) && postedWithin != "0")
                    {
                        cmd.Parameters.AddWithValue("@PostedWithin", postedWithin);
                    }

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        DataList1.DataSource = dt;
                        DataList1.DataBind();
                        JobCount = dt.Rows.Count;
                        lbljobCount.InnerText = JobCount.ToString();
                    }
                }
            }
        }


        // Event handler for country selection
        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> jobTypes = GetSelectedJobTypes();
            string postedWithin = RadioButtonList1.SelectedValue; // Add postedWithin from RadioButtonList

            if (ddlCountry.SelectedValue != "0") // Assuming "0" means "Select Country"
            {
                showJobList(ddlCountry.SelectedValue, jobTypes, postedWithin); // Apply filtering
            }
            else
            {
                showJobList(null, jobTypes, postedWithin); // No country filter applied
            }
        }


        // Event handler for job type checkbox selection
        protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get selected job types
            List<string> jobTypes = GetSelectedJobTypes();
            string country = ddlCountry.SelectedValue != "0" ? ddlCountry.SelectedValue : null;
            string postedWithin = RadioButtonList1.SelectedValue; // Add postedWithin from RadioButtonList

            showJobList(country, jobTypes, postedWithin); // Apply filtering
        }


        // Helper function to get the selected job types from the CheckBoxList
        private List<string> GetSelectedJobTypes()
        {
            List<string> selectedJobTypes = new List<string>();
            foreach (ListItem item in CheckBoxList1.Items)
            {
                if (item.Selected)
                {
                    selectedJobTypes.Add(item.Text);
                }
            }
            return selectedJobTypes;
        }

        // Returns the image URL for the company logo or a default if none exists
        protected string GetImageUrl(object url)
        {
            return url == DBNull.Value || string.IsNullOrEmpty(url.ToString())
                ? ResolveUrl("~/Images/No_image.png")
                : ResolveUrl("~/Images/" + url.ToString().Trim());
        }

        // Displays the relative date for job postings
        public static string RelativeDate(DateTime theDate)
        {
            Dictionary<long, string> thresholds = new Dictionary<long, string>
            {
                { 60, "{0} seconds ago" },
                { 60 * 2, "a minute ago" },
                { 45 * 60, "{0} minutes ago" },
                { 120 * 60, "an hour ago" },
                { 24 * 60 * 60, "{0} hours ago" },
                { 24 * 60 * 60 * 2, "yesterday" },
                { 30 * 24 * 60 * 60, "{0} days ago" },
                { 365 * 24 * 60 * 60, "{0} months ago" },
                { long.MaxValue, "{0} years ago" }
            };

            long since = (DateTime.Now.Ticks - theDate.Ticks) / 10000000;

            foreach (long threshold in thresholds.Keys)
            {
                if (since < threshold)
                {
                    TimeSpan t = new TimeSpan((DateTime.Now.Ticks - theDate.Ticks));
                    return string.Format(thresholds[threshold],
                        (t.Days > 365 ? t.Days / 365 :
                        (t.Days > 0 ? t.Days :
                        (t.Hours > 0 ? t.Hours :
                        (t.Minutes > 0 ? t.Minutes :
                        (t.Seconds > 0 ? t.Seconds : 0)))))).ToString();
                }
            }

            return "";
        }

        protected void lbFilter_Click(object sender, EventArgs e)
        {
            try
            {
                // Initialize the connection string from web.config
                string connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

                // Check if the connection string is null or empty
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new InvalidOperationException("The connection string is not configured.");
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    List<string> queryList = new List<string>();
                    string countryCondition = string.Empty;
                    string jobTypeCondition = string.Empty;
                    string postedDateCondition = string.Empty;

                    // Check if country is selected
                    if (ddlCountry.SelectedValue != "0")
                    {
                        countryCondition = "Country = @Country";
                        queryList.Add(countryCondition);
                    }

                    // Get selected job types
                    jobTypeCondition = selectedcheckBox();
                    if (!string.IsNullOrEmpty(jobTypeCondition))
                    {
                        queryList.Add($"JobType IN ({jobTypeCondition})");
                    }

                    // Get posted date condition from the radio button list
                    if (RadioButtonList1.SelectedValue != "0")
                    {
                        postedDateCondition = selectRadioButton();
                        queryList.Add("Convert(Date, CreateDate)" + postedDateCondition);
                    }

                    // Construct the base query
                    string query = @"SELECT JobId, Title, Salary, JobType, CompanyName, CompanyImage, Country, State, CreateDate 
                             FROM Jobs";

                    if (queryList.Count > 0)
                    {
                        // Combine all conditions with 'AND'
                        query += " WHERE " + string.Join(" AND ", queryList);
                    }

                    // Set up the SQL command
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Add parameters for the country if selected
                        if (!string.IsNullOrEmpty(countryCondition))
                        {
                            cmd.Parameters.AddWithValue("@Country", ddlCountry.SelectedValue);
                        }

                        // Open the connection and execute the query
                        con.Open();
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            dt = new DataTable();
                            sda.Fill(dt);
                            DataList1.DataSource = dt;
                            DataList1.DataBind();
                            JobCount = dt.Rows.Count;
                            lbljobCount.InnerText = JobCount.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }



        private string selectedcheckBox()
        {
            string jobType = string.Empty;
            for (int i = 0; i < CheckBoxList1.Items.Count; i++)
            {
                if (CheckBoxList1.Items[i].Selected)
                {
                    jobType += "'" + CheckBoxList1.Items[i].Text + "',";
                }
            }
            return jobType = jobType.TrimEnd(',');
        }

        protected void lbReset_Click(object sender, EventArgs e)
        {
            ddlCountry.SelectedIndex = 0; // Reset country dropdown
            foreach (ListItem item in CheckBoxList1.Items) item.Selected = false; // Reset job type checkboxes
            RadioButtonList1.ClearSelection(); // Reset the radio button selection
            RadioButtonList1.Items[0].Selected = true; // Select the "Any" option by default

            showJobList(); // Show all jobs without any filters
        }

    }
}
