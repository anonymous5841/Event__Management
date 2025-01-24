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
    public partial class DashboardForm : Form
    {
        public DashboardForm()
        {
            InitializeComponent();
            LoadPreviousWeek();
            LoadUpComingEvent();
            profit();
            LoadRevenue();
            GetVendorStatistics();
            GetEventStatistics();
        }

        private void LoadUpComingEvent()
        {
            // Connection string to your database
            string connectionString = "Data Source=DESKTOP-IP1VHSS;Initial Catalog=db_EventManagement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            // SQL stored procedure name
            string storedProcedureName = "GetUpcomingEvents";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            // Fetch data into a DataTable
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            // Clear existing rows but keep the headers
                            guna2DataGridView1.Rows.Clear();

                            // Map DataTable columns to specific DataGridView columns
                            foreach (DataRow row in dataTable.Rows)
                            {
                                // Add data to rows based on the mapping of SQL columns to DataGridView headers
                                guna2DataGridView1.Rows.Add(
                                    row["Event_Name"].ToString(),
                                      row["E_Type"].ToString(), 
                                    Convert.ToDateTime(row["E_Date"]).ToString("yyyy-MM-dd"), 
                                    row["Profit"].ToString()        
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPreviousWeek()
        {
            // Connection string to your database
            string connectionString = "Data Source=DESKTOP-IP1VHSS;Initial Catalog=db_EventManagement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            // SQL stored procedure name
            string storedProcedureName = "GetLastWeekEvents";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            // Fetch data into a DataTable
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            // Clear existing rows but keep the headers
                            guna2DataGridView2.Rows.Clear();

                            // Map DataTable columns to specific DataGridView columns
                            foreach (DataRow row in dataTable.Rows)
                            {
                                // Add data to rows based on the mapping of SQL columns to DataGridView headers
                                guna2DataGridView2.Rows.Add(
                                    row["Event_Name"].ToString(),
                                     row["E_Type"].ToString(),
                                    Convert.ToDateTime(row["E_Date"]).ToString("yyyy-MM-dd"), 
                                    row["Profit"].ToString()         
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void profit()
        {
            // Connection string to your database
            string connectionString = "Data Source=DESKTOP-IP1VHSS;Initial Catalog=db_EventManagement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True"; // Replace with your connection string

            try
            {
                // Get last week's profit and update label18
                decimal lastWeekProfit = GetProfitSum("GetLastWeekProfitSum", connectionString);
                label18.Text = string.Format("{0:C}", lastWeekProfit); // Format as currency

                // Get last month's profit and update label12
                decimal lastMonthProfit = GetProfitSum("GetLastMonthProfitSum", connectionString);
                label12.Text = string.Format("{0:C}", lastMonthProfit); // Format as currency

                // Get total profit up to now and update label13
                decimal totalProfit = GetProfitSum("GetTotalProfitUptoNow", connectionString);
                label13.Text = string.Format("{0:C}", totalProfit); // Format as currency
            }
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private decimal GetProfitSum(string storedProcedureName, string connectionString)
        {
            decimal profitSum = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    // Execute the procedure and get the result
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        profitSum = Convert.ToDecimal(result);
                    }
                }
            }

            return profitSum;
        }

        private void LoadRevenue()
        {
            // Connection string to your database
            string connectionString = "Data Source=DESKTOP-IP1VHSS;Initial Catalog=db_EventManagement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True"; // Replace with your connection string

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Get Total Revenue
                    label10.Text = string.Format("{0:C}", GetRevenueFromProcedure("GetTotalProfitUptoNow", connection));

                    // Get Last Month Revenue
                    label11.Text = string.Format("{0:C}", GetRevenueFromProcedure("GetTotalProfitUptoNow", connection));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private decimal GetRevenueFromProcedure(string storedProcedureName, SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Execute the procedure and get the result
                object result = command.ExecuteScalar();
                return result != null && result != DBNull.Value ? Convert.ToDecimal(result) : 0;
            }
        }
        private void GetVendorStatistics()
        {
            string connectionString = "Data Source=DESKTOP-IP1VHSS;Initial Catalog=db_EventManagement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True"; // Replace with your connection string

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("GetVendorPercentageCount", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Directly fetch the percentages from the SQL output
                                int approvedPercentage = reader.GetInt32(0); // Approved Percentage
                                int rejectedPercentage = reader.GetInt32(1); // Rejected Percentage
                                int pendingPercentage = reader.GetInt32(2);  // Pending Percentage

                                // Assign values to labels
                                label22.Text = $"{approvedPercentage}%";  // Approved
                                label25.Text = $"{rejectedPercentage}%";  // Rejected
                                label26.Text = $"{pendingPercentage}%";  // Pending
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void GetEventStatistics()
        {
            string connectionString = "Data Source=DESKTOP-IP1VHSS;Initial Catalog=db_EventManagement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True"; // Replace with your connection string

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Get Total Events Before Today
                    int totalEventsBeforeToday = GetEventData("GetTotalEventsBeforeToday", connection);
                    label23.Text = $"{totalEventsBeforeToday}";

                    // Get Total Events Before Today Percentage
                    int totalEventsBeforeTodayPercentage = GetEventData("GetTotalEventsBeforeTodayPercentage", connection);
                    circularProgressBar1.Value = totalEventsBeforeTodayPercentage; // Set progress bar directly
                    circularProgressBar1.Text = $"{totalEventsBeforeTodayPercentage}%";

                    // Get Last Week Events Percentage
                    int lastWeekEventsPercentage = GetEventData("GetLastWeekEventsPercentage", connection);
                    label20.Text = $"{lastWeekEventsPercentage}%";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GetEventData(string storedProcedureName, SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Execute the procedure and get the result
                object result = command.ExecuteScalar();
                return result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;
            }
        }
       
    }
}
