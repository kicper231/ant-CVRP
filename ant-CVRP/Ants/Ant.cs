using GraphRepresentation;
using System.Collections.Generic;

namespace Ants
{
    public class Ant
    {
        public Ant(Graph graph, double capacityLimit, double lengthLimit)
        {
            CapacityLimit = capacityLimit;
            Graph = graph;
            VisitedNodes = new List<Point>();
            UnvisitedNodes = new List<Point>();
            Path = new List<Edge>();
            PathLengthLimit  = lengthLimit;
            CapacityLimit = CapacityLimit;
        }

        public Graph Graph { get; set; }
        public List<Point> VisitedNodes { get; set; }
        public List<Point> UnvisitedNodes { get; set; }
        public List<Edge> Path { get; set; }

        public double PathLength { get; set; }
        public double PathLengthLimit { get; set; }

        public double CapacityLimit { get; set; }
        public double Capacity { get; set; } = 0;


        public void Reset()
        {
            VisitedNodes.Clear();
            UnvisitedNodes.Clear();
            Path.Clear();
            PathLength = 0;
            Capacity = 0;

            Point[] array = new Point[Graph.Points.Count];
            Graph.Points.CopyTo(array);
            UnvisitedNodes.AddRange(array);
        }
    }
}