using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Business_Layer
{
    [Serializable]
    public class DailyRation
    {
        public List<MealTime> mealTimes = new List<MealTime>();
        
    }
}
