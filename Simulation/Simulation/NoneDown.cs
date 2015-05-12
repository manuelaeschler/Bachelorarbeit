﻿using System;
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

        Brick none;
        Brick full;
        Brick horizontal;
        Brick vertical;
        Brick upperLeft;
        Brick upperRight;
        Brick downLeft;
        Brick downRight;

        Brick noneUp;
        Brick noneLeft;
        Brick noneRight;
        Brick up;
        Brick down;
        Brick left;
        Brick right;

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

        
        public Brick subtract(Brick brick)
        {
            if (brick is Full)
                return down;

            if (brick is None)
                return this;

            if (brick is Vertical)
                return noneUp;

            if (brick is Horizontal)
                return up;

            if (brick is UpperLeft)
                return right;

            if (brick is UpperRight)
                return left;

            if (brick is DownLeft)
                return noneLeft;

            if (brick is DownRight)
                return noneRight;

            if (brick is NoneUp)
                return vertical;

            if (brick is NoneDown)
                return none;

            if (brick is NoneLeft)
                return downLeft;

            if (brick is NoneRight)
                return downRight;

            if (brick is Up)
                return horizontal;

            if (brick is Down)
                return full;

            if (brick is Left)
                return upperRight;

            if (brick is Right)
                return upperLeft;
 
            return null;

        }

        
        public void setBricks(Brick[] bricks)
        {
            none = bricks[0];
            full = bricks[1];
            horizontal = bricks[2];
            vertical = bricks[3];
            upperLeft = bricks[4];
            upperRight = bricks[5];
            downLeft = bricks[6];
            downRight = bricks[7];

            noneUp = bricks[8];
            noneLeft = bricks[10];
            noneRight = bricks[11];
            up = bricks[12];
            down = bricks[13];
            left = bricks[14];
            right = bricks[15];
        }

        public float Probability { get { return probability; } set { probability = value; } }

        public Color CouplingColor { get { return Color.Empty; } set { } }

        public PictureBox Picture { get { return null; } set { } }

        public TrackBar Bar { get { return null; } set { } }

        public TextBox Display { set { } }


        public Brick bondInOut(string inCase)
        {
            switch (inCase)
            {
                case "up":
                    return horizontal;
                case "down":
                    return full;
                case "left":
                    return upperRight;
                case "right":
                    return upperLeft;
                default:
                    return this;

            }
        }
    }
}
