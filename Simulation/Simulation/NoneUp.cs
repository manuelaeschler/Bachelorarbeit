using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Simulation
{
    class NoneUp : Brick
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
        //Brick noneUp;
        Brick noneDown;
        Brick noneLeft;
        Brick noneRight;
        Brick up;
        Brick down;
        Brick left;
        Brick right;

        public NoneUp()
        {

        }

        public void draw(float x, float y, float brickSizeX, float brickSizeY, Pen pen, PaintEventArgs e, float size)
        {
            pen.Width = size;

            e.Graphics.DrawLine(pen, (int)x, (int)y, (int)(x), (int)(y - brickSizeY / 2));
            e.Graphics.DrawLine(pen, (int)x, (int)y, (int)(x - brickSizeX / 2), (int)y);
            e.Graphics.DrawLine(pen, (int)x, (int)y, (int)(x + brickSizeX / 2), (int)y);

        }

        
        public Brick getOpposite(int inCase)
        {
            return null;
        }

        
        public Brick subtract(Brick brick)
        {
            if (brick is Full)
                return up;

            if (brick is None)
                return this;

            if (brick is Vertical)
                return noneDown;

            if (brick is Horizontal)
                return down;

            if (brick is UpperLeft)
                return noneLeft;

            if (brick is UpperRight)
                return noneRight;

            if (brick is DownLeft)
                return right;

            if (brick is DownRight)
                return left;

            if (brick is NoneUp)
                return none;

            if (brick is NoneDown)
                return vertical;

            if (brick is NoneLeft)
                return upperLeft;

            if (brick is NoneRight)
                return upperRight;

            if (brick is Up)
                return full;

            if (brick is Down)
                return horizontal;

            if (brick is Left)
                return downRight;

            if (brick is Right)
                return downLeft;
 
            return null;
         
        }

        //TO DO
        public void setBricks(Brick[] bricks)
        {
            throw new NotImplementedException();
        }

        public float Probability { get { return probability; } set { probability = value; } }

        public Color CouplingColor { get { return Color.Empty; } set { } }

        public PictureBox Picture { get { return null; } set{ } }

        public TrackBar Bar { get { return null; } set { } }

        public TextBox Display { set { } }
    }
}
