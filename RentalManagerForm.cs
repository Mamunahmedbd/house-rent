using DataAccess;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ElegantHousingSystem
{
    public partial class RentalManagerForm : Form
    {
        private DataTable rentalDataTable = null;
        private int currentRentalId = -1;

        public RentalManagerForm()
        {
            InitializeComponent();
            SetupFormTheme();
            LoadRentalData();
        }

        private void SetupFormTheme()
        {
            this.Text = "System Rental Management";
            this.Size = new Size(950, 600);
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

                txtAddress.Enabled = false;
                txtRentValue.Enabled = false;
                cmbStatus.Enabled = false;

                btnAdd.BackColor = Color.FromArgb(229, 231, 235);
                btnUpdate.BackColor = Color.FromArgb(229, 231, 235);
                btnDelete.BackColor = Color.FromArgb(229, 231, 235);
            }
            else
            {
                lblRoleWarning.Visible = false;
            }
        }

        private void LoadRentalData()
        {
            try
            {
                string sql = "SELECT RentalID, Address, RentValue, Status FROM RentalProperties ORDER BY RentalID DESC";
                DataSet ds = SQLHelper.GetData(sql);
                if (ds != null && ds.Tables.Count > 0)
                {
                    rentalDataTable = ds.Tables[0];
                    dgvRentals.DataSource = rentalDataTable;
                    SetupGridStyle();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load rental property records: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupGridStyle()
        {
            if (dgvRentals.Columns.Count == 0) return;

            dgvRentals.EnableHeadersVisualStyles = false;
            dgvRentals.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(31, 41, 55);
            dgvRentals.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvRentals.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            dgvRentals.ColumnHeadersHeight = 35;
            dgvRentals.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            SetColumn("RentalID", "Property ID", 90);
            SetColumn("Address", "Property Address", 240);
            SetColumn("RentValue", "Rent Amount ($)", 110);
            SetColumn("Status", "Status", 110);

            dgvRentals.RowHeadersVisible = false;
            dgvRentals.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(249, 250, 251);
            dgvRentals.DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 231, 255);
            dgvRentals.DefaultCellStyle.SelectionForeColor = Color.FromArgb(49, 46, 129);
            dgvRentals.DefaultCellStyle.Font = new Font("Segoe UI", 9F);

            dgvRentals.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRentals.MultiSelect = false;
            dgvRentals.AllowUserToAddRows = false;
            dgvRentals.ReadOnly = true;
            dgvRentals.GridColor = Color.FromArgb(229, 231, 235);
        }

        private void SetColumn(string name, string header, int width)
        {
            if (dgvRentals.Columns.Contains(name))
            {
                dgvRentals.Columns[name].HeaderText = header;
                dgvRentals.Columns[name].Width = width;
            }
        }

        private void dgvRentals_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvRentals.SelectedRows.Count == 0) return;

            DataGridViewRow row = dgvRentals.SelectedRows[0];

            currentRentalId = Convert.ToInt32(row.Cells["RentalID"].Value);
            txtAddress.Text = CellStr(row, "Address");
            txtRentValue.Text = CellStr(row, "RentValue");
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
            if (rentalDataTable == null) return;

            string s = txtSearch.Text.Trim().Replace("'", "''");
            if (string.IsNullOrEmpty(s))
            {
                rentalDataTable.DefaultView.RowFilter = "";
            }
            else
            {
                rentalDataTable.DefaultView.RowFilter = string.Format(
                    "Address LIKE '%{0}%' OR RentValue LIKE '%{0}%' OR Status LIKE '%{0}%'",
                    s);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string address = txtAddress.Text.Trim();
            string rentValue = txtRentValue.Text.Trim();
            string status = cmbStatus.SelectedItem != null ? cmbStatus.SelectedItem.ToString() : "Available";

            if (string.IsNullOrEmpty(address) || string.IsNullOrEmpty(rentValue))
            {
                MessageBox.Show("Property Address and Rent Rate are mandatory!", "Validation Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string insertSql = string.Format(
                    "INSERT INTO RentalProperties (Address, RentValue, Status) VALUES ('{0}', '{1}', '{2}')",
                    address.Replace("'", "''"),
                    rentValue.Replace("'", "''"),
                    status
                );

                int res = SQLHelper.ExecuteCmd(insertSql);
                if (res > 0)
                {
                    MessageBox.Show("Rental property registered successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputs();
                    LoadRentalData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Registration failed: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvRentals.SelectedRows.Count == 0 || currentRentalId <= 0)
            {
                MessageBox.Show("Please select a rental property from the list to update.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string address = txtAddress.Text.Trim();
            string rentValue = txtRentValue.Text.Trim();
            string status = cmbStatus.SelectedItem != null ? cmbStatus.SelectedItem.ToString() : "Available";

            if (string.IsNullOrEmpty(address) || string.IsNullOrEmpty(rentValue))
            {
                MessageBox.Show("Property Address and Rent Rate are mandatory!", "Validation Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string updateSql = string.Format(
                    "UPDATE RentalProperties SET Address = '{0}', RentValue = '{1}', Status = '{2}' WHERE RentalID = {3}",
                    address.Replace("'", "''"),
                    rentValue.Replace("'", "''"),
                    status,
                    currentRentalId
                );

                int res = SQLHelper.ExecuteCmd(updateSql);
                if (res > 0)
                {
                    MessageBox.Show("Rental property updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputs();
                    LoadRentalData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update failed: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvRentals.SelectedRows.Count == 0 || currentRentalId <= 0)
            {
                MessageBox.Show("Please select a rental property from the list to remove.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string address = CellStr(dgvRentals.SelectedRows[0], "Address");

            DialogResult confirm = MessageBox.Show(
                string.Format("Are you sure you want to permanently remove property at '{0}'?", address),
                "Confirm Removal",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    string deleteSql = string.Format("DELETE FROM RentalProperties WHERE RentalID = {0}", currentRentalId);
                    int res = SQLHelper.ExecuteCmd(deleteSql);
                    if (res > 0)
                    {
                        MessageBox.Show("Rental property record removed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearInputs();
                        LoadRentalData();
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
            txtAddress.Text = "";
            txtRentValue.Text = "";
            cmbStatus.SelectedIndex = -1;
            txtSearch.Text = "";

            currentRentalId = -1;

            if (dgvRentals.SelectedRows.Count > 0)
            {
                dgvRentals.ClearSelection();
            }
        }
    }
}