using GraphRepresentation;

namespace Ants
{
    public class Ant
    {
        public Ant(Graph graph, double capacityLimit, double lengthLimit, Point startPoint)
        {
            CapacityLimit = capacityLimit;
            Graph = graph;
            VisitedPoints = new List<Point>();
            UnvisitedPoints = new List<Point>();
            Path = new List<Edge>();
            PathLengthLimit = lengthLimit;
            CapacityLimit = CapacityLimit;
            StartPoint = startPoint;
            ActualPoint = startPoint;

            Point[] array = new Point[Graph.Points.Count];
            Graph.Points.CopyTo(array);
            UnvisitedPoints.AddRange(array);
            UnvisitedPoints.Remove(StartPoint);
        }

        public Graph Graph { get; set; }
        public List<Point> VisitedPoints { get; set; }
        public List<Point> UnvisitedPoints { get; set; }
        public List<Edge> Path { get; set; }

        public Point StartPoint { get; set; }
        public Point ActualPoint { get; set; }

        public double PathLength { get; set; }
        public double PathLengthLimit { get; set; }

        public double CapacityLimit { get; set; }
        public double Capacity { get; set; } = 0;

        public void Reset()
        {
            Path.Clear();
            PathLength = 0;
            Capacity = 0;
            ActualPoint = StartPoint;
        }

        public void ResetTotal()
        {
            VisitedPoints.Clear();
            UnvisitedPoints.Clear();
            Path.Clear();
            PathLength = 0;
            Capacity = 0;

            Point[] array = new Point[Graph.Points.Count];
            Graph.Points.CopyTo(array);
            UnvisitedPoints.AddRange(array);
            UnvisitedPoints.Remove(StartPoint);
        }

        public List<Edge> CopyPath()
        {
            return Path
                .Select(e => new Edge(e.Start, e.End)
                {
                    Length = e.Length,
                    Pheromone = e.Pheromone,
                    Weight = e.Weight,
                    Direction = e.Direction
                })
                .ToList();
        }
    }
}