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
    public partial class Deleted_Events : Form
    {
        public Deleted_Events()
        {
            InitializeComponent();
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

        private void guna2GradientButton1_Click(object sender, EventArgs e) // Search button
        {
            // Get the Event ID from the textbox
            string eventId = guna2TextBox1.Text.Trim();

            if (string.IsNullOrEmpty(eventId))
            {
                MessageBox.Show("Please enter a valid Event ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Pass the Event ID to the next form
            DeletedViewMore nextForm = new DeletedViewMore (eventId);
            loadform(nextForm);
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_Click(object sender, EventArgs e)
        {
            {
                if (guna2TextBox1.Text == "" || guna2TextBox1.Text == "Search")
                {
                    guna2TextBox1.Text = string.Empty;
                }
                guna2TextBox1.ForeColor = Color.White;
            }
        }

        private void guna2TextBox1_Leave(object sender, EventArgs e)
        {
            if (guna2TextBox1.Text == "" || guna2TextBox1.Text == "Search")
            {

                guna2TextBox1.Text = "Search";
                guna2TextBox1.ForeColor = Color.Gray;
            }
        }

        private void Deleted_Events_Load(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-IP1VHSS;Initial Catalog=db_EventManagement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    String Query = "SELECT Event_Id, Event_Name, E_Date, Start_Time, End_Time, E_Type, Attendee_Id_FK, Vendor_Price, Profit_Percent, User_Id_FK FROM vw_Rejected_Events"; ;

                    using (SqlCommand cmd = new SqlCommand(Query, con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
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
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }


        private void guna2DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void guna2TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Block non-numeric input
            }
        }

        private void guna2GradientButton5_Click(object sender, EventArgs e) // Delete button
        {
            string eventId = guna2TextBox1.Text.Trim(); // Get the Event ID from the textbox

            if (string.IsNullOrEmpty(eventId))
            {
                MessageBox.Show("Please enter a valid Event ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Confirm the deletion with the user
            var confirmation = MessageBox.Show($"Are you sure you want to delete the event with ID {eventId}?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmation == DialogResult.Yes)
            {
                // Call the stored procedure to delete the event
                DeleteEvent(eventId);
            }
        }

        private void DeleteEvent(string eventId)
        {
            string connectionString = "Data Source=DESKTOP-IP1VHSS;Initial Catalog=db_EventManagement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("DeleteEvent", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add the parameter for Event ID
                        cmd.Parameters.AddWithValue("@Event_id", eventId);

                        con.Open();

                        // Execute the stored procedure
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Event deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Optionally, refresh the DataGridView or close the form
                        // For example, you can reload the events list after deletion
                        ReloadEventList();
                    }
                }
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions (like if the event doesn't exist)
                MessageBox.Show($"Error occurred while deleting event: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Handle any other exceptions
                MessageBox.Show($"Unexpected error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ReloadEventList()
        {
            // This method is optional. You can reload the DataGridView to reflect the changes after deletion.
            guna2DataGridView2.Rows.Clear(); // Clear the existing rows

            // Re-run the query to fetch the updated list of deleted events
            string connectionString = "Data Source=DESKTOP-IP1VHSS;Initial Catalog=db_EventManagement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
            string query = "SELECT Event_Id, Event_Name, E_Date, Start_Time, End_Time, E_Type, Attendee_Id_FK, Vendor_Price, Profit_Percent, User_Id_FK FROM vw_Rejected_Events";

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
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
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while reloading the event list: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
