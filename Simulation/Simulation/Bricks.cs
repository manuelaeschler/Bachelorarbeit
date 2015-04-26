using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Simulation
{
    interface Brick
    {
        void draw(float x, float y, float brickSizeX, float brickSizeY, Pen pen, PaintEventArgs e, float size);
        Brick getOpposite(int inCase);
        Brick subtract(Brick brick);
        Color BackColor { get; set; }
        float Probability { get;  set; }
        Color BackColor { get; set; }
        void setBricks(Brick[] bricks);

    }
}
