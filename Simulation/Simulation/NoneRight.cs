﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation
{
    class NoneRight : Brick
    {
        private float probability;

        public NoneRight()
        {

        }

        public void draw(float x, float y, float brickSizeX, float brickSizeY, System.Drawing.Pen pen, System.Windows.Forms.PaintEventArgs e, float size)
        {
            pen.Width = size;

            e.Graphics.DrawLine(pen, (int)x, (int)y, (int)(x), (int)(y - brickSizeY / 2));
            e.Graphics.DrawLine(pen, (int)x, (int)y, (int)(x - brickSizeX / 2), (int)y);
            e.Graphics.DrawLine(pen, (int)x, (int)y, (int)(x), (int)(y + brickSizeY / 2)); 

        }

        public Brick getOpposite(int inCase)
        {
            return null;
        }

        public Brick subtract(Brick brick)
        {
            return null;
        }

        public float Probability { get { return probability; } set { probability = value; } }


        public void setBricks(Brick[] bricks)
        {
            throw new NotImplementedException();
        }
    }
}
