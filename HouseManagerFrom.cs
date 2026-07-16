using DataAccess;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ElegantHousingSystem
{
    public partial class HouseManagerFrom : Form
    {
        private DataTable houseDataTable = null;

        public HouseManagerFrom()
        {
            InitializeComponent();
            SetupFormTheme();
            LoadHouseData();
        }

        private void SetupFormTheme()
        {
            this.Text = "House Directory Setup";
            this.Size = new Size(950, 680);
            this.BackColor = Color.FromArgb(243, 244, 246);
            this.StartPosition = FormStartPosition.Manual;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Font = new Font("Segoe UI", 9F);
            this.AutoScaleMode = AutoScaleMode.None;
            this.MaximizeBox = false;

            bool canModify = (Form1.CurrentUserRole == "Admin" || Form1.CurrentUserRole == "Manager");

            if (!canModify)
            {
                lblRoleWarning.Visible = true;
                btnAdd.Enabled = false;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
                btnClear.Enabled = false;

                txtHouseID.Enabled = false;
                txtAddress.Enabled = false;
                txtArea.Enabled = false;
                txtRentPrice.Enabled = false;
                txtDeposit.Enabled = false;
                cmbStatus.Enabled = false;
                txtIntroduction.Enabled = false;

                btnAdd.BackColor = Color.FromArgb(229, 231, 235);
                btnUpdate.BackColor = Color.FromArgb(229, 231, 235);
                btnDelete.BackColor = Color.FromArgb(229, 231, 235);
            }
            else
            {
                lblRoleWarning.Visible = false;
            }
        }

        private void LoadHouseData()
        {
            try
            {
                string sql = "SELECT HouseID, COALESCE(Address, HouseAddress) AS Address, COALESCE(Area, HouseArea) AS Area, RentPrice, Deposit, Status, Introduction FROM HouseInfo ORDER BY HouseID ASC";
                DataSet ds = SQLHelper.GetData(sql);
                if (ds != null && ds.Tables.Count > 0)
                {
                    houseDataTable = ds.Tables[0];
                    dgvHouses.DataSource = houseDataTable;
                    SetupGridStyle();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load housing records: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupGridStyle()
        {
            if (dgvHouses.Columns.Count == 0) return;

            dgvHouses.EnableHeadersVisualStyles = false;
            dgvHouses.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(31, 41, 55);
            dgvHouses.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvHouses.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            dgvHouses.ColumnHeadersHeight = 35;
            dgvHouses.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            SetColumn("HouseID", "House ID", 90);
            SetColumn("Address", "Property Address", 200);
            SetColumn("Area", "Area / Size", 100);
            SetColumn("RentPrice", "Rent Price ($)", 90);
            SetColumn("Deposit", "Deposit ($)", 90);
            SetColumn("Status", "Status", 100);
            SetColumn("Introduction", "Description", 150);

            dgvHouses.RowHeadersVisible = false;
            dgvHouses.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(249, 250, 251);
            dgvHouses.DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 231, 255);
            dgvHouses.DefaultCellStyle.SelectionForeColor = Color.FromArgb(49, 46, 129);
            dgvHouses.DefaultCellStyle.Font = new Font("Segoe UI", 9F);

            dgvHouses.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHouses.MultiSelect = false;
            dgvHouses.AllowUserToAddRows = false;
            dgvHouses.ReadOnly = true;
            dgvHouses.GridColor = Color.FromArgb(229, 231, 235);
        }

        private void SetColumn(string name, string header, int width)
        {
            if (dgvHouses.Columns.Contains(name))
            {
                dgvHouses.Columns[name].HeaderText = header;
                dgvHouses.Columns[name].Width = width;
            }
        }

        private void dgvHouses_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvHouses.SelectedRows.Count == 0) return;

            DataGridViewRow row = dgvHouses.SelectedRows[0];

            txtHouseID.Text = CellStr(row, "HouseID");
            txtHouseID.Enabled = false; // Primary key cannot be changed on update

            txtAddress.Text = CellStr(row, "Address");
            txtArea.Text = CellStr(row, "Area");
            txtRentPrice.Text = CellStr(row, "RentPrice");
            txtDeposit.Text = CellStr(row, "Deposit");
            txtIntroduction.Text = CellStr(row, "Introduction");
            SetCombo(cmbStatus, CellStr(row, "Status"));
        }

        private static string CellStr(DataGridViewRow row, string col)
        {
            if (row.DataGridView == null || !row.DataGridView.Columns.Contains(col) || row.Cells[col].Value == null || row.Cells[col].Value == DBNull.Value)
                return "";
            return row.Cells[col].Value.ToString();
        }

        private static void SetCombo(ComboBox cmb, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                cmb.SelectedIndex = -1;
                return;
            }
            cmb.SelectedIndex = cmb.Items.IndexOf(value);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (houseDataTable == null) return;

            string s = txtSearch.Text.Trim().Replace("'", "''");
            if (string.IsNullOrEmpty(s))
            {
                houseDataTable.DefaultView.RowFilter = "";
            }
            else
            {
                houseDataTable.DefaultView.RowFilter = string.Format(
                    "HouseID LIKE '%{0}%' OR Address LIKE '%{0}%' OR Area LIKE '%{0}%' OR RentPrice LIKE '%{0}%' OR Status LIKE '%{0}%'",
                    s);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string houseID = txtHouseID.Text.Trim();
            string address = txtAddress.Text.Trim();
            string area = txtArea.Text.Trim();
            string rentPrice = txtRentPrice.Text.Trim();
            string deposit = txtDeposit.Text.Trim();
            string status = cmbStatus.SelectedItem != null ? cmbStatus.SelectedItem.ToString() : "Available";
            string introduction = txtIntroduction.Text.Trim();

            if (string.IsNullOrEmpty(houseID) || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(rentPrice))
            {
                MessageBox.Show("House ID, Address, and Rent Price are mandatory!", "Validation Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Check for duplicate HouseID
                string checkSql = string.Format("SELECT COUNT(*) FROM HouseInfo WHERE HouseID = '{0}'", houseID.Replace("'", "''"));
                DataSet ds = SQLHelper.GetData(checkSql);
                if (ds != null && ds.Tables.Count > 0 && Convert.ToInt32(ds.Tables[0].Rows[0][0]) > 0)
                {
                    MessageBox.Show("This House ID is already registered!", "Duplicate Key Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string isVacant = (status == "Available") ? "Yes" : "No";
                string insertSql = string.Format(
                    "INSERT INTO HouseInfo (HouseID, Address, HouseAddress, Area, HouseArea, RentPrice, Deposit, IsVacant, Status, Introduction) " +
                    "VALUES ('{0}', '{1}', '{1}', '{2}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}')",
                    houseID.Replace("'", "''"),
                    address.Replace("'", "''"),
                    area.Replace("'", "''"),
                    rentPrice.Replace("'", "''"),
                    deposit.Replace("'", "''"),
                    isVacant,
                    status,
                    introduction.Replace("'", "''")
                );

                int res = SQLHelper.ExecuteCmd(insertSql);
                if (res > 0)
                {
                    MessageBox.Show("House record added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputs();
                    LoadHouseData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Addition failed: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvHouses.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a house from the list to update.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string houseID = txtHouseID.Text.Trim();
            string address = txtAddress.Text.Trim();
            string area = txtArea.Text.Trim();
            string rentPrice = txtRentPrice.Text.Trim();
            string deposit = txtDeposit.Text.Trim();
            string status = cmbStatus.SelectedItem != null ? cmbStatus.SelectedItem.ToString() : "Available";
            string introduction = txtIntroduction.Text.Trim();

            if (string.IsNullOrEmpty(address) || string.IsNullOrEmpty(rentPrice))
            {
                MessageBox.Show("Address and Rent Price are mandatory!", "Validation Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string isVacant = (status == "Available") ? "Yes" : "No";
                string updateSql = string.Format(
                    "UPDATE HouseInfo SET Address = '{0}', HouseAddress = '{0}', Area = '{1}', HouseArea = '{1}', RentPrice = '{2}', Deposit = '{3}', IsVacant = '{4}', Status = '{5}', Introduction = '{6}' " +
                    "WHERE HouseID = '{7}'",
                    address.Replace("'", "''"),
                    area.Replace("'", "''"),
                    rentPrice.Replace("'", "''"),
                    deposit.Replace("'", "''"),
                    isVacant,
                    status,
                    introduction.Replace("'", "''"),
                    houseID.Replace("'", "''")
                );

                int res = SQLHelper.ExecuteCmd(updateSql);
                if (res > 0)
                {
                    MessageBox.Show("House record updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputs();
                    LoadHouseData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update failed: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvHouses.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a house from the list to remove.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string houseID = txtHouseID.Text.Trim();

            DialogResult confirm = MessageBox.Show(
                string.Format("Are you sure you want to permanently remove house '{0}'?", houseID),
                "Confirm Removal",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    string deleteSql = string.Format("DELETE FROM HouseInfo WHERE HouseID = '{0}'", houseID.Replace("'", "''"));
                    int res = SQLHelper.ExecuteCmd(deleteSql);
                    if (res > 0)
                    {
                        MessageBox.Show("House record removed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearInputs();
                        LoadHouseData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Removal failed: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        private void ClearInputs()
        {
            txtHouseID.Text = "";
            txtAddress.Text = "";
            txtArea.Text = "";
            txtRentPrice.Text = "";
            txtDeposit.Text = "";
            cmbStatus.SelectedIndex = -1;
            txtIntroduction.Text = "";
            txtSearch.Text = "";

            if (Form1.CurrentUserRole == "Admin" || Form1.CurrentUserRole == "Manager")
            {
                txtHouseID.Enabled = true; // Allow entering ID for new house
            }

            if (dgvHouses.SelectedRows.Count > 0)
            {
                dgvHouses.ClearSelection();
            }
        }

        private void HouseManagerFrom_Load(object sender, EventArgs e)
        {
            // Handled in Constructor
        }
    }
}