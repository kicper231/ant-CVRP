using GraphRepresentation;
using AntSolver;

namespace Program
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Graph graph = CVRPDataParser.Parse("DataSets/E-n22-k4.txt");


            Solver solver = new Solver(graph);

            //graph.PrintDemands();
            //Console.WriteLine();
            //graph.PrintDistanceMatrix();
            //Console.WriteLine();
            //graph.PrintPheromoneMatrix();
        }
    }
}