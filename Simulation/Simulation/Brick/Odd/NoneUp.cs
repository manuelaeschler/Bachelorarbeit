﻿using System;
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
		private float startProbability;

        Brick none;
        Brick full;
        Brick horizontal;
        Brick vertical;
        Brick upperLeft;
        Brick upperRight;
        Brick downLeft;
        Brick downRight;
        
        Brick noneDown;
        Brick noneLeft;
        Brick noneRight;
        Brick up;
        Brick down;
        Brick left;
        Brick right;

        Pen penRed;

        public NoneUp()
        {
            penRed = new Pen(Color.Red);

        }

        public void draw(float x, float y, float brickSizeX, float brickSizeY, Pen pen, PaintEventArgs e, float size)
        {
            pen.Width = size;

            e.Graphics.DrawLine(pen, (int)x, (int)y, (int)(x), (int)(y + brickSizeY / 2));
            e.Graphics.DrawLine(pen, (int)x, (int)y, (int)(x - brickSizeX / 2), (int)y);
            e.Graphics.DrawLine(pen, (int)x, (int)y, (int)(x + brickSizeX / 2), (int)y);

            e.Graphics.DrawEllipse(penRed, (int)x - 2, (int)y - 2, 4f, 4f);
			e.Graphics.FillEllipse(new SolidBrush(Color.Red), (int)x - 2, (int)y - 2, 4f, 4f);
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

            noneDown = bricks[9];
            noneLeft = bricks[10];
            noneRight = bricks[11];
            up = bricks[12];
            down = bricks[13];
            left = bricks[14];
            right = bricks[15];
        }

        public float Probability { get { return probability; } set { probability = value; } }

        public Color CouplingColor { get { return Color.Empty; } set { } }

        public PictureBox Picture { get { return null; } set{ } }

        public TrackBar Bar { get { return null; } set { } }

        public TextBox Display { set { } }

		public float StartProbability { get { return startProbability; } set { startProbability = value; } }


        public Brick bondInOut(string inCase)
        {
            switch (inCase)
            {
                case "up":
                    return full;
                case "down":
                    return horizontal;
                case "left":
                    return downRight;
                case "right":
                    return downLeft;
                default:
                    return this;

            }
        }
    }
}