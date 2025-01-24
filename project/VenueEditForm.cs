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
    public partial class VenueEditForm : Form
    {
        private int venueid;
        public VenueEditForm(int venue_id)
        {
            InitializeComponent();

            venueid = venue_id;
            loadVenue();

        }

        private void loadVenue()
        {

            string connectionString = "Data Source=DESKTOP-IP1VHSS;Initial Catalog=db_EventManagement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("GetVenueDetails", connection))
                    {
                        // Specify that we are using a stored procedure
                        command.CommandType = CommandType.StoredProcedure;

                        // Add the input parameter
                        command.Parameters.AddWithValue("@Venue_Id", venueid);

                        // Add output parameters
                        SqlParameter nameParam = new SqlParameter("@Name", SqlDbType.VarChar, 20) { Direction = ParameterDirection.Output };
                        SqlParameter locationParam = new SqlParameter("@Location", SqlDbType.VarChar, 30) { Direction = ParameterDirection.Output };
                        SqlParameter capacityParam = new SqlParameter("@Capacity", SqlDbType.Int) { Direction = ParameterDirection.Output };
                        SqlParameter contactParam = new SqlParameter("@Contact", SqlDbType.VarChar, 11) { Direction = ParameterDirection.Output };

                        // Add the output parameters to the command
                        command.Parameters.Add(nameParam);
                        command.Parameters.Add(locationParam);
                        command.Parameters.Add(capacityParam);
                        command.Parameters.Add(contactParam);

                        // Execute the stored procedure
                        command.ExecuteNonQuery();

                        // Assign the output parameter values to the respective textboxes
                        guna2TextBoxName.Text = nameParam.Value.ToString();
                        guna2TextBoxPhone.Text = contactParam.Value.ToString();
                        guna2TextBoxLocation.Text = locationParam.Value.ToString();

                        // For the numeric up-down control, convert the value to decimal
                        guna2NumericUpDownCapacity.Value = Convert.ToDecimal(capacityParam.Value);
                    }
                }
            }
            catch (SqlException ex)
            {
                // Handle SQL errors
                if (ex.Number == 70003) // Custom error for 'Sponsor does not exist'
                {
                    MessageBox.Show("Venue does not exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"SQL Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Handle general exceptions
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void guna2GradientCircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-IP1VHSS;Initial Catalog=db_EventManagement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            // Read values from the UI controls
            string venueName = guna2TextBoxName.Text.Replace(" ", "").Trim();
            string venueLocation = guna2TextBoxLocation.Text.Trim();
            string venueContact = guna2TextBoxPhone.Text.Trim();
            int venueCapacity = (int)guna2NumericUpDownCapacity.Value;

           
            // Validate inputs
            if (string.IsNullOrWhiteSpace(venueName) ||
                string.IsNullOrWhiteSpace(venueLocation) ||
                string.IsNullOrWhiteSpace(venueContact) ||
                venueContact.Length != 11 ||
                !venueContact.All(char.IsDigit) ||
                venueCapacity <= 0)
            {
                MessageBox.Show("Please fill in all fields with valid data.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("UpdateVenue", conn))
                    {
                        // Set the command type as Stored Procedure
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the command
                        cmd.Parameters.AddWithValue("@Venue_id", venueid);
                        cmd.Parameters.AddWithValue("@Venue_Name", venueName);
                        cmd.Parameters.AddWithValue("@Venue_Location", venueLocation);
                        cmd.Parameters.AddWithValue("@Venue_Capacity", venueCapacity);
                        cmd.Parameters.AddWithValue("@Venue_Contact", venueContact);

                        // Open the connection
                        conn.Open();

                        // Execute the stored procedure
                        cmd.ExecuteNonQuery();

                        // Show success message
                        MessageBox.Show("Venue details updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 60000)
                {
                    MessageBox.Show("Venue does not exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (ex.Number == 50000)
                {
                    MessageBox.Show("Transaction Rolled back due to error!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Handle other unexpected errors
                MessageBox.Show($"Unexpected error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
