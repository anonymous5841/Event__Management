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
    public partial class SponsorEditForm : Form
    {
        private int sponsorid;
        public SponsorEditForm(string sponsor_id)
        {
            InitializeComponent();
            sponsorid = int.Parse(sponsor_id);
            loadSponsor();
            
        }

        private void loadSponsor()
        {
            string connectionString = "Data Source=DESKTOP-IP1VHSS;Initial Catalog=db_EventManagement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("GetSponsorDetails", connection))
                    {
                        // Specify that this is a stored procedure
                        command.CommandType = CommandType.StoredProcedure;

                        // Input parameter
                        command.Parameters.AddWithValue("@Sponsor_Id", sponsorid);

                        // Output parameters
                        SqlParameter nameParam = new SqlParameter("@Name", SqlDbType.VarChar, 20)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(nameParam);

                        SqlParameter contactParam = new SqlParameter("@Contact", SqlDbType.VarChar, 11)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(contactParam);

                        SqlParameter emailParam = new SqlParameter("@Email", SqlDbType.VarChar, 20)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(emailParam);

                        SqlParameter addressParam = new SqlParameter("@Address", SqlDbType.VarChar, 30)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(addressParam);

                        // Execute the stored procedure
                        command.ExecuteNonQuery();

                        // Retrieve the output parameter values
                        string sponsorName = nameParam.Value != DBNull.Value ? nameParam.Value.ToString() : "N/A";
                        string sponsorContact = contactParam.Value != DBNull.Value ? contactParam.Value.ToString() : "N/A";
                        string sponsorEmail = emailParam.Value != DBNull.Value ? emailParam.Value.ToString() : "N/A";
                        string sponsorAddress = addressParam.Value != DBNull.Value ? addressParam.Value.ToString() : "N/A";

                        guna2TextBoxName.Text = sponsorName;
                        guna2TextBoxPhone.Text = sponsorContact;
                        guna2TextBoxEmail.Text = sponsorEmail;
                        guna2TextBoxAddress.Text = sponsorAddress;
                    }
                }
            }
            catch (SqlException ex)
            {
                // Handle SQL errors
                if (ex.Number == 7004) // Custom error for 'Sponsor does not exist'
                {
                    MessageBox.Show("Sponsor does not exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("UpdateSponsor", connection))
                    {
                        // Specify that we are using a stored procedure
                        command.CommandType = CommandType.StoredProcedure;

                        if (string.IsNullOrWhiteSpace(guna2TextBoxName.Text) || string.IsNullOrWhiteSpace(guna2TextBoxEmail.Text) ||
                        string.IsNullOrWhiteSpace(guna2TextBoxAddress.Text) || guna2TextBoxPhone.Text.Length != 11 || !guna2TextBoxPhone.Text.All(char.IsDigit))
                        {
                            MessageBox.Show("Please fill all fields correctly. Ensure the phone number has 11 digits.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // Add input parameters for the stored procedure
                        command.Parameters.AddWithValue("@Sponsor_id", sponsorid);
                        command.Parameters.AddWithValue("@Sponsor_Name", guna2TextBoxName.Text.Replace(" ", "").Trim());
                        command.Parameters.AddWithValue("@Sponsor_Email", guna2TextBoxEmail.Text.Trim());
                        command.Parameters.AddWithValue("@Sponsor_Contact", guna2TextBoxPhone.Text.Trim());
                        command.Parameters.AddWithValue("@Sponsor_Address", guna2TextBoxAddress.Text.Trim());

                        // Execute the stored procedure
                        int rowsAffected = command.ExecuteNonQuery();

                        // Notify user of success
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Sponsor details updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Sponsor does not exist or update failed.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // Handle SQL errors
                MessageBox.Show($"SQL Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Handle general exceptions
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
        }
    }
}