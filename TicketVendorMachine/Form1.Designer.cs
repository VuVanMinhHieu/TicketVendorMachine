namespace TicketVendorMachine
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblDestination = new System.Windows.Forms.Label();
            this.cmbDestination = new System.Windows.Forms.ComboBox();
            this.lblPassengerType = new System.Windows.Forms.Label();
            this.cmbPassengerType = new System.Windows.Forms.ComboBox();
            this.lblPaymentMethod = new System.Windows.Forms.Label();
            this.cmbPaymentMethod = new System.Windows.Forms.ComboBox();
            this.lblFare = new System.Windows.Forms.Label();
            this.btnBuyTicket = new System.Windows.Forms.Button();
            this.dgvTickets = new System.Windows.Forms.DataGridView();
            this.lblReport = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTickets)).BeginInit();
            this.SuspendLayout();

            // lblDestination
            this.lblDestination.Text = "Destination:";
            this.lblDestination.Location = new System.Drawing.Point(30, 20);
            this.lblDestination.Size = new System.Drawing.Size(100, 23);

            // cmbDestination
            this.cmbDestination.Location = new System.Drawing.Point(140, 17);
            this.cmbDestination.Size = new System.Drawing.Size(200, 23);
            this.cmbDestination.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDestination.SelectedIndexChanged += new System.EventHandler(this.cmbDestination_SelectedIndexChanged);

            // lblPassengerType
            this.lblPassengerType.Text = "Passenger type:";
            this.lblPassengerType.Location = new System.Drawing.Point(30, 60);
            this.lblPassengerType.Size = new System.Drawing.Size(110, 23);

            // cmbPassengerType
            this.cmbPassengerType.Location = new System.Drawing.Point(140, 57);
            this.cmbPassengerType.Size = new System.Drawing.Size(200, 23);
            this.cmbPassengerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPassengerType.SelectedIndexChanged += new System.EventHandler(this.cmbPassengerType_SelectedIndexChanged);

            // lblPaymentMethod
            this.lblPaymentMethod.Text = "Payment method:";
            this.lblPaymentMethod.Location = new System.Drawing.Point(30, 100);
            this.lblPaymentMethod.Size = new System.Drawing.Size(110, 23);

            // cmbPaymentMethod
            this.cmbPaymentMethod.Location = new System.Drawing.Point(140, 97);
            this.cmbPaymentMethod.Size = new System.Drawing.Size(200, 23);
            this.cmbPaymentMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            // lblFare
            this.lblFare.Text = "Fare: -- VND";
            this.lblFare.Location = new System.Drawing.Point(30, 140);
            this.lblFare.Size = new System.Drawing.Size(300, 23);
            this.lblFare.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblFare.ForeColor = System.Drawing.Color.DarkBlue;

            // btnBuyTicket
            this.btnBuyTicket.Text = "Buy Ticket";
            this.btnBuyTicket.Location = new System.Drawing.Point(140, 175);
            this.btnBuyTicket.Size = new System.Drawing.Size(200, 35);
            this.btnBuyTicket.BackColor = System.Drawing.Color.SteelBlue;
            this.btnBuyTicket.ForeColor = System.Drawing.Color.White;
            this.btnBuyTicket.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnBuyTicket.Click += new System.EventHandler(this.btnBuyTicket_Click);

            // lblReport
            this.lblReport.Text = "Issued tickets report:";
            this.lblReport.Location = new System.Drawing.Point(30, 225);
            this.lblReport.Size = new System.Drawing.Size(200, 23);
            this.lblReport.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            // dgvTickets
            this.dgvTickets.Location = new System.Drawing.Point(30, 250);
            this.dgvTickets.Size = new System.Drawing.Size(740, 280);
            this.dgvTickets.ReadOnly = true;
            this.dgvTickets.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTickets.AllowUserToAddRows = false;
            this.dgvTickets.BackgroundColor = System.Drawing.Color.White;

            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 560);
            this.Text = "Ticket Vendor Machine — Demo";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Controls.Add(this.lblDestination);
            this.Controls.Add(this.cmbDestination);
            this.Controls.Add(this.lblPassengerType);
            this.Controls.Add(this.cmbPassengerType);
            this.Controls.Add(this.lblPaymentMethod);
            this.Controls.Add(this.cmbPaymentMethod);
            this.Controls.Add(this.lblFare);
            this.Controls.Add(this.btnBuyTicket);
            this.Controls.Add(this.lblReport);
            this.Controls.Add(this.dgvTickets);

            ((System.ComponentModel.ISupportInitialize)(this.dgvTickets)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblDestination;
        private System.Windows.Forms.ComboBox cmbDestination;
        private System.Windows.Forms.Label lblPassengerType;
        private System.Windows.Forms.ComboBox cmbPassengerType;
        private System.Windows.Forms.Label lblPaymentMethod;
        private System.Windows.Forms.ComboBox cmbPaymentMethod;
        private System.Windows.Forms.Label lblFare;
        private System.Windows.Forms.Button btnBuyTicket;
        private System.Windows.Forms.DataGridView dgvTickets;
        private System.Windows.Forms.Label lblReport;
    }
}