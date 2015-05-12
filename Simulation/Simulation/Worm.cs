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
        int size;
        float alpha;
        static bool start;

        static int headX;
        static int headY;
        static int tailX;
        static int tailY;

        public Worm(Brick[,] field)
        {
            this.Field = field;
            this.size = field.GetLength(0);
            rand = new Random();
            alpha = .9f;
            start = true;
        }

        public Brick[,] Field { get { return field; } set { field = value; size = field.GetLength(0); } }

        public bool change(int x, int y)
        {
            if (start)
            {
                if(alpha >= rand.NextDouble())
                {
                    headX = x;
                    headY = y;
                    tailX = x;
                    tailY = y;
                    return moveStart();
                }
                else
                    return false; 
            }
            else
            {
                return move();
            }
        }

        private bool moveStart()
        {
            


            return false;
        }

        private bool move()
        {





            return false;
        }
    }
}
