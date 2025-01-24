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
    public partial class ApprovedEvents : Form
    {
        public ApprovedEvents()
        {
            InitializeComponent();
        }

        private void guna2DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ApprovedEvents_Load(object sender, EventArgs e)
        {
            // Define your connection string
            string ConnectionString = "Data Source=DESKTOP-IP1VHSS;Initial Catalog=db_EventManagement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            // Establish a connection to the database
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                // Query the vw_Approved_Events view
                String Query = "SELECT Event_Id, Event_Name, E_Date, Start_Time, End_Time, E_Type, Attendee_Id_FK, Vendor_Price, Profit_Percent, User_Id_FK FROM vw_Approved_Events";
                SqlCommand cmd = new SqlCommand(Query, con);

                // Execute the query and read results
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        guna2DataGridView2.Rows.Add(
                            reader["Event_Id"],
                            reader["Event_Name"],
                            reader["E_Date"],
                            reader["Start_Time"],
                            reader["End_Time"],
                            reader["E_Type"],
                            reader["Attendee_Id_FK"],
                            reader["Vendor_Price"],
                            reader["Profit_Percent"],
                            reader["User_Id_FK"]
                        );
                    }
                }
            }
        }

    }
}
