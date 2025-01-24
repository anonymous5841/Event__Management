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
    public partial class Sponsors : Form
    {
        public Sponsors()
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
            string sponsorId = guna2TextBox1.Text.Trim();

            if (string.IsNullOrEmpty(sponsorId) || sponsorId == "Search")
            {
                MessageBox.Show("Please enter a valid Sponsor ID to search.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Pass the sponsorId to Form2
            SponsorsViewMore viewMoreForm = new SponsorsViewMore(sponsorId);
            loadform(viewMoreForm);
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

        private void Sponsors_Load(object sender, EventArgs e)
        {
            // Connection string to your database (update this with actual details)
            string ConnectionString = "Data Source=DESKTOP-IP1VHSS;Initial Catalog=db_EventManagement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            // Create a connection to the database
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    // Open the connection
                    con.Open();

                    // Query to select data from the view
                    string Query = "SELECT Sponsor_Id, Sponsor_Name, Contact_Number, Email, Sponsor_Address, User_Id FROM vw_SponsorDetails";

                    // Execute the query
                    SqlCommand cmd = new SqlCommand(Query, con);
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Populate the DataGridView
                    while (reader.Read())
                    {
                        guna2DataGridView1.Rows.Add(
                            reader["Sponsor_Id"],
                            reader["Sponsor_Name"],
                            reader["Contact_Number"],
                            reader["Email"],
                            reader["Sponsor_Address"],
                            reader["User_Id"]
                        );
                    }

                    // Close the reader
                    reader.Close();
                }
                catch (Exception ex)
                {
                    // Handle any errors
                    MessageBox.Show($"Error loading sponsors: {ex.Message}");
                }
            }
        }
        private void guna2TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Block non-numeric input
            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
