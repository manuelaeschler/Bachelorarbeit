using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation
{
	class Worm : Algorithm
	{
		private Brick[,] field;
		Random rand;
		int size;
		float alpha;
		static bool start;

		static int headX;
		static int headY;
		static int tailX;
		static int tailY;

		public Worm(Brick[,] field)
		{
			this.Field = field;
			this.size = field.GetLength(0);
			rand = new Random();
			alpha = .9f;
			start = true;
		}

		public Brick[,] Field { get { return field; } set { field = value; size = field.GetLength(0); } }

		public int TailX { get { return tailX; } }

		public int TailY { get { return tailY; } }

		public bool change(int x, int y)
		{
			if (start)
			{
				if (alpha >= rand.NextDouble())
				{
					headX = x;
					headY = y;
					tailX = x;
					tailY = y;
					start = false;
					return moveStart();
				}
				else
					return false;
			}
			else
			{
				return move();
			}
		}

		private bool moveStart()
		{
			//bool changedHead = headOrTailStart(headX, headY, "head");
			bool changedTail = headOrTailStart(tailX, tailY, "tail");

			if (headX == tailX && headY == tailY)
				start = true;

			//if (changedHead)
			//return true;

			return changedTail;
		}

		private bool move()
		{
			//bool changedHead = headOrTail(headX, headY, "head");
			bool changedTail = headOrTail(tailX, tailY, "tail");

			if (headX == tailX && headY == tailY)
				start = true;

			//if (changedHead)
				//return true;

			return changedTail;
		}

		private bool headOrTailStart(int x, int y, String inCase)
		{
			String direc = direction();

			int neighbourX = getNeighbourCoordX(x, direc);
			int neighbourY = getNeighbourCoordY(y, direc);

			Brick oldBrick = field[x, y];
			Brick newBrick = oldBrick.bondInOut(direc);
			Brick oldNeighbour = field[neighbourX, neighbourY];
			Brick newNeighbour = oldNeighbour.bondInOut(getOppositeDirection(direc));

			double oldPos = 1;
			double newPos = 1;

			oldPos = oldBrick.StartProbability * oldNeighbour.StartProbability;
			newPos = newBrick.StartProbability * newNeighbour.StartProbability;
			double realPos;


			if (oldPos != 0)
				realPos = newPos / oldPos;
			else
			{
				if (newPos != 0)
					realPos = 1;
				else
					realPos = 0;
			}

			if (rand.NextDouble() <= Math.Min(1, realPos))
			{
				field[x, y] = newBrick;
				field[neighbourX, neighbourY] = newNeighbour;
				if (inCase == "head")
				{
					headX = neighbourX;
					headY = neighbourY;
				}
				else
				{
					tailX = neighbourX;
					tailY = neighbourY;
				}

				return true;
			}

			return false;
		}

		private bool headOrTail(int x, int y, String inCase)
		{
			String direc = direction();

			int neighbourX = getNeighbourCoordX(x, direc);
			int neighbourY = getNeighbourCoordY(y, direc);

			Brick oldBrick = field[x, y];
			Brick newBrick = oldBrick.bondInOut(direc);
			Brick oldNeighbour = field[neighbourX, neighbourY];
			Brick newNeighbour = oldNeighbour.bondInOut(getOppositeDirection(direc));

			double oldPos = oldBrick.Probability * oldNeighbour.Probability;
			double newPos = newBrick.Probability * newNeighbour.Probability;
			double realPos;


			if (oldPos != 0)
				realPos = newPos / oldPos;
			else
			{
				if (newPos != 0)
					realPos = 1;
				else
					realPos = 0;
			}

			if (rand.NextDouble() <= Math.Min(1, realPos))
			{
				field[x, y] = newBrick;
				field[neighbourX, neighbourY] = newNeighbour;
				if (inCase == "head")
				{
					headX = neighbourX;
					headY = neighbourY;
				}
				else
				{
					tailX = neighbourX;
					tailY = neighbourY;
				}

				return true;
			}

			return false;
		}


		private int getNeighbourCoordX(int x, String direc)
		{
			switch (direc)
			{

				case "left":
					{
						if (x == 0)
							return size - 1;
						else
							return (x - 1);
					}
						

				case "right":
					{
						if (x == size - 1)
							return 0;
						else
							return (x + 1);
					}

				default:
						return x;
					
			}

		}

		private int getNeighbourCoordY(int y, String direc)
		{
			switch (direc)
			{

				case "up":
					{
						if (y == 0)
							return size - 1;
						else
							return (y - 1);
					}


				case "down":
					{
						if (y == size - 1)
							return 0;
						else
							return (y + 1);
					}

				default:
					return y;

			}

		}

		private String getOppositeDirection(String direc)
		{
			switch (direc)
			{
				case "up":
					return "down";

				case "down":
					return "up";

				case "left":
					return "right";

				case "right":
					return "left";

				default:
					return direc;
			}
		}

		private String direction()
		{
			double random = rand.NextDouble();

			if (random <= 0.25)
				return "up";

			if (random <= 0.5)
				return "down";

			if (random <= 0.75)
				return "left";

			return "right";

		}

		private void calculateUnevenWeightsStart(Brick brick)
		{
			double mix = 1;

			mix *= brick.bondInOut("up").Probability;

			mix *= brick.bondInOut("down").Probability;

			mix *= brick.bondInOut("left").Probability;

			mix *= brick.bondInOut("right").Probability;

			brick.Probability = (float)Math.Pow(mix, 1 / .25f);

		}

		public bool getStart()
		{
			return start;
		}

	}
}
