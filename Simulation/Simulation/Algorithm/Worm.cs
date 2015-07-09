using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation
{
	/**
	 * Perform the worm algorithm step by step
	 */
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

		public int HeadX { get { return headX; } }

		public int HeadY { get { return headY; } }

		/**
		 * Dicides if the worm is on start state and starts the moving
		 * 
		 * @param x	x-coordinate of a random point
		 * @param y y-coordinate of a random point
		 * @return bool	if some chaeges were made
		 */
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

		/**
		 * Starts the moving of the head with start state conditions
		 * 
		 * @return bool	if some chaeges were made
		 */
		private bool moveStart()
		{
			bool changedHead = moveHeadStart(headX, headY);


			if (headX == tailX && headY == tailY)
				start = true;

			return changedHead;

		}

		/**
		 * Starts the moving of the head
		 * 
		 * @return bool	if some chaeges were made
		 */
		private bool move()
		{
			bool changedHead = moveHead(headX, headY);


			if (headX == tailX && headY == tailY)
				start = true;

			return changedHead;

		}

		/**
		 * Evaluate the next state of the lattice field and changes it with the probability of the metropolis condition with start weights
		 * 
		 * @param x	x-coordinate of the head
		 * @param y	y-coordinate of the head
		 * @return bool	if some chaeges were made
		 */
		private bool moveHeadStart(int x, int y)
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

				headX = neighbourX;
				headY = neighbourY;

				return true;
			}

			return false;
		}

		/**
		 * Evaluate the next state of the lattice field and changes it with the probability of the metropolis condition
		 * 
		 * @param x	x-coordinate of the head
		 * @param y	y-coordinate of the head
		 * @return bool	if some chaeges were made
		 */
		private bool moveHead(int x, int y)
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

				headX = neighbourX;
				headY = neighbourY;


				return true;
			}

			return false;
		}

		/**
		 * Evaluates the x-coordiante of the neighbour in a certain direction
		 * 
		 * @param x	x-coordinate of the head
		 * @param direc	direction to be going
		 * @return int	returns the x-ccordinate of the neighbour
		 */
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

		/**
		 * Evaluates the y-coordiante of the neighbour in a certain direction
		 * 
		 * @param y	y-coordinate of the head
		 * @param direc	direction to be going
		 * @return int	returns the y-ccordinate of the neighbour
		 */
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

		/**
		 * Evaluates the opposite direction of a certain direction
		 * 
		 * @param direc	a certian direction
		 * @return String	returns the opposite direction
		 */
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

		/**
		 * Evaluates a random direction
		 * 
		 * @return String returns a random direction
		 */
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


		public bool getStart()
		{
			return start;
		}

		
		public override String ToString()
		{
			return "Worm";
		}

	}
}
