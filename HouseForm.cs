using DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Housing01
{
    public partial class HouseForm : Form
    {
        public HouseForm()
        {
            InitializeComponent();
        }

        private void HouseForm_Load(object sender, EventArgs e)
        {
            //set the query condition and default is to query all house info.
            this.cmbCondition.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbCondition.Items.Clear();
            this.cmbCondition.Items.Add("All");
            this.cmbCondition.Items.Add("Address");
            this.cmbCondition.SelectedIndex = 0;

            //
            this.cmbIsVacant.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbIsVacant.Items.Clear();
            this.cmbIsVacant.Items.Add("Yes");
            this.cmbIsVacant.Items.Add("No");
            this.cmbIsVacant.SelectedIndex = 1;
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            string sqlStr = string.Format("select * from HouseInfo");//Default is to query all house info.
            if (this.cmbCondition.SelectedIndex == 0) //Retrieve all house info.
            {
                sqlStr = string.Format("select * from HouseInfo");
            }
            if (this.cmbCondition.SelectedIndex == 1) //Perform fuzzy query based on the Address field
            {
                sqlStr = string.Format("select * from HouseInfo where Address like '%{0}%'", this.tbValue.Text);
            }
            DataSet ds1 = new DataSet();
            ds1 = SQLHelper.GetData(sqlStr);
            this.dgvHouseList.DataSource = ds1.Tables[0];
            this.SetDgvWidth();
        }
        private void SetDgvWidth() //Set the width of each column in the dgvHouseList
        {
            this.dgvHouseList.Columns[0].Width = 60;  //Set the width of the first column
            this.dgvHouseList.Columns[1].Width = 60;
            this.dgvHouseList.Columns[2].Width = 150;
            this.dgvHouseList.Columns[3].Width = 60;
            this.dgvHouseList.Columns[4].Width = 60;
            this.dgvHouseList.Columns[5].Width = 60;
            this.dgvHouseList.Columns[5].Width = 200;
        }
       
        private DataSet QueryAll()//Retrieve all admin info.
        {
            string sqlStr = string.Format("select * from HouseInfo");
            DataSet ds1 = new DataSet();
            ds1 = SQLHelper.GetData(sqlStr);
            return ds1;
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            //Insert statement
            string sqlStr = string.Format("insert into HouseInfo(HouseID,CategoryID,Address,Area,RentPrice,Deposit,IsVacant,Introduction) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
                this.tbHouseID.Text, this.tbCategoryID.Text, this.tbAddress.Text, this.tbArea.Text, this.tbRentPrice.Text,this.tbDeposit.Text,this.cmbIsVacant.Text,this.tbIntroduction.Text);
            //Execute the SQL statement by using the ExecuteCmd method of SQLHelper
            int n = SQLHelper.ExecuteCmd(sqlStr);
            // If value of n is greater than 0, the meaning is that insert operation is success.
            if (n > 0)
            {
                MessageBox.Show("Insert is success.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DataSet ds1 = new DataSet();
                ds1 = this.QueryAll();
                this.dgvHouseList.DataSource = ds1.Tables[0];
                this.SetDgvWidth();
            }
        }

        private void dgvHouseList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Obtain the index of the selected row
            int n = this.dgvHouseList.CurrentRow.Index;
            //Display user data to the corresponding controls
            this.tbHouseID.Text = this.dgvHouseList[0, n].Value.ToString(); //the value of Row n and column  0 is assgined to tbID and display it.
            this.tbCategoryID.Text = this.dgvHouseList[1, n].Value.ToString();
            this.tbAddress.Text = this.dgvHouseList[2, n].Value.ToString();
            this.tbArea.Text = this.dgvHouseList[3, n].Value.ToString();
            this.tbRentPrice.Text = this.dgvHouseList[4, n].Value.ToString();

            this.tbDeposit.Text = this.dgvHouseList[5, n].Value.ToString();
            this.cmbIsVacant.Text = this.dgvHouseList[6, n].Value.ToString();
            this.tbIntroduction.Text = this.dgvHouseList[7, n].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Form the update statement
            string sqlStr = string.Format("update HouseInfo set CategoryID = '{0}',Address = '{1}' ,Area='{2}',RentPrice='{3}',Deposit='{4}',IsVacant='{5}',Introduction='{6}' where HouseID = '{7}'",
                this.tbCategoryID.Text, this.tbAddress.Text, this.tbArea.Text, this.tbRentPrice.Text, this.tbDeposit.Text, this.cmbIsVacant.Text, this.tbIntroduction.Text, this.tbHouseID.Text);
            //Execute the SQL command.
            int n = SQLHelper.ExecuteCmd(sqlStr);
            // If value of n is greater than 0, the meaning is that update operation is success.
            if (n > 0)
            {
                MessageBox.Show("update is success.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DataSet ds1 = new DataSet();
                ds1 = this.QueryAll();
                this.dgvHouseList.DataSource = ds1.Tables[0];

                this.SetDgvWidth();
            }
            else
            {
                MessageBox.Show("update is failure.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Form the delete statement
            string sqlStr = string.Format("delete from HouseInfo where HouseID = '{0}'", this.tbHouseID.Text);
            //Execute the SQL command.
            int n = SQLHelper.ExecuteCmd(sqlStr);
            // If value of n is greater than 0, the meaning is that delete operation is success.
            if (n > 0)
            {
                MessageBox.Show("House deletion is success.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DataSet ds1 = new DataSet();
                ds1 = this.QueryAll();
                this.dgvHouseList.DataSource = ds1.Tables[0];

                this.SetDgvWidth();
            }
            else
            {
                MessageBox.Show("House deletion is failure.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSelectCategory_Click(object sender, EventArgs e)
        {
            SelectCategory selCategory = new SelectCategory();
            if (selCategory.ShowDialog() == DialogResult.OK)
            {
                this.tbCategoryID.Text = selCategory.CategoryID.ToString(); //Save the CategoryID to tbHouseID control.
            }           
        }

       

        //private void btnSelectHouse_Click(object sender, EventArgs e)
        //{
        //    SelectHouse seHouse = new SelectHouse();
        //    if (seHouse.ShowDialog() == DialogResult.OK)
        //    {
        //        this.tbHouseID.Text = seHouse.HouseID.ToString();
        //    }

        //}
    }
}
