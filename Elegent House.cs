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
            if (txtUser.Text == "admin" && txtPass.Text == "1234")
            {
                btnExi mainForm = new ElegentHouse.cs();
                mainForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid Username or Password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Exit Button Click Event
        private void btnExitLogin_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}