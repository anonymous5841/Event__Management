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
    public partial class EventEditForm : Form
    {
        private int eventid;
        private int userid;
        public EventEditForm(int event_id, int user_id)
        {
            InitializeComponent();
            eventid = event_id;
            userid = user_id;
            GetEventDetails();
        }


        public void GetEventDetails()
        {
            // Connection string - replace with your actual database connection string
            string connectionString = "Data Source=DESKTOP-IP1VHSS;Initial Catalog=db_EventManagement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create the SqlCommand
                    using (SqlCommand command = new SqlCommand("GetEventDetails", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add the Event_Id parameter
                        command.Parameters.AddWithValue("@Event_Id", eventid);

                        // Execute the command and read the data
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    // Populate the fields with the returned data
                                    guna2TextBoxName.Text = reader["Event_Name"].ToString();
                                    guna2TextBoxType.Text = reader["E_Type"].ToString();
                                    guna2TextBoxDescription.Text = reader["E_Description"].ToString();
                                    guna2TextBoxAttendeeid.Text = reader["Attendee_Id_FK"].ToString();
                                    guna2TextBoxSponsorid.Text = reader["Sponsor_Id_FK"].ToString();
                                    guna2TextBoxVenueid.Text = reader["Venue_Id_FK"].ToString();
                                    guna2TextBoxVendorid.Text = reader["Vendor_Id_FK"].ToString();

                                    // Parsing and formatting date, time, and numeric values
                                    DateTime eventDate = Convert.ToDateTime(reader["E_Date"]);
                                    guna2DateTimePicker1.Text = eventDate.ToString("yyyy-MM-dd");

                                    TimeSpan startTime = TimeSpan.Parse(reader["Start_Time"].ToString());
                                    guna2NumericUpDownStartTime_1.Value = startTime.Hours;
                                    guna2NumericUpDownStartTime_2.Value = startTime.Minutes;

                                    TimeSpan endTime = TimeSpan.Parse(reader["End_Time"].ToString());
                                    guna2NumericUpDownendtime_1.Value = endTime.Hours;
                                    guna2NumericUpDownendtime_2.Value = endTime.Minutes;

                                    // Handle numeric fields, leaving them empty if null or 0
                                    numericUpDownvendorprice.Value = reader["Vendor_Price"] != DBNull.Value && Convert.ToInt32(reader["Vendor_Price"]) > 0
                                        ? Convert.ToInt32(reader["Vendor_Price"])
                                        : 0;

                                    numericUpDownprofitprice.Value = reader["Profit_Percent"] != DBNull.Value && Convert.ToInt32(reader["Profit_Percent"]) > 0
                                        ? Convert.ToInt32(reader["Profit_Percent"])
                                        : 0;

                                    numericUpDownsponsorprice.Value = reader["Sponsor_Percent"] != DBNull.Value && Convert.ToInt32(reader["Sponsor_Percent"]) > 0
                                        ? Convert.ToInt32(reader["Sponsor_Percent"])
                                        : 0;

                                    numericUpDownVenueprice.Value = reader["Venue_Price"] != DBNull.Value && Convert.ToInt32(reader["Venue_Price"]) > 0
                                        ? Convert.ToInt32(reader["Venue_Price"])
                                        : 0;

                                    guna2NumericUpDownbrozequantity.Value = reader["Ticket_quantity_bronze"] != DBNull.Value && Convert.ToInt32(reader["Ticket_quantity_bronze"]) > 0
                                        ? Convert.ToInt32(reader["Ticket_quantity_bronze"])
                                        : 0;

                                    guna2NumericUpDowngoldquantity.Value = reader["Ticket_quantity_gold"] != DBNull.Value && Convert.ToInt32(reader["Ticket_quantity_gold"]) > 0
                                        ? Convert.ToInt32(reader["Ticket_quantity_gold"])
                                        : 0;

                                    guna2NumericUpDownperticketprice.Value = reader["per_ticket_price"] != DBNull.Value && Convert.ToInt32(reader["per_ticket_price"]) > 0
                                        ? Convert.ToInt32(reader["per_ticket_price"])
                                        : 0;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Event not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.Close();
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"SQL Error: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private bool ValidateEventInputs(
    string eventName,
    DateTime eventDate,
    string eventType,
    TimeSpan startTime,
    TimeSpan endTime,
    int attendeeIdFK,
    string eventDescription,
    int vendorPrice,
    int profitPercent,
    int sponsorIdFK,
    int sponsorPercent,
    int venueIdFK,
    int vendorIdFK,
    int venuePrice,
    int ticketQuantityBronze,
    int ticketQuantityGold,
    int perTicketPrice)
        {
            // Check if required fields are filled
            if (string.IsNullOrWhiteSpace(eventName))
            {
                MessageBox.Show("Event name is required.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(eventType))
            {
                MessageBox.Show("Event type is required.");
                return false;
            }
            if (attendeeIdFK <= 0)
            {
                MessageBox.Show("Attendee ID must be a positive integer.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(eventDescription))
            {
                MessageBox.Show("Event description is required.");
                return false;
            }
            if (sponsorIdFK <= 0)
            {
                MessageBox.Show("Sponsor ID must be a positive integer.");
                return false;
            }
            if (profitPercent < 0 || profitPercent > 100)
            {
                MessageBox.Show("Profit percentage must be between 0 and 100.");
                return false;
            }
            if (sponsorPercent < 0 || sponsorPercent > 100)
            {
                MessageBox.Show("Sponsor percentage must be between 0 and 100.");
                return false;
            }

            // Check if event name and type are alphabetic
            if (!eventName.All(char.IsLetter))
            {
                MessageBox.Show("Event name must contain only alphabetic characters.");
                return false;
            }
            if (!eventType.All(char.IsLetter))
            {
                MessageBox.Show("Event type must contain only alphabetic characters.");
                return false;
            }

            // Check if numeric fields are positive
            if (vendorPrice < 0 || venuePrice < 0 || ticketQuantityBronze < 0 || ticketQuantityGold < 0 || perTicketPrice < 0)
            {
                MessageBox.Show("Numeric values cannot be negative.");
                return false;
            }

            // Ensure venue ID and price are filled in pairs or left empty
            if ((venueIdFK > 0 && venuePrice <= 0) || (venueIdFK <= 0 && venuePrice > 0))
            {
                MessageBox.Show("Venue ID and Venue Price must be filled in pair or left empty.");
                return false;
            }

            // Ensure ticket details are valid
            if ((perTicketPrice > 0 && (ticketQuantityBronze <= 0 && ticketQuantityGold <= 0)) ||
                (perTicketPrice <= 0 && (ticketQuantityBronze > 0 || ticketQuantityGold > 0)))
            {
                MessageBox.Show("Per Ticket Price, Ticket Quantity Bronze, and Ticket Quantity Gold must be filled in pair or left empty.");
                return false;
            }

            // Check time constraints
            if (endTime <= startTime)
            {
                MessageBox.Show("End time must be after start time.");
                return false;
            }

            // All validations passed
            return true;
        }

        private void guna2GradientCircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-IP1VHSS;Initial Catalog=db_EventManagement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            string formattedDate = guna2DateTimePicker1.Value.ToString("yyyy-MM-dd");
            DateTime eventDate = DateTime.Parse(formattedDate);
            if (eventDate <= DateTime.Now)
            {
                MessageBox.Show("The event date must be in the future!", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get input values from the form
            string eventName = guna2TextBoxName.Text;
            string eventType = guna2TextBoxType.Text;
            TimeSpan startTime = new TimeSpan((int)guna2NumericUpDownStartTime_1.Value, (int)guna2NumericUpDownStartTime_2.Value, 0);
            TimeSpan endTime = new TimeSpan((int)guna2NumericUpDownendtime_1.Value, (int)guna2NumericUpDownendtime_2.Value, 0);
            int attendeeIdFK;
            if (string.IsNullOrWhiteSpace(guna2TextBoxAttendeeid.Text) || !guna2TextBoxAttendeeid.Text.All(char.IsDigit))
            {
                MessageBox.Show("Attendee ID is required and must be a numeric value.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Exit the method if the input is invalid
            }
            attendeeIdFK = Convert.ToInt32(guna2TextBoxAttendeeid.Text);
            string eventDescription = guna2TextBoxDescription.Text;
            int vendorPrice = Convert.ToInt32(numericUpDownvendorprice.Value);
            int vendorIdFK;
            if (string.IsNullOrWhiteSpace(guna2TextBoxVendorid.Text) || !guna2TextBoxVendorid.Text.All(char.IsDigit))
            {
                MessageBox.Show("Vendor ID is required and must be a numeric value.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Exit the method if the input is invalid
            }
            vendorIdFK = Convert.ToInt32(guna2TextBoxVendorid.Text);
            int profitPercent = Convert.ToInt32(numericUpDownprofitprice.Value);
            int sponsorIdFK;
            if (string.IsNullOrWhiteSpace(guna2TextBoxSponsorid.Text) || !guna2TextBoxSponsorid.Text.All(char.IsDigit))
            {
                MessageBox.Show("Sponsor ID is required and must be a numeric value.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Exit the method if the input is invalid
            }
            sponsorIdFK = Convert.ToInt32(guna2TextBoxSponsorid.Text);
            int sponsorPercent = Convert.ToInt32(numericUpDownsponsorprice.Value);

            // Set venueIdFK and venuePrice to 0 if not provided
            int venueIdFK = string.IsNullOrWhiteSpace(guna2TextBoxVenueid.Text) ? 0 : Convert.ToInt32(guna2TextBoxVenueid.Text);
            int venuePrice = string.IsNullOrWhiteSpace(numericUpDownVenueprice.Value.ToString()) ? 0 : Convert.ToInt32(numericUpDownVenueprice.Value.ToString());

            // Set ticket details to 0 if not provided
            int ticketQuantityBronze = Convert.ToInt32(guna2NumericUpDownbrozequantity.Value);
            int ticketQuantityGold = Convert.ToInt32(guna2NumericUpDowngoldquantity.Value);
            int perTicketPrice = Convert.ToInt32(guna2NumericUpDownperticketprice.Value);

            float Price = 0;
            // Validate event inputs
            if (!ValidateEventInputs(eventName, eventDate, eventType, startTime, endTime, attendeeIdFK, eventDescription, vendorPrice, profitPercent, sponsorIdFK, sponsorPercent, venueIdFK, vendorIdFK, venuePrice, ticketQuantityBronze, ticketQuantityGold, perTicketPrice))
            {
                return; // Exit if validation fails
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("UpdateEvent", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add input parameters
                        command.Parameters.AddWithValue("@Event_Name", eventName);
                        command.Parameters.AddWithValue("@Event_id", eventid);
                        command.Parameters.AddWithValue("@Event_Date", eventDate);
                        command.Parameters.AddWithValue("@Event_Type", eventType);
                        command.Parameters.AddWithValue("@Start_Time", startTime);
                        command.Parameters.AddWithValue("@End_Time", endTime);
                        command.Parameters.AddWithValue("@Attendee_id_FK", attendeeIdFK);
                        command.Parameters.AddWithValue("@Event_Description", eventDescription);
                        command.Parameters.AddWithValue("@Vendor_Price", vendorPrice);
                        command.Parameters.AddWithValue("@Profit_Percent", profitPercent);
                        command.Parameters.AddWithValue("@User_id", userid); // Replace with the actual User ID if applicable
                        command.Parameters.AddWithValue("@Sponsor_id_FK", sponsorIdFK);
                        command.Parameters.AddWithValue("@Sponsor_Percent", sponsorPercent);
                        command.Parameters.AddWithValue("@Venue_id_FK", venueIdFK);
                        command.Parameters.AddWithValue("@Vendor_id_FK", vendorIdFK);
                        command.Parameters.AddWithValue("@Venue_Price", venuePrice);
                        command.Parameters.AddWithValue("@Ticket_quantity_bronze", ticketQuantityBronze);
                        command.Parameters.AddWithValue("@Ticket_quantity_gold", ticketQuantityGold);
                        command.Parameters.AddWithValue("@per_ticket_price", perTicketPrice);

                        // Add output parameter
                        SqlParameter priceParam = new SqlParameter
                        {
                            ParameterName = "@Price",
                            SqlDbType = SqlDbType.Float,
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(priceParam);

                        // Open connection and execute the command
                        connection.Open();
                        command.ExecuteNonQuery();

                        // Retrieve the output value
                        if (priceParam.Value != DBNull.Value)
                        {
                            Price = Convert.ToSingle(priceParam.Value);
                        }

                        // Display the output in labelPrice
                        labelPrice.Text = Price.ToString("F2");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    } }
  
