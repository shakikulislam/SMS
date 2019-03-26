﻿﻿using System;
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
    public partial class SearchAndViewItemsUi : Form
    {


        SearchAndView searchAndView = new SearchAndView();
        SqlCommand sqlCommand;
        SqlDataReader sqlDataReader;
        SqlDataAdapter sqlDataAdapter;
        DataTable dataTable;

        public SearchAndViewItemsUi()
        {
            InitializeComponent();
            try
            {
                categoryComboBox.DataSource = GetCategoryCombo();

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }


        private void Connect()
        {
            DatabaseConnection databaseConnection = new DatabaseConnection();
            databaseConnection.ConnectionDatabase();
            sqlCommand = new SqlCommand();
            sqlCommand.Connection = DatabaseConnection.sqlConnection;
        }

        private DataTable GetCategoryCombo()
        {
            Connect();
            sqlCommand.CommandText = @"SELECT * FROM Categories";

            //DatabaseConnection.sqlConnection.Open();
            sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            DatabaseConnection.sqlConnection.Close();
            return dataTable;
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            try
            {
                searchAndView.CompanyName = companyComboBox.Text;
                searchAndView.CategoryName = categoryComboBox.Text;

                Connect();

                if (companyComboBox.Text == "" && categoryComboBox.Text != "")
                {

                    sqlCommand.CommandText = @"SELECT * FROM ItemsSetupView WHERE ( CategoryName = '" + searchAndView.CategoryName + "') ";
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);
                    showDataGridView.DataSource = dataTable;
                    foreach (DataGridViewRow row in showDataGridView.Rows)
                    {
                        row.Cells["Sl"].Value = (row.Index + 1).ToString();
                    }
                }
                else if (companyComboBox.Text != "" && categoryComboBox.Text == "")
                {
                    sqlCommand.CommandText = @"SELECT * FROM ItemsSetupView WHERE ( CompanyName = '" + searchAndView.CompanyName + "') ";
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);
                    showDataGridView.DataSource = dataTable;
                    foreach (DataGridViewRow row in showDataGridView.Rows)
                    {
                        row.Cells["Sl"].Value = (row.Index + 1).ToString();
                    }
                }
                else
                {

                    sqlCommand.CommandText = @"SELECT * FROM ItemsSetupView WHERE ( CategoryName = '" + searchAndView.CategoryName + "') AND ( CompanyName = '" + searchAndView.CompanyName + "') ";

                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);
                    showDataGridView.DataSource = dataTable;
                    foreach (DataGridViewRow row in showDataGridView.Rows)
                    {
                        row.Cells["Sl"].Value = (row.Index + 1).ToString();
                    }
                }




            }
            catch (Exception exception)
            {

                MessageBox.Show(exception.Message);
            }
        }


        private void SearchAndViewItemsUi_Load(object sender, EventArgs e)
        {

        }

        private void categoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            searchAndView.CategoryName = categoryComboBox.Text;


            Connect();

            sqlCommand.CommandText = @"SELECT DISTINCT CompanyName FROM ItemsSetupView WHERE ( CategoryName = '" + searchAndView.CategoryName + "') ";

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);
            companyComboBox.Items.Clear();
            companyComboBox.Text = "";
            int execute = sqlCommand.ExecuteNonQuery();

            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {

                companyComboBox.Items.Add(dataSet.Tables[0].Rows[i][0]);
            }


        }

        

      

    }
}