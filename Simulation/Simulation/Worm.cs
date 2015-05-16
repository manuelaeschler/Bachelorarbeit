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
            
            return move();
        }

        private bool move()
        {
            bool changedHead = headOrTail(headX, headY, "head");
            bool changedTail = headOrTail(tailX, tailY, "tail");

            start = false;

            if (headX == tailX && headY == tailY)
                start = true;

            if (changedHead)
                return true;

            return changedTail;
        }

        private bool headOrTail(int x, int y, String inCase)
        {
            Brick oldBrick = field[x, y];
            String direc = direction();
            Brick newBrick = oldBrick.bondInOut(direc);

            double oldPos = oldBrick.Probability;
            double newPos = newBrick.Probability;
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

            if (rand.NextDouble() <= Math.Min(1, realPos))
            {
                field[x, y] = newBrick;
                changeNextVertex(direc, x, y, inCase);
                return true;
            }


            return false;
        }

        private void changeNextVertex(String direction, int x, int y, String inCase)
        {
            switch (direction)
            {
                case "up":
                    changeUp(x, y, inCase);
                    break;

                case "down":
                    changeDown(x, y, inCase);
                    break;

                case "left":
                    changeLeft(x, y, inCase);
                    break;

                case "right":
                    changeRight(x, y, inCase);
                    break;

            }
        }

        private void changeRight(int x, int y, String inCase)
        {
            if (x == size - 1)
                x = 0;
            else
                x++;

            field[x, y] = field[x, y].bondInOut("left");

            if (inCase == "head")
            {
                headX = x;
                headY = y;
            }
            else
            {
                tailX = x;
                tailY = y;
            }

        }

        private void changeLeft(int x, int y, String inCase)
        {
            if (x == 0)
                x = size - 1;
            else
                x--;

            field[x, y] = field[x, y].bondInOut("right");

            if (inCase == "head")
            {
                headX = x;
                headY = y;
            }
            else
            {
                tailX = x;
                tailY = y;
            }
        }

        private void changeDown(int x, int y, String inCase)
        {
            if (y == size - 1)
                y = 0;
            else
                y++;

            field[x, y] = field[x, y].bondInOut("up");

            if (inCase == "head")
            {
                headX = x;
                headY = y;
            }
            else
            {
                tailX = x;
                tailY = y;
            }
        }

        private void changeUp(int x, int y, String inCase)
        {
            if (y == 0)
                y = size - 1;
            else
                y--;

            field[x, y] = field[x, y].bondInOut("down");

            if (inCase == "head")
            {
                headX = x;
                headY = y;
            }
            else
            {
                tailX = x;
                tailY = y;
            }
        }


        private String direction()
        {
            double random = rand.NextDouble();

            if (random <= 0.25)
                return "up";

            if (random <= 0.5)
                return "down";

            if (random <= 0.75)
                return "left";

            return "right";

        }

    }
}
