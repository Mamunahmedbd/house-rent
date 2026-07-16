using System;
using System.Windows.Forms;

namespace ElegantHousingSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnHouseManager_Click(object sender, EventArgs e)
        {
            HouseManagerFrom houseForm = new HouseManagerFrom();
            houseForm.Show();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            // Open the tenant management form
            TenantManagerFrom tenantForm = new TenantManagerFrom();
            tenantForm.Show();
        }

        private void btnRentalManager_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            // You can leave this empty or add specific code here
        }

        private void lblRentalManage_Click(object sender, EventArgs e)
        {
            RentalManagerForm rentalFrom = new RentalManagerForm();
            rentalFrom.Show();
        }
    }
}

