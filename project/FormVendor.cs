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
    public partial class FormVendor : Form
    {
        private int userid;
        public FormVendor(int user_id)
        {
            InitializeComponent();
            userid = user_id;
            loadVendor();
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

        public void loadVendor()
        {
            string connectionString = "Data Source=DESKTOP-IP1VHSS;Initial Catalog=db_EventManagement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the database connection
                    connection.Open();

                    // Create the SQL Command object
                    using (SqlCommand command = new SqlCommand("GetVendorsByUserId", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add the parameter
                        command.Parameters.AddWithValue("@UserId", userid);

                        // Execute the command and read data
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            guna2DataGridView1.Rows.Clear(); // Clear existing rows
                            while (reader.Read())
                            {
                                // Add rows to the Guna2DataGridView
                                guna2DataGridView1.Rows.Add(
                                    reader["Vendor_Id"].ToString(),         
                                    reader["Company_Name"].ToString(),    
                                    reader["Resource_Person_Contact"].ToString(),  
                                    reader["Contact_Number"].ToString(),   
                                    reader["Email"].ToString(),            
                                    reader["V_Address"].ToString()        
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
        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            loadform(new VendorCreateForm(userid));
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            string inputText = guna2TextBox1.Text; // Replace with your actual TextBox name
            if (string.IsNullOrWhiteSpace(inputText))
            {
                MessageBox.Show("Please enter a valid Venue ID.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (int.TryParse(inputText, out int SponsorID))
            {
                // Call the database validation method
                if (!ValidateVendorID(SponsorID))
                {
                    MessageBox.Show("Vendor not present in the database.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    // Load the EventEditForm with the valid Event ID and user ID
                    loadform(new VendorEditForm(int.Parse(guna2TextBox1.Text)));
                }
            }
            else
            {
                MessageBox.Show("Please enter a numeric Vendor ID.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void guna2CustomGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            loadVendor();
        }
        private bool ValidateVendorID(int vendorID)
        {
            string connectionString = "Data Source=DESKTOP-IP1VHSS;Initial Catalog=db_EventManagement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("ValidateVendorID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add the input parameter for EventID
                        command.Parameters.AddWithValue("@VendorID ", vendorID);

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
                MessageBox.Show($"Error validating Vendor ID: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // Return false in case of error
            }
        }
    }
}
