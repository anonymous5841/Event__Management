using Guna.UI2.WinForms;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Dbproject
{
    public partial class VendorsViewMore : Form
    {
        private string vendorId;
        private Form activeForm = null;

        public VendorsViewMore(string vendorId)
        {
            InitializeComponent();
            this.vendorId = vendorId;
        }

        public void LoadForm(Form childForm)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }

            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            guna2GradientPanel1.Controls.Add(childForm);
            guna2GradientPanel1.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void guna2GradientCircleButton1_Click(object sender, EventArgs e)
        {
            LoadForm(new AllVendors());
        }

        private void VendorsViewMore_Load(object sender, EventArgs e)
        {
            LoadVendorDetails(vendorId);
        }

        private void LoadVendorDetails(string vendorId)
        {
            string connectionString = "Data Source=DESKTOP-IP1VHSS;Initial Catalog=db_EventManagement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    // Query to fetch vendor details based on the provided Vendor ID
                    string query = "SELECT * FROM vw_tb_Vendor WHERE Vendor_Id = @Vendor_Id";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Vendor_Id", vendorId); // Corrected parameter name

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                guna2DataGridView2.Rows.Clear(); // Clear previous data

                                while (reader.Read())
                                {
                                    guna2DataGridView2.Rows.Add(
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
                            else
                            {
                                MessageBox.Show($"No vendor found with ID {vendorId}.", "Vendor Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Clear DataGridView if no record is found
                                guna2DataGridView2.Rows.Clear();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while loading vendor details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void guna2DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Handle cell content click if necessary
        }
    }
}