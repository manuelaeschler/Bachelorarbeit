﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Simulation
{
    class UpperRight : Brick
    {
        static private double probability;
        static private Color couplingColor;
        private PictureBox picture;
        private TrackBar bar;
        private TextBox display;

		private readonly bool upBond;
		private readonly bool downBond;
		private readonly bool leftBond;
		private readonly bool rightBond;

        Brick none;
        Brick full;
        Brick horizontal;
        Brick vertical;
        Brick upperLeft;
        Brick downLeft;
        Brick downRight;

        Brick noneUp;
        Brick noneDown;
        Brick noneLeft;
        Brick noneRight;
        Brick up;
        Brick down;
        Brick left;
        Brick right;

        public UpperRight(Color color, PictureBox picture, TrackBar bar, TextBox display)
        {
            this.CouplingColor = color;
            this.Bar = bar;
            this.Picture = picture;
            this.Display = display;

			this.upBond = true;
			this.downBond = false;
			this.leftBond = false;
			this.rightBond = true;
        }
        public double Probability
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

		public double StartProbability { get { return 0; } set { } }

		public bool UpBond { get { return upBond; } }

		public bool DownBond { get { return downBond; } }

		public bool LeftBond { get { return leftBond; } }

		public bool RightBond { get { return rightBond; } }

        public void draw(float x, float y, float brickSizeX, float brickSizeY, Pen pen, PaintEventArgs e, float size)
        {
            pen.Width = size;

            e.Graphics.DrawLine(pen, (int)x, (int)y, (int)(x + brickSizeX / 2), (int)y);
            e.Graphics.DrawLine(pen, (int)x, (int)y, (int)(x), (int)(y - brickSizeY / 2));

        }

        public Brick subtract(Brick brick)
        {
            if (brick is Full)
                return downLeft;

            if (brick is None)
                return this;

            if (brick is Vertical)
                return downRight;

            if (brick is Horizontal)
                return upperLeft;

            if (brick is UpperLeft)
                return horizontal;

            if (brick is UpperRight)
                return none;

            if (brick is DownLeft)
                return full;

            if (brick is DownRight)
                return vertical;

            if (brick is NoneUp)
                return noneRight;

            if (brick is NoneDown)
                return left;

            if (brick is NoneLeft)
                return down;

            if (brick is NoneRight)
                return noneUp;

            if (brick is Up)
                return right;

            if (brick is Down)
                return noneLeft;

            if (brick is Left)
                return noneDown;

            if (brick is Right)
                return up;

            return null;
        }

        public Brick getOpposite(int inCase)
        {
            switch (inCase)
            {
                case 1:
                    return vertical;
                case 2:
                    return full;
                case 3:
                    return none;
                case 4:
                    return horizontal;
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
            upperLeft = bricks[4];
            downLeft = bricks[6];
            downRight = bricks[7];

            noneUp = bricks[8];
            noneDown = bricks[9];
            noneLeft = bricks[10];
            noneRight = bricks[11];
            up = bricks[12];
            down = bricks[13];
            left = bricks[14];
            right = bricks[15];
        }

        public Brick bondInOut(string inCase)
        {
            switch (inCase)
            {
                case "up":
                    return right;
                case "down":
                    return noneLeft;
                case "left":
                    return noneDown;
                case "right":
                    return up;
                default:
                    return this;

            }
        }

    }
}
