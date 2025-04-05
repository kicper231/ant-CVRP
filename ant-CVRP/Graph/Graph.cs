

namespace GraphRepresentation
{
    public class Graph
    {
        public List<Point> Points { get; set; }

        // TODO: To change
        public Dictionary<(int,int), Edge> Edges { get; set; }
        public int Dimensions { get; set; }
        public double MinimumPheromone { get; set; }

        public Graph(List<Point> Points)
        {
            Edges = new Dictionary<(int,int), Edge>();
            this.Points = Points;
            Dimensions = Points.Count;
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

                        if(!Edges.ContainsKey((Math.Min(i, j), Math.Max(i, j))))
                        {
                           Edges.Add((Math.Min(i, j), Math.Max(i, j)), edge);
                        }
                    }
                }
            }
        }

        public Edge GetEdge(int firstPointId, int secondPointId)
        {
            return Edges[(Math.Min(firstPointId, secondPointId),Math.Max(firstPointId,secondPointId))];
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
            edge.Pheromone = Math.Max(MinimumPheromone, edge.Pheromone * value);
        }

        public void EvaporatePheromone(int firstId,int secondId, double value)
        {
            Edge edge = GetEdge(firstId, secondId);
            edge.Pheromone = Math.Max(MinimumPheromone, edge.Pheromone * value); 
        }

        public void DepositPheromone(Edge edge, double value)
        {
            edge.Pheromone += value;

        }

        public void DepositPheromone(int firstId, int secondId, double value)
        {
            Edge edge = GetEdge(firstId, secondId);
            edge.Pheromone = Math.Max(MinimumPheromone, edge.Pheromone * value);
            edge.Pheromone += value;

        }
        // chat generated 
        public void PrintPheromoneMatrix()
        {
            Console.WriteLine("=== Pheromone Matrix ===");
            Console.Write("     ");
            for (int i = 0; i < Points.Count; i++)
            {
                Console.Write($"{i+1,6}");
            }
            Console.WriteLine();

            for (int i = 0; i < Points.Count; i++)
            {
                Console.Write($"{i+1,4} ");
                for (int j = 0; j < Points.Count; j++)
                {
                    if (i == j)
                    {
                        Console.Write("   -  ");
                        continue;
                    }

                    var key = (Math.Min(i, j), Math.Max(i, j));
                    if (Edges.TryGetValue(key, out var edge))
                    {
                        Console.Write($"{edge.Pheromone,6:F2}");
                    }
                    else
                    {
                        Console.Write("   NA ");
                    }
                }
                Console.WriteLine();
            }
        }


        public void PrintDemands()
        {
            Console.WriteLine("=== Demands ===");
            foreach (var point in Points)
            {
                Console.WriteLine($"Point {point.Id}: Demand = {point.Demand}");
            }
        }

        public void PrintDistanceMatrix()
        {
            Console.WriteLine("=== Distance Matrix ===");
            Console.Write("     ");
            for (int i = 0; i < Points.Count; i++)
            {
                Console.Write($"{i+1,6}");
            }
            Console.WriteLine();

            for (int i = 0; i < Points.Count; i++)
            {
                Console.Write($"{i+1,4} ");
                for (int j = 0; j < Points.Count; j++)
                {
                    if (i == j)
                    {
                        Console.Write("   -  ");
                        continue;
                    }

                    var key = (Math.Min(i, j), Math.Max(i, j));
                    if (Edges.TryGetValue(key, out var edge))
                    {
                        Console.Write($"{edge.Length,6:F2}");
                    }
                    else
                    {
                        Console.Write("   NA ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}