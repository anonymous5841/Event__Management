using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project
{
    public partial class FormAttendee : Form
    {
        private int userid;
        public FormAttendee(int user_id)
        {
            InitializeComponent();
            userid = user_id;
            LoadUserSpecifiedEvent();
        }

        public void LoadUserSpecifiedEvent()
        {

            string connectionString = "Data Source=DESKTOP-IP1VHSS;Initial Catalog=db_EventManagement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the database connection
                    connection.Open();

                    // Create the SQL Command object
                    using (SqlCommand command = new SqlCommand("GetUserSpecifiedEvent", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add the parameter
                        command.Parameters.AddWithValue("@User_id", userid);

                        // Execute the command and read data
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            guna2DataGridView1.Rows.Clear(); // Clear existing rows
                            while (reader.Read())
                            {
                                // Add rows to the Guna2DataGridView
                                guna2DataGridView1.Rows.Add(
                                    reader["Event_Id"].ToString(),
                                    reader["Event_Name"].ToString(),
                                    reader["Feedback"].ToString()
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(guna2TextBoxEventid.Text))
            {
                MessageBox.Show("Event ID cannot be empty!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(guna2TextBoxEventid.Text, out int eventId))
            {
                MessageBox.Show("Event ID must be a valid number!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(guna2TextBoxfeedback.Text))
            {
                MessageBox.Show("Feedback cannot be empty!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string feedback = guna2TextBoxfeedback.Text;

            // Database connection string
            string connectionString = "Data Source=DESKTOP-IP1VHSS;Initial Catalog=db_EventManagement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Define the stored procedure command
                    using (SqlCommand cmd = new SqlCommand("EditDescription", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add input parameters
                        cmd.Parameters.AddWithValue("@Event_id", eventId);
                        cmd.Parameters.AddWithValue("@Feedback", feedback);

                        // Execute the stored procedure
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Feedback updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Refresh the table after successfully editing the description
                        LoadUserSpecifiedEvent();
                    }
                }
            }
            catch (SqlException ex)
            {
                // Check for SQL-specific exceptions (e.g., thrown error from the procedure)
                MessageBox.Show("SQL Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // General exception handling
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            // Database connection string
            string connectionString = "Data Source=DESKTOP-IP1VHSS;Initial Catalog=db_EventManagement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            // Validate if the Event ID textbox is filled
            if (string.IsNullOrWhiteSpace(guna2TextBoxEventid.Text))
            {
                MessageBox.Show("Please enter a valid Event ID.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int eventId;

            // Validate if Event ID is a valid integer
            if (!int.TryParse(guna2TextBoxEventid.Text, out eventId))
            {
                MessageBox.Show("Event ID must be a number.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Create a SQL connection
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Create a SQL command for the stored procedure
                    using (SqlCommand command = new SqlCommand("GetDescription", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add the Event ID input parameter
                        command.Parameters.AddWithValue("@Event_id", eventId);

                        // Add the Feedback output parameter
                        SqlParameter feedbackParam = new SqlParameter
                        {
                            ParameterName = "@Feedback",
                            SqlDbType = SqlDbType.VarChar,
                            Size = 100,
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(feedbackParam);

                        // Open the connection and execute the procedure
                        connection.Open();
                        command.ExecuteNonQuery();

                        // Retrieve the Feedback output parameter value
                        if (feedbackParam.Value != null && feedbackParam.Value != DBNull.Value)
                        {
                            guna2TextBoxfeedback.Text = feedbackParam.Value.ToString();
                        }
                        else
                        {
                            guna2TextBoxfeedback.Text = "No feedback available.";
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // Handle SQL errors (e.g., THROW exception in the procedure)
                MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Handle any other errors
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            string eventIdText = guna2TextBoxEventid.Text.Trim();
            string feedbackText = guna2TextBoxfeedback.Text.Trim();

            // Validate input fields
            if (string.IsNullOrEmpty(eventIdText))
            {
                MessageBox.Show("Event ID cannot be empty!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(feedbackText))
            {
                MessageBox.Show("Feedback cannot be empty!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Parse Event ID
            if (!int.TryParse(eventIdText, out int eventId))
            {
                MessageBox.Show("Event ID must be a valid number!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Database connection string
            string connectionString = "Data Source=DESKTOP-IP1VHSS;Initial Catalog=db_EventManagement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            try
            {
                // Open SQL connection
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Call the stored procedure
                    using (SqlCommand cmd = new SqlCommand("AddDescription", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add parameters
                        cmd.Parameters.AddWithValue("@Event_id", eventId);
                        cmd.Parameters.AddWithValue("@Feedback", feedbackText);

                        // Execute the stored procedure
                        cmd.ExecuteNonQuery();

                        // Success message
                        MessageBox.Show("Feedback added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Optionally clear input fields after success
                        guna2TextBoxEventid.Text = string.Empty;
                        guna2TextBoxfeedback.Text = string.Empty;
                        LoadUserSpecifiedEvent();
                    }
                }
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions (e.g., custom error thrown by procedure)
                MessageBox.Show($"SQL Error: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Handle general exceptions
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2GradientButton4_Click(object sender, EventArgs e)
        {
            LoadUserSpecifiedEvent();
        }
    }

}

