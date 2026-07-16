using DataAccess;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ElegantHousingSystem
{
    public partial class HouseManagerFrom : Form
    {
        private Label lblTitle;
        private Label lblHouseNo;
        private Label lblPrice;
        private Label lblStatus;
        private TextBox txtHouseNo;
        private TextBox txtPrice;
        private ComboBox cmbStatus;
        private Button btnSave;

        public HouseManagerFrom()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            this.Text = "House Management System";
            this.Size = new Size(800, 500);
            this.BackColor = Color.FromArgb(255, 240, 245);
            this.StartPosition = FormStartPosition.CenterScreen;

            lblTitle = new Label();
            lblTitle.Text = "House Manager Setup";
            lblTitle.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(53, 53, 53);
            lblTitle.Location = new Point(30, 20);
            lblTitle.Size = new Size(400, 40);
            this.Controls.Add(lblTitle);

            lblHouseNo = new Label();

            lblHouseNo.Location = new Point(40, 90);
            lblHouseNo.Size = new Size(160, 25);
            this.Controls.Add(lblHouseNo);

            txtHouseNo = new TextBox();
            txtHouseNo.Location = new Point(220, 90);
            txtHouseNo.Size = new Size(200, 25);
            this.Controls.Add(txtHouseNo);

            lblPrice = new Label();
            lblPrice.Text = "Rent Price per Month:";
            lblPrice.Location = new Point(40, 140);
            lblPrice.Size = new Size(160, 25);
            this.Controls.Add(lblPrice);

            txtPrice = new TextBox();
            txtPrice.Location = new Point(220, 140);
            txtPrice.Size = new Size(200, 25);
            this.Controls.Add(txtPrice);

            lblStatus = new Label();
            lblStatus.Text = "Availability Status:";
            lblStatus.Location = new Point(40, 190);
            lblStatus.Size = new Size(160, 25);
            this.Controls.Add(lblStatus);

            cmbStatus = new ComboBox();
            cmbStatus.Location = new Point(220, 190);
            cmbStatus.Size = new Size(200, 25);
            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatus.Items.AddRange(new string[] { "Available", "Rented", "Under Maintenance" });
            cmbStatus.SelectedIndex = 0;
            this.Controls.Add(cmbStatus);

            btnSave = new Button();
            btnSave.Text = "Save House Details";
            btnSave.BackColor = Color.FromArgb(53, 53, 53);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(220, 250);
            btnSave.Size = new Size(200, 40);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Click += new EventHandler(btnSave_Click);
            this.Controls.Add(btnSave);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // 1. Check if the fields are not empty
            if (string.IsNullOrWhiteSpace(label1.Text) || string.IsNullOrWhiteSpace(txtHouseAddress.Text) || string.IsNullOrWhiteSpace(label4.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            // 2. Add the data to the grid
            // Ensure that the control names (txtHouseArea, txtAddress, etc.) match those in the Properties
            dataGridView1.Rows.Add(dataGridView1.Rows.Count + 1, txtHouseArea.Text, txtHouseAddress.Text, txtRentPrice.Text, cmbStatus.Text);

            // 3. Clear the fields after saving
            txtHouseArea.Clear();
            txtHouseAddress.Clear();
            txtRentPrice.Clear();
            cmbStatus.SelectedIndex = -1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Insert statement
            string sqlStr = string.Format("insert into HouseInfo(HouseID,HouseAddress,HouseArea,RentPrice,Status) values('{0}','{1}','{2}','{3}','{4}')",
                this.txtHouseNo.Text, this.txtHouseAddress.Text, this.txtHouseArea.Text, this.txtRentPrice.Text, this.cmbStatus.SelectedItem.ToString());
            //Execute the SQL statement by using the ExecuteCmd method of SQLHelper
            int n = SQLHelper.ExecuteCmd(sqlStr);
            // If value of n is greater than 0, the meaning is that insert operation is success.
            if (n > 0)
            {
                MessageBox.Show("Insert is success.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DataSet ds1 = new DataSet();
                ds1 = this.QueryAll();
                this.dataGridView1.DataSource = ds1.Tables[0];
                //this.SetDgvWidth();
            }
        }
        private DataSet QueryAll()//Retrieve all admin info.
        {
            string sqlStr = string.Format("select * from HouseInfo");
            DataSet ds1 = new DataSet();
            ds1 = SQLHelper.GetData(sqlStr);
            return ds1;
        }
        private void HouseManagerFrom_Load(object sender, EventArgs e)
        {

        }
    }
}