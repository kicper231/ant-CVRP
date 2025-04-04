
using System.Drawing;

namespace AntColony
{
    public class Graph
    {
        public List<Point> Points { get; set; }

        // TODO: To change
        public Dictionary<int, Edge> Edges { get; set; }
        public int Dimensions { get; set; }
        public double MinimumPheromone { get; set; }
        private bool IsSymetric { get; set; }

        public Graph(List<Point> Points, bool isSymetric)
        {
            Edges = new Dictionary<int, Edge>();
            this.Points = Points;
            Dimensions = Points.Count;
            IsSymetric = isSymetric;
            CreateEdges();
        }

        private void CreateEdges()
        {
            for (int i = 0; i < Points.Count; i++)
            {
                for (int j = 0; j < Points.Count; j++)
                {
                    if (i != j)
                    {
                        Edge edge = new Edge(Points[i], Points[j]);
                      //  Edges.Add(Helper.HashFunction(Points[i].Id, Points[j].Id), edge);
                    }
                }
            }
        }

        public Edge GetEdge(int firstPointId, int secondPointId)
        {
            // return Edges[Helper.HashFunction(firstPointId, secondPointId)];
            return new Edge(new Point(2, 2, 5),new Point(2,3,4)); // @Todo
        }

        public void ResetPheromone(double pheromoneValue)
        {
            foreach (var edge in Edges)
            {
                edge.Value.Pheromone = pheromoneValue;
            }
        }

        public void EvaporatePheromone(Edge edge, double value)
        {
            edge.Pheromone = Math.Max(MinimumPheromone, edge.Pheromone * value); // Math.Max is here to prevent Pheromon = 0

            if (IsSymetric)
            {
                var secondEdge = GetEdge(edge.End.Id, edge.Start.Id);
                secondEdge.Pheromone = Math.Max(MinimumPheromone, secondEdge.Pheromone * value);
            }
        }

        public void DepositPheromone(Edge edge, double value)
        {
            edge.Pheromone += value;

            if (IsSymetric)
            {
                var secondEdge = GetEdge(edge.End.Id, edge.Start.Id);
                secondEdge.Pheromone += value;
            }
        }
    }
}