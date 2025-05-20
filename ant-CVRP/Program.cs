using Tests;

namespace Program
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //Graph graph = CVRPDataParser.Parse("DataSets/E-n22-k4.txt");

            //graph.PrintPoints();
            //graph.PrintDistanceMatrix();

            //Solver solver = new Solver(graph, null);

            //solver.Solve();

            //GreedySolver solverGreedy = new GreedySolver(graph);

            //solver.BestSolution.PrintSolution();
            //solverGreedy.Solve();

            //TestMinMax.Run("DataSets/E-n23-k3.txt", "C:\\Users\\Kacper\\source\\repos\\ant-CVRP\\ant-CVRP\\Tests\\Results\\minMax_results_3_n23-k3_1.csv");
            //TestMinMax.Run("DataSets/E-n33-k4.txt", "C:\\Users\\Kacper\\source\\repos\\ant-CVRP\\ant-CVRP\\Tests\\Results\\minMax_results_3_n33-k3_1.csv");
            //TestMinMax.Run("DataSets/E-n51-k5.txt", "C:\\Users\\Kacper\\source\\repos\\ant-CVRP\\ant-CVRP\\Tests\\Results\\minMax_results_3_n51-k3_1.csv");
            //TestCompareAlgorithms.Run("DataSets/E-n23-k3.txt", "C:\\Users\\Kacper\\source\\repos\\ant-CVRP\\ant-CVRP\\Tests\\Results\\compare_n23_elite_false.csv");
            //TestCompareAlgorithms.Run("DataSets/E-n33-k4.txt", "C:\\Users\\Kacper\\source\\repos\\ant-CVRP\\ant-CVRP\\Tests\\Results\\compare_n33_elite_false.csv");
            //TestCompareAlgorithms.Run("DataSets/E-n51-k5.txt", "C:\\Users\\Kacper\\source\\repos\\ant-CVRP\\ant-CVRP\\Tests\\Results\\compare_n51_elite_false.csv");

            TestCompareAlgorithms.Run("DataSets/E-n23-k3.txt", "C:\\Users\\Kacper\\source\\repos\\ant-CVRP\\ant-CVRP\\Tests\\Results\\compare_n23_minMax1.csv");
            TestCompareAlgorithms.Run("DataSets/E-n51-k5.txt", "C:\\Users\\Kacper\\source\\repos\\ant-CVRP\\ant-CVRP\\Tests\\Results\\compare_n51_minMax1.csv");
            TestCompareAlgorithms.Run("DataSets/E-n33-k4.txt", "C:\\Users\\Kacper\\source\\repos\\ant-CVRP\\ant-CVRP\\Tests\\Results\\compare_n33_minMax1.csv");

            //TestCompareAlgorithms.Run("DataSets/E-n23-k3.txt", "C:\\Users\\Kacper\\source\\repos\\ant-CVRP\\ant-CVRP\\Tests\\Results\\compare_n23_minMax.csv");
            //TestCompareAlgorithms.Run("DataSets/E-n51-k5.txt", "C:\\Users\\Kacper\\source\\repos\\ant-CVRP\\ant-CVRP\\Tests\\Results\\compare_n51_minMax.csv");
            //TestCompareAlgorithms.Run("DataSets/E-n33-k4.txt", "C:\\Users\\Kacper\\source\\repos\\ant-CVRP\\ant-CVRP\\Tests\\Results\\compare_n33_minMax.csv");
            //TestAB.Run("DataSets/E-n23-k3.txt", "C:\\Users\\Kacper\\source\\repos\\ant-CVRP\\ant-CVRP\\Tests\\Results\\ab_results_3_n23-k3_1.csv");

            //TestElite.Run("DataSets/E-n23-k3.txt", "C:\\Users\\Kacper\\source\\repos\\ant-CVRP\\ant-CVRP\\Tests\\Results\\elite_n23-k4.csv");
            //graph.PrintDemands();
            //Console.WriteLine();
            //Console.WriteLine();
            //graph.PrintPheromoneMatrix();
        }
    }
}