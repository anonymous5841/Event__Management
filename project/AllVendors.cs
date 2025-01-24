using Guna.UI2.WinForms;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Dbproject
{
    public partial class AllVendors : Form
    {
        public AllVendors()
        {
            InitializeComponent();
        }

        private System.Windows.Forms.Form activeform = null;

        public void LoadForm(System.Windows.Forms.Form childform)
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
            // Get the Vendor ID from the textbox
            string vendorId = guna2TextBox1.Text.Trim();

            if (string.IsNullOrEmpty(vendorId))
            {
                MessageBox.Show("Please enter a valid Vendor ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Pass the Vendor ID to the next form
            VendorsViewMore nextForm = new VendorsViewMore(vendorId);
            LoadForm(nextForm);
        }

        private void guna2TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Block non-numeric input
            }
        }

        private void guna2TextBox1_Click(object sender, EventArgs e)
        {
            if (guna2TextBox1.Text == "" || guna2TextBox1.Text == "Search")
            {
                guna2TextBox1.Text = string.Empty;
            }
            guna2TextBox1.ForeColor = Color.White;
        }

        private void guna2TextBox1_Leave(object sender, EventArgs e)
        {
            if (guna2TextBox1.Text == "" || guna2TextBox1.Text == "Search")
            {
                guna2TextBox1.Text = "Search";
                guna2TextBox1.ForeColor = Color.Gray;
            }
        }

        private void AllVendors_Load(object sender, EventArgs e)
        {
            LoadVendorsData();
        }

        private void LoadVendorsData()
        {
            // Define the connection string to your database
            string connectionString = "Data Source=DESKTOP-IP1VHSS;Initial Catalog=db_EventManagement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            // Define the SQL query to fetch all data from the view
            string query = "SELECT * FROM vw_tb_Vendor";

            // Create and open the database connection
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    // Create the SQL command and execute it
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Clear existing rows in the DataGridView
                            guna2DataGridView1.Rows.Clear();

                            // Read the data and populate the DataGridView
                            while (reader.Read())
                            {
                                guna2DataGridView1.Rows.Add(
                                    reader["Vendor_Id"],
                                    reader["Company_Name"],
                                    reader["Resource_Person_Contact"],
                                    reader["Contact_Number"],
                                    reader["Email"],
                                    reader["V_Address"],
                                    reader["User_Id_FK"]
                                );
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Display an error message if something goes wrong
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void guna2GradientButton5_Click(object sender, EventArgs e) // Delete button
        {
            if (guna2DataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a vendor to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get the selected Vendor_Id
            string vendorId = guna2DataGridView1.SelectedRows[0].Cells[0].Value.ToString();

            // Confirm deletion
            var confirmResult = MessageBox.Show($"Are you sure you want to delete vendor with ID {vendorId}?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.Yes)
            {
                // Define the connection string to your database
                string connectionString = "your_connection_string_here"; // Replace with your actual connection string

                // Define the delete query
                string deleteQuery = "DELETE FROM vw_tb_Vendor WHERE Vendor_Id = @Vendor_Id";

                // Create and open the database connection
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    try
                    {
                        con.Open();

                        // Create the SQL command for deletion
                        using (SqlCommand cmd = new SqlCommand(deleteQuery, con))
                        {
                            cmd.Parameters.AddWithValue("@Vendor_Id", vendorId);
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Vendor deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Refresh the DataGridView
                                LoadVendorsData();
                            }
                            else
                            {
                                MessageBox.Show("No vendor found with the specified ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void guna2CustomGradientPanel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}