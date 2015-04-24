using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Simulation
{
    class UpperRight : Brick
    {
        static private float probability;
        Brick none;
        Brick full;
        Brick horizontal;
        Brick vertical;
        Brick upperLeft;
        Brick downLeft;
        Brick downRight;

        public UpperRight()
        {
            
        }

        public float Probability { get { return probability; } set { probability = value; } }

        public void draw(float x, float y, float brickSizeX, float brickSizeY, Pen pen, PaintEventArgs e, float size)
        {
            pen.Width = size;

            e.Graphics.DrawLine(pen, (int)x, (int)y, (int)(x + brickSizeX / 2), (int)y);
            e.Graphics.DrawLine(pen, (int)x, (int)y, (int)(x), (int)(y - brickSizeY / 2));

        }

        public Brick subtract(Brick brick)
        {
            if (brick is Full)
                return downLeft;

            if (brick is None)
                return this;

            if (brick is Vertical)
                return downRight;

            if (brick is Horizontal)
                return upperLeft;

            if (brick is UpperLeft)
                return horizontal;

            if (brick is UpperRight)
                return none;

            if (brick is DownLeft)
                return full;

            if (brick is DownRight)
                return vertical;

            return null;
        }

        public Brick getOpposite(int inCase)
        {
            switch (inCase)
            {
                case 1:
                    return vertical;
                case 2:
                    return full;
                case 3:
                    return none;
                case 4:
                    return horizontal;
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
            downLeft = bricks[6];
            downRight = bricks[7];
        }
    }
}
