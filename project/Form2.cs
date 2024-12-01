using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            }
            if (guna2TextBox2.Text == "" || guna2TextBox2.Text == "New Password")
            {
                label3.Visible = true;
            }
            if (guna2TextBox3.Text == "" || guna2TextBox3.Text == "Phone Number")
            {
                label1.Visible = true;
            }
        }
    }
}
