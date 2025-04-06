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
            PointPath = new List<Point>();
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
        public List<Point> PointPath { get; set; }

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
            PointPath.Clear();
        }

        public void ResetTotal()
        {
            VisitedPoints.Clear();
            UnvisitedPoints.Clear();
            PointPath.Clear();
            Path.Clear();
            PathLength = 0;
            Capacity = 0;

            Point[] array = new Point[Graph.Points.Count];
            Graph.Points.CopyTo(array);
            UnvisitedPoints.AddRange(array);
            UnvisitedPoints.Remove(StartPoint);
        }

        public List<Point> CopyPath()
        {
            return PointPath.ToList();
        }
    }
}