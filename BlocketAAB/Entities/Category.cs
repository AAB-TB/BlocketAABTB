using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlocketAAB.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public Category()
        {
        }

        
        public Category(string categoryName)
        {
            CategoryName = categoryName;
        }
    }
}
