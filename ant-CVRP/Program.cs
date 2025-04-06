using AntSolver;
using GraphRepresentation;

namespace Program
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Graph graph = CVRPDataParser.Parse("DataSets/E-n22-k4.txt");

            graph.PrintPoints();
            graph.PrintDistanceMatrix();

            Solver solver = new Solver(graph, null);

            solver.Solve();
            

            GreedySolver solverGreedy = new GreedySolver(graph);

            solverGreedy.Solve();

            // solver.BestSolution.PrintSolution();

            //graph.PrintDemands();
            //Console.WriteLine();
            //Console.WriteLine();
            //graph.PrintPheromoneMatrix();
        }
    }
}