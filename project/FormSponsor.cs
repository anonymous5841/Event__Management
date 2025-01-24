using Dbproject;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project
{
    public partial class FormSponsor : Form
    {
        private int userId;
        public FormSponsor(int User_id)
        {
            InitializeComponent();
            userId = User_id;
            loadSponsor();
        }

        public void loadSponsor()
        {
            string connectionString = "Data Source=DESKTOP-IP1VHSS;Initial Catalog=db_EventManagement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the database connection
                    connection.Open();

                    // Create the SQL Command object
                    using (SqlCommand command = new SqlCommand("GetSponsorsByUserId", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add the parameter
                        command.Parameters.AddWithValue("@UserId", userId);

                        // Execute the command and read data
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            guna2DataGridView1.Rows.Clear(); // Clear existing rows
                            while (reader.Read())
                            {
                                // Add rows to the Guna2DataGridView
                                guna2DataGridView1.Rows.Add(
                                    reader["Sponsor_Id"].ToString(),
                                    reader["Sponsor_Name"].ToString(), 
                                    reader["Email"].ToString(),
                                    reader["Contact_Number"].ToString(),
                                    reader["Sponsor_Address"].ToString()      
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


        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            loadform(new SponsorCreateForm(userId));
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            // Get input text and validate
            string inputText = guna2TextBoxedit.Text; // Replace with your actual TextBox name
            if (string.IsNullOrWhiteSpace(inputText))
            {
                MessageBox.Show("Please enter a valid Sponsor ID.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (int.TryParse(inputText, out int SponsorID))
            {
                // Call the database validation method
                if (!ValidateSponsorID(SponsorID))
                {
                    MessageBox.Show("Sponsor not present in the database.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    // Load the EventEditForm with the valid Event ID and user ID
                    loadform(new SponsorEditForm(inputText));
                }
            }
            else
            {
                MessageBox.Show("Please enter a numeric Sponsor ID.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            loadSponsor();
        }

        private bool ValidateSponsorID(int sponsorID)
        {
            string connectionString = "Data Source=DESKTOP-IP1VHSS;Initial Catalog=db_EventManagement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("ValidateSponsorID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add the input parameter for EventID
                        command.Parameters.AddWithValue("@SponsorID", sponsorID);

                        // Add the output parameter for IsPresent
                        SqlParameter isPresentParam = new SqlParameter("@IsPresent", SqlDbType.Bit)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(isPresentParam);

                        // Execute the stored procedure
                        command.ExecuteNonQuery();

                        // Return the value of the output parameter
                        return Convert.ToBoolean(isPresentParam.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error validating Sponsor ID: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // Return false in case of error
            }
        }

    }
}
