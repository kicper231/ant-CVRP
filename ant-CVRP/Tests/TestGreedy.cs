using AntSolver;
using GraphRepresentation;
using System.Globalization;

namespace Tests
{
    public class TestCompareAlgorithms
    {
        public static void Run(string datasetPath, string outputCsvPath)
        {
            Graph graph = CVRPDataParser.Parse(datasetPath);
            string directory = Path.GetDirectoryName(outputCsvPath)!;

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
                Console.WriteLine($"Created directory: {directory}");
            }

            List<string> results = new List<string>();
            results.Add("Run,Algorithm,Rating");

            double sumGreedy = 0;
            double sumAnt = 0;

            for (int run = 1; run <= 10; run++)
            {
                Console.WriteLine($"\n=== Run {run} ===");

                var greedySolver = new GreedySolver(graph);
                var greedySolution = greedySolver.Solve();
                double greedyRating = greedySolution.Rating;
                sumGreedy += greedyRating;
                Console.WriteLine($"Greedy rating: {greedyRating}");

                // Ant
                var parameters = new Parameters
                {
                    Alfa = 1,
                    Beta = 3,
                    AntsCount = 30,
                    Iterations = 500,
                    EvaporationRate = 0.1,
                    Q = 200,
                    EliteBoost = 3,
                    EliteCount = 5,
                    StartPheromone = 0.5,
                    MinPheronome = 0.1,
                    MaxPheronome = 50.00,
                    CapacityLimit = graph.CapacityLimit,
                    LengthLimit = 99999,
                    EliteMode = false,
                    MinMaxMode = false,
                    LogStep = 10
                };

                var antSolver = new Solver(graph, parameters);
                antSolver.Solve();
                double antRating = antSolver.BestSolution.Rating;
                sumAnt += antRating;

                // Zbieżność
                //foreach (var entry in antSolver.SolutionsLog)
                //{
                //    results.Add($"{run},Ant,{entry.Iteration},{entry.Rating.ToString(CultureInfo.InvariantCulture)}");
                //}

                // Finalny wynik
                results.Add($"{run},Ant,{antRating.ToString(CultureInfo.InvariantCulture)}");
                //Console.WriteLine($"Ant rating: {antRating}");
            }

            double avgGreedy = sumGreedy / 10;
            double avgAnt = sumAnt / 10;

            //results.Add($"avg,Greedy,{avgGreedy.ToString(CultureInfo.InvariantCulture)}");
            //results.Add($"avg,Ant,{avgAnt.ToString(CultureInfo.InvariantCulture)}");

            File.WriteAllLines(outputCsvPath, results);
            Console.WriteLine($"\nResults saved to {outputCsvPath}");
        }
    }
}