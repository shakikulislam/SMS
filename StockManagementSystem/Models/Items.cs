using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementSystem.Models
{
    public class Items
    {
        private int id;
        private string name;
        private int reorderLevel;
        private string companyId;
        private string categoryId;
        private int availableQuantity;

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

        public int ReorderLevel
        {
            get { return reorderLevel; }
            set { reorderLevel = value; }
        }

        public string CompanyId
        {
            get { return companyId; }
            set { companyId = value; }
        }

        public string CategoryId
        {
            get { return categoryId; }
            set { categoryId = value; }
        }

        public int AvailableQuantity
        {
            get { return availableQuantity; }
            set { availableQuantity = value; }
        }
    }
}
