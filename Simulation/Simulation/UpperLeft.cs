using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Simulation
{
    class UpperLeft : Brick
    {
        static private float probability;
        static private Color couplingColor;
        private PictureBox picture;
        private TrackBar bar;
        Brick none;
        Brick full;
        Brick horizontal;
        Brick vertical;
        Brick upperRight;
        Brick downLeft;
        Brick downRight;

        public UpperLeft(Color color, PictureBox picture, TrackBar bar)
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

            e.Graphics.DrawLine(pen, (int)x, (int)y, (int)(x), (int)(y - brickSizeY / 2));
            e.Graphics.DrawLine(pen, (int)x, (int)y, (int)(x - brickSizeX / 2), (int)y);

        }

        public Brick subtract(Brick brick)
        {
            if (brick is Full)
                return downRight;

            if (brick is None)
                return this;

            if (brick is Vertical)
                return downLeft;

            if (brick is Horizontal)
                return upperRight;

            if (brick is UpperLeft)
                return none;

            if (brick is UpperRight)
                return horizontal;

            if (brick is DownLeft)
                return vertical;

            if (brick is DownRight)
                return full;

            return null;
        }

        public Brick getOpposite(int inCase)
        {
            switch (inCase)
            {
                case 1:
                    return full;
                case 2:
                    return vertical;
                case 3:
                    return horizontal;
                case 4:
                    return none;
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
            upperRight = bricks[5];
            downLeft = bricks[6];
            downRight = bricks[7];
        }

    }
}
