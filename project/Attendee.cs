using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Dbproject
{
    public partial class Attendee : Form
    {
        string ConnectionString = "Data Source=DESKTOP-IP1VHSS;Initial Catalog=db_EventManagement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        public Attendee()
        {
            InitializeComponent();
        }

        private void Attendee_Load(object sender, EventArgs e)
        {
            LoadData(); // Load data on form load
        }

        // Function to load data into the DataGridView
        private void LoadData()
        {
            guna2DataGridView1.Rows.Clear(); // Clear the DataGridView

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT * FROM vw_Attendee";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        guna2DataGridView1.Rows.Add(
                            reader["Attendee_Id"],
                            reader["Admin_User_ID"],
                            reader["User_ID"],
                            reader["Feedback"]
                        );
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        // Add Button Click Event
        private void guna2GradientButton4_Click(object sender, EventArgs e) // Add
        {
            if (string.IsNullOrWhiteSpace(guna2TextBox1.Text))
            {
                MessageBox.Show("Please enter the Attendee ID to add.");
                return;
            }

            int attendeeId = int.Parse(guna2TextBox1.Text);
            string feedback = "Default Feedback"; // Replace or ask user for input

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();
                    string query = "INSERT INTO tb_Attendee (Attendee_Id, Admin_User_Id_FK, User_Id_FK, Feedback) " +
                                   "VALUES (@AttendeeId, @AdminId, @UserId, @Feedback)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@AttendeeId", attendeeId);
                    cmd.Parameters.AddWithValue("@AdminId", 1); // Replace with the correct Admin_User_Id_FK
                    cmd.Parameters.AddWithValue("@UserId", 2);  // Replace with the correct User_Id_FK
                    cmd.Parameters.AddWithValue("@Feedback", feedback);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record Added Successfully!");
                    LoadData(); // Refresh the DataGridView
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        // Delete Button Click Event
        private void guna2GradientButton1_Click(object sender, EventArgs e) // Delete
        {
            if (string.IsNullOrWhiteSpace(guna2TextBox1.Text))
            {
                MessageBox.Show("Please enter the Attendee ID to delete.");
                return;
            }

            int attendeeId = int.Parse(guna2TextBox1.Text);

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();
                    string query = "DELETE FROM tb_Attendee WHERE Attendee_Id = @AttendeeId";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@AttendeeId", attendeeId);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Record Deleted Successfully!");
                    }
                    else
                    {
                        MessageBox.Show("No record found with the specified Attendee ID.");
                    }
                    LoadData(); // Refresh the DataGridView
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void guna2TextBox1_Click(object sender, EventArgs e)
        {
            if (guna2TextBox1.Text == "Search")
            {
                guna2TextBox1.Text = "";
                guna2TextBox1.ForeColor = Color.White;
            }
        }

        private void guna2TextBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(guna2TextBox1.Text))
            {
                guna2TextBox1.Text = "Search";
                guna2TextBox1.ForeColor = Color.Gray;
            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
