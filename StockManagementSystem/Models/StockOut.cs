using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementSystem.Models
{
    public class StockOut
    {
        //public int ID { get; set; }
        //public string StockId { get; set; }
        //public string ItemId { get; set; }
        //public string StockOutQuantity { get; set; }
        //public string StockOutType { get; set; }
        //public string Date { get; set; }
        private int id;
        private string stockId;
        private string itemId;
        private string stockOutQuantity;
        private string stockOutType;
        private string date;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string StockId
        {
            get { return stockId; }
            set { stockId = value; }
        }

        public string ItemId
        {
            get { return itemId; }
            set { itemId = value; }
        }

        public string StockOutQuantity
        {
            get { return stockOutQuantity; }
            set { stockOutQuantity = value; }
        }

        public string StockOutType
        {
            get { return stockOutQuantity; }
            set { stockOutQuantity = value; }
        }

        public string Date
        {
            get { return date; }
            set { date = value; }
        }
        
    }
}
