using ant_CVRP.Solver;

namespace GraphRepresentation
{
    public class Graph
    {
        public List<Point> Points { get; set; }

        public Dictionary<(int, int), Edge> Edges { get; set; }

        public int Dimensions { get; set; }
        public double MinimumPheromone { get; set; }

        public double MaximumPheronome { get; set; }

        public double StartingPheronome { get; set; }

        public Point StartPoint { get; set; }

        public int CapacityLimit { get; set; }

        public bool MinMaxMode { get; set; }

        public Graph(List<Point> Points, int capacityLimit)
        {
            Edges = new Dictionary<(int, int), Edge>();
            this.Points = Points;
            Dimensions = Points.Count;
            StartPoint = Points[0];
            CapacityLimit = capacityLimit;
            CreateEdges();
        }

        public void Reset()
        {
            for (int i = 1; i <= Points.Count; i++)
            {
                for (int j = 1; j <= Points.Count; j++)
                {
                    if (i != j)
                    {
                        Edge edge = GetEdge(i, j);
                        edge.Pheromone = StartingPheronome;
                    }
                }
            }
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
                        edge.Pheromone = StartingPheronome;

                        if (!Edges.ContainsKey((Math.Min(edge.Start.Id, edge.End.Id), Math.Max(edge.Start.Id, edge.End.Id))))
                        {
                            Edges.Add((Math.Min(edge.Start.Id, edge.End.Id), Math.Max(edge.Start.Id, edge.End.Id)), edge);
                        }
                    }
                }
            }
        }

        public Edge GetEdge(int firstPointId, int secondPointId)
        {
            return Edges[(Math.Min(firstPointId, secondPointId), Math.Max(firstPointId, secondPointId))];
        }

        public Point GetPoint(int pointId)
        {
            Point result = Points[pointId - 1];
            if (result.Id != pointId)
            {
                throw new InvalidOperationException("");
            }
            return result;
        }

        public void EvaporatePheromone(int firstId, int secondId, double value)
        {
            Edge edge = GetEdge(firstId, secondId);

            if (MinMaxMode)
            {
                edge.Pheromone = Math.Max(MinimumPheromone, edge.Pheromone * value);
            }
            else
            {
                edge.Pheromone *= value;
            }
        }

        public void DepositPheromone(int firstId, int secondId, double value)
        {
            Edge edge = GetEdge(firstId, secondId);
            if (MinMaxMode)
            {
                edge.Pheromone = Math.Min(MaximumPheronome, edge.Pheromone + value);
            }
            else
            {
                edge.Pheromone += value;
            }
        }

        // chat generated
        public void PrintPheromoneMatrix()
        {
            Console.WriteLine("=== Pheromone Matrix ===");
            Console.Write("     ");
            for (int i = 0; i < Points.Count; i++)
            {
                Console.Write($"{i + 1,6}");
            }
            Console.WriteLine();

            for (int i = 1; i <= Points.Count; i++)
            {
                Console.Write($"{i + 1,4} ");
                for (int j = 1; j <= Points.Count; j++)
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
                Console.Write($"{i + 1,6}");
            }
            Console.WriteLine();

            for (int i = 1; i <= Points.Count; i++)
            {
                Console.Write($"{i + 1,4} ");
                for (int j = 1; j <= Points.Count; j++)
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

        public void PrintPoints()
        {
            Console.WriteLine("=== Points ===");
            Console.WriteLine(" ID   X     Y   Demand");
            foreach (var point in Points)
            {
                Console.WriteLine($"{point.Id,3} {point.X,5} {point.Y,5}   {point.Demand,6}");
            }
        }

        // Chat gtp generated
        public static Solution CreateManualSolution(Graph graph)
        {
            var routeNodeIds = new List<List<int>>
    {
        new List<int> { 1,22, 5, 6, 9, 10, 8, 1 },             // Route #1
        new List<int> { 1,19, 20, 21, 23, 18, 15, 16, 17, 4, 3, 2, 7, 12, 13, 1 }, // Route #2
        new List<int> { 1,14, 11,1 }                          // Route #3
    };

            routeNodeIds = new List<List<int>>
{
    new List<int> { 1, 23, 22, 21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 1 }, // Route #1
    new List<int> { 1, 11, 1 },                                           // Route #2
    new List<int> { 1, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 }                     // Route #3
};

            routeNodeIds = new List<List<int>>
{
    new List<int> { 1, 22, 5, 6, 9, 10, 8, 1 },                     // Route #1
    new List<int> { 1, 19, 20, 21, 23, 18, 15, 16, 17, 4, 3, 2, 7, 12, 13, 1 }, // Route #2
    new List<int> { 1, 14, 11, 1 }                                  // Route #3
        };

            routeNodeIds = new List<List<int>>
{
    new List<int> { 1, 18, 21, 19, 16, 13, 1 },       // Route #1
    new List<int> { 1, 17, 20, 22, 15, 1 },           // Route #2
    new List<int> { 1, 14, 12, 5, 4, 9, 11, 1 },      // Route #3
    new List<int> { 1, 10, 8, 6, 3, 2, 7, 1 }         // Route #4
};

            var solution = new Solution(graph);

            foreach (var nodeIds in routeNodeIds)
            {
                var routePoints = new List<Point>();

                foreach (int current in nodeIds)
                {
                    routePoints.Add(graph.GetPoint(current));
                }

                solution.Paths.Add(routePoints);
            }

            solution.RateSolution();
            return solution;
        }
    }
}