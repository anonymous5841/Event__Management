using Dbproject;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace project
{
    public partial class AdminDashboard : Form
    {
        public AdminDashboard()
        {
            InitializeComponent();
            button2.Enabled = false;
        }

        private void guna2GradientButtonVendor_Click(object sender, EventArgs e)
        {
            DashboardButton.FillColor = Color.Transparent;
            guna2GradientButton4.FillColor = Color.Transparent;
            guna2GradientButtonVendor.FillColor = Color.CadetBlue;
            guna2GradientButtonVenue.FillColor = Color.Transparent;
            guna2GradientButtonSponsor.FillColor = Color.Transparent;
            guna2GradientButton1.FillColor = Color.Transparent;
            guna2GradientButton2.FillColor = Color.Transparent;
            guna2GradientButton3.FillColor = Color.Transparent;
            guna2GradientButtonLogout.FillColor = Color.Transparent;
            loadform(new ApprovedEvents());
        }


        private void guna2GradientButton4_Click(object sender, EventArgs e)
        {
            DashboardButton.FillColor = Color.Transparent;
            guna2GradientButton4.FillColor = Color.CadetBlue;
            guna2GradientButtonVendor.FillColor = Color.Transparent;
            guna2GradientButtonVenue.FillColor = Color.Transparent;
            guna2GradientButtonSponsor.FillColor = Color.Transparent;
            guna2GradientButton1.FillColor = Color.Transparent;
            guna2GradientButton2.FillColor = Color.Transparent;
            guna2GradientButton3.FillColor = Color.Transparent;
            guna2GradientButtonLogout.FillColor = Color.Transparent;
            loadform(new EventsApproval());
        }

        private void guna2CustomGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            button3.BackColor = Color.LightGray;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.BackColor = Color.FromArgb(26, 31, 39);
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.BackColor = Color.Red;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.FromArgb(26, 31, 39);
        }
        public Point mouseLocation;
        private void mouse_down(object sender, MouseEventArgs e)
        {
            mouseLocation = new Point(-e.X, -e.Y);
        }

        private void mouse_move(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePose = Control.MousePosition;
                mousePose.Offset(mouseLocation.X, mouseLocation.Y);
                Location = mousePose;
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

        private void DashboardButton_Click(object sender, EventArgs e)
        {
            DashboardButton.FillColor = Color.CadetBlue;
            guna2GradientButton4.FillColor = Color.Transparent;
            guna2GradientButtonVendor.FillColor = Color.Transparent;
            guna2GradientButtonVenue.FillColor = Color.Transparent;
            guna2GradientButtonSponsor.FillColor = Color.Transparent;
            guna2GradientButton1.FillColor = Color.Transparent;
            guna2GradientButton2.FillColor = Color.Transparent;
            guna2GradientButton3.FillColor = Color.Transparent;
            guna2GradientButtonLogout.FillColor = Color.Transparent;
            loadform(new DashboardForm());

        }

        private void guna2GradientButtonVenue_Click(object sender, EventArgs e)
        {
            DashboardButton.FillColor = Color.Transparent;
            guna2GradientButton4.FillColor = Color.Transparent;
            guna2GradientButtonVendor.FillColor = Color.Transparent;
            guna2GradientButtonVenue.FillColor = Color.CadetBlue; ;
            guna2GradientButtonSponsor.FillColor = Color.Transparent;
            guna2GradientButton1.FillColor = Color.Transparent;
            guna2GradientButton2.FillColor = Color.Transparent;
            guna2GradientButton3.FillColor = Color.Transparent;
            guna2GradientButtonLogout.FillColor = Color.Transparent;
            loadform(new AllVendors());
        }

        private void guna2GradientButtonSponsor_Click(object sender, EventArgs e)
        {
            DashboardButton.FillColor = Color.Transparent;
            guna2GradientButton4.FillColor = Color.Transparent;
            guna2GradientButtonVendor.FillColor = Color.Transparent;
            guna2GradientButtonVenue.FillColor = Color.Transparent;
            guna2GradientButtonSponsor.FillColor = Color.CadetBlue;
            guna2GradientButton1.FillColor = Color.Transparent;
            guna2GradientButton2.FillColor = Color.Transparent;
            guna2GradientButton3.FillColor = Color.Transparent;
            guna2GradientButtonLogout.FillColor = Color.Transparent;
            loadform(new Deleted_Events());
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            DashboardButton.FillColor = Color.Transparent;
            guna2GradientButton4.FillColor = Color.Transparent;
            guna2GradientButtonVendor.FillColor = Color.Transparent;
            guna2GradientButtonVenue.FillColor = Color.Transparent;
            guna2GradientButtonSponsor.FillColor = Color.Transparent;
            guna2GradientButton1.FillColor = Color.CadetBlue;
            guna2GradientButton2.FillColor = Color.Transparent;
            guna2GradientButton3.FillColor = Color.Transparent;
            guna2GradientButtonLogout.FillColor = Color.Transparent;
            loadform(new EventHistory());
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            DashboardButton.FillColor = Color.Transparent;
            guna2GradientButton4.FillColor = Color.Transparent;
            guna2GradientButtonVendor.FillColor = Color.Transparent;
            guna2GradientButtonVenue.FillColor = Color.Transparent;
            guna2GradientButtonSponsor.FillColor = Color.Transparent;
            guna2GradientButton1.FillColor = Color.Transparent;
            guna2GradientButton2.FillColor = Color.CadetBlue;
            guna2GradientButton3.FillColor = Color.Transparent;
            guna2GradientButtonLogout.FillColor = Color.Transparent;
            loadform(new Sponsors());
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            DashboardButton.FillColor = Color.Transparent;
            guna2GradientButton4.FillColor = Color.Transparent;
            guna2GradientButtonVendor.FillColor = Color.Transparent;
            guna2GradientButtonVenue.FillColor = Color.Transparent;
            guna2GradientButtonSponsor.FillColor = Color.Transparent;
            guna2GradientButton1.FillColor = Color.Transparent;
            guna2GradientButton2.FillColor = Color.Transparent;
            guna2GradientButton3.FillColor = Color.CadetBlue;
            guna2GradientButtonLogout.FillColor = Color.Transparent;
            loadform(new Attendee());
        }

      
        private void guna2GradientButtonLogout_Click_1(object sender, EventArgs e)
        {
            DashboardButton.FillColor = Color.Transparent;
            guna2GradientButton4.FillColor = Color.Transparent;
            guna2GradientButtonVendor.FillColor = Color.Transparent;
            guna2GradientButtonVenue.FillColor = Color.Transparent;
            guna2GradientButtonSponsor.FillColor = Color.Transparent;
            guna2GradientButton1.FillColor = Color.Transparent;
            guna2GradientButton2.FillColor = Color.Transparent;
            guna2GradientButton3.FillColor = Color.Transparent;
            guna2GradientButtonLogout.FillColor = Color.CadetBlue;

            this.Hide();
            Form1 form = new Form1();
            form.Show();
        }
    }
}
