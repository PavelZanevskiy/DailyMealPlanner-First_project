using Business_Layer;
using Data_Objects.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Data_Objects.Objects
{
    public class DailyRationDao : IDailyRationDao
    {
        public DailyRation dailyRation = new DailyRation();
        int ID = 0;
        Database database = new Database();
        public List<MealTime> GetRation()
        {
            
            for (int i = 0; i < 3; i++)
            {
                dailyRation.mealTimes.Add(new MealTime());
            }
            dailyRation.mealTimes[0].Name = "Завтрак";
            dailyRation.mealTimes[1].Name = "Обед";
            dailyRation.mealTimes[2].Name = "Ужин";
            return dailyRation.mealTimes;
        }
        public MealProduct GetMealProduct(MealProduct product)
        {
            for (int i = 0; i < dailyRation.mealTimes.Count; i++)
            {
                for (int j = 0; j < dailyRation.mealTimes[i].Products.Count; j++)
                {
                    if (product.ID == dailyRation.mealTimes[i].Products[j].ID)
                    {
                        return dailyRation.mealTimes[i].Products[j];
                    }
                }
            }
            return null;
        }

        public List<MealTime> AddMealTime(string name)
        {
            int counter = 0;
            for(int i = 0; i < dailyRation.mealTimes.Count; i++)
            {
                if (dailyRation.mealTimes[i].Name == name)
                {
                    counter++;
                    name += counter.ToString();  
                }
            }
            dailyRation.mealTimes.Add(new MealTime() { Name = name });
            return dailyRation.mealTimes;
        }

        public List<MealTime> RemoveMealTime(string name)
        {
            for (int i = 0; i < dailyRation.mealTimes.Count; i++)
            {
                if (dailyRation.mealTimes[i].Name == name)
                {
                    dailyRation.mealTimes.Remove(dailyRation.mealTimes[i]);
                    return dailyRation.mealTimes;
                }
            }
            return null;
        }

        public List<MealTime> AddProduct(Product product,string name)
        {
            for (int i = 0; i < dailyRation.mealTimes.Count; i++)
            {
                if (dailyRation.mealTimes[i].Name==name)
                {
                    MealProduct mealProduct = new MealProduct
                    {
                        Name = product.Name,
                        Fats = product.Fats,
                        Proteins = product.Proteins,
                        Carbs = product.Carbs,
                        Calories = product.Calories,
                        Grams = product.Grams,
                        ID = ID
                    };
                    ID++;
                    dailyRation.mealTimes[i].Products.Add(mealProduct);
                    return dailyRation.mealTimes;
                }
            }
            return null;
        }

        public List<MealTime> RemoveProduct(int ID)
        {
            for (int i = 0; i < dailyRation.mealTimes.Count; i++)
            {
               for(int j = 0; j < dailyRation.mealTimes[i].Products.Count;j++)
                {
                    if (dailyRation.mealTimes[i].Products[j].ID == ID)
                    {
                        dailyRation.mealTimes[i].Products.Remove(dailyRation.mealTimes[i].Products[j]);
                        return dailyRation.mealTimes;
                    }
                }
            }
            return null;
        }
        public MealTime GetDefaultMeal()
        {
            if (dailyRation.mealTimes.Count != 0)
            {
                return dailyRation.mealTimes[0];
            }
            return null;
        }

        public void ChangeWeight(int Id,double value,string name)
        {
            for(int i = 0; i < dailyRation.mealTimes.Count; i++)
            {
                for(int j = 0;j < dailyRation.mealTimes[i].Products.Count;j++)
                {
                    if (dailyRation.mealTimes[i].Products[j].ID == Id)
                    {
                        dailyRation.mealTimes[i].Products[j].Grams = Convert.ToInt32(value);
                        for (int k = 0; k < database.products.Count; k++)
                        {
                            if (database.products[k].Name == name)
                            {
                                dailyRation.mealTimes[i].Products[j].Calories = ((value * database.products[k].Calories) / 100);
                            }
                        }

                    }
                }
            }
        }
        public double TestCalories(int Id, double value, string name)
        {
            double temp=0;
            value = Convert.ToInt32(value);
            for (int i = 0; i < dailyRation.mealTimes.Count; i++)
            {
                for (int j = 0; j < dailyRation.mealTimes[i].Products.Count; j++)
                {
                    if (dailyRation.mealTimes[i].Products[j].ID == Id)
                    {
                        double diff = GetCaloriesSum() - dailyRation.mealTimes[i].Products[j].Calories;

                        for (int k = 0; k < database.products.Count; k++)
                        {
                            if (database.products[k].Name == name)
                            {
                                temp = ((value * database.products[k].Calories) / 100) + GetCaloriesSum()-diff;
                                return temp;
                            }
                        }
                    }
                }
            }
            return 0;
        }
        
        public double GetCaloriesSum()
        {
            double caloriesSum = 0;
            for (int i = 0; i < dailyRation.mealTimes.Count; i++)
            {
                for (int j = 0; j < dailyRation.mealTimes[i].Products.Count; j++)
                {
                    caloriesSum += dailyRation.mealTimes[i].Products[j].Calories;
                }
            }
            return caloriesSum;
        }
        
        public double CheckCaloriesSum(Product product)
        {
            double caloriesSum = 0;
            caloriesSum += GetCaloriesSum() + product.Calories;               
            return caloriesSum;
        }
       
        public void ClearRation()
        {
            dailyRation.mealTimes.Clear();
        }
        public void SerializeRation()
        {
            XmlSerializer formatter = new XmlSerializer(typeof(DailyRation));

            using (FileStream fs = new FileStream("Ration.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, dailyRation);
            }
        }
        
    }
}


