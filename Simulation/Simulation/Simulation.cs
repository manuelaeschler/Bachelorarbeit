﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Timers.Timer;
using System.IO;

namespace Simulation
{
	/**
	 * Coordinates all actions with the window in two differents threds
	 */
	public partial class simulation : Form
	{
		int size;
		int velocity;
		int currentPoint;
		Brick[,] field;
		Brick[,] currentField;
		Brick[,] change;
		Color[,] track;
		Brick[] bricks;
		float[,] probabilities = new float[8, 8];
		ListDictionary<Color, Brick> coupling = new ListDictionary<Color, Brick>();
		Track trackObj;

		Boolean run;
		Boolean didchange;
		Boolean thermalize;
		double beta;
		string currentModel;
		Algorithm currentAlgorithm;
		Algorithm flipAlgo;
		Algorithm wormAlgo;

		Brick none;
		Brick full;
		Brick vertical;
		Brick horizontal;
		Brick upperLeft;
		Brick upperRight;
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

		Thread startthread;

		/**
		 * Intialize all compponents for the window and makes preperations for the simulation
		 */
		public simulation()
		{
			InitializeComponent();

			none = new None(Color.Red, pictureNone, noneBar, noneWeight);
			full = new Full(Color.Orange, pictureFull, fullBar, fullWeight);
			vertical = new Vertical(Color.Yellow, pictureVertical, verticalBar, verticalWeight);
			horizontal = new Horizontal(Color.Green, pictureHorizontal, horizontalBar, horizontalWeight);
			upperLeft = new UpperLeft(Color.LightBlue, pictureUpperLeft, upperLeftBar, upperLeftWeight);
			upperRight = new UpperRight(Color.RoyalBlue, pictureUpperRight, upperRightBar, upperRightWeight);
			downLeft = new DownLeft(Color.Violet, pictureDownLeft, downLeftBar, downLeftWeight);
			downRight = new DownRight(Color.White, pictureDownRight, downRightBar, downRightWeight);

			noneUp = new NoneUp();
			noneDown = new NoneDown();
			noneLeft = new NoneLeft();
			noneRight = new NoneRight();
			up = new Up();
			down = new Down();
			left = new Left();
			right = new Right();

			//Validaiton 8 er
			size = 8;
			currentPoint = 0;

			bricks = new Brick[16];

			bricks[0] = none;
			bricks[1] = full;
			bricks[2] = horizontal;
			bricks[3] = vertical;
			bricks[4] = upperLeft;
			bricks[5] = upperRight;
			bricks[6] = downLeft;
			bricks[7] = downRight;

			bricks[8] = noneUp;
			bricks[9] = noneDown;
			bricks[10] = noneLeft;
			bricks[11] = noneRight;
			bricks[12] = up;
			bricks[13] = down;
			bricks[14] = left;
			bricks[15] = right;

			coupling.Add(Color.Red, none);
			coupling.Add(Color.Orange, full);
			coupling.Add(Color.Yellow, vertical);
			coupling.Add(Color.Green, horizontal);
			coupling.Add(Color.LightBlue, upperLeft);
			coupling.Add(Color.RoyalBlue, upperRight);
			coupling.Add(Color.Violet, downLeft);
			coupling.Add(Color.White, downRight);

			none.setBricks(bricks);
			full.setBricks(bricks);
			horizontal.setBricks(bricks);
			vertical.setBricks(bricks);
			upperLeft.setBricks(bricks);
			upperRight.setBricks(bricks);
			downLeft.setBricks(bricks);
			downRight.setBricks(bricks);

			noneUp.setBricks(bricks);
			noneDown.setBricks(bricks);
			noneLeft.setBricks(bricks);
			noneRight.setBricks(bricks);
			up.setBricks(bricks);
			down.setBricks(bricks);
			left.setBricks(bricks);
			right.setBricks(bricks);

			field = new Brick[size, size];
			change = new Brick[size, size];
			currentField = new Brick[size, size];
			track = new Color[size, size];
			wormAlgo = new Worm(field);
			flipAlgo = new Flip(field);
			run = false;
			thermalize = false;
			beta = (double)temperaturBar.Value / 100;

			trackObj = new Track(field);

			currentAlgorithm = flipAlgo;

			velocity =	velocityBar.Value;
			flip.BackColor = Color.LightSkyBlue;
			fillField(field);
			fillField(change);
			fillField(currentField);

			sizeOfLattice.Text = size.ToString();

			pictureNone.Refresh();
			pictureFull.Invalidate();
			pictureVertical.Invalidate();
			pictureHorizontal.Invalidate();
			pictureUpperLeft.Invalidate();
			pictureUpperRight.Invalidate();
			pictureDownLeft.Invalidate();
			pictureDownRight.Invalidate();

			isingNormal_Click(new Object(), new EventArgs());

			startthread = new Thread(startThread);
		}

		/**
		 * Defines the order of the colors for the backcolor of the verties
		 * 
		 * @param color	the current color of a vertex
		 * @return Color	returns the next color in order
		 */
		private Color nextColor(Color color)
		{
			if (color == Color.Red)
				return Color.Orange;

			if (color == Color.Orange)
				return Color.Yellow;

			if (color == Color.Yellow)
				return Color.Green;

			if (color == Color.Green)
				return Color.LightBlue;

			if (color == Color.LightBlue)
				return Color.RoyalBlue;

			if (color == Color.RoyalBlue)
				return Color.Violet;

			if (color == Color.Violet)
				return Color.White;

			return Color.Red;
		}

		/**
		 * resets a field
		 * 
		 * @param fields	field which gets reset
		 */
		private void fillField(Brick[,] fields)
		{
			for (int i = 0; i < size; i++)
			{
				for (int j = 0; j < size; j++)
				{
					fields[i, j] = none;
				}
			}
		}


//Algorithm

		/**
		 * Selects the flip algorithm and makes preperations
		 */
		private void flip_Click(object sender, EventArgs e)
		{
			flip.BackColor = Color.LightSkyBlue;
			worm.BackColor = Color.Empty;

			field = currentAlgorithm.Field;

			flipAlgo.Field = field;

			currentAlgorithm = flipAlgo;

		}

		/**
		 * Selects the worm algorithm and makes preperations
		 */
		private void worm_Click(object sender, EventArgs e)
		{
			flip.BackColor = Color.Empty;
			worm.BackColor = Color.LightSkyBlue;

			field = currentAlgorithm.Field;

			wormAlgo.Field = field;

			currentAlgorithm = wormAlgo;

			updateAllUnevenWeights();

		}


//preferences weights

		/**
		 * Selects the ising normal model and makes preperations
		 */
		private void isingNormal_Click(object sender, EventArgs e)
		{
			isingNormal.BackColor = Color.LightSkyBlue;
			isingDual.BackColor = Color.Empty;
			fermionBound.BackColor = Color.Empty;
			fermionFree.BackColor = Color.Empty;

			betaTextBox.Text = beta.ToString();
			massLabel.Visible = false;
			massMaximum.Visible = false;
			massMinimum.Visible = false;
			massNull.Visible = false;

			currentModel = "isingNormal";
			calculateIsingNormal();

			setProbabilityBars();
		}


		/**
		 * Selects the fermion fre model and makes preperations
		 */
		private void fermionFree_Click(object sender, EventArgs e)
		{
			currentModel = "fermionFree";

			isingNormal.BackColor = Color.Empty;
			isingDual.BackColor = Color.Empty;
			fermionBound.BackColor = Color.Empty;
			fermionFree.BackColor = Color.LightSkyBlue;

			betaTextBox.Text = (Math.Round(beta - 1.5d, 3)).ToString();
			massLabel.Visible = true;
			massMaximum.Visible = true;
			massMinimum.Visible = true;
			massNull.Visible = true;

			none.Probability = beta - 1.5f + 2;
			full.Probability = 0f;
			horizontal.Probability = 1;
			vertical.Probability = 1;
			upperLeft.Probability = 1 / Math.Sqrt(2);
			upperRight.Probability = 1 / Math.Sqrt(2);
			downLeft.Probability = 1 / Math.Sqrt(2);
			downRight.Probability = 1 / Math.Sqrt(2);

			updateAllUnevenWeights(); ;

			setProbabilityBars();
		}

		/**
		 * Selects the ising dual model and makes preperations
		 */
		private void isingDual_Click(object sender, EventArgs e)
		{
			isingNormal.BackColor = Color.Empty;
			isingDual.BackColor = Color.LightSkyBlue;
			fermionBound.BackColor = Color.Empty;
			fermionFree.BackColor = Color.Empty;

			betaTextBox.Text = beta.ToString();
			massLabel.Visible = false;
			massMaximum.Visible = false;
			massMinimum.Visible = false;
			massNull.Visible = false;

			currentModel = "isingDual";
			calculateIsingDual();

			setProbabilityBars();

		}

		/**
		 * Selects the fermion bound state model and makes preperations
		 */
		private void fermionBound_Click(object sender, EventArgs e)
		{
			currentModel = "fermionBound";

			isingNormal.BackColor = Color.Empty;
			isingDual.BackColor = Color.Empty;
			fermionBound.BackColor = Color.LightSkyBlue;
			fermionFree.BackColor = Color.Empty;

			betaTextBox.Text = (Math.Round(beta - 1.5d, 3)).ToString();
			massLabel.Visible = true;
			massMaximum.Visible = true;
			massMinimum.Visible = true;
			massNull.Visible = true;

			none.Probability = Math.Pow((2 + beta - 1.5d), 2);
			full.Probability = 0d;
			horizontal.Probability = 1;
			vertical.Probability = 1;
			upperLeft.Probability = 0.5d;
			upperRight.Probability = 0.5d;
			downLeft.Probability = 0.5d;
			downRight.Probability = 0.5d;

			updateAllUnevenWeights();

			setProbabilityBars();
		}


//calculating weights with beta

		/**
		 * Calculates the weights for the ising nomal model
		 */
		private void calculateIsingNormal()
		{
			none.Probability = 1d;
			full.Probability = Math.Tanh(beta) * Math.Tanh(beta);
			horizontal.Probability = Math.Tanh(beta);
			vertical.Probability = Math.Tanh(beta);
			upperLeft.Probability = Math.Tanh(beta);
			upperRight.Probability = Math.Tanh(beta);
			downLeft.Probability = Math.Tanh(beta);
			downRight.Probability = Math.Tanh(beta);

			updateAllUnevenWeights();

		}

		/**
		 * Calculates the weights for the ising dual model
		 */
		private void calculateIsingDual()
		{
			none.Probability = 1d;
			full.Probability = Math.Exp(-4 * beta);
			horizontal.Probability = Math.Exp(-2 * beta);
			vertical.Probability = Math.Exp(-2 * beta);
			upperLeft.Probability = Math.Exp(-2 * beta);
			upperRight.Probability = Math.Exp(-2 * beta);
			downLeft.Probability = Math.Exp(-2 * beta);
			downRight.Probability = Math.Exp(-2 * beta);

			updateAllUnevenWeights();
		}

		/**
		 * Sets the Scrollbars of the vertex weights
		 */
		private void setProbabilityBars()
		{
			if (none.Probability >= 1)
				noneBar.Value = 1000;
			else
				noneBar.Value = (int)(none.Probability * 1000);

			fullBar.Value = (int)(full.Probability * 1000);
			verticalBar.Value = (int)(vertical.Probability * 1000);
			horizontalBar.Value = (int)(horizontal.Probability * 1000);
			upperLeftBar.Value = (int)(upperLeft.Probability * 1000);
			upperRightBar.Value = (int)(upperRight.Probability * 1000);
			downLeftBar.Value = (int)(downLeft.Probability * 1000);
			downRightBar.Value = (int)(downRight.Probability * 1000);
		}

//Start, stop and pause

		/**
		 * Starts the simulations by creating a new thread
		 */
		private void start_Click(object sender, EventArgs e)
		{
			if (!run)
			{
				sizeOfLattice.ReadOnly = true;
				run = true;

				startthread = new Thread(startThread);
				startthread.Start();

				flip.Enabled = false;
				worm.Enabled = false;
			}
		}

		/**
		 * The thread methode. Runs the simulation in a while loop
		 */
		private void startThread()
		{
			Random rand = new Random();

			int x;
			int y;

			while (run)
			{
				x = (int)(rand.NextDouble() * size);
				y = (int)(rand.NextDouble() * size);

				didchange = currentAlgorithm.change(x, y);

				if (!thermalize && didchange)
				{
					subtractFields();
					graphicsPanel.Invalidate();
					if (velocity != 0)
						Thread.Sleep(velocity);
				}

				currentPoint = (++currentPoint) % (size * size);

				if (currentAlgorithm is Flip && currentPoint == (size * size) - 1)
					currentField = (Brick[,])field.Clone();

				

				if (currentAlgorithm is Worm && ((Worm)wormAlgo).getStart())
				{
					currentField = (Brick[,])field.Clone();
					trackObj.resetColor();
				}

			}
		}

		/**
		 * Pauses the simulation and abort the thread
		 */
		private void pause_Click(object sender, EventArgs e)
		{
			run = false;
			startthread.Abort();
			worm.Enabled = true;
		}

		/**
		 * Stops the simulaiton and resets all fields
		 */
		private void stop_Click(object sender, EventArgs e)
		{

			sizeOfLattice.ReadOnly = false;
			run = false;
			startthread.Join();
			fillField(field);
			fillField(change);
			fillField(currentField);

			currentPoint = 0;
			flip.Enabled = true;
			worm.Enabled = true;

			startthread.Abort();

			wormAlgo = new Worm(field);
			if (currentAlgorithm is Worm)
				currentAlgorithm = wormAlgo;
		

		}


		/**
		 * Calculates the field for the change display
		 */
		private void subtractFields()
		{
			for (int i = 0; i < size; i++)
			{
				for (int j = 0; j < size; j++)
				{
					change[i, j] = currentField[i, j].subtract(field[i, j]);
				}
			}
		}


//painting

		/**
		 * Paints the main display
		 */
		private void picture_Paint(object sender, PaintEventArgs e)
		{
			Pen pen = new Pen(Brushes.Black);

			if (currentAlgorithm is Worm)
			{
				trackObj.Field = field;
				trackObj.resetColor();

				track = trackObj.findTrack(((Worm)wormAlgo).HeadX, ((Worm)wormAlgo).HeadY);
			}


			for (int i = 0; i < field.GetLength(0); i++)
			{
				for (int j = 0; j < field.GetLength(1); j++)
				{
					pen.Color = track[i, j];

					float brickSizeX = graphicsPanel.Size.Width / (float)field.GetLength(0);
					float brickSizeY = graphicsPanel.Size.Height / (float)field.GetLength(1);

					float posX = (float)((float)i + 0.5) * brickSizeX;
					float posY = (float)((float)j + 0.5) * brickSizeY;

					field[i, j].draw(posX, posY, brickSizeX, brickSizeY, pen, e, 1f);
				}
			}
			changePanel.Invalidate();
		}

		/**
		 * Paints the change display
		 */
		private void changePanel_Paint(object sender, PaintEventArgs e)
		{
			Pen pen = new Pen(Brushes.Red);

			for (int i = 0; i < change.GetLength(0); i++)
			{
				for (int j = 0; j < change.GetLength(1); j++)
				{
					float brickSizeX = changePanel.Size.Width / (float)change.GetLength(0);
					float brickSizeY = changePanel.Size.Height / (float)change.GetLength(1);

					float posX = (float)((float)i + 0.5) * brickSizeX;
					float posY = (float)((float)j + 0.5) * brickSizeY;

					change[i, j].draw(posX, posY, brickSizeX, brickSizeY, pen, e, 0.5f);
				}
			}
		}


		//illustrations of the vertices

		/**
		 * Paints the vertex None
		 */
		private void pictureNone_Paint(object sender, PaintEventArgs e)
		{

		}

		/**
		 * Paints the vertex Full
		 */
		private void pictureFull_Paint(object sender, PaintEventArgs e)
		{
			Pen pen = new Pen(Brushes.Black);
			pen.Width = 3f;

			e.Graphics.DrawLine(pen, 0, 15, 31, 15);
			e.Graphics.DrawLine(pen, 15, 0, 15, 31);
		}

		/**
		 * Paints the vertex Vertical
		 */
		private void pictureVertical_Paint(object sender, PaintEventArgs e)
		{
			Pen pen = new Pen(Brushes.Black);

			pen.Width = 3f;

			e.Graphics.DrawLine(pen, 15, 0, 15, 31);
		}

		/**
		 * Paints the vertex Horizintal
		 */
		private void pictureHorizontal_Paint(object sender, PaintEventArgs e)
		{
			Pen pen = new Pen(Brushes.Black);
			pen.Width = 3f;

			e.Graphics.DrawLine(pen, 0, 15, 31, 15);
		}

		/**
		 * Paints the vertex UpperLeft
		 */
		private void pictureUpperLeft_Paint(object sender, PaintEventArgs e)
		{
			Pen pen = new Pen(Brushes.Black);
			pen.Width = 3f;

			e.Graphics.DrawLine(pen, 0, 15, 16, 15);
			e.Graphics.DrawLine(pen, 15, 0, 15, 16);
		}

		/**
		 * Paints the vertex UpperRight
		 */
		private void pictureUpperRight_Paint(object sender, PaintEventArgs e)
		{
			Pen pen = new Pen(Brushes.Black);
			pen.Width = 3f;

			e.Graphics.DrawLine(pen, 14, 15, 31, 15);
			e.Graphics.DrawLine(pen, 15, 0, 15, 16);

		}

		/**
		 * Paints the vertex DownLeft
		 */
		private void pictureDownLeft_Paint(object sender, PaintEventArgs e)
		{
			Pen pen = new Pen(Brushes.Black);

			pen.Width = 3f;

			e.Graphics.DrawLine(pen, 0, 15, 16, 15);
			e.Graphics.DrawLine(pen, 15, 14, 15, 31);
		}

		/**
		 * Paints the vertex DownRight
		 */
		private void pictureDownRight_Paint(object sender, PaintEventArgs e)
		{
			Pen pen = new Pen(Brushes.Black);

			pen.Width = 3f;

			e.Graphics.DrawLine(pen, 14, 15, 31, 15);
			e.Graphics.DrawLine(pen, 15, 14, 15, 31);
		}


//changing the weights by the scolling bars

		/**
		 * Makes related changes with the new value of the None
		 */
		private void noneBar_Scroll(object sender, EventArgs e)
		{
			none.Probability = (float)noneBar.Value / 1000f;


			List<Brick> bri = coupling[none.CouplingColor];
			foreach (Brick brick in bri)
			{
				if (!(brick is None))
				{
					brick.Bar.Value = noneBar.Value;
					brick.Probability = none.Probability;
				}

			}

			if (currentModel == "fermionFree")
			{
				if (noneBar.Value >= 500)
				{
					temperaturBar.Value = (noneBar.Value - 500) / 10;
				}
				betaTextBox.Text = ((float)(noneBar.Value) / 1000 - 2f).ToString();
			}

			if (currentModel == "fermionBound")
			{
				if (noneBar.Value >= 500)
					temperaturBar.Value = (int)Math.Sqrt((float)(noneBar.Value) - 500f);
				betaTextBox.Text = ((float)Math.Sqrt((float)(noneBar.Value) - 500f) / 1000f).ToString();
			}

			updateAllUnevenWeights();
		}

		/**
		 * Makes related changes with the new value of the Full
		 */
		private void fullBar_Scroll(object sender, EventArgs e)
		{
			full.Probability = (float)fullBar.Value / 1000f;

			List<Brick> bri = coupling[full.CouplingColor];
			foreach (Brick brick in bri)
			{
				if (!(brick is Full))
				{
					brick.Bar.Value = fullBar.Value;
					brick.Probability = full.Probability;
				}
			}

			updateAllUnevenWeights();
		}

		/**
		 * Makes related changes with the new value of the Vertical
		 */
		private void verticalBar_Scroll(object sender, EventArgs e)
		{
			vertical.Probability = (float)verticalBar.Value / 1000f;


			List<Brick> bri = coupling[vertical.CouplingColor];
			foreach (Brick brick in bri)
			{
				if (!(brick is Vertical))
				{
					brick.Bar.Value = verticalBar.Value;
					brick.Probability = vertical.Probability;
				}

			}

			updateAllUnevenWeights();
		}

		/**
		 * Makes related changes with the new value of the Horizintal
		 */
		private void horizontalBar_Scroll(object sender, EventArgs e)
		{
			horizontal.Probability = (float)horizontalBar.Value / 1000f;


			List<Brick> bri = coupling[horizontal.CouplingColor];
			foreach (Brick brick in bri)
			{
				if (!(brick is Horizontal))
				{
					brick.Bar.Value = horizontalBar.Value;
					brick.Probability = horizontal.Probability;
				}
			}

			updateAllUnevenWeights();
		}

		/**
		 * Makes related changes with the new value of the UpperLeft
		 */
		private void upperLeftBar_Scroll(object sender, EventArgs e)
		{
			upperLeft.Probability = (float)upperLeftBar.Value / 1000f;


			List<Brick> bri = coupling[upperLeft.CouplingColor];
			foreach (Brick brick in bri)
			{
				if (!(brick is UpperLeft))
				{
					brick.Bar.Value = upperLeftBar.Value;
					brick.Probability = upperLeft.Probability;
				}

			}

			updateAllUnevenWeights();
		}

		/**
		 * Makes related changes with the new value of the UpperRight
		 */
		private void upperRightBar_Scroll(object sender, EventArgs e)
		{
			upperRight.Probability = (float)upperRightBar.Value / 1000f;


			List<Brick> bri = coupling[upperRight.CouplingColor];
			foreach (Brick brick in bri)
			{
				if (!(brick is UpperRight))
				{
					brick.Bar.Value = upperRightBar.Value;
					brick.Probability = upperRight.Probability;
				}
			}

			updateAllUnevenWeights();
		}

		/**
		 * Makes related changes with the new value of the DownLeft
		 */
		private void downLeftBar_Scroll(object sender, EventArgs e)
		{
			downLeft.Probability = (float)downLeftBar.Value / 1000f;


			List<Brick> bri = coupling[downLeft.CouplingColor];
			foreach (Brick brick in bri)
			{
				if (!(brick is DownLeft))
				{
					brick.Bar.Value = downLeftBar.Value;
					brick.Probability = downLeft.Probability;
				}

			}

			updateAllUnevenWeights();
		}

		/**
		 * Makes related changes with the new value of the DownRight
		 */
		private void downRightBar_Scroll(object sender, EventArgs e)
		{
			downRight.Probability = (float)downRightBar.Value / 1000f;

			List<Brick> bri = coupling[downRight.CouplingColor];
			foreach (Brick brick in bri)
			{
				if (!(brick is DownRight))
				{
					brick.Bar.Value = downRightBar.Value;
					brick.Probability = downRight.Probability;
				}

			}

			updateAllUnevenWeights();
		}


//rest

		/**
		 * Sets the velocity of the simulation
		 */
		private void velocityBar_Scroll(object sender, EventArgs e)
		{
			velocity = velocityBar.Value;
		}

		/**
		 * Changes the size of the lattice
		 * 
		 * @throw	cathces a format exception if the input has the wrong format
		 */
		private void sizeOfLattice_TextChanged(object sender, EventArgs e)
		{
			int suggestSize;
			try
			{
				suggestSize = Convert.ToInt32(sizeOfLattice.Text);
			}
			catch (System.FormatException)
			{
				suggestSize = size;
			}

			if (!string.IsNullOrWhiteSpace(sizeOfLattice.Text) && size!= 0)
			{
				size = suggestSize;

				field = new Brick[size, size];
				change = new Brick[size, size];
				currentField = new Brick[size, size];
				track = new Color[size, size];
				fillField(field);
				fillField(change);
				fillField(currentField);
				wormAlgo.Field = field;
				flipAlgo.Field = field;
				trackObj.Field = field;

			}

		}

		/**
		 * Calculates the new wights of the current model with the changed value(beta or mass)
		 */
		private void temperaturBar_Scroll(object sender, EventArgs e)
		{
			beta = (float)temperaturBar.Value / 100;

			switch (currentModel)
			{
				case "isingNormal":
					{
						betaTextBox.Text = beta.ToString();
						isingNormal_Click(new Object(), new EventArgs());
						break;
					}
				case "isingDual":
					{
						betaTextBox.Text = beta.ToString();
						isingDual_Click(new Object(), new EventArgs());
						break;
					}

				case "fermionFree":
					{
						fermionFree_Click(new Object(), new EventArgs());
						break;
					}

				case "fermionBound":
					{
						fermionBound_Click(new Object(), new EventArgs());
						break;
					}

				default:
					{
						betaTextBox.Text = beta.ToString();
						break;
					}
			}

		}

		/**
		 * Sets thermalize true and pauses the main thread for 3 seconds. Sets thermalize false.
		 */
		private void thermalisation_Click(object sender, EventArgs e)
		{
			thermalize = true;
			Thread.Sleep(3000);
			thermalize = false;
		}

		/**
		 * Abort the start thred
		 */
		private void simulation_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (startthread.IsAlive)
				startthread.Abort();
		}

		/**
		 * Calculates the weights with the critical value for the current model
		 */
		private void criticalValue_Click(object sender, EventArgs e)
		{
			switch (currentModel)
			{
				case "isingNormal":
					{
						temperaturBar.Value = 42;
						beta = 0.42f;
						isingNormal_Click(new Object(), new EventArgs());
						break;
					}

				case "isingDual":
					{
						temperaturBar.Value = 44;
						beta = 0.440686f;
						isingDual_Click(new Object(), new EventArgs());
						break;
					}

				case "fermionFree":
					{
						temperaturBar.Value = 150;
						beta = 1.5f;
						fermionFree_Click(new Object(), new EventArgs());
						break;
					}

				case "fermionBound":
					{
						temperaturBar.Value = (int)(100f * (1.5f - 0.68650627f));
						beta = ((float)temperaturBar.Value) / 100f;
						fermionBound_Click(new Object(), new EventArgs());
						break;
					}

				default:
					break;

			}
		}

		/**
		 * Updates all weights which ar not variable by a scroll bar
		 */
		private void updateAllUnevenWeights()
		{
			calculateUnevenStartWeights(noneUp);
			calculateUnevenStartWeights(noneDown);
			calculateUnevenStartWeights(noneLeft);
			calculateUnevenStartWeights(noneRight);

		}

		/**
		 * Calculate the start weight of the brick, but only for NoneUp, NoneDown, NoneLeft and NoneRight
		 * @param brick	the brick, which start weight should be calculated
		 */
		private void calculateUnevenStartWeights(Brick brick)
		{
			if (brick.bondInOut("left").Probability == 0 || brick.bondInOut("right").Probability == 0 || brick.bondInOut("down").Probability == 0 || brick.bondInOut("up").Probability == 0)
				brick.StartProbability = 0;
			else
				brick.StartProbability = 1;
		}


//click the vertices and change back color

		/**
		 * Change backcolor of the None foreward
		 */
		private void pictureNone_Click(object sender, EventArgs e)
		{
			coupling.remove(pictureNone.BackColor, none);
			pictureNone.BackColor = nextColor(pictureNone.BackColor);
			none.CouplingColor = pictureNone.BackColor;
			coupling.Add(pictureNone.BackColor, none);

		}

		/**
		 * Change backcolor of the Full foreward
		 */
		private void pictureFull_Click(object sender, EventArgs e)
		{
			coupling.remove(pictureFull.BackColor, full);
			pictureFull.BackColor = nextColor(pictureFull.BackColor);
			full.CouplingColor = pictureFull.BackColor;
			coupling.Add(pictureFull.BackColor, full);
		}

		/**
		 * Change backcolor of the Vertical foreward
		 */
		private void pictureVertical_Click(object sender, EventArgs e)
		{
			coupling.remove(pictureVertical.BackColor, vertical);
			pictureVertical.BackColor = nextColor(pictureVertical.BackColor);
			vertical.CouplingColor = pictureVertical.BackColor;
			coupling.Add(pictureVertical.BackColor, vertical);
		}

		/**
		 * Change backcolor of the Horizontal foreward
		 */
		private void pictureHorizontal_Click(object sender, EventArgs e)
		{
			coupling.remove(pictureHorizontal.BackColor, horizontal);
			pictureHorizontal.BackColor = nextColor(pictureHorizontal.BackColor);
			horizontal.CouplingColor = pictureHorizontal.BackColor;
			coupling.Add(pictureHorizontal.BackColor, horizontal);
		}

		/**
		 * Change backcolor of the UpperLeft foreward
		 */
		private void pictureUpperLeft_Click(object sender, EventArgs e)
		{
			coupling.remove(pictureUpperLeft.BackColor, upperLeft);
			pictureUpperLeft.BackColor = nextColor(pictureUpperLeft.BackColor);
			upperLeft.CouplingColor = pictureUpperLeft.BackColor;
			coupling.Add(pictureUpperLeft.BackColor, upperLeft);
		}

		/**
		 * Change backcolor of the UpperRight foreward
		 */
		private void pictureUpperRight_Click(object sender, EventArgs e)
		{
			coupling.remove(pictureUpperRight.BackColor, upperRight);
			pictureUpperRight.BackColor = nextColor(pictureUpperRight.BackColor);
			upperRight.CouplingColor = pictureUpperRight.BackColor;
			coupling.Add(pictureUpperRight.BackColor, upperRight);
		}


		/**
		 * Change backcolor of the DownLeft foreward
		 */
		private void pictureDownLeft_Click(object sender, EventArgs e)
		{
			coupling.remove(pictureDownLeft.BackColor, downLeft);
			pictureDownLeft.BackColor = nextColor(pictureDownLeft.BackColor);
			downLeft.CouplingColor = pictureDownLeft.BackColor;
			coupling.Add(pictureDownLeft.BackColor, downLeft);
		}

		/**
		 * Change backcolor of the DownRight foreward
		 */
		private void pictureDownRight_Click(object sender, EventArgs e)
		{
			coupling.remove(pictureDownRight.BackColor, downRight);
			pictureDownRight.BackColor = nextColor(pictureDownRight.BackColor);
			downRight.CouplingColor = pictureDownRight.BackColor;
			coupling.Add(pictureDownRight.BackColor, downRight);
		}


	}
}