using System;
using System.Windows.Forms;

namespace ElegantHousingSystem
{
    public partial class Form1 : Form
    {
        public static string CurrentUsername = "";
        public static string CurrentUserRole = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
            
            // Prevent child form from shrinking below its designed layout bounds
            childForm.MinimumSize = childForm.Size;
            
            // Add to panel first to ensure parent-relative coordinates are active
            pnlContent.Controls.Add(childForm);
            
            if (childForm is UserManagerForm)
            {
                childForm.Dock = DockStyle.Fill;
            }
            else
            {
                // Center the form inside pnlContent
                // Use Math.Max(0, ...) to ensure coordinates never go negative, allowing full scrolling
                childForm.Location = new System.Drawing.Point(
                    Math.Max(0, (pnlContent.Width - childForm.Width) / 2),
                    Math.Max(0, (pnlContent.Height - childForm.Height) / 2)
                );
                childForm.Anchor = AnchorStyles.None; // Maintain centering on resize
            }
            
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
    }
}

