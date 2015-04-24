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
        void draw(float x, float y, float brickSizeX, float brickSizeY, Pen pen, PaintEventArgs e, float sizeSmall, float sizeBig);
        Brick getOpposite(int inCase);
        Brick subtract(Brick brick);
        float Probability { get;  set; }
        void setBricks(Brick[] bricks);

    }
}
