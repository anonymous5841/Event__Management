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
    public partial class SponsorsViewMore : Form
    {
        private string SponsorId;
        public SponsorsViewMore(string sponsor_Id)
        {
            InitializeComponent();
            SponsorId = sponsor_Id;
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

        private void guna2GradientCircleButton1_Click(object sender, EventArgs e)
        {
            guna2DataGridView1.Rows.Clear();
            loadform(new Sponsors());
        }

        private void SponsorsViewMore_Load(object sender, EventArgs e)
        {
            LoadSponsorData();
        }

        private void LoadSponsorData()
        {
            string ConnectionString = "Data Source=DESKTOP-IP1VHSS;Initial Catalog=db_EventManagement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();

                    // Query to fetch data for the specific Sponsor ID
                    String Query = "SELECT Sponsor_Id, Sponsor_Name, Contact_Number, Email, Sponsor_Address " +
                                   "FROM tb_Sponsors WHERE Sponsor_Id = @SponsorId";

                    SqlCommand cmd = new SqlCommand(Query, con);
                    cmd.Parameters.AddWithValue("@SponsorId", this.SponsorId);

                    var reader = cmd.ExecuteReader();

                    // Clear any existing rows
                    guna2DataGridView1.Rows.Clear();

                    while (reader.Read())
                    {
                        guna2DataGridView1.Rows.Add(reader["Sponsor_Id"], reader["Sponsor_Name"],
                            reader["Contact_Number"], reader["Email"], reader["Sponsor_Address"]);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void guna2GradientButton5_Click(object sender, EventArgs e) // Delete button
        {
            // Connection string
            string ConnectionString = "Data Source=DESKTOP-IP1VHSS;Initial Catalog=db_EventManagement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            // Sponsor ID to delete (assuming you have a way to get this value, e.g., from a selected row or input field)
            int sponsorId = Convert.ToInt32(SponsorId); // Example: Using a TextBox named txtSponsorId

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();

                    // Using the stored procedure
                    using (SqlCommand cmd = new SqlCommand("DeleteSponsor", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Sponsor_id", sponsorId); // Pass the Sponsor ID as a parameter

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Sponsor entry deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            guna2DataGridView1.Rows.Clear(); // Clear the table (adjust as per your logic to reload data)
                        }
                        else
                        {
                            MessageBox.Show("No entry found to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"SQL Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        } 

    }
}
