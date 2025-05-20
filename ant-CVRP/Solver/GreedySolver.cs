using ant_CVRP.Solver;
using GraphRepresentation;

namespace AntSolver
{
    public class GreedySolver
    {
        private Graph Graph { get; set; }
        private List<Point> UnvisitedPoints { get; set; }
        private Point StartPoint { get; set; }
        private int CapacityLimit { get; set; }

        public GreedySolver(Graph graph)
        {
            Graph = graph;
            StartPoint = graph.StartPoint;
            CapacityLimit = graph.CapacityLimit;
            UnvisitedPoints = graph.Points.Where(p => p.Id != StartPoint.Id).ToList();
        }

        public Solution Solve()
        {
            Solution solution = new Solution(Graph);

            while (UnvisitedPoints.Any())
            {
                List<Point> route = new List<Point>();
                Point current = StartPoint;
                int capacityUsed = 0;
                route.Add(current);

                while (true)
                {
                    var candidates = UnvisitedPoints
                        .Where(p => p.Demand + capacityUsed <= CapacityLimit)
                        .ToList();

                    if (!candidates.Any())
                        break;

                    Point next = candidates
                        .OrderBy(p => Graph.GetEdge(current.Id, p.Id).Length)
                        .First();

                    capacityUsed += next.Demand;
                    route.Add(next);
                    UnvisitedPoints.Remove(next);
                    current = next;
                }

                route.Add(StartPoint);
                solution.Paths.Add(route);
            }

            solution.RateSolution();
            solution.PrintSolution();
            return solution;
        }
    }
}