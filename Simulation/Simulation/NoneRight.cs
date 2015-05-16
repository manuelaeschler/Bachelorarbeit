using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Simulation
{
    class NoneRight : Brick
    {
        private float probability;

        Brick none;
        Brick full;
        Brick horizontal;
        Brick vertical;
        Brick upperLeft;
        Brick upperRight;
        Brick downLeft;
        Brick downRight;

        Brick noneUp;
        Brick noneDown;
        Brick noneLeft;
        Brick up;
        Brick down;
        Brick left;
        Brick right;

        Pen penRed;

        public NoneRight()
        {
            penRed = new Pen(Color.Red);
        }

        public void draw(float x, float y, float brickSizeX, float brickSizeY, Pen pen, PaintEventArgs e, float size)
        {
            pen.Width = size;

            e.Graphics.DrawLine(pen, (int)x, (int)y, (int)(x), (int)(y - brickSizeY / 2));
            e.Graphics.DrawLine(pen, (int)x, (int)y, (int)(x - brickSizeX / 2), (int)y);
            e.Graphics.DrawLine(pen, (int)x, (int)y, (int)(x), (int)(y + brickSizeY / 2));
            e.Graphics.DrawEllipse(penRed, (int)x - 2, (int)y - 2, 4f, 4f);
        }

        public Brick getOpposite(int inCase)
        {
            return null;
        }

        
        public Brick subtract(Brick brick)
        {
            if (brick is Full)
                return right;

            if (brick is None)
                return this;

            if (brick is Vertical)
                return left;

            if (brick is Horizontal)
                return noneLeft;

            if (brick is UpperLeft)
                return down;

            if (brick is UpperRight)
                return noneUp;

            if (brick is DownLeft)
                return up;

            if (brick is DownRight)
                return noneDown;

            if (brick is NoneUp)
                return upperRight;

            if (brick is NoneDown)
                return downRight;

            if (brick is NoneLeft)
                return horizontal;

            if (brick is NoneRight)
                return none;

            if (brick is Up)
                return downLeft;

            if (brick is Down)
                return upperLeft;

            if (brick is Left)
                return vertical;

            if (brick is Right)
                return full;
 
            return null;

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
            downRight = bricks[7];

            noneUp = bricks[8];
            noneDown = bricks[9];
            noneLeft = bricks[10];
            up = bricks[12];
            down = bricks[13];
            left = bricks[14];
            right = bricks[15];
        }

        public float Probability { get { return probability; } set { probability = value; } }

        public Color CouplingColor { get { return Color.Empty; } set { } }

        public PictureBox Picture { get { return null; } set { } }

        public TrackBar Bar { get { return null; } set { } }

        public TextBox Display { set { } }

        public Brick bondInOut(string inCase)
        {
            switch (inCase)
            {
                case "up":
                    return downLeft;
                case "down":
                    return upperLeft;
                case "left":
                    return vertical;
                case "right":
                    return full;
                default:
                    return this;

            }
        }
    }
}
