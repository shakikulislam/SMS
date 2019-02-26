using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StockManagementSystem.Models;

namespace StockManagementSystem
{
    public partial class CompanySetupUi : Form
    {
        CompanySetup companySetup=new CompanySetup();
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
        
        public CompanySetupUi()
        {
            InitializeComponent();

            // Display Record
            dataTable = ShowData();
            displayDataGridView.DataSource = dataTable;
            displayDataGridView.Columns[0].Width = 50;
            displayDataGridView.Columns[1].Width = 310;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(nameTextBox.Text))
            {
                companySetup.Name = nameTextBox.Text;
            }
            else
            {
                MessageBox.Show("Please Give a Company Name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                nameTextBox.Focus();
                return;
            }

            if (Exists(companySetup.Name))
            {
                MessageBox.Show("This Company Already Exists.");
                nameTextBox.Focus();
                return;
            }

            try
            {
                // Save Data
                Connect();
                sqlCommand.CommandText = @"INSERT INTO CompanyS(Name)VALUES('" + companySetup.Name + "')";
                int isExecuted = sqlCommand.ExecuteNonQuery();
                MessageBox.Show(isExecuted > 0 ? "Company Saved." : "Not Saved.");
                DatabaseConnection.sqlConnection.Close();

                nameTextBox.Clear();

                // Display Record
                dataTable=ShowData();
                displayDataGridView.DataSource = dataTable;
                displayDataGridView.Columns[0].Width = 50;
                displayDataGridView.Columns[1].Width = 310;
                

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
                sqlCommand.CommandText = @"SELECT * FROM CompanyS WHERE Name = '" + name + "'";
                sqlDataReader = sqlCommand.ExecuteReader();

                string data = "";
                if (sqlDataReader.Read())
                {
                    data = sqlDataReader["Name"].ToString();
                }
                if (!String.IsNullOrEmpty(data))
                {
                    isExists = true;
                }
                else
                {
                    isExists = false;
                }

                DatabaseConnection.sqlConnection.Close();


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

            return isExists;
        }

        private DataTable ShowData()
        {
            try
            {
                
                Connect();
                sqlCommand.CommandText = "SELECT *FROM CompanyS";
                sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                
                DatabaseConnection.sqlConnection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dataTable;
        }

        private void nameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                SaveButton.PerformClick();
                e.SuppressKeyPress = true;
            }
        }
    }
}
