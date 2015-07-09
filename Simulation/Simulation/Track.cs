using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Simulation
{
	/**
	 * Finds the worm track and paints it red
	 */
	class Track
	{
		private Color[,] track;
		private Brick[,] field;

		private Color red;
		private Color black;
		private int size;

		public Track(Brick[,] field)
		{
			this.field = field;
			this.red = Color.Red;
			this.black = Color.Black;
			this.size = field.GetLength(0);

			track = new Color[size, size];
			resetColor();
		}

		public Brick[,] Field { set { field = value; size = field.GetLength(0); track = new Color[size, size]; } }

		/**
		 * Colors the colorchart black
		 */
		public void resetColor()
		{
			for (int i = 0; i < size; i++)
			{
				for (int j = 0; j < size; j++)
				{
					track[i, j] = black;
				}
			}
		}

		/**
		 * Finds the track of the worm recursive
		 * 
		 * @param x	current x-coodrinate head
		 * @param y	current y-coodrinate head
		 * @return	returns the tracked colorchart
		 */
		public Color[,] findTrack(int x, int y)
		{
			recursiveFinding(x, y);

			return track;
		}

		/**
		 * Finds the track recursive with stop condition
		 * 
		 * @param x	current x-coordinate of the tracking
		 * @param y	current y-coordinate of the tracking
		 */
		private void recursiveFinding(int x, int y)
		{
			x = inField(x);
			y = inField(y);

			Brick brick = field[x, y];

			if (track[x, y] == black)
			{
				track[x, y] = red;

				if (brick.UpBond)
					recursiveFinding(x, y - 1);
				if (brick.DownBond)
					recursiveFinding(x, y + 1);
				if (brick.LeftBond)
					recursiveFinding(x - 1, y);
				if (brick.RightBond)
					recursiveFinding(x + 1, y);
			}
		}

		/**
		 * Evaluates the position in the field of a cooridnate, usefull for coordinates which are out of field
		 * 
		 * @param i	Coordinate to find its number in field
		 * @return int	returns the indentical coordinate to i, but in field
		 */
		private int inField(int i)
		{
			if (i < 0)
				return size - 1;
			if (i == size)
				return 0;

			return i;
		}

	}
}
