using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Deployment.Internal;
using System.Drawing;
using System.Linq;
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

        private bool UpdateReorderLevel(int id,int reorderLevel)
        {
            bool isExecute = false;
            try
            {
                Connect();
                sqlCommand.CommandText = "UPDATE Items SET ReorderLevel='" + reorderLevel + "' WHERE ID='" + id + "'";
                int executeNonQuery= sqlCommand.ExecuteNonQuery();
                isExecute = executeNonQuery>0 ? isExecute = true : isExecute = false;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            DatabaseConnection.sqlConnection.Close();
            return isExecute;
        }

        private bool UpdateAvailableQuantity(int id, int availableQuantity)
        {
            bool isExecute = false;
            try
            {
                Connect();
                sqlCommand.CommandText = "UPDATE Items  SET AvailableQuantity='" + availableQuantity + "' WHERE ID='" + id + "'";
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
        
        private int CalculateAvailableQuantity(int availableQuantity,int stockOutQuantity)
        {
            int result = 0;
            return result = availableQuantity - stockOutQuantity;
        }

        private int CalculateReorderLevel(int reorderLevel, int stockOutQuantity)
        {
            int result=0;
            return result = reorderLevel - stockOutQuantity;
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

                    // Worning and Update reorder level....
                    bool isSuccess = ReorderLevelWorning(items.Id);
                    if (isSuccess)
                    {
                        if (
                            MessageBox.Show(itemComboBox.Text + " reorder level is low.\nPlease update reorder level.",
                                "Reorder level update information", MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Information) == DialogResult.OK)
                        {
                            items.ReorderLevel =
                                Convert.ToInt32(Interaction.InputBox("Please enter reorder level.",
                                    "Update reorder level", "", -1, -1));

                            bool isUpdate = UpdateReorderLevel(items.Id, items.ReorderLevel);
                            if (isUpdate)
                            {
                                MessageBox.Show("Reorder level update successful.", "Confirmation Message");
                                reorderLevelTextBox.Text = ReorderLevel(items.Id);
                            }
                            else
                            {
                                MessageBox.Show("Reorder level no update.\nPlease try again.");
                            }
                        }
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

        int addIndex = 1;
        private void AddButton_Click(object sender, EventArgs e)
        {
            try
            {
                int reorderLevel = Convert.ToInt32(reorderLevelTextBox.Text);
                int availableQuantity = Convert.ToInt32(availableQuantityTextBox.Text);
                int stockOutQuantity = Convert.ToInt32(stockOutQuantityTextBox.Text);
                int resultAvailableQuantity=CalculateAvailableQuantity(availableQuantity, stockOutQuantity);
                int resultReorderLevel= CalculateReorderLevel(reorderLevel, stockOutQuantity);

                bool updateAvailableQuantiy = UpdateAvailableQuantity(items.Id, resultAvailableQuantity);
                if (!updateAvailableQuantiy)
                {
                    MessageBox.Show("Available quantity update faild.");
                }
                bool updateReorderLevel = UpdateReorderLevel(items.Id, resultReorderLevel);
                if (!updateReorderLevel)
                {
                    MessageBox.Show("Reorder level update faild.");
                }

                string item = itemComboBox.Text;
                string company = companyComboBox.Text;
                int quantity = Convert.ToInt32(stockOutQuantityTextBox.Text);

                displayDataGridView.Rows.Add(addIndex, item, company, quantity);
                addIndex++;

                itemComboBox.Text = "";
                stockOutQuantityTextBox.Clear();


            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
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
                }
                else if (stockOutQuantity > availableQuantity)
                {
                    MessageBox.Show("Please add items or\nCheck stock out quantity");
                }
                
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
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

    }
}
