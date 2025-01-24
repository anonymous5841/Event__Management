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
    public partial class SponsorCreateForm : Form
    {
        private int userId;
        public SponsorCreateForm(int user_id)
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

            // Get input values
            string sponsorName = guna2TextBoxName.Text.Replace(" ", "").Trim();
            string sponsorEmail = guna2TextBoxEmail.Text.Trim();
            string sponsorContact = guna2TextBoxPhone.Text.Trim();
            string sponsorAddress = guna2TextBoxAddress.Text.Trim();

            // Input validation
            if (string.IsNullOrWhiteSpace(sponsorName) || string.IsNullOrWhiteSpace(sponsorEmail) ||
                string.IsNullOrWhiteSpace(sponsorAddress) || sponsorContact.Length != 11 || !sponsorContact.All(char.IsDigit))
            {
                MessageBox.Show("Please fill all fields correctly. Ensure the phone number has 11 digits.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("RegisterSponsor", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add parameters
                        cmd.Parameters.AddWithValue("@Sponsor_Name", sponsorName);
                        cmd.Parameters.AddWithValue("@Sponsor_Email", sponsorEmail);
                        cmd.Parameters.AddWithValue("@Sponsor_Contact", sponsorContact);
                        cmd.Parameters.AddWithValue("@Sponsor_Address", sponsorAddress);
                        cmd.Parameters.AddWithValue("@User_id", userId);

                        // Execute the procedure
                        cmd.ExecuteNonQuery();

                        // Success message
                        MessageBox.Show("Sponsor registered successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 60000)
                {
                    MessageBox.Show("Sponsor already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (ex.Number == 50000)
                {
                    MessageBox.Show("Transaction rooled back due to error!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
   