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
    public partial class VendorEditForm : Form
    {
        private int vendorid;
        public VendorEditForm(int vendor_id)
        {
            InitializeComponent();
            
            vendorid = vendor_id;
            loadVendor();
                
        }

        private void loadVendor()
        {
            string connectionString = "Data Source=DESKTOP-IP1VHSS;Initial Catalog=db_EventManagement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("GetVendorDetails", connection))
                    {
                        // Specify that we are using a stored procedure
                        command.CommandType = CommandType.StoredProcedure;

                        // Add the input parameter
                        command.Parameters.AddWithValue("@Vendor_id", vendorid);

                        // Add output parameters
                        SqlParameter nameParam = new SqlParameter("@Name", SqlDbType.VarChar, 15) { Direction = ParameterDirection.Output };
                        SqlParameter rpParam = new SqlParameter("@RP", SqlDbType.VarChar, 11) { Direction = ParameterDirection.Output };
                        SqlParameter contactParam = new SqlParameter("@Contact", SqlDbType.VarChar, 11) { Direction = ParameterDirection.Output };
                        SqlParameter emailParam = new SqlParameter("@Email", SqlDbType.VarChar, 20) { Direction = ParameterDirection.Output };
                        SqlParameter addressParam = new SqlParameter("@Address", SqlDbType.VarChar, 30) { Direction = ParameterDirection.Output };

                        // Add the output parameters to the command
                        command.Parameters.Add(nameParam);
                        command.Parameters.Add(rpParam);
                        command.Parameters.Add(contactParam);
                        command.Parameters.Add(emailParam);
                        command.Parameters.Add(addressParam);

                        // Execute the stored procedure
                        command.ExecuteNonQuery();

                        // Assign the output parameter values to the respective textboxes
                        guna2TextBoxName.Text = nameParam.Value.ToString();
                        guna2TextBoxRP.Text = rpParam.Value.ToString();
                        guna2TextBoxPhone.Text = contactParam.Value.ToString();
                        guna2TextBoxEmail.Text = emailParam.Value.ToString();
                        guna2TextBoxAddress.Text = addressParam.Value.ToString();
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 7004) // Custom error for 'Sponsor does not exist'
                {
                    MessageBox.Show("Vendor does not exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            // Read input values from textboxes
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
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("UpdateVendor", conn))
                    {
                        // Set command type as Stored Procedure
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add parameters
                        cmd.Parameters.AddWithValue("@Vendor_id", vendorid);
                        cmd.Parameters.AddWithValue("@Company_Name", companyName);
                        cmd.Parameters.AddWithValue("@R_P_Contact", resourcePersonContact);
                        cmd.Parameters.AddWithValue("@Contact_Number", contactNumber);
                        cmd.Parameters.AddWithValue("@Vendor_Email", vendorEmail);
                        cmd.Parameters.AddWithValue("@Vendor_Address", vendorAddress);

                        // Open connection
                        conn.Open();

                        // Execute the stored procedure
                        cmd.ExecuteNonQuery();

                        // Success message
                        MessageBox.Show("Vendor details updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (SqlException ex)
            {
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
                // Handle other exceptions
                MessageBox.Show($"Unexpected Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
         }
    }
}
