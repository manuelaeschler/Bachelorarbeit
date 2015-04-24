using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation
{
    class Flip : Algorithm
    {
        private Brick[,] field;
        private float[,] probabilities;
        Random rand;

        Brick old1;
        Brick old2;
        Brick old3;
        Brick old4;

        Brick new1;
        Brick new2;
        Brick new3;
        Brick new4;

        public Flip(Brick[,] field, float[,] probabilities)
        {
            this.rand = new Random();
            this.field = field;
            this.probabilities = probabilities;
        }

        public float[,] Probabilities { set { probabilities = value; } }
        public Brick[,] Field { get { return field; } set { field = value; } }

        public Boolean change(int x, int y)
        {
            if (x + 1 != field.GetLength(0))
            {
                if(y + 1 != field.GetLength(1))
                    return ifPossible(x, x + 1, y, y + 1);
                else
                    return ifPossible(x, x + 1, y, 0);
            }
            else
            {
                if (y + 1 != field.GetLength(1))
                    return ifPossible(x, 0, y, y + 1);
                else
                    return ifPossible(x, 0, y, 0);
            }

        }


        private Boolean ifPossible(int x1, int x2, int y1, int y2)
        {
            old1 = field[x1, y1];
            old2 = field[x2, y1];
            old3 = field[x1, y2];
            old4 = field[x2, y2];

            new1 = old1.getOpposite(1);
            new2 = old2.getOpposite(2);
            new3 = old3.getOpposite(3);
            new4 = old4.getOpposite(4);

            double pos = rand.NextDouble();
            
            double oldPos = old1.Probability * old2.Probability * old3.Probability * old4.Probability;
            double newPos = new1.Probability * new2.Probability * new3.Probability * new4.Probability;

            double realPos;

            if (oldPos != 0)
                realPos = newPos / oldPos;
            else
            {
                if (newPos != 0)
                    realPos = 1;
                else
                    realPos = 0;
            }
                

            
            if(pos <= Math.Min(1, realPos))
            {
                field[x1, y1] = new1;
                field[x2, y1] = new2;
                field[x1, y2] = new3;
                field[x2, y2] = new4;
                return true;
            }
            return false;
        }
    }
}
