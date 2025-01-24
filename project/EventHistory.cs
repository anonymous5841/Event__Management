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
    public partial class EventHistory : Form
    {
        public EventHistory()
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

        private void EventHistory_Load(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-IP1VHSS;Initial Catalog=db_EventManagement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            try
            {
                // Create the connection and command objects
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Open the connection
                    conn.Open();

                    // Define the query to fetch data from the view
                    string query = "SELECT * FROM vw_ApprovedOrRejectedEvents";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Execute the query and retrieve data
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Assuming you have a DataGridView (guna2DataGridView1) to display the data
                            guna2DataGridView2.Rows.Clear(); // Clear existing rows

                            while (reader.Read())
                            {
                                // Add rows to the DataGridView (adjust column names as per your view)
                                guna2DataGridView2.Rows.Add(
                                    reader["Event_Id"].ToString(),
                                    reader["Event_Name"].ToString(),
                                    reader["E_Date"].ToString(),
                                    reader["Start_Time"].ToString(),
                                    reader["End_Time"].ToString(),
                                    reader["E_Type"].ToString(),
                                    reader["Attendee_Id_FK"].ToString(),
                                    reader["Vendor_Price"].ToString(),
                                    reader["Profit_Percent"].ToString(),
                                    reader["User_Id_FK"].ToString(),
                                    reader["Event_Status"].ToString() // Event_Status is from Vendor Registration
                                );
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions (e.g., if the query fails)
                MessageBox.Show($"SQL Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Handle general exceptions
                MessageBox.Show($"An unexpected error occurred: {ex.Message}");
            }
        }


        private void guna2DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
