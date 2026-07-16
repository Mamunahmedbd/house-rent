using System;
using System.Drawing;
using System.Windows.Forms;

namespace ElegantHousingSystem
{
    public partial class TenantManagerFrom : Form
    {
        private Label lblTitle, lblName, lblPhone, lblEmail;
        private TextBox txtName, txtPhone, txtEmail;

        private void TenantManagerFrom_Load(object sender, EventArgs e)
        {

        }

        private Button btnSave;

        public TenantManagerFrom()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            this.Text = "Tenant Management System";
            this.Size = new Size(500, 400);
            this.BackColor = Color.FromArgb(255, 240, 245); // Lavender Blush
            this.StartPosition = FormStartPosition.CenterScreen;

            lblTitle = new Label { Text = "Tenant Manager Setup", Font = new Font("Segoe UI", 18, FontStyle.Bold), ForeColor = Color.FromArgb(53, 53, 53), Location = new Point(30, 20), Size = new Size(400, 40) };
            this.Controls.Add(lblTitle);

            // Full Name
            lblName = new Label { Text = "Full Name:", Location = new Point(40, 90), Size = new Size(160, 25) };
            txtName = new TextBox { Location = new Point(220, 90), Size = new Size(200, 25) };
            this.Controls.Add(lblName); this.Controls.Add(txtName);

            // Phone Number
            lblPhone = new Label { Text = "Phone Number:", Location = new Point(40, 140), Size = new Size(160, 25) };
            txtPhone = new TextBox { Location = new Point(220, 140), Size = new Size(200, 25) };
            this.Controls.Add(lblPhone); this.Controls.Add(txtPhone);

            // Email
            lblEmail = new Label { Text = "Email Address:", Location = new Point(40, 190), Size = new Size(160, 25) };
            txtEmail = new TextBox { Location = new Point(220, 190), Size = new Size(200, 25) };
            this.Controls.Add(lblEmail); this.Controls.Add(txtEmail);
            this.Controls.Add(lblEmail); this.Controls.Add(txtEmail);

            // Save Button
            btnSave = new Button { Text = "Save Tenant", BackColor = Color.FromArgb(53, 53, 53), ForeColor = Color.White, Location = new Point(220, 250), Size = new Size(200, 40), FlatStyle = FlatStyle.Flat };
            btnSave.Click += (s, e) => {
                if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtPhone.Text))
                {
                    MessageBox.Show("Please fill in Name and Phone!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                MessageBox.Show($"Tenant Saved Successfully!\n\nName: {txtName.Text}\nPhone: {txtPhone.Text}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Clear(); txtPhone.Clear(); txtEmail.Clear();
            };
            this.Controls.Add(btnSave);
        }
    }
}