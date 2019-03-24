using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementSystem.Models
{
    class SearchAndView
    {
        private string category;
        private string company;

        public string Category
        {
            get { return category ; }
            set { category = value; }
        }
        public string Company
        {
            get { return company; }
            set { company = value; }
        }
    }
}
