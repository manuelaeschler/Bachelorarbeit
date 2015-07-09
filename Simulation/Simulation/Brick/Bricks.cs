using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Simulation
{
	/**
	 * Holds all information of a brick and coordiantes the changning by the two algorithms
	 */
    interface Brick
    {
		/**
		 * Draw the picture of the brick in the lattice field
		 * 
		 * @param x	x-coordinate to draw the brick
		 * @param y	y-coordinate to draw the brick
		 * @param brickSizeX	size in x direction of the brick
		 * @param brickSizeY	size in y direction of the brick
		 * @param pen	pen to draw the brick
		 * @param e	paint event of the panel
		 * @param dize	pen size
		 */
		void draw(float x, float y, float brickSizeX, float brickSizeY, Pen pen, PaintEventArgs e, float size);

		/**
		 * Evaluates the opposite/inverse oft the brick
		 * 
		 * @param inCase	relative position to the other vertex to change
		 * @return Brick	return the inverse brick
		 */
        Brick getOpposite(int inCase);

		/**
		 * Elavaluates the diffrence of the two bricks
		 * 
		 * @param brick	brick to compare
		 * @return Brick	returns the brick, which represent the diffrence of the two bricks
		 */
        Brick subtract(Brick brick);

		/**
		 * Evaluates the new brick, when a bond comes in or goes out in a certain direction
		 * 
		 * @param inCase	direction the bond goes
		 * @return Brick	resulted brick after bond changing
		 */
        Brick bondInOut(String inCase);
		/**
		 * Sets all brick as class variable
		 * 
		 * @param bricks	array with all bricks
		 */
		void setBricks(Brick[] bricks);

        PictureBox Picture { get; set; }
        TrackBar Bar { get; set; }
        double Probability { get;  set; }
        Color CouplingColor { get; set; }
        TextBox Display { set; }
		double StartProbability { get; set; }
		bool UpBond { get; }
		bool DownBond { get; }
		bool LeftBond { get; }
		bool RightBond { get; }
        

    }
}
