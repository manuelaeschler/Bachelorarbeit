using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation
{
    class Worm : Algorithm
    {
        private Brick[,] field;
        Random rand;

        public Worm(Brick[,] field)
        {
            this.Field = field;
            rand = new Random();
        }

        public Brick[,] Field { get { return field; } set { field = value; } }

        public bool change(int x, int y)
        {
            return false;
        }
    }
}
