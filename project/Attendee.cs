using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Dbproject
{
    public partial class Attendee : Form
    {
        private int userid;
        string ConnectionString = "Data Source=DESKTOP-IP1VHSS;Initial Catalog=db_EventManagement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        public Attendee(int user_id)
        {
            InitializeComponent();
            userid = user_id;
        }

        private void Attendee_Load(object sender, EventArgs e)
        {
            LoadData(); // Load data on form load
        }

        // Function to load data into the DataGridView
        private void LoadData()
        {
            guna2DataGridView1.Rows.Clear(); // Clear the DataGridView

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT * FROM vw_Attendee";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        guna2DataGridView1.Rows.Add(
                            reader["Attendee_Id"],
                            reader["Admin_User_ID"],
                            reader["User_ID"],
                            reader["Feedback"]
                        );
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }





        // Add Button Click Event
        private void guna2GradientButton4_Click(object sender, EventArgs e)
        {
            // Get Admin ID from the textbox and trim whitespace
            string adminIdText = guna2TextBox1.Text.Trim(); // Assuming guna2TextBoxAdminId is the textbox for Admin ID

            // Validate Admin ID
            if (string.IsNullOrEmpty(adminIdText))
            {
                MessageBox.Show("Admin ID cannot be empty!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Parse Admin ID
            if (!int.TryParse(adminIdText, out int adminId))
            {
                MessageBox.Show("Admin ID must be a valid number!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Set default feedback
            string defaultFeedback = "No feedback provided";

            // Database connection string
            string connectionString = "Data Source=DESKTOP-IP1VHSS;Initial Catalog=db_EventManagement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            try
            {
                // Open SQL connection
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Call the stored procedure
                    using (SqlCommand cmd = new SqlCommand("RegisterAttendee", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add parameters
                        cmd.Parameters.AddWithValue("@Admin_User_Id_FK", userid); // Admin ID from textbox
                        cmd.Parameters.AddWithValue("@User_Id_FK", adminId);       // User ID from the form constructor
                        cmd.Parameters.AddWithValue("@Feedback", defaultFeedback); // Pass the default feedback

                        // Execute the stored procedure
                        cmd.ExecuteNonQuery();

                        // Success message
                        MessageBox.Show("Attendee added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Optionally clear the Admin ID input field after success
                        guna2TextBox1.Text = string.Empty;

                        // Refresh the data grid view
                        LoadData();
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


        // Delete Button Click Event
        private void guna2GradientButton1_Click(object sender, EventArgs e) // Delete
        {
            if (string.IsNullOrWhiteSpace(guna2TextBox1.Text))
            {
                MessageBox.Show("Please enter the Attendee ID to delete.");
                return;
            }

            int attendeeId = int.Parse(guna2TextBox1.Text);

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();
                    string query = "DELETE FROM tb_Attendee WHERE Attendee_Id = @AttendeeId";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@AttendeeId", attendeeId);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Record Deleted Successfully!");
                    }
                    else
                    {
                        MessageBox.Show("No record found with the specified Attendee ID.");
                    }
                    LoadData(); // Refresh the DataGridView
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void guna2TextBox1_Click(object sender, EventArgs e)
        {
            if (guna2TextBox1.Text == "Search")
            {
                guna2TextBox1.Text = "";
                guna2TextBox1.ForeColor = Color.White;
            }
        }

        private void guna2TextBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(guna2TextBox1.Text))
            {
                guna2TextBox1.Text = "Search";
                guna2TextBox1.ForeColor = Color.Gray;
            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
