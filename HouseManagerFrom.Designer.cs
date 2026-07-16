namespace ElegantHousingSystem
{
    partial class HouseManagerFrom
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblRoleWarning = new System.Windows.Forms.Label();
            this.lblSubTitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlInputs = new System.Windows.Forms.Panel();
            this.lblInputHeader = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtIntroduction = new System.Windows.Forms.TextBox();
            this.lblIntroduction = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtDeposit = new System.Windows.Forms.TextBox();
            this.lblDeposit = new System.Windows.Forms.Label();
            this.txtRentPrice = new System.Windows.Forms.TextBox();
            this.lblRentPrice = new System.Windows.Forms.Label();
            this.txtArea = new System.Windows.Forms.TextBox();
            this.lblArea = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.cmbHouseID = new System.Windows.Forms.ComboBox();
            this.lblHouseID = new System.Windows.Forms.Label();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.dgvHouses = new System.Windows.Forms.DataGridView();
            this.pnlHeader.SuspendLayout();
            this.pnlInputs.SuspendLayout();
            this.pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHouses)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.pnlHeader.Controls.Add(this.lblRoleWarning);
            this.pnlHeader.Controls.Add(this.lblSubTitle);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(950, 80);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblRoleWarning
            // 
            this.lblRoleWarning.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRoleWarning.AutoSize = true;
            this.lblRoleWarning.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRoleWarning.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(113)))), ((int)(((byte)(113)))));
            this.lblRoleWarning.Location = new System.Drawing.Point(530, 32);
            this.lblRoleWarning.Name = "lblRoleWarning";
            this.lblRoleWarning.Size = new System.Drawing.Size(388, 17);
            this.lblRoleWarning.TabIndex = 2;
            this.lblRoleWarning.Text = "⚠️ Read Only: Only Admin / Manager can modify house records.";
            this.lblRoleWarning.Visible = false;
            // 
            // lblSubTitle
            // 
            this.lblSubTitle.AutoSize = true;
            this.lblSubTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(175)))));
            this.lblSubTitle.Location = new System.Drawing.Point(26, 47);
            this.lblSubTitle.Name = "lblSubTitle";
            this.lblSubTitle.Size = new System.Drawing.Size(377, 15);
            this.lblSubTitle.TabIndex = 1;
            this.lblSubTitle.Text = "Register, update and track all housing properties, rates, and occupancy.";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(22, 13);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(189, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "House Directory";
            // 
            // pnlInputs
            // 
            this.pnlInputs.AutoScroll = true;
            this.pnlInputs.BackColor = System.Drawing.Color.White;
            this.pnlInputs.Controls.Add(this.lblInputHeader);
            this.pnlInputs.Controls.Add(this.btnClear);
            this.pnlInputs.Controls.Add(this.btnDelete);
            this.pnlInputs.Controls.Add(this.btnUpdate);
            this.pnlInputs.Controls.Add(this.btnAdd);
            this.pnlInputs.Controls.Add(this.txtIntroduction);
            this.pnlInputs.Controls.Add(this.lblIntroduction);
            this.pnlInputs.Controls.Add(this.cmbStatus);
            this.pnlInputs.Controls.Add(this.lblStatus);
            this.pnlInputs.Controls.Add(this.txtDeposit);
            this.pnlInputs.Controls.Add(this.lblDeposit);
            this.pnlInputs.Controls.Add(this.txtRentPrice);
            this.pnlInputs.Controls.Add(this.lblRentPrice);
            this.pnlInputs.Controls.Add(this.txtArea);
            this.pnlInputs.Controls.Add(this.lblArea);
            this.pnlInputs.Controls.Add(this.txtAddress);
            this.pnlInputs.Controls.Add(this.lblAddress);
            this.pnlInputs.Controls.Add(this.cmbHouseID);
            this.pnlInputs.Controls.Add(this.lblHouseID);
            this.pnlInputs.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlInputs.Location = new System.Drawing.Point(0, 80);
            this.pnlInputs.Name = "pnlInputs";
            this.pnlInputs.Size = new System.Drawing.Size(350, 600);
            this.pnlInputs.TabIndex = 1;
            // 
            // lblInputHeader
            // 
            this.lblInputHeader.AutoSize = true;
            this.lblInputHeader.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInputHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblInputHeader.Location = new System.Drawing.Point(24, 20);
            this.lblInputHeader.Name = "lblInputHeader";
            this.lblInputHeader.Size = new System.Drawing.Size(167, 21);
            this.lblInputHeader.TabIndex = 0;
            this.lblInputHeader.Text = "House Profile Details";
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.btnClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClear.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(213)))), ((int)(((byte)(219)))));
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnClear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.btnClear.Location = new System.Drawing.Point(180, 560);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(140, 38);
            this.btnClear.TabIndex = 18;
            this.btnClear.Text = "Clear Fields";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(165)))), ((int)(((byte)(165)))));
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnDelete.Location = new System.Drawing.Point(28, 560);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(140, 38);
            this.btnDelete.TabIndex = 17;
            this.btnDelete.Text = "Remove House";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(253)))), ((int)(((byte)(245)))));
            this.btnUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpdate.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(243)))), ((int)(((byte)(208)))));
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnUpdate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(150)))), ((int)(((byte)(105)))));
            this.btnUpdate.Location = new System.Drawing.Point(180, 510);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(140, 38);
            this.btnUpdate.TabIndex = 16;
            this.btnUpdate.Text = "Save Changes";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(242)))), ((int)(((byte)(255)))));
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(210)))), ((int)(((byte)(254)))));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnAdd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.btnAdd.Location = new System.Drawing.Point(28, 510);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(140, 38);
            this.btnAdd.TabIndex = 15;
            this.btnAdd.Text = "Add House";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtIntroduction
            // 
            this.txtIntroduction.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIntroduction.Location = new System.Drawing.Point(28, 428);
            this.txtIntroduction.Multiline = true;
            this.txtIntroduction.Name = "txtIntroduction";
            this.txtIntroduction.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtIntroduction.Size = new System.Drawing.Size(292, 60);
            this.txtIntroduction.TabIndex = 14;
            // 
            // lblIntroduction
            // 
            this.lblIntroduction.AutoSize = true;
            this.lblIntroduction.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIntroduction.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.lblIntroduction.Location = new System.Drawing.Point(25, 410);
            this.lblIntroduction.Name = "lblIntroduction";
            this.lblIntroduction.Size = new System.Drawing.Size(144, 15);
            this.lblIntroduction.TabIndex = 13;
            this.lblIntroduction.Text = "Introduction / Description:";
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Items.AddRange(new object[] {
            "Available",
            "Rented",
            "Under Maintenance"});
            this.cmbStatus.Location = new System.Drawing.Point(28, 368);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(292, 25);
            this.cmbStatus.TabIndex = 12;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.lblStatus.Location = new System.Drawing.Point(25, 350);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(107, 15);
            this.lblStatus.TabIndex = 11;
            this.lblStatus.Text = "Availability Status:";
            // 
            // txtDeposit
            // 
            this.txtDeposit.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeposit.Location = new System.Drawing.Point(28, 308);
            this.txtDeposit.Name = "txtDeposit";
            this.txtDeposit.Size = new System.Drawing.Size(292, 25);
            this.txtDeposit.TabIndex = 10;
            // 
            // lblDeposit
            // 
            this.lblDeposit.AutoSize = true;
            this.lblDeposit.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDeposit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.lblDeposit.Location = new System.Drawing.Point(25, 290);
            this.lblDeposit.Name = "lblDeposit";
            this.lblDeposit.Size = new System.Drawing.Size(126, 15);
            this.lblDeposit.TabIndex = 9;
            this.lblDeposit.Text = "Deposit Required ($):";
            // 
            // txtRentPrice
            // 
            this.txtRentPrice.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRentPrice.Location = new System.Drawing.Point(28, 248);
            this.txtRentPrice.Name = "txtRentPrice";
            this.txtRentPrice.Size = new System.Drawing.Size(292, 25);
            this.txtRentPrice.TabIndex = 8;
            // 
            // lblRentPrice
            // 
            this.lblRentPrice.AutoSize = true;
            this.lblRentPrice.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRentPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.lblRentPrice.Location = new System.Drawing.Point(25, 230);
            this.lblRentPrice.Name = "lblRentPrice";
            this.lblRentPrice.Size = new System.Drawing.Size(81, 15);
            this.lblRentPrice.TabIndex = 7;
            this.lblRentPrice.Text = "Rent Price ($):";
            // 
            // txtArea
            // 
            this.txtArea.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtArea.Location = new System.Drawing.Point(28, 188);
            this.txtArea.Name = "txtArea";
            this.txtArea.Size = new System.Drawing.Size(292, 25);
            this.txtArea.TabIndex = 6;
            // 
            // lblArea
            // 
            this.lblArea.AutoSize = true;
            this.lblArea.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblArea.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.lblArea.Location = new System.Drawing.Point(25, 170);
            this.lblArea.Name = "lblArea";
            this.lblArea.Size = new System.Drawing.Size(107, 15);
            this.lblArea.TabIndex = 5;
            this.lblArea.Text = "House Area / Size:";
            // 
            // txtAddress
            // 
            this.txtAddress.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress.Location = new System.Drawing.Point(28, 128);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(292, 25);
            this.txtAddress.TabIndex = 4;
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.lblAddress.Location = new System.Drawing.Point(25, 110);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(103, 15);
            this.lblAddress.TabIndex = 3;
            this.lblAddress.Text = "Property Address:";
            // 
            // cmbHouseID
            // 
            this.cmbHouseID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHouseID.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbHouseID.FormattingEnabled = true;
            this.cmbHouseID.Location = new System.Drawing.Point(28, 68);
            this.cmbHouseID.Name = "cmbHouseID";
            this.cmbHouseID.Size = new System.Drawing.Size(292, 25);
            this.cmbHouseID.TabIndex = 2;
            // 
            // lblHouseID
            // 
            this.lblHouseID.AutoSize = true;
            this.lblHouseID.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHouseID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.lblHouseID.Location = new System.Drawing.Point(25, 50);
            this.lblHouseID.Name = "lblHouseID";
            this.lblHouseID.Size = new System.Drawing.Size(117, 15);
            this.lblHouseID.TabIndex = 1;
            this.lblHouseID.Text = "House ID (Required):";
            // 
            // pnlGrid
            // 
            this.pnlGrid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.pnlGrid.Controls.Add(this.lblSearch);
            this.pnlGrid.Controls.Add(this.txtSearch);
            this.pnlGrid.Controls.Add(this.dgvHouses);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Location = new System.Drawing.Point(350, 80);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Padding = new System.Windows.Forms.Padding(20);
            this.pnlGrid.Size = new System.Drawing.Size(600, 600);
            this.pnlGrid.TabIndex = 2;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.lblSearch.Location = new System.Drawing.Point(20, 22);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(91, 17);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "🔍 Search List:";
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(120, 19);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(460, 25);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // dgvHouses
            // 
            this.dgvHouses.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvHouses.BackgroundColor = System.Drawing.Color.White;
            this.dgvHouses.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvHouses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHouses.Location = new System.Drawing.Point(23, 60);
            this.dgvHouses.Name = "dgvHouses";
            this.dgvHouses.Size = new System.Drawing.Size(557, 517);
            this.dgvHouses.TabIndex = 2;
            this.dgvHouses.SelectionChanged += new System.EventHandler(this.dgvHouses_SelectionChanged);
            // 
            // HouseManagerFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 680);
            this.Controls.Add(this.pnlGrid);
            this.Controls.Add(this.pnlInputs);
            this.Controls.Add(this.pnlHeader);
            this.Name = "HouseManagerFrom";
            this.Text = "House Directory Setup";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlInputs.ResumeLayout(false);
            this.pnlInputs.PerformLayout();
            this.pnlGrid.ResumeLayout(false);
            this.pnlGrid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHouses)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubTitle;
        private System.Windows.Forms.Label lblRoleWarning;
        private System.Windows.Forms.Panel pnlInputs;
        private System.Windows.Forms.Label lblInputHeader;
        private System.Windows.Forms.Label lblHouseID;
        private System.Windows.Forms.ComboBox cmbHouseID;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label lblArea;
        private System.Windows.Forms.TextBox txtArea;
        private System.Windows.Forms.Label lblRentPrice;
        private System.Windows.Forms.TextBox txtRentPrice;
        private System.Windows.Forms.Label lblDeposit;
        private System.Windows.Forms.TextBox txtDeposit;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label lblIntroduction;
        private System.Windows.Forms.TextBox txtIntroduction;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Panel pnlGrid;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.DataGridView dgvHouses;
    }
}