using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using StockManagementSystem.Models;

namespace StockManagementSystem
{
    public partial class ItemSetupUi : Form
    {
        ItemSetup itemSetup = new ItemSetup();
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

        public ItemSetupUi()
        {
            InitializeComponent();
            try
            {
                categoryComboBox.DataSource = GetCategoryCombo();
                companyComboBox.DataSource = GetCompanyCombo();



            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void ItemSetupUi_Load(object sender, EventArgs e)
        {
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {

            SaveData();
            // clear();

        }

        private DataTable GetCategoryCombo()
        {
            Connect();
            sqlCommand.CommandText = @"SELECT ID, Name FROM Categories";

            //DatabaseConnection.sqlConnection.Open();
            sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            DatabaseConnection.sqlConnection.Close();
            return dataTable;
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

        public void clear()
        {
            itemnameTextBox.Text = "";
            reorderLevelTextBox.Text = "";
            categoryComboBox.SelectedIndex = 0;
            companyComboBox.SelectedIndex = 0;
            itemValiditionLabel.Text = "";
            reorderValidationLabel.Text = "";

        }
        private void SaveData()
        {
            try
            {

                itemSetup.ItemName = itemnameTextBox.Text;
                if (itemnameTextBox.Text == "")
                {
                    itemValiditionLabel.Text = "Invalid Item Name !!";
                }

                else if (Exists(itemnameTextBox.Text))
                {
                    itemValiditionLabel.Text = "Exists Item Name !!";
                }
                else if (reorderLevelTextBox.Text == "")
                {

                    reorderValidationLabel.Text = "Invalid Reorder Level !!";
                }
                else
                {
                    itemSetup.ReorderLevel = Convert.ToInt32(reorderLevelTextBox.Text);
                    Connect();
                    sqlCommand.CommandText = @"INSERT INTO Items  ([Name],[CompanyId],[CategoryId],[ReorderLevel])      
                    VALUES ('" + itemSetup.ItemName + "'," + companyComboBox.SelectedValue + "," + categoryComboBox.SelectedValue + "," + itemSetup.ReorderLevel + ")";

                    int isExecuted = sqlCommand.ExecuteNonQuery();
                    if (isExecuted > 0)
                    {
                        MessageBox.Show("success");
                        clear();
                    }
                    else
                    {
                        MessageBox.Show("Faild to save!");
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool Exists(string name)
        {

            bool isExists = false;

            try
            {
                Connect();
                sqlCommand.CommandText = @"SELECT * FROM Items WHERE Name = '" + itemSetup.ItemName + "'";
                // SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                //sqlConnection.Open();
                sqlDataReader = sqlCommand.ExecuteReader();
                string data = "";
                if (sqlDataReader.Read())
                {
                    data = name;
                }

                if (!String.IsNullOrEmpty(data))
                {
                    isExists = true;
                }
                else
                {
                    isExists = false;
                }

                // sqlConnection.Close();

            }
            catch (Exception exception)
            {

                MessageBox.Show(exception.Message);
            }

            return isExists;
        }


    }
}
