using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Security.Cryptography;
using System.Web.UI.WebControls.WebParts;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using Dbproject;

namespace project

{

    public partial class Form1 : System.Windows.Forms.Form
    {
        private Form form = null;
        public Form1()
        {
            InitializeComponent();
            label2.Visible = false;
            label3.Visible = false;
            button3.Enabled = false;

        }

        private void guna2TextBox1_Click(object sender, EventArgs e)
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
            if (guna2TextBox2.Text == "" || guna2TextBox2.Text == "Password")
            {
                guna2TextBox2.Text = string.Empty;
                label3.Visible = false;
            }
            guna2TextBox2.ForeColor = Color.White;
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
            if (guna2TextBox2.Text == "" || guna2TextBox2.Text == "Password")
            {
                label3.Visible = true;
                guna2TextBox2.Text = "Password";
                guna2TextBox2.ForeColor = Color.Gray;
            }
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-IP1VHSS;Initial Catalog=db_EventManagement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    // Create the SqlCommand for the stored procedure
                    using (SqlCommand cmd = new SqlCommand("ValidateUser", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Clear();
                        // Add input parameters
                        cmd.Parameters.AddWithValue("@u_Email", guna2TextBox1.Text.Trim());
                        cmd.Parameters.AddWithValue("@u_User_Password", guna2TextBox2.Text.Trim());

                        // Add output parameters
                        SqlParameter userIdParam = new SqlParameter("@User_Id", System.Data.SqlDbType.Int)
                        {
                            Direction = System.Data.ParameterDirection.Output
                        };
                        cmd.Parameters.Add(userIdParam);

                        SqlParameter IsAttendeeParam = new SqlParameter("@IsAttendee", System.Data.SqlDbType.Int)
                        {
                            Direction = System.Data.ParameterDirection.Output
                        };
                        cmd.Parameters.Add(IsAttendeeParam);

                        SqlParameter statusParam = new SqlParameter("@status", System.Data.SqlDbType.VarChar, 10)
                        {
                            Direction = System.Data.ParameterDirection.Output
                        };
                        cmd.Parameters.Add(statusParam);

                        // Execute the stored procedure
                        cmd.ExecuteNonQuery();

                        // Retrieve the output values
                        int userId = (int)userIdParam.Value;
                        int IsAttendee = (int)IsAttendeeParam.Value;
                        string status = statusParam.Value as string;

                        // Check if the user exists and navigate based on the status
                        if (userId == -1)
                        {
                            MessageBox.Show("Invalid email or password!", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            if (status == "Administra")
                            {

                                // Navigate to Admin Form
                                this.Hide();
                                AdminDashboard AD = new AdminDashboard(userId);
                                AD.Show();
                            }
                            else if (status == "Employee")
                            {
                                // Navigate to Employee Form
                                this.Hide();
                                MainEmployeeForm employeeForm = new MainEmployeeForm(userId, IsAttendee);
                                employeeForm.Show();
                            }

                        }
                    }
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 50000)
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
            panel1.Controls.Add(childform);
            panel1.Tag = childform;
            childform.BringToFront();
            childform.Show();
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            loadform(new Form2());

        }


        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void button4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void button2_MouseEnter(object sender, EventArgs e)
        {

            button2.BackColor = Color.Red;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {

            button2.BackColor = Color.FromArgb(64, 64, 64);
        }

        private void button4_MouseEnter(object sender, EventArgs e)
        {
            button4.BackColor = Color.LightGray;
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            button4.BackColor = Color.FromArgb(64, 64, 64);
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            guna2Button3.FillColor = Color.FromArgb(26, 31, 39);
            guna2Button2.FillColor = Color.FromArgb(255, 77, 165);
            form = new Signup();
            loadform(form);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            guna2Button2.FillColor = Color.FromArgb(26, 31, 39);
            guna2Button3.FillColor = Color.FromArgb(255, 77, 165);
            guna2Button3.BorderColor = Color.FromArgb(255, 77, 165);
            form.Close();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            form = new Signup();
            loadform(form);
        }
    }

}
