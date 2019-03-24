using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementSystem
{
    class ItemSetup
    {
        private int id;
        private string itemName;
        private int companyId;
        private int catagoryId;
        private int reorderLevel;
        //public List<string> Nmaes;

        public int Id {
            get { return id; }
            set { id = value; }
        }
        public string ItemName 
        {
            get { return itemName; }
            set { itemName = value; } 
        }
        public int CompanyId
        {
            get { return companyId; }
            set { companyId = value; } 
        }
        public int CatagoryId 
        {
            get { return catagoryId; }
            set { catagoryId = value; }
        }
        public int ReorderLevel 
        { 
            get {return reorderLevel; }
            set { reorderLevel = value; } 
        }

       
    }
}
