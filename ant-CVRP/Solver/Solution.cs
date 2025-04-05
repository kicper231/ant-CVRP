using GraphRepresentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ant_CVRP.Solver
{
    public class Solution
    {
        public List<List<Edge>> Paths { get; set; }

        public double Rating { get; set; }

        public Solution() {
            Paths = new List<List<Edge>>() { };
            Rating = Double.MaxValue;
        }


        public void RateSolution ()
        {

        }

    }
}
