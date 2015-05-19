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
        static private Color couplingColor;
        private PictureBox picture;
        private TrackBar bar;
        private TextBox display;

        Brick none;
        Brick full;
        Brick horizontal;
        Brick vertical;
        Brick upperLeft;
        Brick upperRight;
        Brick downRight;

        Brick noneUp;
        Brick noneDown;
        Brick noneLeft;
        Brick noneRight;
        Brick up;
        Brick down;
        Brick left;
        Brick right;

        public DownLeft(Color color, PictureBox picture, TrackBar bar, TextBox display)
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

		public float StartProbability { get { return 0; } set { } }

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

            if (brick is NoneUp)
                return right;

            if (brick is NoneDown)
                return noneLeft;

            if (brick is NoneLeft)
                return noneDown;

            if (brick is NoneRight)
                return up;

            if (brick is Up)
                return noneRight;

            if (brick is Down)
                return left;

            if (brick is Left)
                return down;

            if (brick is Right)
                return noneUp;

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
                    return noneRight;
                case "down":
                    return left;
                case "left":
                    return down;
                case "right":
                    return noneUp;
                default:
                    return this;

            }
        }

    }
}
