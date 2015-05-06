using System;
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

namespace Simulation
{
    public partial class simulation : Form
    {
        int size;
        int velocity;
        int currentPoint;
        List<Algorithm> algorithm = new List<Algorithm>();
        Brick[,] field;
        Brick[,] currentField;
        Brick[,] change;
        Brick[] bricks = new Brick[8];
        float[,] probabilities = new float[8,8];
        ListDictionary<Color, Brick> coupling = new ListDictionary<Color, Brick>();
        

        Boolean run;
        Boolean random;
        Boolean didchange;
        Boolean thermalize;
        float beta;
        string currentModel;

        Brick none;
        Brick full;
        Brick vertical;
        Brick horizontal;
        Brick upperLeft;
        Brick upperRight;
        Brick downLeft;
        Brick downRight;

        Thread startthread;

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

            size = 10;
            currentPoint = 0;

            bricks[0] = none;
            bricks[1] = full;
            bricks[2] = horizontal;
            bricks[3] = vertical;
            bricks[4] = upperLeft;
            bricks[5] = upperRight;
            bricks[6] = downLeft;
            bricks[7] = downRight;

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
            
            field = new Brick[size, size];
            change = new Brick[size, size];
            currentField = new Brick[size, size];
            algorithm.Add(new Worm(field));
            algorithm.Add(new Flip(field));
            run = false;
            random = false;
            thermalize = false;
            serial.BackColor = Color.LightSkyBlue;
            beta = (float)temperaturBar.Value/100;

            velocity = velocityBar.Value;
            fillField(field);
            fillField(change);
            fillField(currentField);

            sizeOfLattice.Text = size.ToString();
            startthread = new Thread(startThread);

            pictureNone.Refresh();
            pictureFull.Invalidate();
            pictureVertical.Invalidate();
            pictureHorizontal.Invalidate();
            pictureUpperLeft.Invalidate();
            pictureUpperRight.Invalidate();
            pictureDownLeft.Invalidate();
            pictureDownRight.Invalidate();

        }

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
        private void flip_Click(object sender, EventArgs e)
        {
            flip.BackColor = Color.LightSkyBlue;
            worm.BackColor = Color.Empty;

            field = algorithm.Last().Field;
            Algorithm al = null;

            foreach (Algorithm algo in algorithm)
            {
                if (algo is Flip)
                    al = algo;
            }
            algorithm.Remove(al);
            al.Field = field;
            algorithm.Add(al);
        }

        private void worm_Click(object sender, EventArgs e)
        {
            flip.BackColor = Color.Empty;
            worm.BackColor = Color.LightSkyBlue;

            field = algorithm.Last().Field;
            Algorithm al = null;

            foreach (Algorithm algo in algorithm)
            {
                if (algo is Worm)
                    al = algo;
            }
            algorithm.Remove(al);
            al.Field = field;
            algorithm.Add(al);
        }
       

        //preferences weights
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

        private void fermionFree_Click(object sender, EventArgs e)
        {
            currentModel = "fermionFree";

            isingNormal.BackColor = Color.Empty;
            isingDual.BackColor = Color.Empty;
            fermionBound.BackColor = Color.Empty;
            fermionFree.BackColor = Color.LightSkyBlue;

            betaTextBox.Text = (beta - 1.5f).ToString();
            massLabel.Visible = true;
            massMaximum.Visible = true;
            massMinimum.Visible = true;
            massNull.Visible = true;

            none.Probability = beta - 1.5f + 2;
            full.Probability = 0f;
            horizontal.Probability = 1;
            vertical.Probability = 1;
            upperLeft.Probability = (float)(1 / Math.Sqrt(2));
            upperRight.Probability = (float)(1 / Math.Sqrt(2));
            downLeft.Probability = (float)(1 / Math.Sqrt(2));
            downRight.Probability = (float)(1 / Math.Sqrt(2));

            setProbabilityBars();
        }

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

        private void fermionBound_Click(object sender, EventArgs e)
        {
            currentModel = "fermionBound";

            isingNormal.BackColor = Color.Empty;
            isingDual.BackColor = Color.Empty;
            fermionBound.BackColor = Color.LightSkyBlue;
            fermionFree.BackColor = Color.Empty;

            betaTextBox.Text = (beta - 1.5f).ToString();
            massLabel.Visible = true;
            massMaximum.Visible = true;
            massMinimum.Visible = true;
            massNull.Visible = true;

            none.Probability = (float)Math.Pow((2+ beta - 1.5f), 2);
            full.Probability = 0f;
            horizontal.Probability = 1;
            vertical.Probability = 1;
            upperLeft.Probability = 0.5f;
            upperRight.Probability = 0.5f;
            downLeft.Probability = 0.5f;
            downRight.Probability = 0.5f;

            setProbabilityBars();
        }


        //calculating weights with beta
        private void calculateIsingNormal()
        {
            none.Probability = 1f;
            full.Probability = (float)( Math.Tanh(beta)*Math.Tanh(beta));
            horizontal.Probability = (float)(Math.Tanh(beta));
            vertical.Probability = (float)(Math.Tanh(beta));
            upperLeft.Probability = (float)(Math.Tanh(beta));
            upperRight.Probability = (float)(Math.Tanh(beta));
            downLeft.Probability = (float)(Math.Tanh(beta));
            downRight.Probability = (float)(Math.Tanh(beta));
        }

        private void calculateIsingDual()
        {
            none.Probability = 1f;
            full.Probability = (float)(Math.Exp(-4*beta));
            horizontal.Probability = (float)(Math.Exp(-2 * beta));
            vertical.Probability = (float)(Math.Exp(-2 * beta));
            upperLeft.Probability = (float)(Math.Exp(-2 * beta));
            upperRight.Probability = (float)(Math.Exp(-2 * beta));
            downLeft.Probability = (float)(Math.Exp(-2 * beta));
            downRight.Probability = (float)(Math.Exp(-2 * beta));
        }


        //set the values

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


        //start, stop, pause
        private void start_Click(object sender, EventArgs e)
        { 
            if (!run)
            {
                sizeOfLattice.ReadOnly = true;
                run = true;
                startthread.Start();
            }
        }

        private void startThread()
        {
            Random rand = new Random();

            int row;
            int col;

            Algorithm current;
            while (run)
            {
                current = algorithm.Last();
                graphicsPanel.Invalidate();

                if (random)
                {
                     row= (int)(rand.NextDouble() * size);
                     col = (int)(rand.NextDouble() * size);
                }
                     else
                {
                     row = currentPoint%size;
                     col = currentPoint/size;
                }

                if ((currentPoint/size) % 2 == 0)
                     didchange = current.change(row, col);
                else
                     didchange = current.change(col, row);

                        
                if (didchange && !thermalize)
                {
                     subtractFields();
                     graphicsPanel.Invalidate();
                     if(velocity != 0)
                         Thread.Sleep(velocity);
                }
                           

                if (!run)
                     return;

                currentPoint = (++currentPoint)%(size*size);
                if(currentPoint == (size*size)-1)
                    currentField = (Brick[,])field.Clone();
            } 
        }

        private void pause_Click(object sender, EventArgs e)
        {
            run = false;
        }

        private void stop_Click(object sender, EventArgs e)
        {
            if (run) 
            {
                sizeOfLattice.ReadOnly = false;
                run = false;
                startthread.Join();
                fillField(field);
                fillField(change);
                fillField(currentField);

                currentPoint = 0;
            }

        }


        //calculate changes
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
        private void picture_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Brushes.Black);

            for (int i = 0; i < field.GetLength(0); i++ )
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    float brickSizeX = graphicsPanel.Size.Width/(float)field.GetLength(0);
                    float brickSizeY = graphicsPanel.Size.Height/(float)field.GetLength(1);

                    float posX = (float)((float)i + 0.5)*brickSizeX;
                    float posY = (float)((float)j + 0.5)*brickSizeY;

                    field[i, j].draw(posX, posY, brickSizeX, brickSizeY,pen , e, 1f);
                }
            }
            changePanel.Invalidate();
        }

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


        //illustration of the vertices
        private void pictureNone_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureFull_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Brushes.Black);
            pen.Width = 3f;

            e.Graphics.DrawLine(pen, 0, 15, 31, 15);
            e.Graphics.DrawLine(pen, 15, 0, 15, 31);
        }

        private void pictureVertical_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Brushes.Black);

            pen.Width = 3f;

            e.Graphics.DrawLine(pen, 15, 0, 15, 31);
        }

        private void pictureHorizontal_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Brushes.Black);
            pen.Width = 3f;

            e.Graphics.DrawLine(pen, 0, 15, 31, 15);
;
        }

        private void pictureUpperLeft_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Brushes.Black);
            pen.Width = 3f;

            e.Graphics.DrawLine(pen, 0, 15, 16, 15);
            e.Graphics.DrawLine(pen, 15, 0, 15, 16);


        }

        private void pictureUpperRight_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Brushes.Black);
            pen.Width = 3f;

            e.Graphics.DrawLine(pen, 14, 15, 31, 15);
            e.Graphics.DrawLine(pen, 15, 0, 15, 16);

        }

        private void pictureDownLeft_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Brushes.Black);

            pen.Width = 3f;

            e.Graphics.DrawLine(pen, 0, 15, 16, 15);
            e.Graphics.DrawLine(pen, 15, 14, 15, 31);
        }

        private void pictureDownRight_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Brushes.Black);

            pen.Width = 3f;

            e.Graphics.DrawLine(pen, 14, 15, 31, 15);
            e.Graphics.DrawLine(pen, 15, 14, 15, 31);
        }


        //changing the weights over the scolling bars
        private void noneBar_Scroll(object sender, EventArgs e)
        {
            none.Probability = (float)noneBar.Value/1000f;

            List<Brick> bri = coupling[none.CouplingColor];
            foreach (Brick brick in bri)
            {
                if (!(brick is None))
                {
                    brick.Bar.Value = noneBar.Value;
                    brick.Probability = none.Probability;
                }

            }

            if(currentModel == "fermionFree")
            {
                if (noneBar.Value >= 500)
                {
                    temperaturBar.Value = (noneBar.Value - 500) / 10;
                }
                betaTextBox.Text = ((float)(noneBar.Value) / 1000 - 2f).ToString();
            }

            if (currentModel == "fermionBound")
            {
                if(noneBar.Value >= 500)
                    temperaturBar.Value = (int)Math.Sqrt((float)(noneBar.Value) - 500f);
                betaTextBox.Text = ((float)Math.Sqrt((float)(noneBar.Value) - 500f)/1000f).ToString();
            }
        }

        private void fullBar_Scroll(object sender, EventArgs e)
        {
            full.Probability = (float)fullBar.Value/1000f;

            List<Brick> bri = coupling[full.CouplingColor];
            foreach (Brick brick in bri)
            {
                if (!(brick is Full))
                {
                    brick.Bar.Value = noneBar.Value;
                    brick.Probability = none.Probability;
                }
            }
        }

        private void verticalBar_Scroll(object sender, EventArgs e)
        {
            vertical.Probability = (float)verticalBar.Value/1000f;

            List<Brick> bri = coupling[vertical.CouplingColor];
            foreach (Brick brick in bri)
            {
                if (!(brick is Vertical))
                {
                    brick.Bar.Value = noneBar.Value;
                    brick.Probability = none.Probability;
                }

            }
        }

        private void horizontalBar_Scroll(object sender, EventArgs e)
        {
            horizontal.Probability = (float)horizontalBar.Value / 1000f;

            List<Brick> bri = coupling[horizontal.CouplingColor];
            foreach (Brick brick in bri)
            {
                if (!(brick is Horizontal))
                {
                    brick.Bar.Value = noneBar.Value;
                    brick.Probability = none.Probability;
                }
            }
        }

        private void upperLeftBar_Scroll(object sender, EventArgs e)
        {
            upperLeft.Probability = (float)upperLeftBar.Value / 1000f;

            List<Brick> bri = coupling[upperLeft.CouplingColor];
            foreach (Brick brick in bri)
            {
                if (!(brick is UpperLeft))
                {
                    brick.Bar.Value = noneBar.Value;
                    brick.Probability = none.Probability;
                }

            }
        }

        private void upperRightBar_Scroll(object sender, EventArgs e)
        {
            upperRight.Probability = (float)upperRightBar.Value / 1000f;

            List<Brick> bri = coupling[upperRight.CouplingColor];
            foreach (Brick brick in bri)
            {
                if (!(brick is UpperRight))
                {
                    brick.Bar.Value = noneBar.Value;
                    brick.Probability = none.Probability;
                }
            }
        }

        private void downLeftBar_Scroll(object sender, EventArgs e)
        {
            downLeft.Probability = (float)downLeftBar.Value / 1000f;

            List<Brick> bri = coupling[downLeft.CouplingColor];
            foreach (Brick brick in bri)
            {
                if (!(brick is DownLeft))
                {
                    brick.Bar.Value = noneBar.Value;
                    brick.Probability = none.Probability;
                }

            }
        }

        private void downRightBar_Scroll(object sender, EventArgs e)
        {
            downRight.Probability = (float)downRightBar.Value / 1000f;

            List<Brick> bri = coupling[downRight.CouplingColor];
            foreach (Brick brick in bri)
            {
                if (!(brick is DownRight))
                {
                    brick.Bar.Value = noneBar.Value;
                    brick.Probability = none.Probability;
                }

            }
        }


        //rest
        private void velocityBar_Scroll(object sender, EventArgs e)
        {
            velocity = velocityBar.Value;
        }

        private void sizeOfLattice_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(sizeOfLattice.Text) && Convert.ToInt32(sizeOfLattice.Text) != 0)
            {
                size = Convert.ToInt32(sizeOfLattice.Text);

                field = new Brick[size, size];
                change = new Brick[size, size];
                currentField = new Brick[size, size];
                fillField(field);
                fillField(change);
                fillField(currentField);
                foreach (Algorithm al in algorithm)
                {
                    al.Field = field;
                }
                
            }

        }

        private void serial_Click(object sender, EventArgs e)
        {
            rand.BackColor = Color.Empty;
            serial.BackColor = Color.LightSkyBlue;

            random = false;
        }

        private void rand_Click(object sender, EventArgs e)
        {
            rand.BackColor = Color.LightSkyBlue;
            serial.BackColor = Color.Empty;

            random = true;
        }

        private void temperaturBar_Scroll(object sender, EventArgs e)
        {
            beta = (float)temperaturBar.Value/100;

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
                        betaTextBox.Text = (beta - 1.5f).ToString();
                        fermionFree_Click(new Object(), new EventArgs());
                        break;
                    }

                case "fermionBound":
                    {
                        betaTextBox.Text = (beta - 1.5f).ToString();
                        fermionBound_Click(new Object(), new EventArgs());
                        break;
                    }

                default:
                    {
                        break;
                    }
            }   
 
        }

        private void thermalisation_Click(object sender, EventArgs e)
        {
            thermalize = true;
            Thread.Sleep(3000);
            thermalize = false;
        }

        private void simulation_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(startthread.IsAlive)
                startthread.Abort();
        }


        //click the vertices and change back color
        private void pictureNone_Click(object sender, EventArgs e)
        {
            coupling.remove(pictureNone.BackColor, none);
            pictureNone.BackColor = nextColor(pictureNone.BackColor);
            none.CouplingColor = pictureNone.BackColor;
            coupling.Add(pictureNone.BackColor, none);

        }

        private void pictureFull_Click(object sender, EventArgs e)
        {
            coupling.remove(pictureFull.BackColor, full);
            pictureFull.BackColor = nextColor(pictureFull.BackColor);
            full.CouplingColor = pictureFull.BackColor;
            coupling.Add(pictureFull.BackColor, full);
        }

        private void pictureVertical_Click(object sender, EventArgs e)
        {
            coupling.remove(pictureVertical.BackColor, vertical);
            pictureVertical.BackColor = nextColor(pictureVertical.BackColor);
            vertical.CouplingColor = pictureVertical.BackColor;
            coupling.Add(pictureVertical.BackColor, vertical);
        }

        private void pictureHorizontal_Click(object sender, EventArgs e)
        {
            coupling.remove(pictureHorizontal.BackColor, horizontal);
            pictureHorizontal.BackColor = nextColor(pictureHorizontal.BackColor);
            horizontal.CouplingColor = pictureHorizontal.BackColor;
            coupling.Add(pictureHorizontal.BackColor, horizontal);
        }

        private void pictureUpperLeft_Click(object sender, EventArgs e)
        {
            coupling.remove(pictureUpperLeft.BackColor, upperLeft);
            pictureUpperLeft.BackColor = nextColor(pictureUpperLeft.BackColor);
            upperLeft.CouplingColor = pictureUpperLeft.BackColor;
            coupling.Add(pictureUpperLeft.BackColor, upperLeft);
        }

        private void pictureUpperRight_Click(object sender, EventArgs e)
        {
            coupling.remove(pictureUpperRight.BackColor, upperRight);
            pictureUpperRight.BackColor = nextColor(pictureUpperRight.BackColor);
            upperRight.CouplingColor = pictureUpperRight.BackColor;
            coupling.Add(pictureUpperRight.BackColor, upperRight);
        }

        private void pictureDownLeft_Click(object sender, EventArgs e)
        {
            coupling.remove(pictureDownLeft.BackColor, downLeft);
            pictureDownLeft.BackColor = nextColor(pictureDownLeft.BackColor);
            downLeft.CouplingColor = pictureDownLeft.BackColor;
            coupling.Add(pictureDownLeft.BackColor, downLeft);
        }

        private void pictureDownRight_Click(object sender, EventArgs e)
        {
            coupling.remove(pictureDownRight.BackColor, downRight);
            pictureDownRight.BackColor = nextColor(pictureDownRight.BackColor);
            downRight.CouplingColor = pictureDownRight.BackColor;
            coupling.Add(pictureDownRight.BackColor, downRight);
        }

        private void criticalValue_Click(object sender, EventArgs e)
        {
            switch(currentModel){
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
                        beta = 0.44f;
                        isingDual_Click(new Object(), new EventArgs());
                        break;
                    }

                case "fermionFree":
                    {
                        temperaturBar.Value = 150;
                        beta = 1.5f;
                        //betaTextBox.Text = beta.ToString();
                        fermionFree_Click(new Object(), new EventArgs());
                        break;
                    }

                case "fermionBound":
                    {
                        temperaturBar.Value = (int)(100f * (1.5f - 0.6865f));
                        beta = ((float)temperaturBar.Value) / 100f;
                        fermionBound_Click(new Object(), new EventArgs());
                        break;
                    }

                default:
                    break;

            }
        }

 
    }
}
