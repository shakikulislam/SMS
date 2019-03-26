using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Deployment.Internal;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Microsoft.VisualBasic;
using StockManagementSystem.Models;

namespace StockManagementSystem
{
    public partial class StockOutUi : Form
    {
        CompanySetup companySetup=new CompanySetup();
        Items items=new Items();

        SqlCommand sqlCommand;
        SqlDataAdapter sqlDataAdapter;
        DataTable dataTable;
        SqlDataReader sqlDataReader;

        private void Connect()
        {
            DatabaseConnection databaseConnection = new DatabaseConnection();
            databaseConnection.ConnectionDatabase();
            sqlCommand=new SqlCommand();
            sqlCommand.Connection = DatabaseConnection.sqlConnection;
        }

        public StockOutUi()
        {
            InitializeComponent();

            dataTable = CompanyList();
            companyComboBox.DataSource = dataTable;

        }


        private DataTable CompanyList()
        {
            try
            {
                Connect();
                sqlCommand.CommandText = "SELECT *FROM Companys";
                sqlDataAdapter =new SqlDataAdapter(sqlCommand);
                dataTable =new DataTable();
                sqlDataAdapter.Fill(dataTable);

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

            return dataTable;
        }

        private void companyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            companySetup.Id = Convert.ToInt32(companyComboBox.SelectedValue);
            itemComboBox.Text = "";
            dataTable = ItemsList(companySetup.Id);
            itemComboBox.DataSource = dataTable;
        }

        private DataTable ItemsList(int id)
        {
            try
            {
                Connect();
                sqlCommand.CommandText = "SELECT *FROM Items WHERE CompanyId='" + id + "' ";
                sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            DatabaseConnection.sqlConnection.Close();
            return dataTable;
        }

        private string ReorderLevel(int id)
        {
            string reorderLevel = "";
            try
            {
                Connect();
                sqlCommand.CommandText = "SELECT ReorderLevel FROM Items WHERE ID='" + id + "' ";
                sqlDataReader = sqlCommand.ExecuteReader();
                reorderLevel = sqlDataReader.Read() ? sqlDataReader["ReorderLevel"].ToString() : "";
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            DatabaseConnection.sqlConnection.Close();
            return reorderLevel;
        }

        private string AvailableQuantity(int id)
        {
            string availableQuantity = "";
            try
            {
                Connect();
                sqlCommand.CommandText = "SELECT AvailableQuantity FROM Items WHERE ID='" + id + "' ";
                sqlDataReader = sqlCommand.ExecuteReader();
                availableQuantity = sqlDataReader.Read() ? sqlDataReader["AvailableQuantity"].ToString() : "";
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            DatabaseConnection.sqlConnection.Close();
            return availableQuantity;
        }

        private void itemComboBox_TextChanged(object sender, EventArgs e)
        {
            reorderLevelTextBox.Clear();
            availableQuantityTextBox.Clear();
            AddButton.Enabled = false;
        }

        private bool UpdateAvailableQuantity(string itemName, int availableQuantity)
        {
            bool isExecute = false;
            try
            {
                Connect();
                sqlCommand.CommandText = "UPDATE Items  SET AvailableQuantity='" + availableQuantity + "' WHERE Name='" + itemName + "'";
                int executeNonQuery = sqlCommand.ExecuteNonQuery();
                isExecute = executeNonQuery > 0 ? isExecute = true : isExecute = false;
                
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            DatabaseConnection.sqlConnection.Close();
            return isExecute;
        }
        
        private int CalculateAvailableQuantity(int stockOutQuantity)
        {
            int result = 0;
            int availableQuantity = 0;
            try
            {
                Connect();
                sqlCommand.CommandText = "SELECT AvailableQuantity FROM Items WHERE Name='" + items.Name + "'";
                sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    availableQuantity = Convert.ToInt32(sqlDataReader["AvailableQuantity"]);
                    result = availableQuantity - stockOutQuantity;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            return result;
        }
        
        private void itemComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(itemComboBox.Text))
                {
                    items.Id = Convert.ToInt32(itemComboBox.SelectedValue);


                    reorderLevelTextBox.Text = ReorderLevel(items.Id);
                    availableQuantityTextBox.Text = AvailableQuantity(items.Id);
                    AddButton.Enabled = true;
                }
                else
                {
                    reorderLevelTextBox.Clear();
                    availableQuantityTextBox.Clear();
                    AddButton.Enabled = false;
                }
            }
            catch (Exception)
            {
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            try
            {
                items.Name = itemComboBox.Text;
                items.ReorderLevel = Convert.ToInt32(reorderLevelTextBox.Text);
                items.AvailableQuantity = Convert.ToInt32(availableQuantityTextBox.Text);
                int stockOutQuantity = Convert.ToInt32(stockOutQuantityTextBox.Text);
                companySetup.Name = companyComboBox.Text;

                bool isCheckStock = CheckStock(items.ReorderLevel, items.AvailableQuantity, stockOutQuantity);
                if (isCheckStock)
                {
                    MessageBox.Show("Available quantity is very low...\nPlease stock in.");
                    return;
                }

                bool isSuccess = FindDataGridView(items.Name, companySetup.Name);
                if (isSuccess)
                {
                    MessageBox.Show("This item allready added.\nPlease sell double click and delete it.", "Information");

                    for (int i = 0; i < displayDataGridView.Rows.Count; i++)
                    {
                        string companyFind = displayDataGridView.Rows[i].Cells[2].Value.ToString();
                        string itemFind = displayDataGridView.Rows[i].Cells[1].Value.ToString();

                        if (items.Name == itemFind && companySetup.Name == companyFind)
                        {
                            displayDataGridView.Rows[i].Selected = true;
                            displayDataGridView.FirstDisplayedScrollingRowIndex = i;
                        }
                    }
                    return;
                }
                else
                {
                    displayDataGridView.Rows.Add("", items.Name, companySetup.Name, stockOutQuantity);
                    itemComboBox.Text = "";
                    stockOutQuantityTextBox.Clear();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void displayDataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            displayDataGridView.Rows[e.RowIndex].Cells[0].Value = e.RowIndex + 1;
        }

        private void stockOutQuantityTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                AddButton.PerformClick();
                e.SuppressKeyPress = true;
                itemComboBox.Focus();
            }
        }

        private bool FindDataGridView(string itemName, string companyName)
        {
            bool isFind = false;
            for (int i = 0; i < displayDataGridView.Rows.Count; i++)
            {
                if (itemName == displayDataGridView.Rows[i].Cells[1].Value && companyName == displayDataGridView.Rows[i].Cells[2].Value)
                {
                    isFind = true;
                }
                else
                {
                    isFind = false;
                }
            }
            return isFind;
        }

        private bool CheckStock(int reOrderLabel, int quantity, int outQuantity)
        {
            bool isCheck = false;
            if (quantity-outQuantity<reOrderLabel)
            {
                isCheck = true;
            }
            else
            {
                isCheck = false;
            }
            return isCheck;
        }

        private void displayDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (displayDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                displayDataGridView.CurrentRow.Selected = true;

                if (MessageBox.Show("Are you sure delete this items?","Confirmation message",MessageBoxButtons.YesNo,MessageBoxIcon.Information)==DialogResult.Yes)
                {
                    displayDataGridView.Rows.RemoveAt(displayDataGridView.CurrentRow.Index);
                }                
            }
            
        }
        
        private bool AddStockOut(int itemId, int stockOutQuantity, string StockOutType, string date)
        {
            bool isSuccess = false;
            try
            {
                Connect();
                sqlCommand.CommandText = "INSERT INTO StockOut(ItemId,StockOutQuantity,StockOutType,Date)VALUES('"+itemId+"','"+stockOutQuantity+"','"+StockOutType+"','"+date+"')";
                int execute= sqlCommand.ExecuteNonQuery();
                if (execute>0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            return isSuccess;
        }

        private int ItemId(string name)
        {
            int itemId = 0;
            Connect();
            sqlCommand.CommandText = "SELECT ID FROM Items WHERE Name='" + name + "'";
            sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader.Read())
            {
                itemId =Convert.ToInt32(sqlDataReader["ID"]);
            }
            else
            {
                throw new Exception("Item id not found...");
            }
            return itemId;
        }
        private void SellButton_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < displayDataGridView.Rows.Count; i++)
                {
                    int stockOutQuantity = Convert.ToInt32(displayDataGridView.Rows[i].Cells[3].Value);
                    items.Name = displayDataGridView.Rows[i].Cells[1].Value.ToString();

                    int currentAvailableQuantity= CalculateAvailableQuantity(stockOutQuantity);

                    UpdateAvailableQuantity(items.Name,currentAvailableQuantity);

                    int itemId= ItemId(items.Name);

                    DateTime dateTime=DateTime.Now;
                    AddStockOut(itemId, stockOutQuantity, "Sell", dateTime.ToShortDateString());
                }
                displayDataGridView.Rows.Clear();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
        
        private void DamageButton_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < displayDataGridView.Rows.Count; i++)
                {
                    int stockOutQuantity = Convert.ToInt32(displayDataGridView.Rows[i].Cells[3].Value);
                    items.Name = displayDataGridView.Rows[i].Cells[1].Value.ToString();

                    int currentAvailableQuantity= CalculateAvailableQuantity(stockOutQuantity);

                    UpdateAvailableQuantity(items.Name,currentAvailableQuantity);

                    int itemId= ItemId(items.Name);

                    DateTime dateTime=DateTime.Now;
                    AddStockOut(itemId, stockOutQuantity, "Damage", dateTime.ToShortDateString());
                }
                displayDataGridView.Rows.Clear();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void LostButton_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < displayDataGridView.Rows.Count; i++)
                {
                    int stockOutQuantity = Convert.ToInt32(displayDataGridView.Rows[i].Cells[3].Value);
                    items.Name = displayDataGridView.Rows[i].Cells[1].Value.ToString();

                    int currentAvailableQuantity = CalculateAvailableQuantity(stockOutQuantity);

                    UpdateAvailableQuantity(items.Name, currentAvailableQuantity);

                    int itemId = ItemId(items.Name);

                    DateTime dateTime = DateTime.Now;
                    AddStockOut(itemId, stockOutQuantity, "Lost", dateTime.ToShortDateString());
                }
                displayDataGridView.Rows.Clear();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void stockOutQuantityTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char chr = e.KeyChar;
            if (!Char.IsDigit(chr) && chr !=8)
            {
                e.Handled = true;
                return;
            }
        }

    }
}
