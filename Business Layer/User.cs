using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    public class User
    {
        public double Height { get; set; }
        public double Weight { get; set; }
        public int Age { get; set; }
        public double ActivityIndex { get; set; }
        public double Calories { get; set; }
        public enum Activity 
        { 
            Low,
            Normal,
            Medium,
            High
        }
    }
}
