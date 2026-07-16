using System;
using System.Windows.Forms;

namespace ElegantHousingSystem
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        // Login Button Click Event
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUser.Text.Trim();
            string password = txtPass.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Query matching user from database using SQLHelper
                string sql = string.Format("SELECT Role FROM Users WHERE Username = '{0}' AND Password = '{1}'", username.Replace("'", "''"), password.Replace("'", "''"));
                System.Data.DataSet ds = DataAccess.SQLHelper.GetData(sql);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Form1.CurrentUsername = username;
                    Form1.CurrentUserRole = ds.Tables[0].Rows[0]["Role"].ToString();

                    Form1 mainForm = new Form1();
                    mainForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid Username or Password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Login failed due to database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Exit Button Click Event
        private void btnExitLogin_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}