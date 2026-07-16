using DataAccess;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ElegantHousingSystem
{
    public partial class UserManagerForm : Form
    {
        private DataTable userDataTable = null;

        public UserManagerForm()
        {
            InitializeComponent();
            SetupFormTheme();
            LoadUserData();
        }

        private void SetupFormTheme()
        {
            this.Text = "System User Directory";
            this.Size = new Size(950, 560);
            this.BackColor = Color.FromArgb(243, 244, 246); // Modern Light Gray Background
            this.StartPosition = FormStartPosition.Manual;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Font = new Font("Segoe UI", 9F);
            this.AutoScaleMode = AutoScaleMode.None;
            this.MaximizeBox = false;

            // Display Access Warning if user is not Admin
            if (Form1.CurrentUserRole != "Admin")
            {
                lblRoleWarning.Visible = true;
                btnAdd.Enabled = false;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
                btnClear.Enabled = false;

                txtUsername.Enabled = false;
                txtPassword.Enabled = false;
                cmbRole.Enabled = false;
                txtEmail.Enabled = false;
                txtPhone.Enabled = false;
                
                btnAdd.BackColor = Color.FromArgb(229, 231, 235);
                btnUpdate.BackColor = Color.FromArgb(229, 231, 235);
                btnDelete.BackColor = Color.FromArgb(229, 231, 235);
            }
            else
            {
                lblRoleWarning.Visible = false;
            }
        }

        private void LoadUserData()
        {
            try
            {
                string sql = "SELECT UserID, Username, Password, Role, Email, Phone FROM Users";
                DataSet ds = SQLHelper.GetData(sql);
                if (ds != null && ds.Tables.Count > 0)
                {
                    userDataTable = ds.Tables[0];
                    dgvUsers.DataSource = userDataTable;
                    SetupGridStyle();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load user data: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupGridStyle()
        {
            if (dgvUsers.Columns.Count > 0)
            {
                // Set Header styling
                dgvUsers.EnableHeadersVisualStyles = false;
                dgvUsers.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(31, 41, 55); // Match header panel color
                dgvUsers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvUsers.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
                dgvUsers.ColumnHeadersHeight = 35;
                dgvUsers.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

                // Adjust headers text
                dgvUsers.Columns["UserID"].HeaderText = "User ID";
                dgvUsers.Columns["UserID"].Width = 70;
                dgvUsers.Columns["Username"].HeaderText = "Username";
                dgvUsers.Columns["Username"].Width = 110;
                dgvUsers.Columns["Password"].HeaderText = "Password";
                dgvUsers.Columns["Password"].Width = 90;
                dgvUsers.Columns["Role"].HeaderText = "Role";
                dgvUsers.Columns["Role"].Width = 90;
                dgvUsers.Columns["Email"].HeaderText = "Email";
                dgvUsers.Columns["Email"].Width = 150;
                dgvUsers.Columns["Phone"].HeaderText = "Phone";
                dgvUsers.Columns["Phone"].Width = 110;

                // Row stylings
                dgvUsers.RowHeadersVisible = false;
                dgvUsers.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(249, 250, 251);
                dgvUsers.DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 231, 255); // Indigo selection
                dgvUsers.DefaultCellStyle.SelectionForeColor = Color.FromArgb(49, 46, 129);
                dgvUsers.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
                
                dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvUsers.MultiSelect = false;
                dgvUsers.AllowUserToAddRows = false;
                dgvUsers.ReadOnly = true;
                dgvUsers.GridColor = Color.FromArgb(229, 231, 235);
            }
        }

        private void dgvUsers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvUsers.SelectedRows[0];
                txtUsername.Text = row.Cells["Username"].Value.ToString();
                txtPassword.Text = row.Cells["Password"].Value.ToString();
                cmbRole.SelectedItem = row.Cells["Role"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtPhone.Text = row.Cells["Phone"].Value.ToString();

                // Prevent modifying primary admin credentials to protect access
                if (txtUsername.Text == "admin")
                {
                    txtUsername.Enabled = false;
                    cmbRole.Enabled = false;
                }
                else if (Form1.CurrentUserRole == "Admin")
                {
                    txtUsername.Enabled = true;
                    cmbRole.Enabled = true;
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (userDataTable != null)
            {
                string searchVal = txtSearch.Text.Trim().Replace("'", "''");
                if (string.IsNullOrEmpty(searchVal))
                {
                    userDataTable.DefaultView.RowFilter = "";
                }
                else
                {
                    userDataTable.DefaultView.RowFilter = string.Format(
                        "Username LIKE '%{0}%' OR Role LIKE '%{0}%' OR Email LIKE '%{0}%' OR Phone LIKE '%{0}%'",
                        searchVal
                    );
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string role = cmbRole.SelectedItem != null ? cmbRole.SelectedItem.ToString() : "";
            string email = txtEmail.Text.Trim();
            string phone = txtPhone.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(role))
            {
                MessageBox.Show("Username, Password, and Role are mandatory!", "Validation Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string checkSql = string.Format("SELECT COUNT(*) FROM Users WHERE Username = '{0}'", username.Replace("'", "''"));
                DataSet ds = SQLHelper.GetData(checkSql);
                if (ds != null && ds.Tables.Count > 0 && Convert.ToInt32(ds.Tables[0].Rows[0][0]) > 0)
                {
                    MessageBox.Show("This Username is already registered!", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string insertSql = string.Format(
                    "INSERT INTO Users (Username, Password, Role, Email, Phone) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}')",
                    username.Replace("'", "''"),
                    password.Replace("'", "''"),
                    role,
                    email.Replace("'", "''"),
                    phone.Replace("'", "''")
                );

                int res = SQLHelper.ExecuteCmd(insertSql);
                if (res > 0)
                {
                    MessageBox.Show("User profile registered successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputs();
                    LoadUserData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Registration failed: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a user to update.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string originalUsername = dgvUsers.SelectedRows[0].Cells["Username"].Value.ToString();
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string role = cmbRole.SelectedItem != null ? cmbRole.SelectedItem.ToString() : "";
            string email = txtEmail.Text.Trim();
            string phone = txtPhone.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(role))
            {
                MessageBox.Show("Username, Password, and Role are mandatory!", "Validation Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (originalUsername != username)
                {
                    string checkSql = string.Format("SELECT COUNT(*) FROM Users WHERE Username = '{0}'", username.Replace("'", "''"));
                    DataSet ds = SQLHelper.GetData(checkSql);
                    if (ds != null && ds.Tables.Count > 0 && Convert.ToInt32(ds.Tables[0].Rows[0][0]) > 0)
                    {
                        MessageBox.Show("This Username is already in use by another account!", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                string updateSql = string.Format(
                    "UPDATE Users SET Username = '{0}', Password = '{1}', Role = '{2}', Email = '{3}', Phone = '{4}' WHERE Username = '{5}'",
                    username.Replace("'", "''"),
                    password.Replace("'", "''"),
                    role,
                    email.Replace("'", "''"),
                    phone.Replace("'", "''"),
                    originalUsername.Replace("'", "''")
                );

                int res = SQLHelper.ExecuteCmd(updateSql);
                if (res > 0)
                {
                    MessageBox.Show("User profile updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputs();
                    LoadUserData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update failed: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a user to delete.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string username = dgvUsers.SelectedRows[0].Cells["Username"].Value.ToString();

            if (username == "admin")
            {
                MessageBox.Show("The primary admin account cannot be deleted to prevent system lockouts!", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                string.Format("Are you sure you want to permanently delete user '{0}'?", username),
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
            
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    string deleteSql = string.Format("DELETE FROM Users WHERE Username = '{0}'", username.Replace("'", "''"));
                    int res = SQLHelper.ExecuteCmd(deleteSql);
                    if (res > 0)
                    {
                        MessageBox.Show("User profile deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearInputs();
                        LoadUserData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Deletion failed: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        private void ClearInputs()
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            cmbRole.SelectedIndex = -1;
            txtEmail.Text = "";
            txtPhone.Text = "";
            txtSearch.Text = "";
            
            if (Form1.CurrentUserRole == "Admin")
            {
                txtUsername.Enabled = true;
                cmbRole.Enabled = true;
            }
            if (dgvUsers.SelectedRows.Count > 0)
            {
                dgvUsers.ClearSelection();
            }
        }
    }
}
