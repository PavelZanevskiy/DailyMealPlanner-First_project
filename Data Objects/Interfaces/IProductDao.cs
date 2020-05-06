using Business_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Objects.Interfaces
{
    public interface IProductDao
    {
        List<Product> GetProducts();
        string GetProductName(Product product);
        Product GetProduct(Product product);
    }
}
