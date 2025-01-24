using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Dbproject
{
    public partial class Venue : Form
    {
        public Venue()
        {
            InitializeComponent();
        }

        // Method to load venue data into the DataGridView
        private void LoadVenueData()
        {
            // Define the connection string (replace with your actual connection details)
            string connectionString = "Data Source=DESKTOP-IP1VHSS;Initial Catalog=db_EventManagement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            // Clear existing rows in the DataGridView
            guna2DataGridView2.Rows.Clear();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // SQL query to select data from the view
                string query = "SELECT * FROM vw_VenueDetails";

                // Create a command to execute the query
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Execute the query and get a data reader
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Loop through the results and populate the DataGridView
                        while (reader.Read())
                        {
                            // Add a new row for each venue and its associated user details
                            guna2DataGridView2.Rows.Add(
                                reader["Venue_Id"],          // Venue_Id
                                reader["Venue_Name"],        // Venue_Name
                                reader["Venue_Location"],    // Venue_Location
                                reader["Capacity"],          // Capacity
                                reader["Contact_Number"],    // Contact_Number
                                reader["User_Id_FK"]         // User_Id_FK
                            );
                        }
                    }
                }
            }
        }

        // Delete button click event
        private void guna2GradientButton5_Click(object sender, EventArgs e)
        {
            // Get the Venue_Id entered by the user in the search box (guna2TextBoxSearch)
            string venueIdText = guna2TextBox2.Text;

            if (string.IsNullOrEmpty(venueIdText))
            {
                MessageBox.Show("Please enter a Venue ID to search.");
                return;
            }

            int venueId;

            // Validate if the entered Venue_Id is a valid integer
            if (!int.TryParse(venueIdText, out venueId))
            {
                MessageBox.Show("Please enter a valid numeric Venue ID.");
                return;
            }

            // Define the connection string (replace with your actual connection details)
            string connectionString = "Data Source=DESKTOP-IP1VHSS;Initial Catalog=db_EventManagement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    // SQL query to delete the venue based on the Venue_Id
                    string query = "DELETE FROM tb_Venue WHERE Venue_Id = @Venue_Id";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Add parameter for Venue_Id
                        cmd.Parameters.AddWithValue("@Venue_Id", venueId);

                        // Execute the delete query
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Venue deleted successfully!");
                        }
                        else
                        {
                            MessageBox.Show("No venue found with the given ID.");
                        }
                    }

                    // Reload the data in the DataGridView after deletion
                    LoadVenueData();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"SQL Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"General Error: {ex.Message}");
                }
            }
        }

        // Form load event, called when the form is loaded
        private void Venue_Load(object sender, EventArgs e)
        {
            // Initially load the data into the DataGridView
            LoadVenueData();
        }
    }
}
