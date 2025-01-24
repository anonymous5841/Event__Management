using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project
{
    public partial class FormEvent : System.Windows.Forms.Form
    {
        private int userid;
        public FormEvent(int user_id)
        {
            InitializeComponent();
            userid = user_id;
        }

        private void guna2GradientCircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
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


        private void guna2GradientButtonCreate_Click(object sender, EventArgs e)
        {
            // Connection string for your database
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
            int vendorPrice = Convert.ToInt32(numericUpDownVendorprice.Value);
            int vendorIdFK;
            if (string.IsNullOrWhiteSpace(guna2TextBoxVendorId.Text) || !guna2TextBoxVendorId.Text.All(char.IsDigit))
            {
                MessageBox.Show("Vendor ID is required and must be a numeric value.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Exit the method if the input is invalid
            }
            vendorIdFK = Convert.ToInt32(guna2TextBoxVendorId.Text);
            int profitPercent = Convert.ToInt32(numericUpDownProfirprice.Value);
            int sponsorIdFK;
            if (string.IsNullOrWhiteSpace(guna2TextBoxSponsorid.Text) || !guna2TextBoxSponsorid.Text.All(char.IsDigit))
            {
                MessageBox.Show("Sponsor ID is required and must be a numeric value.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Exit the method if the input is invalid
            }
            sponsorIdFK = Convert.ToInt32(guna2TextBoxSponsorid.Text);
            int sponsorPercent = Convert.ToInt32(numericUpDownSponsorPercent.Value);

            // Set venueIdFK and venuePrice to 0 if not provided
            int venueIdFK = string.IsNullOrWhiteSpace(guna2TextBoxVenueid.Text) ? 0 : Convert.ToInt32(guna2TextBoxVenueid.Text);
            int venuePrice = string.IsNullOrWhiteSpace(numericUpDownVenuePrice.Value.ToString()) ? 0 : Convert.ToInt32(numericUpDownVenuePrice.Value.ToString());

            // Set ticket details to 0 if not provided
            int ticketQuantityBronze = Convert.ToInt32(guna2NumericUpDownBronzequantity.Value);
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
                    using (SqlCommand command = new SqlCommand("RegisterEvent", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add input parameters
                        command.Parameters.AddWithValue("@Event_Name", eventName);
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
    } 
    }
