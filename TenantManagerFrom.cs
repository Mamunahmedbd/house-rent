using DataAccess;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ElegantHousingSystem
{
    public partial class TenantManagerFrom : Form
    {
        private DataTable tenantDataTable = null;
        private int currentTenantId = -1;

        public TenantManagerFrom()
        {
            InitializeComponent();
            SetupFormTheme();
            LoadTenantData();
        }

        private void SetupFormTheme()
        {
            this.Text = "Tenant Directory";
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

                txtFullName.Enabled = false;
                cmbGender.Enabled = false;
                txtPhone.Enabled = false;
                txtEmail.Enabled = false;
                txtIDNumber.Enabled = false;
                txtOccupation.Enabled = false;
                txtAddress.Enabled = false;
                txtEmergency.Enabled = false;
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

        private void LoadTenantData()
        {
            try
            {
                string sql = "SELECT TenantID, FullName, Gender, Phone, Email, IDNumber, Occupation, Address, EmergencyContact, Status, RegisteredDate FROM Tenants ORDER BY TenantID DESC";
                DataSet ds = SQLHelper.GetData(sql);
                if (ds != null && ds.Tables.Count > 0)
                {
                    tenantDataTable = ds.Tables[0];
                    dgvTenants.DataSource = tenantDataTable;
                    SetupGridStyle();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load tenant records: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupGridStyle()
        {
            if (dgvTenants.Columns.Count == 0) return;

            dgvTenants.EnableHeadersVisualStyles = false;
            dgvTenants.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(31, 41, 55);
            dgvTenants.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvTenants.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            dgvTenants.ColumnHeadersHeight = 35;
            dgvTenants.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            SetColumn("TenantID", "Tenant ID", 75);
            SetColumn("FullName", "Full Name", 150);
            SetColumn("Gender", "Gender", 80);
            SetColumn("Phone", "Phone", 115);
            SetColumn("Email", "Email", 160);
            SetColumn("IDNumber", "ID Number", 120);
            SetColumn("Occupation", "Occupation", 110);
            SetColumn("EmergencyContact", "Emergency Contact", 120);
            SetColumn("Status", "Status", 95);

            if (dgvTenants.Columns.Contains("RegisteredDate"))
            {
                dgvTenants.Columns["RegisteredDate"].HeaderText = "Registered";
                dgvTenants.Columns["RegisteredDate"].Width = 110;
                dgvTenants.Columns["RegisteredDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            }

            if (dgvTenants.Columns.Contains("Address"))
            {
                dgvTenants.Columns["Address"].Visible = false;
            }

            dgvTenants.RowHeadersVisible = false;
            dgvTenants.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(249, 250, 251);
            dgvTenants.DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 231, 255);
            dgvTenants.DefaultCellStyle.SelectionForeColor = Color.FromArgb(49, 46, 129);
            dgvTenants.DefaultCellStyle.Font = new Font("Segoe UI", 9F);

            dgvTenants.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTenants.MultiSelect = false;
            dgvTenants.AllowUserToAddRows = false;
            dgvTenants.ReadOnly = true;
            dgvTenants.GridColor = Color.FromArgb(229, 231, 235);
        }

        private void SetColumn(string name, string header, int width)
        {
            if (dgvTenants.Columns.Contains(name))
            {
                dgvTenants.Columns[name].HeaderText = header;
                dgvTenants.Columns[name].Width = width;
            }
        }

        private void dgvTenants_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTenants.SelectedRows.Count == 0) return;

            DataGridViewRow row = dgvTenants.SelectedRows[0];

            currentTenantId = Convert.ToInt32(row.Cells["TenantID"].Value);
            txtFullName.Text = CellStr(row, "FullName");
            SetCombo(cmbGender, CellStr(row, "Gender"));
            txtPhone.Text = CellStr(row, "Phone");
            txtEmail.Text = CellStr(row, "Email");
            txtIDNumber.Text = CellStr(row, "IDNumber");
            txtOccupation.Text = CellStr(row, "Occupation");
            txtAddress.Text = CellStr(row, "Address");
            txtEmergency.Text = CellStr(row, "EmergencyContact");
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
            if (tenantDataTable == null) return;

            string s = txtSearch.Text.Trim().Replace("'", "''");
            if (string.IsNullOrEmpty(s))
            {
                tenantDataTable.DefaultView.RowFilter = "";
            }
            else
            {
                tenantDataTable.DefaultView.RowFilter = string.Format(
                    "FullName LIKE '%{0}%' OR Phone LIKE '%{0}%' OR Email LIKE '%{0}%' OR IDNumber LIKE '%{0}%' OR Occupation LIKE '%{0}%' OR Status LIKE '%{0}%'",
                    s);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string fullName = txtFullName.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string gender = cmbGender.SelectedItem != null ? cmbGender.SelectedItem.ToString() : "";
            string email = txtEmail.Text.Trim();
            string idNumber = txtIDNumber.Text.Trim();
            string occupation = txtOccupation.Text.Trim();
            string address = txtAddress.Text.Trim();
            string emergency = txtEmergency.Text.Trim();
            string status = cmbStatus.SelectedItem != null ? cmbStatus.SelectedItem.ToString() : "Active";

            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("Full Name and Phone Number are mandatory!", "Validation Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (!string.IsNullOrEmpty(idNumber))
                {
                    string checkSql = string.Format("SELECT COUNT(*) FROM Tenants WHERE IDNumber = '{0}'", idNumber.Replace("'", "''"));
                    DataSet ds = SQLHelper.GetData(checkSql);
                    if (ds != null && ds.Tables.Count > 0 && Convert.ToInt32(ds.Tables[0].Rows[0][0]) > 0)
                    {
                        MessageBox.Show("A tenant with this ID Number is already registered!", "Duplicate Record", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                string insertSql = string.Format(
                    "INSERT INTO Tenants (FullName, Gender, Phone, Email, IDNumber, Occupation, Address, EmergencyContact, Status, RegisteredDate) " +
                    "VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', GETDATE())",
                    fullName.Replace("'", "''"),
                    gender,
                    phone.Replace("'", "''"),
                    email.Replace("'", "''"),
                    idNumber.Replace("'", "''"),
                    occupation.Replace("'", "''"),
                    address.Replace("'", "''"),
                    emergency.Replace("'", "''"),
                    status
                );

                int res = SQLHelper.ExecuteCmd(insertSql);
                if (res > 0)
                {
                    MessageBox.Show("Tenant registered successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputs();
                    LoadTenantData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Registration failed: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvTenants.SelectedRows.Count == 0 || currentTenantId <= 0)
            {
                MessageBox.Show("Please select a tenant from the list to update.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string fullName = txtFullName.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string gender = cmbGender.SelectedItem != null ? cmbGender.SelectedItem.ToString() : "";
            string email = txtEmail.Text.Trim();
            string idNumber = txtIDNumber.Text.Trim();
            string occupation = txtOccupation.Text.Trim();
            string address = txtAddress.Text.Trim();
            string emergency = txtEmergency.Text.Trim();
            string status = cmbStatus.SelectedItem != null ? cmbStatus.SelectedItem.ToString() : "Active";

            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("Full Name and Phone Number are mandatory!", "Validation Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (!string.IsNullOrEmpty(idNumber))
                {
                    string checkSql = string.Format("SELECT COUNT(*) FROM Tenants WHERE IDNumber = '{0}' AND TenantID <> {1}", idNumber.Replace("'", "''"), currentTenantId);
                    DataSet ds = SQLHelper.GetData(checkSql);
                    if (ds != null && ds.Tables.Count > 0 && Convert.ToInt32(ds.Tables[0].Rows[0][0]) > 0)
                    {
                        MessageBox.Show("This ID Number is already used by another tenant!", "Duplicate Record", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                string updateSql = string.Format(
                    "UPDATE Tenants SET FullName = '{0}', Gender = '{1}', Phone = '{2}', Email = '{3}', IDNumber = '{4}', Occupation = '{5}', Address = '{6}', EmergencyContact = '{7}', Status = '{8}' WHERE TenantID = {9}",
                    fullName.Replace("'", "''"),
                    gender,
                    phone.Replace("'", "''"),
                    email.Replace("'", "''"),
                    idNumber.Replace("'", "''"),
                    occupation.Replace("'", "''"),
                    address.Replace("'", "''"),
                    emergency.Replace("'", "''"),
                    status,
                    currentTenantId
                );

                int res = SQLHelper.ExecuteCmd(updateSql);
                if (res > 0)
                {
                    MessageBox.Show("Tenant profile updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputs();
                    LoadTenantData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update failed: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvTenants.SelectedRows.Count == 0 || currentTenantId <= 0)
            {
                MessageBox.Show("Please select a tenant from the list to remove.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string name = CellStr(dgvTenants.SelectedRows[0], "FullName");

            DialogResult confirm = MessageBox.Show(
                string.Format("Are you sure you want to permanently remove tenant '{0}'?", name),
                "Confirm Removal",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    string deleteSql = string.Format("DELETE FROM Tenants WHERE TenantID = {0}", currentTenantId);
                    int res = SQLHelper.ExecuteCmd(deleteSql);
                    if (res > 0)
                    {
                        MessageBox.Show("Tenant record removed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearInputs();
                        LoadTenantData();
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
            txtFullName.Text = "";
            cmbGender.SelectedIndex = -1;
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtIDNumber.Text = "";
            txtOccupation.Text = "";
            txtAddress.Text = "";
            txtEmergency.Text = "";
            cmbStatus.SelectedIndex = -1;
            txtSearch.Text = "";

            currentTenantId = -1;

            if (dgvTenants.SelectedRows.Count > 0)
            {
                dgvTenants.ClearSelection();
            }
        }
    }
}
