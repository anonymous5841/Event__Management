using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project

{
    public partial class EventViewForm : Form
    {
        private int userid;
        private Form form;

        public EventViewForm(int user_id)
        {
            userid = user_id;
            InitializeComponent();
            loadevent();
        }

        private void loadevent()
        {
            string connectionString = "Data Source=DESKTOP-IP1VHSS;Initial Catalog=db_EventManagement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("GetEvent", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        if (userid <= 0) // Validate User ID
                        {
                            MessageBox.Show("Invalid User ID.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        command.Parameters.AddWithValue("@User_id", userid);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                return;
                            }

                            guna2DataGridView1.Rows.Clear();

                            while (reader.Read())
                            {
                                guna2DataGridView1.Rows.Add(
                                    reader["Event_Id"]?.ToString(),
                                    reader["Event_Name"]?.ToString(),
                                    reader["E_Type"]?.ToString(),
                                    reader["E_Date"] != DBNull.Value ? Convert.ToDateTime(reader["E_Date"]).ToString("yyyy-MM-dd") : "N/A",
                                    reader["Sponsor_Id_FK"]?.ToString(),
                                    reader["Sponsor_Percent"]?.ToString(),
                                    reader["Attendee_Id_FK"]?.ToString(),
                                    reader["Profit_Percent"]?.ToString(),
                                    reader["Profit"]?.ToString()
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

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            loadform(new FormEvent(userid));
        }


        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            loadevent();
        }
        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            // Get input text and validate
            string inputText = guna2TextBox1.Text; // Replace with your actual TextBox name
            if (string.IsNullOrWhiteSpace(inputText))
            {
                MessageBox.Show("Please enter a valid Event ID.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (int.TryParse(inputText, out int eventID))
            {
                // Call the database validation method
                if (!ValidateEventID(eventID))
                {
                    MessageBox.Show("Event not present in the database.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    // Load the EventEditForm with the valid Event ID and user ID
                    loadform(new EventEditForm(eventID, userid));
                }
            }
            else
            {
                MessageBox.Show("Please enter a numeric Event ID.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private bool ValidateEventID(int eventID)
        {
            string connectionString = "Data Source=DESKTOP-IP1VHSS;Initial Catalog=db_EventManagement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("ValidateEventID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add the input parameter for EventID
                        command.Parameters.AddWithValue("@EventID", eventID);

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
                MessageBox.Show($"Error validating Event ID: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // Return false in case of error
            }
        }

    }
}
