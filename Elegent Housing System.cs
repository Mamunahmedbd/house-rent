using System;
using System.Windows.Forms;

namespace ElegantHousingSystem
{
    public partial class Form1 : Form
    {
        public static string CurrentUsername = "";
        public static string CurrentUserRole = "";
        private Form activeChildForm = null;
        private bool isLoggingOut = false;

        public Form1()
        {
            InitializeComponent();
            this.pnlContent.SizeChanged += (s, e) => {
                if (activeChildForm != null)
                {
                    CenterChildForm(activeChildForm);
                }
            };
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                isLoggingOut = true;

                Form loginForm = Application.OpenForms["Form2"];
                if (loginForm != null)
                {
                    Form2 lf = loginForm as Form2;
                    if (lf != null)
                    {
                        lf.ClearFields();
                    }
                    loginForm.Show();
                }
                else
                {
                    Form2 newLoginForm = new Form2();
                    newLoginForm.Show();
                }

                this.Close();
            }
        }

        private void CenterChildForm(Form childForm)
        {
            if (childForm == null || childForm.IsDisposed) return;
            
            // Center the form inside pnlContent
            // Use Math.Max(0, ...) to ensure coordinates never go negative, allowing full scrolling
            childForm.Location = new System.Drawing.Point(
                Math.Max(0, (pnlContent.Width - childForm.Width) / 2),
                Math.Max(0, (pnlContent.Height - childForm.Height) / 2)
            );
        }

        private void ShowFormInContentPanel(Form childForm)
        {
            // Clear current controls in content panel
            pnlContent.Controls.Clear();
            
            // Set panel background and enable AutoScroll for small screen compatibility
            pnlContent.BackColor = System.Drawing.Color.FromArgb(243, 244, 246);
            pnlContent.AutoScroll = true;
            
            // Setup child form properties for embedding
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.StartPosition = FormStartPosition.Manual; // Prevent default screen centering behavior
            
            // Reset font and disable AutoScale to prevent inheriting the dashboard's large Segoe Script font
            childForm.Font = new System.Drawing.Font("Segoe UI", 9F);
            childForm.AutoScaleMode = AutoScaleMode.None;
            
            // Prevent child form from shrinking below its designed layout bounds
            childForm.MinimumSize = childForm.Size;
            
            // Add to panel first to ensure parent-relative coordinates are active
            pnlContent.Controls.Add(childForm);
            
            // Save active child form reference for size change event centering
            activeChildForm = childForm;
            
            // Position child form initially
            CenterChildForm(childForm);
            
            childForm.Show();
        }

        private void btnHouseManager_Click(object sender, EventArgs e)
        {
            ShowFormInContentPanel(new HouseManagerFrom());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ShowFormInContentPanel(new TenantManagerFrom());
        }

        private void btnRentalManager_Click(object sender, EventArgs e)
        {
            ShowFormInContentPanel(new RentalManagerForm());
        }

        private void label2_Click(object sender, EventArgs e)
        {
            ShowFormInContentPanel(new RentalManagerForm());
        }

        private void lblRentalManage_Click(object sender, EventArgs e)
        {
            ShowFormInContentPanel(new RentalManagerForm());
        }

        private void btnUserManager_Click(object sender, EventArgs e)
        {
            ShowFormInContentPanel(new UserManagerForm());
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            if (!isLoggingOut)
            {
                Application.Exit();
            }
        }
    }
}

