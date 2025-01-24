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
    public partial class VendorCreateForm : Form
    {
        private int userid;
        public VendorCreateForm(int user_id)
        {
            InitializeComponent();
            userid = user_id;
        }

        private void guna2GradientCircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2GradientButtonCreate_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-IP1VHSS;Initial Catalog=db_EventManagement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            // Collect input values from the form
            string companyName = guna2TextBoxName.Text.Replace(" ", "").Trim();
            string resourcePersonContact = guna2TextBoxRP.Text.Trim();
            string contactNumber = guna2TextBoxPhone.Text.Trim();
            string vendorEmail = guna2TextBoxEmail.Text.Trim();
            string vendorAddress = guna2TextBoxAddress.Text.Trim();

            if (string.IsNullOrWhiteSpace(companyName) || string.IsNullOrWhiteSpace(vendorAddress) ||
                string.IsNullOrWhiteSpace(vendorEmail) || contactNumber.Length != 11 || resourcePersonContact.Length != 11 || !contactNumber.All(char.IsDigit) || !resourcePersonContact.All(char.IsDigit))
            {
                MessageBox.Show("Please fill all fields correctly. Ensure the phone number has 11 digits.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                // Connect to the database
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Create a SQL command for the stored procedure
                    using (SqlCommand command = new SqlCommand("RegisterVendor", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters for the stored procedure
                        command.Parameters.AddWithValue("@Company_Name", companyName);
                        command.Parameters.AddWithValue("@R_P_Contact", resourcePersonContact);
                        command.Parameters.AddWithValue("@Contact_Number", contactNumber);
                        command.Parameters.AddWithValue("@Vendor_Email", vendorEmail);
                        command.Parameters.AddWithValue("@Vendor_Address", vendorAddress);
                        command.Parameters.AddWithValue("@User_id", userid);

                        // Execute the stored procedure
                        command.ExecuteNonQuery();

                        // Show success message
                        MessageBox.Show("Vendor registered successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (SqlException ex)
            {
                // Handle SQL errors
                if (ex.Number == 60000) // Custom error for "Vendor already exists!"
                {
                    MessageBox.Show("Vendor already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                // Handle general exceptions
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
 }

