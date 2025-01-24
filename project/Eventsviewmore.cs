using Guna.UI2.WinForms;
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

namespace Dbproject
{
    public partial class Eventsviewmore : Form
    {
        private string eventId;
        public Eventsviewmore(string eventid)
        {
            InitializeComponent();
            eventId = eventid; // Store the Event ID
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private System.Windows.Forms.Form activeform = null;
        public void loadform(System.Windows.Forms.Form childform)
        {
            if (activeform != null)
            {
                activeform.Close();
            }

            activeform = childform;
            childform.TopLevel = false;
            childform.FormBorderStyle = FormBorderStyle.None;
            childform.Dock = DockStyle.Fill;
            guna2GradientPanel1.Controls.Add(childform);
            guna2GradientPanel1.Tag = childform;
            childform.BringToFront();
            childform.Show();
        }

        private void guna2GradientCircleButton1_Click(object sender, EventArgs e)
        {
            loadform(new EventsApproval());
        }

        private void Eventsviewmore_Load(object sender, EventArgs e)
        {
            LoadEventDetails(eventId);
        }

        private void LoadEventDetails(string eventId)
        {
            // Define your connection string
            string connectionString = "Data Source=DESKTOP-IP1VHSS;Initial Catalog=db_EventManagement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    // Query to fetch event details based on the provided Event ID
                    string query = "SELECT Event_Id, Event_Name, E_Date, Start_Time, End_Time, E_Type, Attendee_Id_FK, Vendor_Price, Profit_Percent, User_Id_FK, E_Description " +
                                   "FROM vw_Eventsss WHERE Event_Id = @EventId";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@EventId", eventId); // Use parameterized query to prevent SQL injection

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            // Populate the DataGridView with the event details
                            guna2DataGridView2.Rows.Clear(); // Clear any existing rows
                            guna2DataGridView2.Rows.Add(
                                reader["Event_Id"],
                                reader["Event_Name"],
                                reader["E_Date"],
                                reader["Start_Time"],
                                reader["End_Time"],
                                reader["E_Type"],
                                reader["Attendee_Id_FK"],
                                reader["Vendor_Price"],
                                reader["Profit_Percent"],
                                reader["User_Id_FK"]
                            );

                            // Display the description in a separate textbox
                            guna2TextBox3.Text = reader["E_Description"]?.ToString() ?? "No Description Available";
                        }
                        else
                        {
                            MessageBox.Show($"No event found with ID {eventId}.", "Event Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Clear DataGridView and Description TextBox if no record is found
                            guna2DataGridView2.Rows.Clear();
                            guna2TextBox3.Text = string.Empty;  //description  box
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while loading event details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void guna2DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2GradientButton4_Click(object sender, EventArgs e) // Approval button
        {
            // Replace with your actual connection string
            string connectionString = "Data Source=DESKTOP-IP1VHSS;Initial Catalog=db_EventManagement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            try
            {
                // SQL Query to update the event status
                string query = "UPDATE tb_Vendor_Registration SET Event_Status = 'Approved' WHERE Event_Id_FK = @EventId";

                // Establish the connection
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Create the SQL command
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Assume eventId is defined elsewhere in the class and already has a value
                        // Add parameter to prevent SQL injection
                        command.Parameters.AddWithValue("@EventId", eventId);

                        // Open the connection
                        connection.Open();

                        // Execute the query
                        int rowsAffected = command.ExecuteNonQuery();

                        // Provide feedback to the user
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Event status updated to Approved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No event found with the provided Event ID.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle errors
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void guna2GradientButton5_Click(object sender, EventArgs e) // Decline button
        {
            // Replace with your actual connection string
            string connectionString = "Data Source=DESKTOP-IP1VHSS;Initial Catalog=db_EventManagement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            try
            {
                // SQL Query to update the event status
                string query = "UPDATE tb_Vendor_Registration SET Event_Status = 'Rejected' WHERE Event_Id_FK = @EventId";

                // Establish the connection
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Create the SQL command
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Assume eventId is defined elsewhere in the class and already has a value
                        // Add parameter to prevent SQL injection
                        command.Parameters.AddWithValue("@EventId", eventId);

                        // Open the connection
                        connection.Open();

                        // Execute the query
                        int rowsAffected = command.ExecuteNonQuery();

                        // Provide feedback to the user
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Event status updated to Rejected successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No event found with the provided Event ID.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle errors
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
