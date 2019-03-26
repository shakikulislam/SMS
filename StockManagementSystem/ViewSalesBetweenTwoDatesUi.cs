using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using StockManagementSystem.Models;

namespace StockManagementSystem
{
    public partial class ViewSalesBetweenTwoDatesUi : Form
    {
        private SqlCommand sqlCommand;
        private SqlDataAdapter sqlDataAdapter;
        private DataTable dataTable;

        private void Connect()
        {
            DatabaseConnection databaseConnection = new DatabaseConnection();
            databaseConnection.ConnectionDatabase();
            sqlCommand = new SqlCommand();
            sqlCommand.Connection = DatabaseConnection.sqlConnection;
        }

        public ViewSalesBetweenTwoDatesUi()
        {
            InitializeComponent();

            fromDateTimePicker.MaxDate=DateTime.Today;
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            displayDataGridView.DataSource = null;

            displayDataGridView.ColumnCount = 1;
            displayDataGridView.Columns[0].Name = "SL";
            displayDataGridView.Columns[0].Width = 50;

            dataTable = Display();
            displayDataGridView.DataSource = dataTable;
            displayDataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            displayDataGridView.Columns[2].Width = 100;

            foreach (DataGridViewRow row in displayDataGridView.Rows)
            {
                row.Cells["SL"].Value = (row.Index + 1).ToString();
            }
        }

        public DataTable Display()
        {
            try
            {
                Connect();
                sqlCommand.CommandText =
                    "SELECT Name, SUM(StockOutQuantity) AS 'Sale Quantity' FROM SalesView  WHERE Date>='" +
                    fromDateTimePicker.Value.ToShortDateString() + "' AND Date<='" +
                    toDateTimePicker.Value.ToShortDateString() + "' GROUP BY Name";
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

        private void fromDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            toDateTimePicker.Enabled = true;
            toDateTimePicker.MinDate = fromDateTimePicker.Value;
        }
    }
}
