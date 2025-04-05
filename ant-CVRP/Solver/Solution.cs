using GraphRepresentation;

namespace ant_CVRP.Solver
{
    public class Solution
    {
        public List<List<Edge>> Paths { get; set; }

        public double Rating { get; set; }

        public Solution()
        {
            Paths = new List<List<Edge>>() { };
            Rating = Double.MaxValue;
        }

        public void PrintSolution()
        {
            RateSolution();
            Console.WriteLine("=== Solution ===");
            int routeNumber = 1;

            foreach (var route in Paths)
            {
                Console.Write($"Route {routeNumber}: ");
                if (route.Count > 0)
                {
                    Console.Write($"{route[0].Start.Id + 1} -> ");
                    Console.Write($"{route[0].End.Id + 1} -> ");
                    int actualId = route[0].End.Id;

                    foreach (var edge in route.Skip(1))
                    {
                        actualId = actualId == edge.End.Id ? edge.Start.Id : edge.End.Id;
                        Console.Write($"{actualId + 1} -> ");
                    }
                    Console.WriteLine("1");
                }
                else
                {
                    Console.WriteLine("Empty");
                }

                double routeLength = route.Sum(e => e.Length);
                routeLength += route.Last().Direction ? route.Last().End.DistanceTo(route[0].Start) : route.Last().Start.DistanceTo(route[0].Start);
                Console.WriteLine($"  Route Length: {routeLength:F2}");
                routeNumber++;
            }
            Console.WriteLine($"Total Rating (Cost): {Rating:F2}");
        }

        public void RateSolution()
        {
            double totalLength = 0;

            foreach (var route in Paths)
            {
                foreach (var edge in route)
                {
                    totalLength += edge.Length;
                }
                if (route.Count > 0)
                {
                    var last = route[^1]; // ostatnia krawędź
                    double returnDistance = last.Direction ? last.End.DistanceTo(route[0].Start) : last.Start.DistanceTo(route[0].Start);
                    totalLength += returnDistance;
                }
            }

            Rating = totalLength;
        }
    }
}