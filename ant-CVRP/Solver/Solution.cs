using GraphRepresentation;

namespace ant_CVRP.Solver
{
    public class Solution
    {
        private Graph Graph { get; set; }
        public List<List<Point>> Paths { get; set; }

        public List<int> Demands { get; set; }
        public List<int> Lengths { get; set; }

        public double Rating { get; set; }

        public Solution(Graph graph)
        {
            Graph = graph;
            Paths = new List<List<Point>>() { };
            Rating = Double.MaxValue;
            Demands = new List<int>();
            Lengths = new List<int>();
        }

        public void PrintSolution()
        {
            RateSolution();
            Console.WriteLine("=== Solution ===");
            int routeNumber = 0;

            foreach (var route in Paths)
            {
                Console.Write($"Route {routeNumber + 1}: ");
                if (route.Count > 0)
                {
                    foreach (var point in route)
                    {
                        Console.Write($"{point.Id} ->");
                    }
                }
                else
                {
                    Console.WriteLine("Empty");
                }

                Console.WriteLine($"  Route Length: {Lengths[routeNumber]:F2}  Demand: {Demands[routeNumber]:F2}");
                routeNumber++;
            }
            Console.WriteLine($"Total Rating (Cost): {Rating:F2}");
        }

        public void RateSolution()
        {
            int totalLength = 0;
            int demand = 0;

            foreach (var route in Paths)
            {
                Point currentPoint = route[0];
                int currentLenght = 0;

                foreach (var nextPoint in route.Skip(1))
                {
                    Edge edge = Graph.GetEdge(currentPoint.Id, nextPoint.Id);
                    currentLenght += edge.Length;

                    demand += nextPoint.Demand;
                    currentPoint = nextPoint;
                }
                Demands.Add(demand);
                totalLength += currentLenght;
                Lengths.Add(currentLenght);
                demand = 0;
            }

            Rating = totalLength;
        }
    }
}