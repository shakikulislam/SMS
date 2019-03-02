using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StockManagementSystem.Models;
using System.Data.SqlClient;


namespace StockManagementSystem
{
    public partial class StockInUi : Form
    {
        Item item = new Item();
        SqlCommand sqlCommand;
        SqlDataReader sqlDataReader;
        SqlDataAdapter sqlDataAdapter;
        DataTable dataTable;

        private void Connect()
        {
            DatabaseConnection databaseConnection = new DatabaseConnection();
            databaseConnection.ConnectionDatabase();
            sqlCommand = new SqlCommand();
            sqlCommand.Connection = DatabaseConnection.sqlConnection;
        } 

        public StockInUi()
        {
            InitializeComponent();
            try
            {
                companyComboBox.DataSource = GetCompanyCombo();


            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            item.CompanyId = Convert.ToInt32(companyComboBox.SelectedValue);

          
            double availableQuantity = Convert.ToDouble(availableQuantityTextBox.Text);
            double stockInQuantity = Convert.ToDouble(stockInQuantityTextBox.Text);
            double quantity = 0;
            try
            {
                // Save Data
                Connect();
                DateTime nowDateTime = DateTime.Now;
                sqlCommand.CommandText = @"INSERT INTO StockIn([Available Quantity],[Stock In Quantity],[Date],[Company ID],[Item ID]) VALUES('" + availableQuantity + "','" + stockInQuantity + "','" + nowDateTime.ToShortDateString() + "','" + companyComboBox.SelectedValue + "','" + itemComboBox.SelectedValue + "')";
                //DatabaseConnection.sqlConnection.Open();
                quantity = Add(availableQuantity, stockInQuantity);
                Display(quantity);

                int isExecuted = sqlCommand.ExecuteNonQuery();
                MessageBox.Show(isExecuted > 0 ? "Company Saved." : "Not Saved.");

                DatabaseConnection.sqlConnection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private double Add(double availableQuantity, double stockInQuantity)
        {
            double quantity = availableQuantity + stockInQuantity;
            return quantity;
        }

        private void Display(double quantity)
        {
            availableQuantityTextBox.Text = quantity.ToString();

        }
        private DataTable GetCompanyCombo()
        {
            Connect();
            sqlCommand.CommandText = @"SELECT ID, Name FROM CompanyS";

            //DatabaseConnection.sqlConnection.Open();
            sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            DatabaseConnection.sqlConnection.Close();
            return dataTable;



        }
        private DataTable GetItemCombo(Item item)
        {
            Connect();
            sqlCommand.CommandText = @"SELECT ID ,Name FROM Items where CompanyId=" + item.CompanyId + "";                        
           // DatabaseConnection.sqlConnection.Open();
            sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            DatabaseConnection.sqlConnection.Close();
            return dataTable;


        }

        private void companyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            item.CompanyId = Convert.ToInt32(companyComboBox.SelectedValue);
            itemComboBox.DataSource = GetItemCombo(item);



            Connect();
            sqlCommand.CommandText = "SELECT * FROM CompanyItemView  where Company='" + companyComboBox.Text + "'";
            SqlDataReader sqlDataReader;

            try
            {
                //DatabaseConnection.sqlConnection.Open();
                sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {

                    string itemName = sqlDataReader["Name"].ToString();
                    itemComboBox.Text = itemName;

                }
                DatabaseConnection.sqlConnection.Close();

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            
        }

        private void itemComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Connect();
                
                sqlCommand.CommandText = "SELECT * From ItemsStockInView where Name='" + itemComboBox.Text + "'";
                SqlDataReader sqlDataReader;


                //DatabaseConnection.sqlConnection.Open();
                sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {

                    string reorderLevel = sqlDataReader["ReorderLevel"].ToString();
                    string availableQuantity = sqlDataReader["Available Quantity"].ToString();
                    reorderLevelTextBox.Text = reorderLevel;
                    availableQuantityTextBox.Text = availableQuantity;
                }

                DatabaseConnection.sqlConnection.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

           
        }




    }
}
