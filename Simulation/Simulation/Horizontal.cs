
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Simulation
{
    class Horizontal : Brick
    {
        static private float probability;
        static private Color couplingColor;
        private PictureBox picture;
        private TrackBar bar;
        Brick none;
        Brick full;
        Brick vertical;
        Brick upperLeft;
        Brick upperRight;
        Brick downLeft;
        Brick downRight;

        public Horizontal(Color color, PictureBox picture, TrackBar bar)
        {
            this.CouplingColor = color;
            this.Bar = bar;
            this.Picture = picture;
        }

        public float Probability { get { return probability; } set { probability = value; } }

        public Color CouplingColor { get { return couplingColor; } set { couplingColor = value; } }

        public PictureBox Picture { get { return picture; } set { picture = value; } }

        public TrackBar Bar { get { return bar; } set { bar = value; } }

        public void draw(float x, float y, float brickSizeX, float brickSizeY, Pen pen, PaintEventArgs e, float size)
        {
            pen.Width = size;

            e.Graphics.DrawLine(pen, (int)x, (int)y, (int)(x - brickSizeX / 2), (int)y);
            e.Graphics.DrawLine(pen, (int)x, (int)y, (int)(x + brickSizeX / 2), (int)y);

        }

        public Brick subtract(Brick brick)
        {
            if (brick is Full)
                return vertical;

            if (brick is None)
                return this;

            if (brick is Vertical)
                return full;

            if (brick is Horizontal)
                return none;

            if (brick is UpperLeft)
                return upperRight;

            if (brick is UpperRight)
                return upperLeft;

            if (brick is DownLeft)
                return downRight;

            if (brick is DownRight)
                return downLeft;

            return null;
        }

        public Brick getOpposite(int inCase)
        {
            switch (inCase)
            {
                case 1:
                    return downLeft;
                case 2:
                    return downRight;
                case 3:
                    return upperLeft;
                case 4:
                    return upperRight;
                default:
                    return this;

            }
        }

        public void setBricks(Brick[] bricks)
        {
            none = bricks[0];
            full = bricks[1];
            vertical = bricks[3];
            upperLeft = bricks[4];
            upperRight = bricks[5];
            downLeft = bricks[6];
            downRight = bricks[7];
        }

    }
}
