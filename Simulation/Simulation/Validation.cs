using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation
{
	class Validation
	{

		private int[] values;
		private double expect;
		private double[] energyExpect;
		private double[] specHeat;
		private double[] statError;
		private double squaredExpect;

		private int length = 100000;
		private double beta;
		private int position;

		public Validation(double[] energyExpect, double[] specHeat, double[] statError)
		{
			this.energyExpect = energyExpect;
			this.specHeat = specHeat;
			this.statError = statError;

			//Console.Write("Statistical error: " + statError + "\nEnergy expecation: " + energyExpect + "\nSpecific heat: " + specHeat);
		}

		public void calculate(int position, String fileName, double beta)
		{
			this.position = position;
			this.beta = beta;
			readFile(fileName);
			expectations();
			statisticalError();
			specificHeat();
		}

		private void readFile(String fileName)
		{
			String text = System.IO.File.ReadAllText(@"C:\Users\Hallo\Desktop\Uni\Semester 8\BA\Validation Ising\" + fileName + ".txt");
			 
			String[] split = text.Split('	');
			//length = split.GetLength(0);
			values = new int[length];

			 for (int x = 0; x < length; x++)
			 {
				 String s = split[x];
				 values[x] = Convert.ToInt32(s);
			 }
		}

		private void expectations()
		{
			int i = 0;
			double j = 0;
			
			for(int x = 0; x < length; x++)
			{
				i += values[x];
				j += Math.Pow(values[x],2);
			}

			expect = (double)i / (double)length;
			energyExpect[position] = -expect / beta;
			squaredExpect = j / (double)length;
			
		}

		private void statisticalError()
		{
			statError[position] = Math.Sqrt(squaredExpect - (expect*expect));
		}

		private void specificHeat()
		{
			specHeat[position] = Math.Pow(statError[position], 2) - expect; 
		}


	}
}
