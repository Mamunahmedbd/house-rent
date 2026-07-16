using DataAccess;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ElegantHousingSystem
{
    public partial class UserManagerForm : Form
    {
        public UserManagerForm()
        {
            InitializeComponent();
            SetupFormTheme();
            LoadUserData();
        }

        private void SetupFormTheme()
        {
            this.Text = "User Management System";
            this.Size = new Size(850, 520);
            this.BackColor = Color.FromArgb(255, 240, 245); // Lavender Blush theme
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Security check: Disable modification buttons if the logged-in user is not an Admin
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
                    dgvUsers.DataSource = ds.Tables[0];
                    SetupGridStyle();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load user data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupGridStyle()
        {
            if (dgvUsers.Columns.Count > 0)
            {
                dgvUsers.Columns["UserID"].HeaderText = "User ID";
                dgvUsers.Columns["UserID"].Width = 80;
                dgvUsers.Columns["Username"].HeaderText = "Username";
                dgvUsers.Columns["Username"].Width = 120;
                dgvUsers.Columns["Password"].HeaderText = "Password";
                dgvUsers.Columns["Password"].Width = 100;
                dgvUsers.Columns["Role"].HeaderText = "Role";
                dgvUsers.Columns["Role"].Width = 100;
                dgvUsers.Columns["Email"].HeaderText = "Email";
                dgvUsers.Columns["Email"].Width = 180;
                dgvUsers.Columns["Phone"].HeaderText = "Phone";
                dgvUsers.Columns["Phone"].Width = 120;

                dgvUsers.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(252, 230, 235);
                dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvUsers.MultiSelect = false;
                dgvUsers.AllowUserToAddRows = false;
                dgvUsers.ReadOnly = true;
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

                // Prevent editing admin username to avoid lockouts
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string role = cmbRole.SelectedItem != null ? cmbRole.SelectedItem.ToString() : "";
            string email = txtEmail.Text.Trim();
            string phone = txtPhone.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(role))
            {
                MessageBox.Show("Username, Password, and Role are required!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Check if user already exists
                string checkSql = string.Format("SELECT COUNT(*) FROM Users WHERE Username = '{0}'", username.Replace("'", "''"));
                DataSet ds = SQLHelper.GetData(checkSql);
                if (ds != null && ds.Tables.Count > 0 && Convert.ToInt32(ds.Tables[0].Rows[0][0]) > 0)
                {
                    MessageBox.Show("Username already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                int result = SQLHelper.ExecuteCmd(insertSql);
                if (result > 0)
                {
                    MessageBox.Show("User added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputs();
                    LoadUserData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to add user: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a user to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                MessageBox.Show("Username, Password, and Role are required!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // If username is changing, ensure new username is unique
                if (originalUsername != username)
                {
                    string checkSql = string.Format("SELECT COUNT(*) FROM Users WHERE Username = '{0}'", username.Replace("'", "''"));
                    DataSet ds = SQLHelper.GetData(checkSql);
                    if (ds != null && ds.Tables.Count > 0 && Convert.ToInt32(ds.Tables[0].Rows[0][0]) > 0)
                    {
                        MessageBox.Show("New username already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                int result = SQLHelper.ExecuteCmd(updateSql);
                if (result > 0)
                {
                    MessageBox.Show("User updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputs();
                    LoadUserData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to update user: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a user to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string username = dgvUsers.SelectedRows[0].Cells["Username"].Value.ToString();

            if (username == "admin")
            {
                MessageBox.Show("The primary admin user cannot be deleted!", "Forbidden", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show($"Are you sure you want to delete user '{username}'?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    string deleteSql = string.Format("DELETE FROM Users WHERE Username = '{0}'", username.Replace("'", "''"));
                    int result = SQLHelper.ExecuteCmd(deleteSql);
                    if (result > 0)
                    {
                        MessageBox.Show("User deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearInputs();
                        LoadUserData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to delete user: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
