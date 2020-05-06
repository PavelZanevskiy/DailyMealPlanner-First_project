using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    public class MealTime
    {
        public ObservableCollection<MealProduct> Products { get; set; }
        public MealTime()
        {
            this.Products = new ObservableCollection<MealProduct>();
        }
        public string Name { get; set; }
    }
}
