using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation
{
    interface Algorithm
    {
        Brick[,] Field { get; set; }
        float[,] Probabilities {set; }

        Boolean change(int x, int y);

    }
}
