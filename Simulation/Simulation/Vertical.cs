using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Simulation
{
    class Vertical : Brick
    {

        static private float probability;
        Brick none;
        Brick full;
        Brick horizontal;
        Brick upperLeft;
        Brick upperRight;
        Brick downLeft;
        Brick downRight;

        public Vertical()
        {
            
        }

        public float Probability { get { return probability; } set { probability = value; } }

        public void draw(float x, float y, float brickSizeX, float brickSizeY, Pen pen, PaintEventArgs e, float size)
        {
            pen.Width = size;

            e.Graphics.DrawLine(pen, (int)x, (int)y, (int)(x), (int)(y - brickSizeY/2));
            e.Graphics.DrawLine(pen, (int)x, (int)y, (int)(x), (int)(y + brickSizeY / 2));

        }

        public Brick subtract(Brick brick)
        {
            if (brick is Full)
                return horizontal;

            if (brick is None)
                return this;

            if (brick is Vertical)
                return none;

            if (brick is Horizontal)
                return full;

            if (brick is UpperLeft)
                return downLeft;

            if (brick is UpperRight)
                return downRight;

            if (brick is DownLeft)
                return upperLeft;

            if (brick is DownRight)
                return upperRight;

            return null;
        }


        public Brick getOpposite(int inCase)
        {
            switch (inCase)
            {
                
                case 1:
                    return upperRight;
                case 2:
                    return upperLeft;
                case 3:
                    return downRight;
                case 4:
                    return downLeft;
                default:
                    return this;

            }
        }

        public void setBricks(Brick[] bricks)
        {
            none = bricks[0];
            full = bricks[1];
            horizontal = bricks[2];
            upperLeft = bricks[4];
            upperRight = bricks[5];
            downLeft = bricks[6];
            downRight = bricks[7];
        }
    }
}
