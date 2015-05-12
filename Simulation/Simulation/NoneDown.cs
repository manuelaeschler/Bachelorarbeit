using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Simulation
{
    class NoneDown : Brick
    {
        private float probability;
        private Color backColor;

        public NoneDown()
        {

        }

        public void draw(float x, float y, float brickSizeX, float brickSizeY, Pen pen, PaintEventArgs e, float size)
        {
            pen.Width = size;

            e.Graphics.DrawLine(pen, (int)x, (int)y, (int)(x), (int)(y + brickSizeY / 2));
            e.Graphics.DrawLine(pen, (int)x, (int)y, (int)(x - brickSizeX / 2), (int)y);
            e.Graphics.DrawLine(pen, (int)x, (int)y, (int)(x + brickSizeX / 2), (int)y);

        }

        public Brick getOpposite(int inCase)
        {
            return null;
        }

        //TO DO
        public Brick subtract(Brick brick)
        {/*
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
 */
            return null;

        }

        //TO DO
        public void setBricks(Brick[] bricks)
        {
            throw new NotImplementedException();
        }

        public float Probability { get { return probability; } set { probability = value; } }

        public Color CouplingColor { get { return backColor; } set { backColor = value; } }

        public PictureBox Picture { get { return null; } set { } }

        public TrackBar Bar { get { return null; } set { } }

        public TextBox Display { set { } }
    }
}
