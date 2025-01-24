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
    public partial class VenueCreateForm : Form
    {
        private int userId;
        public VenueCreateForm(int user_id)
        {
            InitializeComponent();
            userId = user_id;
        }

        private void guna2GradientCircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2GradientButtonCreate_Click(object sender, EventArgs e)
        {

            string connectionString = "Data Source=DESKTOP-IP1VHSS;Initial Catalog=db_EventManagement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            // Gather input values
            string venueName = guna2TextBoxName.Text.Replace(" ", "").Trim();
            string venueLocation = guna2TextBoxLocation.Text.Trim();
            int venueCapacity = (int)guna2NumericUpDownCapacity.Value; // Convert decimal to int
            string venueContact = guna2TextBoxPhone.Text.Trim();

            // Input validation
            if (string.IsNullOrWhiteSpace(venueName) || string.IsNullOrWhiteSpace(venueLocation) ||
                string.IsNullOrWhiteSpace(venueContact) || venueContact.Length != 11 || !venueContact.All(char.IsDigit))
            {
                MessageBox.Show("Please fill all fields correctly. Ensure the phone number has 11 digits.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open(); // Open the connection

                    using (SqlCommand command = new SqlCommand("RegisterVenue", connection))
                    {
                        // Specify stored procedure
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters
                        command.Parameters.AddWithValue("@Venue_Name", venueName);
                        command.Parameters.AddWithValue("@Venue_Location", venueLocation);
                        command.Parameters.AddWithValue("@Venue_Capacity", venueCapacity);
                        command.Parameters.AddWithValue("@Venue_Contact", venueContact);
                        command.Parameters.AddWithValue("@User_id", userId);

                        // Execute the stored procedure
                        command.ExecuteNonQuery();

                        // Success feedback
                        MessageBox.Show("Venue registered successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 60000) // Custom error for "Vendor already exists!"
                {
                    MessageBox.Show("Venue already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (ex.Number == 50000)
                {
                    MessageBox.Show("Transaction rooled back due to error!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show($"SQL Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
