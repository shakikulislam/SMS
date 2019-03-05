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

        private bool ReorderLevelWorning(int id)
        {
            bool isExecute = false;
            int reorderLevel = 0;
            try
            {
                Connect();
                sqlCommand.CommandText = "SELECT ReorderLevel FROM Items WHERE ID='" + id + "' ";
                sqlDataReader = sqlCommand.ExecuteReader();
                reorderLevel = sqlDataReader.Read() ? Convert.ToInt32(sqlDataReader["ReorderLevel"]) : reorderLevel=0;
                isExecute = reorderLevel < 5 ? isExecute = true : isExecute = false;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            DatabaseConnection.sqlConnection.Close();
            return isExecute;
        }

        private bool UpdateAvailableQuantityAndReorderLevel(string itemName, int reorderLevel, int availableQuantity)
        {
            bool isExecute = false;
            try
            {
                Connect();
                sqlCommand.CommandText = "UPDATE Items  SET ReorderLevel='" + reorderLevel + "', AvailableQuantity='" + availableQuantity + "' WHERE Name='" + itemName + "'";
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

        private int CalculateReorderLevel(int stockOutQuantity)
        {
            int result=0;
            int reorderLevel=0;
            try
            {
                Connect();
                sqlCommand.CommandText = "SELECT ReorderLevel FROM Items WHERE Name='" + items.Name + "'";
                sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    reorderLevel = Convert.ToInt32(sqlDataReader["ReorderLevel"]);
                    result = reorderLevel - stockOutQuantity;
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

                    // Worning reorder level....
                    bool isSuccess = ReorderLevelWorning(items.Id);
                    if (isSuccess)
                    {
                        MessageBox.Show(itemComboBox.Text + " reorder level is low.\nPlease update reorder level.",
                            "Reorder level update information", MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Information);
                    }
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
                //int reorderLevel = Convert.ToInt32(reorderLevelTextBox.Text);
                //int availableQuantity = Convert.ToInt32(availableQuantityTextBox.Text);
                //int stockOutQuantity = Convert.ToInt32(stockOutQuantityTextBox.Text);
                //int resultAvailableQuantity=CalculateAvailableQuantity(availableQuantity, stockOutQuantity);
                //int resultReorderLevel= CalculateReorderLevel(reorderLevel, stockOutQuantity);

                //bool updateAvailableQuantiy = UpdateAvailableQuantity(items.Id, resultAvailableQuantity);
                //if (!updateAvailableQuantiy)
                //{
                //    MessageBox.Show("Available quantity update faild.");
                //}
                //bool updateReorderLevel = UpdateReorderLevel(items.Id, resultReorderLevel);
                //if (!updateReorderLevel)
                //{
                //    MessageBox.Show("Reorder level update faild.");
                //}
                items.Name = itemComboBox.Text;
                companySetup.Name = companyComboBox.Text;
                int quantity = Convert.ToInt32(stockOutQuantityTextBox.Text);

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
                    displayDataGridView.Rows.Add("", items.Name, companySetup.Name, quantity);

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

        private void stockOutQuantityTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int reorderLevel = Convert.ToInt32(ReorderLevel(items.Id));
                int availableQuantity = Convert.ToInt32(AvailableQuantity(items.Id));
                int stockOutQuantity = 0;
                stockOutQuantity = stockOutQuantityTextBox.Text != ""? stockOutQuantity = Convert.ToInt32(stockOutQuantityTextBox.Text): stockOutQuantity = 0;
                if (stockOutQuantity>reorderLevel)
                {
                    MessageBox.Show("Please update reorder level or\nCheck stock out quantity");
                    stockOutQuantityTextBox.Clear();
                }
                else if (stockOutQuantity > availableQuantity)
                {
                    MessageBox.Show("Please add items or\nCheck stock out quantity");
                    stockOutQuantityTextBox.Clear();
                }
                
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                stockOutQuantityTextBox.Clear();
            }
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

        private void SellButton_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < displayDataGridView.Rows.Count; i++)
                {
                    int stockOutQuantity = Convert.ToInt32(displayDataGridView.Rows[i].Cells[3].Value);
                    items.Name = displayDataGridView.Rows[i].Cells[1].Value.ToString();

                    int currentAvailableQuantity= CalculateAvailableQuantity(stockOutQuantity);
                    int currentReorderLevel = CalculateReorderLevel(stockOutQuantity);

                    UpdateAvailableQuantityAndReorderLevel(items.Name,currentReorderLevel,currentAvailableQuantity);

                    DateTime dateTime=DateTime.Now;
                    bool isAdded= AddStockOut(items.Id, stockOutQuantity, "Sell", dateTime.ToShortDateString());
                    if (isAdded)
                    {
                        displayDataGridView.Rows.Clear();
                    }
                }
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

                    int currentAvailableQuantity = CalculateAvailableQuantity(stockOutQuantity);
                    int currentReorderLevel = CalculateReorderLevel(stockOutQuantity);

                    UpdateAvailableQuantityAndReorderLevel(items.Name, currentReorderLevel, currentAvailableQuantity);

                    DateTime dateTime = DateTime.Now;
                    bool isAdded = AddStockOut(items.Id, stockOutQuantity, "Damage", dateTime.ToShortDateString());
                    if (isAdded)
                    {
                        displayDataGridView.Rows.Clear();
                    }

                }
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
                    int currentReorderLevel = CalculateReorderLevel(stockOutQuantity);

                    UpdateAvailableQuantityAndReorderLevel(items.Name, currentReorderLevel, currentAvailableQuantity);

                    DateTime dateTime = DateTime.Now;
                    bool isAdded = AddStockOut(items.Id, stockOutQuantity, "Lost", dateTime.ToShortDateString());
                    if (isAdded)
                    {
                        displayDataGridView.Rows.Clear();
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

    }
}
