using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public class Class1
    {
        public static int DiceRolling()
        {
            Random number = new Random();
            return number.Next(1, 7);
        }
    }
}
