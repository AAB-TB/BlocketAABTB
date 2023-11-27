using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlocketAAB.Entities
{
    public class Advertisement
    {   
        public int AdvertisementId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }

       
        public Advertisement()
        {
        }

      
        public Advertisement(string title, string description, decimal price, int categoryId)
        {
            Title = title;
            Description = description;
            Price = price;
            CategoryId = categoryId;
        }
        
    }
}
