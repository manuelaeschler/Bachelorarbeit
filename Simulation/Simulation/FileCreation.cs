using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation
{
	class FileCreation
	{

		public FileCreation(String text, String fileName)
		{
			System.IO.File.WriteAllText(@"C:\Users\Hallo\Desktop\Uni\Semester 8\BA\Validation Flip FermionFree\" + fileName + ".txt", text);
		}




	}
}
