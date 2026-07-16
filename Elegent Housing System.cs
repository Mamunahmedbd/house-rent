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
            
            // Setup child form properties for embedding
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            
            // Center the form inside pnlContent
            childForm.Location = new System.Drawing.Point(
                (pnlContent.Width - childForm.Width) / 2,
                (pnlContent.Height - childForm.Height) / 2
            );
            childForm.Anchor = AnchorStyles.None; // Maintain centering on resize
            
            // Add to panel and show
            pnlContent.Controls.Add(childForm);
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

