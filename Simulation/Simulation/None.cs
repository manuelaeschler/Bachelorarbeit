using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Simulation
{
    class None : Brick
    {
        static private float probability;
        static private Color couplingColor;
        private PictureBox picture;
        private TrackBar bar;
        private TextBox display;
        Brick full;
        Brick horizontal;
        Brick vertical;
        Brick upperLeft;
        Brick upperRight;
        Brick downLeft;
        Brick downRight;

        public None(Color color, PictureBox picture, TrackBar bar, TextBox display)
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

        }

        public Brick subtract(Brick brick)
        {
            return brick;
        }

        public Brick getOpposite(int inCase)
        {
            switch (inCase)
            {
                case 1:
                    return downRight;
                case 2:
                    return downLeft;
                case 3:
                    return upperRight;
                case 4:
                    return upperLeft;
                default:
                    return this;

            }
        }


        public void setBricks(Brick[] bricks)
        {
            full = bricks[1];
            horizontal = bricks[2];
            vertical = bricks[3];
            upperLeft = bricks[4];
            upperRight = bricks[5];
            downLeft = bricks[6];
            downRight = bricks[7];
        }

    }
}
