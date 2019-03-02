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
            item.Id = Convert.ToInt32(itemComboBox.SelectedValue);
          
            int stockInQuantity = Convert.ToInt32(stockInQuantityTextBox.Text);
            
            try
            {
                int availableQuantity= Add(item.Id, stockInQuantity);
                bool isUpdate= UpdateAvailableQuantity(item.Id, availableQuantity);
                if (!isUpdate)
                {
                    MessageBox.Show("Update Faild.");
                }
                DateTime nowDateTime = DateTime.Now;
                // Save Data
                Connect();
                sqlCommand.CommandText = @"INSERT INTO StockIn(StockInQuantity,Date,CompanyID,ItemID) VALUES('" + stockInQuantity + "','" + nowDateTime.ToShortDateString() + "','" + item.CompanyId + "','" + item.Id + "')";
                //DatabaseConnection.sqlConnection.Open();
                int isExecuted = sqlCommand.ExecuteNonQuery();
                if (isExecuted>0)
                {
                    MessageBox.Show("Saved");
                    reorderLevelTextBox.Clear();
                    availableQuantityTextBox.Clear();
                    stockInQuantityTextBox.Clear();
                }
                else
                {
                    MessageBox.Show("Not Saved.");
                }


                DatabaseConnection.sqlConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private int Add(int id,int stockInQuantity)
        {
            int result=0;
            int availableQuantity=0;
            try
            {
                Connect();
                sqlCommand.CommandText = "SELECT AvailableQuantity FROM Items WHERE ID='" + id + "'";
                sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    availableQuantity = Convert.ToInt32(sqlDataReader["AvailableQuantity"]);
                }
                if (availableQuantity > 0)
                {
                    result = availableQuantity + stockInQuantity;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            return result;
        }

        private bool UpdateAvailableQuantity(int id,int availableQuantity)
        {
            bool isSuccess = false;
            try
            {
                Connect();
                sqlCommand.CommandText = "UPDATE Items SET AvailableQuantity='" + availableQuantity + "' WHERE ID='"+id+"'";
                int isExecuted = sqlCommand.ExecuteNonQuery();
                if (isExecuted>0)
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
        private DataTable GetCompanyCombo()
        {
            Connect();
            sqlCommand.CommandText = @"SELECT ID, Name FROM Companys";

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
            sqlCommand.CommandText = "SELECT * FROM Items  where CompanyId='" + companyComboBox.SelectedValue + "'";
            

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

                sqlCommand.CommandText = "SELECT * From Items where ID='" + itemComboBox.SelectedValue + "'";
                SqlDataReader sqlDataReader;


                //DatabaseConnection.sqlConnection.Open();
                sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {

                    string reorderLevel = sqlDataReader["ReorderLevel"].ToString();
                    string availableQuantity = sqlDataReader["AvailableQuantity"].ToString();
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
