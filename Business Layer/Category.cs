using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    public class Category
    {
        public ObservableCollection<Product> Products { get; set; }
        public Category()
        {
            this.Products = new ObservableCollection<Product>();
        }
        public string Description { get; set; }
        public string Name { get; set; }
        
    }

    
}
