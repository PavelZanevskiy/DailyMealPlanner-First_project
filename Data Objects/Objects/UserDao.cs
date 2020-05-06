using Business_Layer;
using Data_Objects.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Business_Layer.User;

namespace Data_Objects.Objects
{
    public class UserDao : IUserDao
    {
        User user= new User();
        public User SetUserInfo(double height,double weight,int age)
        {
            user.Height = height;
            user.Weight = weight;
            user.Age = age;
            return user;
        }
        public User SetActivity(Activity activity)
        {
            switch (activity)
            {
                case Activity.Low: 
                    user.ActivityIndex = 1.2;
                    break;
                case Activity.Normal:
                    user.ActivityIndex = 1.375;
                    break;
                case Activity.Medium:
                    user.ActivityIndex = 1.55;
                    break;
                case Activity.High:
                    user.ActivityIndex = 1.725;
                    break;
                default:
                    break;
            }
            return user;
        }
        public string GetARM(User user)
        {
            return user.ActivityIndex.ToString();
        }
        public string GetBMR(User user)
        {
            return (447.593 + 9.247 * user.Weight + 3.098 * user.Height - 4.330 * user.Age).ToString();
        }
        public Array GetActivities()
        {
            return Enum.GetValues(typeof(Activity));
        }
        public Activity DefaultActivity()
        {
            return Activity.Normal;
        }
        public string GetCalories(User user)
        {
            return (Convert.ToDouble(GetARM(user)) * Convert.ToDouble(GetBMR(user))).ToString();
        }
        public User GetUser()
        {
            return user;
        }
    }
}
