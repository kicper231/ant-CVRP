using AntSolver;
using GraphRepresentation;
using System.Globalization;

namespace Tests
{
    public class TestAB
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
            results.Add("Alpha,Beta,Run,Rating");

            for (int alpha = 1; alpha <= 10; alpha++)
            {
                for (int beta = 1; beta <= 10; beta++)
                {
                    double sumRatings = 0.0;

                    for (int run = 1; run <= 5; run++)
                    {
                        Parameters parameters = new Parameters
                        {
                            Alfa = alpha,
                            Beta = beta,
                            AntsCount = 30,
                            Iterations = 300,
                            EvaporationRate = 0.1,
                            Q = 200,
                            EliteBoost = 3,
                            EliteCount = 5,
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

                        results.Add($"{alpha},{beta},{run},{rating.ToString(CultureInfo.InvariantCulture)}");
                        Console.WriteLine($"[Alpha={alpha} | Beta={beta} | Run={run}] → Rating: {rating}");
                    }

                    double avgRating = sumRatings / 5.0;
                    results.Add($"{alpha},{beta},avg,{avgRating.ToString(CultureInfo.InvariantCulture)}");
                    Console.WriteLine($"→ Average for Alpha={alpha}, Beta={beta}: {avgRating}\n");
                }
            }

            File.WriteAllLines(outputCsvPath, results);
            Console.WriteLine($"\nTest results saved to: {outputCsvPath}");
        }
    }
}