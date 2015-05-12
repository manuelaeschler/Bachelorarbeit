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
        static private Color couplingColor;
        private PictureBox picture;
        private TrackBar bar;
        private TextBox display;

        Brick none;
        Brick full;
        Brick horizontal;
        Brick upperLeft;
        Brick upperRight;
        Brick downLeft;
        Brick downRight;

        Brick noneUp;
        Brick noneDown;
        Brick noneLeft;
        Brick noneRight;
        Brick up;
        Brick down;
        Brick left;
        Brick right;

        public Vertical(Color color, PictureBox picture, TrackBar bar, TextBox display)
        {
            this.CouplingColor = color;
            this.Bar = bar;
            this.Picture = picture;
            this.Display = display;
        }

        public float Probability
        {
            get { return probability; }
            set
            {
                probability = value;
                display.Text = probability.ToString();
            }
        }

        public Color CouplingColor { get { return couplingColor; } set { couplingColor = value; } }

        public PictureBox Picture { get { return picture; } set { picture = value; } }

        public TrackBar Bar { get { return bar; } set { bar = value; } }

        public TextBox Display { set { display = value; } }

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

            if (brick is NoneUp)
                return noneDown;

            if (brick is NoneDown)
                return noneUp;

            if (brick is NoneLeft)
                return right;

            if (brick is NoneRight)
                return left;

            if (brick is Up)
                return down;

            if (brick is Down)
                return up;

            if (brick is Left)
                return noneRight;

            if (brick is Right)
                return noneLeft;

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

            noneUp = bricks[8];
            noneDown = bricks[9];
            noneLeft = bricks[10];
            noneRight = bricks[11];
            up = bricks[12];
            down = bricks[13];
            left = bricks[14];
            right = bricks[15];
        }

        public Brick bondInOut(string inCase)
        {
            switch (inCase)
            {
                case "up":
                    return down;
                case "down":
                    return up;
                case "left":
                    return noneRight;
                case "right":
                    return noneLeft;
                default:
                    return this;

            }
        }

    }
}
