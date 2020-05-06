using Business_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Objects.Interfaces
{
    public interface IDailyRationDao
    {
        List<MealTime> GetRation();
        List<MealTime> AddMealTime(string name);
        List<MealTime> AddProduct(Product product, string name);
        List<MealTime> RemoveMealTime(string name);
        List<MealTime> RemoveProduct(int ID);
        MealTime GetDefaultMeal();
        void ChangeWeight(int Id, double value, string name);
        double GetCaloriesSum();
        double CheckCaloriesSum(Product product);
        double TestCalories(int Id, double value, string name);
        void ClearRation();
        MealProduct GetMealProduct(MealProduct product);
        void SerializeRation();
    }
}
