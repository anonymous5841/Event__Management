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
    public partial class FormVendor : Form
    {
        public FormVendor()
        {
            InitializeComponent();
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
       
        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            loadform(new VendorCreateForm());
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            loadform(new VendorEditForm());
        }
    }
}
