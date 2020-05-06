using Business_Layer;
using Data_Objects;
using Data_Objects.Objects;
using Data_Objects.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class Service
    {
        static readonly IProductDao productDao = new ProductDao();
        static readonly IDailyRationDao dailyRationDao = new DailyRationDao();
        static readonly IUserDao userDao = new UserDao();

        public List<Product> GetProductsToList()
        {
            return productDao.GetProducts();
        }
        public string GetProdName(Product product)
        {
            return productDao.GetProductName(product);
        }
        public Product GetProduct(Product product)
        {
            return productDao.GetProduct(product);
        }
        public List<MealTime> GetRation()
        {
            return dailyRationDao.GetRation();
        }
        public List<MealTime> AddMealTime(string name)
        {
            return dailyRationDao.AddMealTime(name);
        }
        public List<MealTime> AddProduct(Product product, string name)
        {
            return dailyRationDao.AddProduct(product, name);
        }
        public List<MealTime> RemoveMealTime(string name)
        {
            return dailyRationDao.RemoveMealTime(name);
        }
        public List<MealTime> RemoveProduct(int ID)
        {
            return dailyRationDao.RemoveProduct(ID);
        }
        public User SetUserInfo(double height, double weight, int age)
        {
            return userDao.SetUserInfo(height, weight, age);
        }
        public string GetBMR(User user)
        {
            return userDao.GetBMR(user);
        }
        public User.Activity DefaultActivity()
        {
            return userDao.DefaultActivity();
        }
        public Array GetActivity()
        {
            return userDao.GetActivities();
        }
        public User SetActivity(User.Activity activity)
        {
            return userDao.SetActivity(activity);
        }
        public string GetARM(User user)
        {
            return userDao.GetARM(user);
        }
        public string GetCalories(User user)
        {
            return userDao.GetCalories(user);
        }
        public User GetUser()
        {
            return userDao.GetUser();
        }
        public MealTime GetDefaultMeal()
        {
            return dailyRationDao.GetDefaultMeal();
        }
        public void ChangeWeight(int Id, double value, string name)
        {
            dailyRationDao.ChangeWeight(Id,value,name);
        }
        public double GetCaloriesSum()
        {
            return dailyRationDao.GetCaloriesSum();
        }
        public double CheckCaloriesSum(Product product)
        {
            return dailyRationDao.CheckCaloriesSum(product);
        }
        public double TestCalories(int Id, double value, string name)
        {
            return dailyRationDao.TestCalories(Id, value, name);
        }
        public void ClearRation()
        {
            dailyRationDao.ClearRation();
        }
        public MealProduct GetMealProduct(MealProduct product)
        {
            return dailyRationDao.GetMealProduct(product);
        }
        public void SerializeRation()
        {
            dailyRationDao.SerializeRation();
        }
    }

}
