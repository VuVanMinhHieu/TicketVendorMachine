using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TicketVendorMachine
{
    public partial class Form1 : Form
    {
        private string connStr = "Server=(local)\\SQLEXPRESS;Database=TicketVendorDB;Integrated Security=True;";
        private decimal fareAmount = 0;

        public Form1()
        {
            InitializeComponent();
            LoadDestinations();
            LoadPassengerTypes();
            LoadPaymentMethods();
        }

        private void LoadDestinations()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string query = "SELECT DestinationID, StationName FROM Destinations WHERE StationName != 'Ben Thanh'";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    cmbDestination.DisplayMember = "StationName";
                    cmbDestination.ValueMember = "DestinationID";
                    cmbDestination.DataSource = dt;
                    cmbDestination.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading destinations: " + ex.Message);
            }
        }

        private void LoadPassengerTypes()
        {
            cmbPassengerType.Items.AddRange(new string[] { "Regular", "Student", "Elderly" });
            cmbPassengerType.SelectedIndex = 0;
        }

        private void LoadPaymentMethods()
        {
            cmbPaymentMethod.Items.AddRange(new string[] { "Credit Card", "Momo", "VNPay", "ZaloPay" });
            cmbPaymentMethod.SelectedIndex = 0;
        }

        private void cmbDestination_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDestination.SelectedValue == null) return;

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string query = "SELECT FareAmount FROM Fares WHERE OriginID = 1 AND DestinationID = @DestID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@DestID", cmbDestination.SelectedValue);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        fareAmount = Convert.ToDecimal(result);
                        ApplyDiscount();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching fare: " + ex.Message);
            }
        }

        private void ApplyDiscount()
        {
            decimal discount = 0;
            switch (cmbPassengerType.SelectedItem?.ToString())
            {
                case "Student": discount = 0.50m; break;
                case "Elderly": discount = 1.00m; break;
                default: discount = 0.00m; break;
            }
            decimal baseFare = fareAmount;
            decimal finalFare = baseFare - (baseFare * discount);
            lblFare.Text = $"Fare: {finalFare:N0} VND";
            fareAmount = finalFare;
        }

        private void cmbPassengerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDestination.SelectedIndex >= 0)
                cmbDestination_SelectedIndexChanged(sender, e);
        }

        private void btnBuyTicket_Click(object sender, EventArgs e)
        {
            if (cmbDestination.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a destination.", "Validation");
                return;
            }
            if (fareAmount <= 0)
            {
                MessageBox.Show("Invalid fare amount.", "Validation");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();
                    try
                    {
                        // Insert payment
                        string payQuery = @"INSERT INTO Payments (PaymentMethod, Amount, Status, PaymentTime)
                                            OUTPUT INSERTED.PaymentID
                                            VALUES (@Method, @Amount, 'Approved', GETDATE())";
                        SqlCommand payCmd = new SqlCommand(payQuery, conn, transaction);
                        payCmd.Parameters.AddWithValue("@Method", cmbPaymentMethod.SelectedItem.ToString());
                        payCmd.Parameters.AddWithValue("@Amount", fareAmount);
                        int paymentID = (int)payCmd.ExecuteScalar();

                        // Generate ticket code
                        string ticketCode = $"TK-{DateTime.Now:yyyyMMdd}-{new Random().Next(1000, 9999)}";

                        // Insert ticket
                        string ticketQuery = @"INSERT INTO Tickets
                                               (TicketCode, OriginID, DestinationID, PassengerType, FareAmount, PaymentID, IssueDateTime, IsValid)
                                               OUTPUT INSERTED.TicketID
                                               VALUES (@Code, 1, @DestID, @PaxType, @Fare, @PayID, GETDATE(), 1)";
                        SqlCommand ticketCmd = new SqlCommand(ticketQuery, conn, transaction);
                        ticketCmd.Parameters.AddWithValue("@Code", ticketCode);
                        ticketCmd.Parameters.AddWithValue("@DestID", cmbDestination.SelectedValue);
                        ticketCmd.Parameters.AddWithValue("@PaxType", cmbPassengerType.SelectedItem.ToString());
                        ticketCmd.Parameters.AddWithValue("@Fare", fareAmount);
                        ticketCmd.Parameters.AddWithValue("@PayID", paymentID);
                        int ticketID = (int)ticketCmd.ExecuteScalar();

                        // Log transaction
                        string logQuery = @"INSERT INTO TransactionLogs (TicketID, MachineID, Action, LogTime)
                                            VALUES (@TID, 'TVM-001', 'Ticket issued', GETDATE())";
                        SqlCommand logCmd = new SqlCommand(logQuery, conn, transaction);
                        logCmd.Parameters.AddWithValue("@TID", ticketID);
                        logCmd.ExecuteNonQuery();

                        transaction.Commit();

                        MessageBox.Show($"Ticket issued!\nTicket Code: {ticketCode}",
                                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadTicketReport();
                        ClearForm();
                    }
                    catch
                    {
                        transaction.Rollback();
                        MessageBox.Show("Transaction failed. Payment reversed.", "Error",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error: " + ex.Message);
            }
        }

        private void LoadTicketReport()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string query = @"SELECT
                                        t.TicketCode    AS [Ticket Code],
                                        o.StationName   AS [From],
                                        d.StationName   AS [To],
                                        t.PassengerType AS [Passenger Type],
                                        t.FareAmount    AS [Fare (VND)],
                                        p.PaymentMethod AS [Payment],
                                        t.IssueDateTime AS [Issued At]
                                     FROM Tickets t
                                     JOIN Destinations o ON t.OriginID      = o.DestinationID
                                     JOIN Destinations d ON t.DestinationID = d.DestinationID
                                     JOIN Payments     p ON t.PaymentID     = p.PaymentID
                                     ORDER BY t.IssueDateTime DESC";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvTickets.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading report: " + ex.Message);
            }
        }

        private void ClearForm()
        {
            cmbDestination.SelectedIndex = -1;
            cmbPassengerType.SelectedIndex = 0;
            cmbPaymentMethod.SelectedIndex = 0;
            lblFare.Text = "Fare: -- VND";
            fareAmount = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadTicketReport();
        }
    }
}