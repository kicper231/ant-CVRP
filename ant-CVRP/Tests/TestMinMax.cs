using AntSolver;
using GraphRepresentation;
using System.Globalization;

namespace Tests
{
    public class TestMinMax
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
            results.Add("Min,Max,Run,Rating");

            double[] min = [0.01, 0.1, 0.5, 1];
            double[] max = [10, 30, 50, 100];

            foreach (var minx in min)
            {
                foreach (var maxs in max)
                {
                    double sumRatings = 0.0;

                    for (int run = 1; run <= 5; run++)
                    {
                        Parameters parameters = new Parameters
                        {
                            Alfa = 1,
                            Beta = 3,
                            AntsCount = 30,
                            Iterations = 300,
                            EvaporationRate = 0.1,
                            Q = 200,
                            EliteBoost = 3,
                            EliteCount = 5,
                            StartPheromone = 0.5,
                            MinPheronome = minx,
                            MaxPheronome = maxs,
                            CapacityLimit = graph.CapacityLimit,
                            LengthLimit = 99999,
                            EliteMode = false,
                            MinMaxMode = false,
                            LogStep = 10
                        };

                        Solver solver = new Solver(graph, parameters);
                        solver.Solve();

                        double rating = solver.BestSolution.Rating;
                        sumRatings += rating;

                        results.Add($"{minx},{maxs},{run},{rating.ToString(CultureInfo.InvariantCulture)}");
                        Console.WriteLine($"[minx={minx} | max={maxs} | Run={run}] → Rating: {rating}");

                        //foreach (var entry in solver.SolutionsLog)
                        //{
                        //    results.Add($"{run},Ant,{entry.Iteration},{entry.Rating.ToString(CultureInfo.InvariantCulture)}");
                        //}
                    }

                    double avgRating = sumRatings / 5.0;
                    results.Add($"{minx},{maxs},avg,{avgRating.ToString(CultureInfo.InvariantCulture)}");
                    Console.WriteLine($"→ Average for Min={minx}, Max={maxs}: {avgRating}\n");
                }
            }

            File.WriteAllLines(outputCsvPath, results);
            Console.WriteLine($"\nTest results saved to: {outputCsvPath}");
        }
    }
}