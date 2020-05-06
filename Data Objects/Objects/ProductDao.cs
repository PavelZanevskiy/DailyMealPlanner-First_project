using Business_Layer;
using Data_Objects.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Objects.Objects
{ 
    public class ProductDao : IProductDao
    {
        Database database = new Database();
        public List<Product> GetProducts()
        {
            return database.products;
        }
        public Product GetProduct(Product product)
        {
            for (int i = 0; i < database.products.Count; i++)
            {
                if (product == database.products[i])
                {
                    return database.products[i];
                }
            }
            return null;
        }
        public string GetProductName(Product product)
        {
            for (int i = 0; i < database.products.Count; i++)
            {
                if (product == database.products[i])
                {
                    return database.products[i].Name;
                }
            }
            return null;
        }
    }
}
