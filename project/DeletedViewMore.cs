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
    public partial class DeletedViewMore : Form
    {
        private string eventId;
        public DeletedViewMore(string eventId)
        {
            InitializeComponent();
            this.eventId = eventId; // Store the Event ID
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
            loadform(new Deleted_Events());
        }

        private void DeletedViewMore_Load(object sender, EventArgs e)
        {
            LoadDeletedEventDetails(eventId);
        }
        private void LoadDeletedEventDetails(string eventId)
        {
            // Define your connection string
            string connectionString = "Data Source=DESKTOP-IP1VHSS;Initial Catalog=db_EventManagement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    // Query to fetch event details based on the provided Event ID
                    string query = "SELECT Event_Id, Event_Name, E_Date, Start_Time, End_Time, E_Type, Attendee_Id_FK, Vendor_Price, Profit_Percent, User_Id_FK, E_Description " +
                                   "FROM vw_Rejected_Events WHERE Event_Id = @EventId";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@EventId", eventId); // Use parameterized query to prevent SQL injection

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            // Populate the DataGridView with the event details
                            guna2DataGridView2.Rows.Clear(); // Clear any existing rows
                            guna2DataGridView2.Rows.Add(
                                reader["Event_Id"],
                                reader["Event_Name"],
                                reader["E_Date"],
                                reader["Start_Time"],
                                reader["End_Time"],
                                reader["E_Type"],
                                reader["Attendee_Id_FK"],
                                reader["Vendor_Price"],
                                reader["Profit_Percent"],
                                reader["User_Id_FK"]
                            );

                            // Display the description in a separate textbox
                            guna2TextBox3.Text = reader["E_Description"]?.ToString() ?? "No Description Available";
                        }
                        else
                        {
                            MessageBox.Show($"No event found with ID {eventId}.", "Event Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Clear DataGridView and Description TextBox if no record is found
                            guna2DataGridView2.Rows.Clear();
                            guna2TextBox3.Text = string.Empty;  //description  box
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while loading event details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        private void guna2DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }
}
