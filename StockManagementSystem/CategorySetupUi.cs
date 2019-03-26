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
    public partial class CategorySetupUi : Form
    {
        int categoryId;
        
        CategorySetup categorySetup = new CategorySetup();
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
        
        public CategorySetupUi()
        {
            InitializeComponent();
        }

        private void CategorySetupUi_Load(object sender, EventArgs e)
        {
            dataTable = ShowData();
            displayDataGridView.DataSource = dataTable;
            foreach (DataGridViewRow row in displayDataGridView.Rows)
            {
                row.Cells["Sl"].Value = (row.Index + 1).ToString();
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            string name;

            if (!string.IsNullOrEmpty(nameTextBox.Text))
            {
                name = nameTextBox.Text;
            }
            else
            {
                MessageBox.Show("Please Give a Category Name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Exists(name))
            {
                MessageBox.Show("Category Already Exists.");
                return;
            }

            try
            {
                Connect();                
                if (SaveButton.Text == "Update")
                {
                    sqlCommand.CommandText = @"UPDATE Categories SET Name='" + name + "' WHERE ID = '" + categoryId + "'";
                }
                else
                {
                    sqlCommand.CommandText = "INSERT INTO Categories (Name) VALUES('" + name + "')";
                }
               
                int isExecuted = sqlCommand.ExecuteNonQuery();
                if (isExecuted > 0)
                {
                    MessageBox.Show("Saved");
                    SaveButton.Text = "Save";
                }
                else
                {
                    MessageBox.Show("Not Saved");
                }
                
                DatabaseConnection.sqlConnection.Close();
                nameTextBox.Clear();

                dataTable = ShowData();
                displayDataGridView.DataSource = dataTable;
                foreach (DataGridViewRow row in displayDataGridView.Rows)
                {
                    row.Cells["Sl"].Value = (row.Index + 1).ToString();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
                       
        }

        private DataTable ShowData()
        {
            try
            {
                Connect();
                
                sqlCommand.CommandText = @"SELECT *FROM Categories";               
                sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);                
                DatabaseConnection.sqlConnection.Close();
             
            }
             
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            return dataTable;
        }
        private bool Exists(string name)
        {
            bool isExists = false;

            try
            {
                Connect();                
                sqlCommand.CommandText = @"SELECT * FROM Categories WHERE Name = '" + name + "'";
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

        private void displayDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
             {
                try
                {
                    nameTextBox.Text = displayDataGridView.CurrentRow.Cells["Name"].Value.ToString();
                    categoryId = Convert.ToInt32(displayDataGridView.CurrentRow.Cells["ID"].Value.ToString());
                    SaveButton.Text = "Update";

                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
             }

    }
        
      

}

