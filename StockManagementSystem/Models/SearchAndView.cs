using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementSystem.Models
{
    class SearchAndView
    {
        private string name;
        private string categoryName;
        private string companyName;
        private int availableQuantity;
        private int reorderLevel;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
        }

        public string CompanyName
        {
            get { return companyName; }
            set { companyName = value; }
        }

        public int AvailableQuantity
        {
            get { return availableQuantity; }
            set { availableQuantity = value; }
        }

        public int ReorderLevel
        {
            get { return reorderLevel; }
            set { reorderLevel = value; }
        }
    }
}
