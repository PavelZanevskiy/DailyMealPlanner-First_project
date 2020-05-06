using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Business_Layer;

namespace Data_Objects
{
    public class Database
    {
        public Database()
        {
            ReadFile();
        }
        private static readonly string FILE_NAME = "D:/DailyMeal/projectOOP/FoodProducts.xml";
        public List<Category> categories=new List<Category>();
        public List<Product> products = new List<Product>();
        private Category category;
        private Product product;
        XDocument xDoc = XDocument.Load(FILE_NAME);
        public void ReadFile()
        {
            foreach (XElement catEl in xDoc.Element("Db").Elements("Category"))
            {

                category = new Category();
                category.Name = catEl.Attribute("name").Value;
                category.Description = catEl.Attribute("description").Value;
                categories.Add(category);
            }
            string name = null;
            int i = 0;
            foreach (XElement prodEl in xDoc.Element("Db").Elements("Category").Elements("Product"))
            {
                if (name != prodEl.Parent.Attribute("name").Value)
                {
                    if (name != null)
                    {
                        i++;
                    }
                    name = prodEl.Parent.Attribute("name").Value;
                }

                product = new Product();
                product.Name = prodEl.Element("Name").Value;
                product.Grams = Convert.ToInt32(prodEl.Element("Gramms").Value);
                product.Proteins = Convert.ToDouble(prodEl.Element("Protein").Value);
                product.Fats = Convert.ToDouble(prodEl.Element("Fats").Value);
                product.Carbs = Convert.ToDouble(prodEl.Element("Carbs").Value);
                product.Calories = Convert.ToDouble(prodEl.Element("Calories").Value);
                product.Category = categories[i].Name;
                products.Add(product);
                categories[i].Products.Add(product);
            }
        }

    }
}
