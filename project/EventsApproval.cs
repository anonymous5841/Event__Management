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
    public partial class EventsApproval : Form
    {
        public EventsApproval()
        {
            InitializeComponent();
        }

        private void guna2GradientButtonVendor_Click(object sender, EventArgs e)
        {

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
            // Get the Event ID from the textbox
            string eventId = guna2TextBox1.Text.Trim();

            if (string.IsNullOrEmpty(eventId))
            {
                MessageBox.Show("Please enter a valid Event ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Pass the Event ID to the next form
            Eventsviewmore nextForm = new Eventsviewmore(eventId);
            loadform(nextForm);
        }


        private void guna2TextBox1_TextChanged(object sender, EventArgs e) //textbox
        {

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

        private void EventsApproval_Load(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-IP1VHSS;Initial Catalog=db_EventManagement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT Event_Id, Event_Name, E_Date, Start_Time, End_Time, E_Type, User_Id_FK FROM vw_Events";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            guna2DataGridView1.Rows.Add(
                                reader["Event_Id"],
                                reader["Event_Name"],
                                reader["E_Date"],
                                reader["Start_Time"],
                                reader["End_Time"],
                                reader["E_Type"],
                                reader["User_Id_FK"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Block non-numeric input
            }
        }

    }
}
