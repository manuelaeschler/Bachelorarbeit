using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation
{
	class Validation
	{

		private double[] values;
		private double expect;
		private double[] energyExpect;


		private int length = 100000;
		private double beta;
		private static int position = 0;

		public Validation(double[] energyExpect)
		{
			this.energyExpect = energyExpect;


			//Console.Write("Statistical error: " + statError + "\nEnergy expecation: " + energyExpect + "\nSpecific heat: " + specHeat);
		}

		public void calculate(String fileName, double beta)
		{
			//this.position = 0;
			this.beta = beta;
			readFile(fileName);
			expectations();

		}

		public void readFile(String fileName)
		{
			String text = System.IO.File.ReadAllText(@"C:\Users\Hallo\Desktop\Uni\Semester 8\BA\Validation Worm Wenger\" + fileName + ".txt");
			 
			String[] split = text.Split('\n');

			values = new double[length];

			 for (int x = 0; x < length; x++)
			 {
				 String s = split[x].Split(' ')[0];
				 values[x] = Convert.ToDouble(s);
			 }

			 expectations();
		}

		public void expectations()
		{
			double i = 0;
			//double j = 0;
			
			for(int x = 0; x < length; x++)
			{
				i += values[x];
				//j += Math.Pow(values[x],2);
			}

			expect = i / (double)length;
			energyExpect[position++] = expect;
			//squaredExpect = j / (double)length;
			
		}


	}
}
