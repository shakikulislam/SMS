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
        SqlConnection sqlConnection;
        SqlCommand sqlCommand;
        SqlDataReader sqlDataReader;
        SqlDataAdapter sqlDataAdapter;
        DataTable dataTable;

        string connectionString = @"Server=SHAKIKUL-PC\SQLEXPRESS;Database=StockManagementSystemDb;Integrated Security=true";
        
        public CompanySetupUi()
        {
            InitializeComponent();
            try
            {
                ShowData();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error...\n"+exception.Message);
            }
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
                MessageBox.Show("Company Already Exists.");
                nameTextBox.Focus();
                return;
            }

            try
            {
                // Save Data
                sqlConnection = new SqlConnection(connectionString);
                string query = @"INSERT INTO CompanyS(Name)VALUES('" + companySetup.Name + "')";
                sqlCommand = new SqlCommand(query, sqlConnection);
                sqlConnection.Open();
                int isExecuted = sqlCommand.ExecuteNonQuery();
                MessageBox.Show(isExecuted > 0 ? "Company Saved." : "Not Saved.");
                sqlConnection.Close();

                nameTextBox.Clear();
                ShowData();
                

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
                sqlConnection = new SqlConnection(connectionString);
                string query = @"SELECT * FROM CompanyS WHERE Name = '" + name + "'";
                sqlCommand = new SqlCommand(query, sqlConnection);
                sqlConnection.Open();
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

                sqlConnection.Close();


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

            return isExists;
        }

        private void ShowData()
        {
            try
            {
                sqlConnection = new SqlConnection(connectionString);
                string queryShowData = @"SELECT *FROM CompanyS";
                sqlCommand= new SqlCommand(queryShowData, sqlConnection);
                sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                displayDataGridView.DataSource = dataTable;
                displayDataGridView.Columns[0].Width = 50;
                displayDataGridView.Columns[1].Width = 310;

                sqlConnection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
