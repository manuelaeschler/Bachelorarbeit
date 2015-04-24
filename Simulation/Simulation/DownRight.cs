using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Simulation
{
    class DownRight : Brick
    {
        static private float probability;
        Brick none;
        Brick full;
        Brick horizontal;
        Brick vertical;
        Brick upperLeft;
        Brick upperRight;
        Brick downLeft;

        public DownRight()
        {
            
        }

        public float Probability { get { return probability; } set { probability = value; } }

        public void draw(float x, float y, float brickSizeX, float brickSizeY, Pen pen, PaintEventArgs e, float sizeSmall, float sizeBig)
        {
            if (sizeSmall != 0)
            {
                pen.Width = sizeSmall;

                e.Graphics.DrawLine(pen, (int)x, (int)y, (int)(x), (int)(y - brickSizeY / 2));
                e.Graphics.DrawLine(pen, (int)x, (int)y, (int)(x - brickSizeX / 2), (int)y); 
            }

            pen.Width = sizeBig;

            e.Graphics.DrawLine(pen, (int)x, (int)y, (int)(x), (int)(y + brickSizeY / 2));
            e.Graphics.DrawLine(pen, (int)x, (int)y, (int)(x + brickSizeX / 2), (int)y);

        }

        public Brick subtract(Brick brick)
        {
            if (brick is Full)
                return upperLeft;

            if (brick is None)
                return this;

            if (brick is Vertical)
                return upperRight;

            if (brick is Horizontal)
                return downLeft;

            if (brick is UpperLeft)
                return full;

            if (brick is UpperRight)
                return vertical;

            if (brick is DownLeft)
                return horizontal;

            if (brick is DownRight)
                return none;

            return null;
        }

        public Brick getOpposite(int inCase)
        {
            switch (inCase)
            {
                case 1:
                    return none;
                case 2:
                    return horizontal;
                case 3:
                    return vertical;
                case 4:
                    return full;
                default:
                    return this;

            }
        }

        public void setBricks(Brick[] bricks)
        {
            none = bricks[0];
            full = bricks[1];
            horizontal = bricks[2];
            vertical = bricks[3];
            upperLeft = bricks[4];
            upperRight = bricks[5];
            downLeft = bricks[6];
        }
    }
}
