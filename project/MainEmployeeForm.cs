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
    public partial class MainEmployeeForm : System.Windows.Forms.Form
    {
        private int userId;
        public MainEmployeeForm(int User_id, int isAttendee)
        {
            InitializeComponent();
            button2.Enabled = false;
            userId = User_id;
            if (isAttendee == 1)
            {
                guna2GradientButtonAttendee.Enabled = true;
                guna2GradientButtonAttendee.Visible = true;
            }
            else
            {
                guna2GradientButtonAttendee.Enabled = false;
                guna2GradientButtonAttendee.Visible = false;
            }

        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {

            button1.BackColor = Color.Red;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {

            button1.BackColor = Color.FromArgb(26, 31, 39);
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            button3.BackColor = Color.LightGray;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.BackColor = Color.FromArgb(26, 31, 39);
        }

        public Point mouseLocation;
        private void mouse_down(object sender, MouseEventArgs e)
        {
            mouseLocation = new Point(-e.X,-e.Y);
        }

        private void mouse_move(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePose = Control.MousePosition;
                mousePose.Offset(mouseLocation.X,mouseLocation.Y);
                Location = mousePose;
            }
        }
      
        private void guna2GradientButtonEvent_Click(object sender, EventArgs e)
        {
            guna2GradientButtonEvent.FillColor = Color.HotPink;
            guna2GradientButtonVendor.FillColor = Color.Transparent;
            guna2GradientButtonVenue.FillColor = Color.Transparent;
            guna2GradientButtonSponsor.FillColor = Color.Transparent;
            guna2GradientButtonLogout.FillColor = Color.Transparent;
            guna2GradientButtonAttendee.FillColor = Color.Transparent;

            loadform(new EventViewForm(userId));
        }

        private void guna2GradientButtonVendor_Click(object sender, EventArgs e)
        {
            guna2GradientButtonEvent.FillColor = Color.Transparent;
            guna2GradientButtonVendor.FillColor = Color.HotPink;
            guna2GradientButtonVenue.FillColor = Color.Transparent;
            guna2GradientButtonSponsor.FillColor = Color.Transparent;
            guna2GradientButtonLogout.FillColor = Color.Transparent;
            guna2GradientButtonAttendee.FillColor = Color.Transparent;

            loadform(new FormVendor(userId));

        }

        private void guna2GradientButtonVenue_Click(object sender, EventArgs e)
        {
            guna2GradientButtonEvent.FillColor = Color.Transparent;
            guna2GradientButtonVendor.FillColor = Color.Transparent;
            guna2GradientButtonVenue.FillColor = Color.HotPink;
            guna2GradientButtonSponsor.FillColor = Color.Transparent;
            guna2GradientButtonLogout.FillColor = Color.Transparent;
            guna2GradientButtonAttendee.FillColor = Color.Transparent;

            loadform(new FormVenue(userId));
        }

        private void guna2GradientButtonSponsor_Click(object sender, EventArgs e)
        {
            
            guna2GradientButtonEvent.FillColor = Color.Transparent;
            guna2GradientButtonVendor.FillColor = Color.Transparent;
            guna2GradientButtonVenue.FillColor = Color.Transparent;
            guna2GradientButtonSponsor.FillColor = Color.HotPink;
            guna2GradientButtonLogout.FillColor = Color.Transparent;
            guna2GradientButtonAttendee.FillColor = Color.Transparent;

            loadform(new FormSponsor(userId));
        }

        private void guna2GradientButtonLogout_Click(object sender, EventArgs e)
        {
            guna2GradientButtonEvent.FillColor = Color.Transparent;
            guna2GradientButtonVendor.FillColor = Color.Transparent;
            guna2GradientButtonVenue.FillColor = Color.Transparent;
            guna2GradientButtonSponsor.FillColor = Color.Transparent;
            guna2GradientButtonLogout.FillColor = Color.HotPink;
            guna2GradientButtonAttendee.FillColor = Color.Transparent;


            this.Hide();
            Form1 form = new Form1();
            form.Show();
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

        private void guna2GradientButtonAttendee_Click(object sender, EventArgs e)
        {
            guna2GradientButtonEvent.FillColor = Color.Transparent;
            guna2GradientButtonVendor.FillColor = Color.Transparent;
            guna2GradientButtonVenue.FillColor = Color.Transparent;
            guna2GradientButtonSponsor.FillColor = Color.Transparent;
            guna2GradientButtonLogout.FillColor = Color.Transparent;
            guna2GradientButtonAttendee.FillColor = Color.HotPink;

            loadform(new FormAttendee(userId));
        }
    }
}
