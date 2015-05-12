using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation
{
    class Right : Brick
    {
        public void draw(float x, float y, float brickSizeX, float brickSizeY, System.Drawing.Pen pen, System.Windows.Forms.PaintEventArgs e, float size)
        {
            throw new NotImplementedException();
        }

        public Brick getOpposite(int inCase)
        {
            throw new NotImplementedException();
        }

        public Brick subtract(Brick brick)
        {
            throw new NotImplementedException();
        }

        public System.Windows.Forms.PictureBox Picture
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public System.Windows.Forms.TrackBar Bar
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public float Probability
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public System.Drawing.Color CouplingColor
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public System.Windows.Forms.TextBox Display
        {
            set { throw new NotImplementedException(); }
        }

        public void setBricks(Brick[] bricks)
        {
            throw new NotImplementedException();
        }
    }
}
