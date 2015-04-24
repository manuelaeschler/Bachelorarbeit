using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Simulation
{
    class DownLeft : Brick
    {
        static private float probability;
        Brick none;
        Brick full;
        Brick horizontal;
        Brick vertical;
        Brick upperLeft;
        Brick upperRight;
        Brick downRight;

        public DownLeft()
        {
            
        }

        public float Probability { get { return probability; } set { probability = value; } }

        public void draw(float x, float y, float brickSizeX, float brickSizeY, Pen pen, PaintEventArgs e, float size)
        {
            
            pen.Width = size;

            e.Graphics.DrawLine(pen, (int)x, (int)y, (int)(x), (int)(y + brickSizeY / 2));
            e.Graphics.DrawLine(pen, (int)x, (int)y, (int)(x - brickSizeX / 2), (int)y);

        }

        public Brick subtract(Brick brick)
        {
            if (brick is Full)
                return upperRight;

            if (brick is None)
                return this;

            if (brick is Vertical)
                return upperLeft;

            if (brick is Horizontal)
                return downRight;

            if (brick is UpperLeft)
                return vertical;

            if (brick is UpperRight)
                return full;

            if (brick is DownLeft)
                return none;

            if (brick is DownRight)
                return horizontal;

            return null;
        }

        public Brick getOpposite(int inCase)
        {
            switch (inCase)
            {
                case 1:
                    return horizontal;
                case 2:
                    return none;
                case 3:
                    return full;
                case 4:
                    return vertical;
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
            downRight = bricks[7];
        }
    }
}
