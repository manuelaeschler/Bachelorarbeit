using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI
{
    class RandomGenerator
    {
        Random rand;
        public RandomGenerator()
        {
            rand = new Random();
        }

        public double GetRandomNumber()
        {
            
            double x = rand.NextDouble();
            x = x * 2 - 1;
            return x;
        }
    }
}
