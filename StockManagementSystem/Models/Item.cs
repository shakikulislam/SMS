using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementSystem.Models
{
    class Item
    {
    //    public int ID { get; set; }
    //    public string Name { get; set; }
        public string ReorderLevel { get; set; }
        public int CompanyId { get; set; }
        public int CategoryId { get; set; }

        private int id;
        private string name;
        //private string ReorderLevel;
        //private int CompanyId;
        //private int CategoryId;


        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        
       
    }
}
