using Business_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Business_Layer.User;

namespace Data_Objects.Interfaces
{
    public interface IUserDao
    {
        User SetUserInfo(double height, double weight, int age);
        string GetBMR(User user);
        Activity DefaultActivity();
        Array GetActivities();
        User SetActivity(Activity activity);
        string GetARM(User user);
        string GetCalories(User user);
        User GetUser();
    }
}
