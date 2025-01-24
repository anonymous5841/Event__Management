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

namespace project
{
    public partial class Form2 : System.Windows.Forms.Form
    {
        public Form2()
        {
            InitializeComponent();
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
        }

        void guna2TextBox1_Click(object sender, EventArgs e)
        {
            if (guna2TextBox1.Text == "" || guna2TextBox1.Text == "Email")
            {
                guna2TextBox1.Text = string.Empty;
                label2.Visible = false;
            }
            guna2TextBox1.ForeColor = Color.White;
        }

        private void guna2TextBox2_Click(object sender, EventArgs e)
        {
            if (guna2TextBox2.Text == "" || guna2TextBox2.Text == "New Password")
            {
                guna2TextBox2.Text = string.Empty;
                label3.Visible = false;
            }
            guna2TextBox2.ForeColor = Color.White;
        }


        private void guna2TextBox3_Click(object sender, EventArgs e)
        {
            if (guna2TextBox3.Text == "" || guna2TextBox3.Text == "Phone Number")
            {
                guna2TextBox3.Text = string.Empty;
                label1.Visible = false;
            }
            guna2TextBox3.ForeColor = Color.White;
        }

        private void guna2TextBox1_Leave(object sender, EventArgs e)
        {
            if (guna2TextBox1.Text == "" || guna2TextBox1.Text == "Email")
            {
                label2.Visible = true;
                guna2TextBox1.Text = "Email";
                guna2TextBox1.ForeColor = Color.Gray;
            }
        }

        private void guna2TextBox2_Leave(object sender, EventArgs e)
        {
            if (guna2TextBox2.Text == "" || guna2TextBox2.Text == "New Password")
            {
                label3.Visible = true;
                guna2TextBox2.Text = "New Password";
                guna2TextBox2.ForeColor = Color.Gray;
            }
        }

        private void guna2TextBox3_Leave(object sender, EventArgs e)
        {
            if (guna2TextBox3.Text == "" || guna2TextBox3.Text == "Phone Number")
            {
                label1.Visible = true;
                guna2TextBox3.Text = "Phone Number";
                guna2TextBox3.ForeColor = Color.Gray;
            }
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            if (guna2TextBox1.Text == "" || guna2TextBox1.Text == "Email")
            {
                label2.Visible = true;
                return;
            }
            if (guna2TextBox2.Text == "" || guna2TextBox2.Text == "New Password")
            {
                label3.Visible = true;
                return;
            }
            if (guna2TextBox3.Text == "" || guna2TextBox3.Text == "Phone Number")
            {
                label1.Visible = true;
                return;
            }

            // SQL connection string
            string connectionString = "Data Source=DESKTOP-IP1VHSS;Initial Catalog=db_EventManagement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            // Creating SQL connection and command
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    // Create a command to call the stored procedure
                    using (SqlCommand cmd = new SqlCommand("ForgotPassword", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Adding parameters for the stored procedure
                        cmd.Parameters.AddWithValue("@u_Email", guna2TextBox1.Text);
                        cmd.Parameters.AddWithValue("@u_Contact", guna2TextBox3.Text);
                        cmd.Parameters.AddWithValue("@u_NewPassword", guna2TextBox2.Text);

                        // Execute the stored procedure
                        cmd.ExecuteNonQuery();

                        // Show a success message after the password is updated
                        MessageBox.Show("Password successfully updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Optionally, you can clear the text boxes or redirect the user to a login page
                        Form1 form = new Form1();
                        form.Show();
                        this.Hide();

                    }
                }
                catch (SqlException ex)
                {
                    // Handle exceptions
                    if (ex.Number == 60000)
                    {
                        MessageBox.Show("Invalid Email or Contact number!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (ex.Number == 50000)
                    {
                        MessageBox.Show("Transaction Rolled back due to error!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    // Handle general exceptions (non-SQL exceptions)
                    MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                finally
                {
                    // Close the connection
                    con.Close();
                }
            }
        }
    }
}
