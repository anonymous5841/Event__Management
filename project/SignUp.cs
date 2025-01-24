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
    public partial class Signup : Form
    {
        public Signup()
        {
            InitializeComponent();
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label6.Visible = false;
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }



        private void guna2TextBox1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void guna2TextBox1_Click(object sender, EventArgs e)
        {
            if (guna2TextBox1.Text == "" || guna2TextBox1.Text == "FirstName")
            {
                guna2TextBox1.Text = string.Empty;
                label1.Visible = false;
            }
            guna2TextBox1.ForeColor = Color.White;
        }
        private void guna2TextBox1_Leave_1(object sender, EventArgs e)
        {
            if (guna2TextBox1.Text == "" || guna2TextBox1.Text == "FirstName")
            {
                label1.Visible = true;
                guna2TextBox1.Text = "FirstName";
                guna2TextBox1.ForeColor = Color.Gray;
            }
        }

        private void guna2TextBox2_Click(object sender, EventArgs e)
        {
            if (guna2TextBox2.Text == "" || guna2TextBox2.Text == "LastName")
            {
                guna2TextBox2.Text = string.Empty;

            }
            guna2TextBox2.ForeColor = Color.White;
        }

        private void guna2TextBox2_Leave(object sender, EventArgs e)
        {
            if (guna2TextBox2.Text == "" || guna2TextBox2.Text == "LastName")
            {

                guna2TextBox2.Text = "LastName";
                guna2TextBox2.ForeColor = Color.Gray;
            }
        }

        private void guna2TextBox3_Click(object sender, EventArgs e)
        {
            if (guna2TextBox3.Text == "" || guna2TextBox3.Text == "Email")
            {
                guna2TextBox3.Text = string.Empty;
                label2.Visible = false;

            }
            guna2TextBox3.ForeColor = Color.White;
        }

        private void guna2TextBox3_Leave(object sender, EventArgs e)
        {
            if (guna2TextBox3.Text == "" || guna2TextBox3.Text == "Email")
            {
                label2.Visible = true;
                guna2TextBox3.Text = "Email";
                guna2TextBox3.ForeColor = Color.Gray;
            }
        }
        private void guna2TextBox4_Click(object sender, EventArgs e)
        {
            if (guna2TextBox4.Text == "" || guna2TextBox4.Text == "Number")
            {
                guna2TextBox4.Text = string.Empty;

            }
            guna2TextBox4.ForeColor = Color.White;
        }

        private void guna2TextBox4_Leave(object sender, EventArgs e)
        {
            if (guna2TextBox4.Text == "" || guna2TextBox4.Text == "Number")
            {

                guna2TextBox4.Text = "Number";
                guna2TextBox4.ForeColor = Color.Gray;
            }
        }

        private void guna2TextBox5_Click(object sender, EventArgs e)
        {
            if (guna2TextBox5.Text == "" || guna2TextBox5.Text == "Password")
            {
                guna2TextBox5.Text = string.Empty;
                label3.Visible = false;

            }
            guna2TextBox5.ForeColor = Color.White;
        }

        private void guna2TextBox5_Leave(object sender, EventArgs e)
        {
            if (guna2TextBox5.Text == "" || guna2TextBox5.Text == "Password")
            {
                label3.Visible = true;
                guna2TextBox5.Text = "Password";
                guna2TextBox5.ForeColor = Color.Gray;
            }
        }

        private void guna2TextBox6_Click(object sender, EventArgs e)
        {
            if (guna2TextBox6.Text == "" || guna2TextBox6.Text == "Re-Password")
            {
                guna2TextBox6.Text = string.Empty;
                label4.Visible = false;

            }
            guna2TextBox6.ForeColor = Color.White;
        }

        private void guna2TextBox6_Leave(object sender, EventArgs e)
        {
            if (guna2TextBox6.Text == "" || guna2TextBox6.Text == "Re-Password")
            {
                label4.Visible = true;
                guna2TextBox6.Text = "Re-Password";
                guna2TextBox6.ForeColor = Color.Gray;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox7_Click(object sender, EventArgs e)
        {
            if (guna2TextBox7.Text == "" || guna2TextBox7.Text == "Address")
            {
                guna2TextBox7.Text = string.Empty;
                label6.Visible = false;

            }
            guna2TextBox7.ForeColor = Color.White;
        }

        private void guna2TextBox7_Leave(object sender, EventArgs e)
        {
            if (guna2TextBox7.Text == "" || guna2TextBox7.Text == "Address")
            {
                label6.Visible = true;
                guna2TextBox7.Text = "Address";
                guna2TextBox7.ForeColor = Color.Gray;
            }
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            // Define the connection string (replace with your actual connection details)
            string connectionString = "Data Source=DESKTOP-IP1VHSS;Initial Catalog=db_EventManagement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            // Get the values from your front-end controls (e.g., TextBoxes)
            string u_Firstname = guna2TextBox1.Text; // Assuming the TextBox for first name is named guna2TextBox1
            string u_Lastname = guna2TextBox2.Text; // Assuming the TextBox for last name is named guna2TextBox2
            string u_Email = guna2TextBox3.Text; // Assuming the TextBox for email is named guna2TextBox3
            string u_Contact = guna2TextBox4.Text; // Assuming the TextBox for contact is named guna2TextBox4
            string u_User_Password = guna2TextBox5.Text; // Assuming the TextBox for password is named guna2TextBox5
            string u_User_Role = guna2ComboBox1.SelectedItem.ToString(); // Assuming the ComboBox for role is named guna2ComboBox1
            string u_User_Address = guna2TextBox7.Text; // Assuming the TextBox for address is named guna2TextBox7

            try
            {
                // Create the connection and command objects
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Open the connection
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("InsertUser", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        // Add parameters to the command
                        cmd.Parameters.AddWithValue("@u_Firstname", u_Firstname);
                        cmd.Parameters.AddWithValue("@u_Lastname", u_Lastname);
                        cmd.Parameters.AddWithValue("@u_Email", u_Email);
                        cmd.Parameters.AddWithValue("@u_User_Password", u_User_Password);
                        cmd.Parameters.AddWithValue("@u_User_Role", u_User_Role);
                        cmd.Parameters.AddWithValue("@u_Contact", u_Contact);
                        cmd.Parameters.AddWithValue("@u_User_Address", u_User_Address);

                        // Execute the command
                        cmd.ExecuteNonQuery();

                        // Success message
                        MessageBox.Show("User has been inserted successfully!");
                    }
                }
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions (e.g., if the email already exists or any DB issues)
                MessageBox.Show($"SQL Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Handle general exceptions
                MessageBox.Show($"An unexpected error occurred: {ex.Message}");
            }
        }
    }
    }
