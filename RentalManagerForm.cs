using System;
using System.Drawing;
using System.Windows.Forms;

namespace ElegantHousingSystem
{
    public partial class RentalManagerForm : Form
    {
        public RentalManagerForm()
        {
            InitializeComponent();
            // Call the styling method
            InitializeCustomStyles();
        }

        private void InitializeCustomStyles()
        {
            this.BackColor = Color.FromArgb(250, 240, 245);
            this.Text = "Elegant Housing System - Rental Management";

            // Changed the name here to btSave to match the Designer
            if (btSave != null)
            {
                btSave.Text = "Save Entry";
                btSave.BackColor = Color.FromArgb(199, 21, 133);
                btSave.ForeColor = Color.White;
                btSave.FlatStyle = FlatStyle.Flat;
                btSave.FlatAppearance.BorderSize = 0;
                btSave.Cursor = Cursors.Hand;
            }

            if (dataGridView1 != null)
            {
                dataGridView1.BackgroundColor = Color.White;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(252, 230, 235);
            }
        }
        
      private void btSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAddress.Text) || string.IsNullOrWhiteSpace(txtRentValue.Text))
            {
                MessageBox.Show("Please fill all fields.");
                return;
            }

            dataGridView1.Rows.Add(dataGridView1.Rows.Count + 1, txtAddress.Text, txtRentValue.Text, cmbStatus.Text);

            txtAddress.Clear();
            txtRentValue.Clear();
            cmbStatus.SelectedIndex = -1;
        }
    }
}