using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Simulation
{
    class Full : Brick
    {
        static private float probability;
        static private Color backColor;
        Brick none;
        Brick horizontal;
        Brick vertical;
        Brick upperLeft;
        Brick upperRight;
        Brick downLeft;
        Brick downRight;

        public Full(Color backColor)
        {
            this.BackColor = backColor;
        }

        public float Probability { get { return probability; } set { probability = value; } }

        public Color BackColor { get { return backColor; } set { backColor = value; } }

        public void draw(float x, float y, float brickSizeX, float brickSizeY, Pen pen, PaintEventArgs e, float size)
        {
            pen.Width = size;

            e.Graphics.DrawLine(pen, (int)x, (int)y, (int)(x), (int)(y - brickSizeY / 2));
            e.Graphics.DrawLine(pen, (int)x, (int)y, (int)(x), (int)(y + brickSizeY / 2));

            e.Graphics.DrawLine(pen, (int)x, (int)y, (int)(x - brickSizeX / 2), (int)y);
            e.Graphics.DrawLine(pen, (int)x, (int)y, (int)(x + brickSizeX / 2), (int)y);

        }

        public Brick subtract(Brick brick)
        {
            if (brick is Full)
                return none;

            if (brick is None)
                return this;

            if (brick is Vertical)
                return horizontal;

            if (brick is Horizontal)
                return vertical;

            if (brick is UpperLeft)
                return downRight;

            if (brick is UpperRight)
                return downLeft;

            if (brick is DownLeft)
                return upperRight;

            if (brick is DownRight)
                return upperLeft;

            return null;
        }

        public Brick getOpposite(int inCase)
        {
            switch (inCase)
            {
                case 1:
                    return upperLeft;
                case 2:
                    return upperRight;
                case 3:
                    return downLeft;
                case 4:
                    return downRight;
                default:
                    return this;

            }
        }

        public void setBricks(Brick[] bricks)
        {
            none = bricks[0];
            horizontal = bricks[2];
            vertical = bricks[3];
            upperLeft = bricks[4];
            upperRight = bricks[5];
            downLeft = bricks[6];
            downRight = bricks[7];
        }
    }
}
