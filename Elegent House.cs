using System;
using System.Windows.Forms;

namespace ElegantHousingSystem
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            SetupCustomInteractions();
        }

        #region Modern UI Setup & Event Handlers

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool ReleaseCapture();
        
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        private void SetupCustomInteractions()
        {
            // Wire dragging events to the background form and elements to allow moving the borderless form
            this.MouseDown += DragForm_MouseDown;
            this.pnlCard.MouseDown += DragForm_MouseDown;
            this.lblTitle.MouseDown += DragForm_MouseDown;
            this.lblSubtitle.MouseDown += DragForm_MouseDown;
            this.picLogo.MouseDown += DragForm_MouseDown;
            this.picBanner.MouseDown += DragForm_MouseDown;

            // Highlight border (panel BackColor) when text box gets focus
            txtUser.Enter += (s, e) => pnlUserBorder.BackColor = System.Drawing.Color.FromArgb(59, 130, 246);
            txtUser.Leave += (s, e) => pnlUserBorder.BackColor = System.Drawing.Color.FromArgb(40, 48, 64);

            txtPass.Enter += (s, e) => pnlPassBorder.BackColor = System.Drawing.Color.FromArgb(59, 130, 246);
            txtPass.Leave += (s, e) => pnlPassBorder.BackColor = System.Drawing.Color.FromArgb(40, 48, 64);

            // Load banner image dynamically with robust local path resolution
            try
            {
                string bannerPath = System.IO.Path.Combine(Application.StartupPath, "Resources", "login_banner.png");
                if (System.IO.File.Exists(bannerPath))
                {
                    picBanner.Image = System.Drawing.Image.FromFile(bannerPath);
                }
                else
                {
                    string fallbackPath = System.IO.Path.Combine(Application.StartupPath, "..", "..", "Resources", "login_banner.png");
                    if (System.IO.File.Exists(fallbackPath))
                    {
                        picBanner.Image = System.Drawing.Image.FromFile(fallbackPath);
                    }
                    else
                    {
                        picBanner.BackColor = System.Drawing.Color.FromArgb(24, 28, 36);
                    }
                }
            }
            catch
            {
                picBanner.BackColor = System.Drawing.Color.FromArgb(24, 28, 36);
            }
        }

        private void DragForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnForgetPassword_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Please contact your system administrator to reset your password.",
                "Forget Password Help",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        #endregion

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
                    MessageBox.Show(
                        "Invalid Username or Password!\n\n" +
                        "Default Accounts:\n" +
                        "- Username: admin  | Password: 1234\n" +
                        "- Username: manager1 | Password: 1234\n" +
                        "- Username: user1   | Password: 1234", 
                        "Login Failed", 
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Login failed due to database connection error.\n\n" +
                    "Make sure SQL Server is running.\n\n" +
                    "Error details: " + ex.Message, 
                    "Database Connection Error", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error
                );
            }
        }

        public void ClearFields()
        {
            txtPass.Text = "";
            txtUser.Focus();
        }
    }
}