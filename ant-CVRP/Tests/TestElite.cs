using AntSolver;
using GraphRepresentation;
using System.Globalization;

namespace Tests
{
    public class TestElite
    {
        public static void Run(string datasetPath, string outputCsvPath)
        {
            Graph graph = CVRPDataParser.Parse(datasetPath);

            string directory = Path.GetDirectoryName(outputCsvPath);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
                Console.WriteLine($"Created directory: {directory}");
            }

            var results = new List<string>();
            results.Add("EliteAntCount,Run,Rating");

            int[] elite = [1, 5, 10, 30];

            foreach (int i in elite)
            {
                double sumRatings = 0.0;

                for (int run = 1; run <= 10; run++)
                {
                    Parameters parameters = new Parameters
                    {
                        Alfa = 1,
                        Beta = 3,
                        AntsCount = 30,
                        Iterations = 500,
                        EvaporationRate = 0.1,
                        Q = 200,
                        EliteBoost = 3,
                        EliteCount = i,
                        StartPheromone = 0.5,
                        MinPheronome = 0.01,
                        MaxPheronome = 50.00,
                        CapacityLimit = graph.CapacityLimit,
                        LengthLimit = 99999,
                        EliteMode = true,
                        MinMaxMode = false,
                        LogStep = 10
                    };

                    Solver solver = new Solver(graph, parameters);
                    solver.Solve();

                    double rating = solver.BestSolution.Rating;
                    sumRatings += rating;

                    results.Add($"{i},{run},{rating.ToString(CultureInfo.InvariantCulture)}");
                    Console.WriteLine($"[Elite={i}  Run={run}] → Rating: {rating}");
                }

                double avgRating = sumRatings / 10.0;
                results.Add($"{i},avg,{avgRating.ToString(CultureInfo.InvariantCulture)}");
                Console.WriteLine($"→ Average for elite = {i}: {avgRating}\n");
            }

            File.WriteAllLines(outputCsvPath, results);
            Console.WriteLine($"\nTest results saved to: {outputCsvPath}");
        }
    }
}