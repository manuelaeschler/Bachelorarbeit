using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Simulation
{
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

		public Color[,] findTrack(int x, int y)
		{
			recursiveFinding(x, y);

			return track;
		}

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
