using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace project

{
   
    public partial class Form1 : System.Windows.Forms.Form
    {
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
            if (guna2TextBox1.Text == "" || guna2TextBox1.Text == "Email")
            {
                label2.Visible = true;
            }
            if (guna2TextBox2.Text == "" || guna2TextBox2.Text == "Password")
            {
                label3.Visible = true;
            }

            this.Hide();
            MainEmployeeForm m = new MainEmployeeForm();
            m.Show();

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

            //panel3.Visible = true;
            // Form3 f = new Form3();
            //f.Show();
            //this.Hide();
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

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            guna2Button2.FillColor = Color.FromArgb(26, 31, 39);
            guna2Button3.FillColor = Color.FromArgb(255, 77, 165);
            guna2Button3.BorderColor = Color.FromArgb(255, 77, 165);

        }
    }

}
