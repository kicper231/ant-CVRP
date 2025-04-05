using AntSolver;
using GraphRepresentation;

namespace Program
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Graph graph = CVRPDataParser.Parse("DataSets/Example.txt");

            graph.PrintDistanceMatrix();

            Solver solver = new Solver(graph, null);

            solver.Solve();

            solver.BestSolution.PrintSolution();

            //graph.PrintDemands();
            //Console.WriteLine();
            //Console.WriteLine();
            //graph.PrintPheromoneMatrix();
        }
    }
}