using ant_CVRP.Solver;
using Ants;
using GraphRepresentation;
using System.Diagnostics;

namespace AntSolver
{
    public class Solver
    {
        public Parameters Parameters { get; set; }
        public Solution BestSolution { get; set; }

        private List<Ant> Ants { get; set; }
        private Graph Graph { get; set; }
        private Stopwatch Stopwatch { get; set; }
        
        private static readonly Random _random = new Random();

        private List<Solution> Solutions { get; set; }

        public Solver(Graph graph, Parameters? parameters)
        {
            Ants = new List<Ant>();
            Graph = graph;

            Solutions = new List<Solution>();
            if (parameters == null)
            {
                Parameters = new Parameters();
            }
            else
            {
                Parameters = parameters!;
            }

            Graph.MinimumPheromone = Parameters!.MinPheronome;
            Parameters.CapacityLimit = graph.CapacityLimit;
        }

        public void Solve()
        {
            //Solution a = Graph.CreateManualSolution(Graph);
            //a.PrintSolution();

            Graph.PrintPheromoneMatrix();
            // Create Ants
            CreateAnts();
            //Solution solution = OneAntSolution(Ants[0]);
            //solution.PrintSolution();
            //Solutions.Add(solution);

            for (int i = 1; i <= Parameters.Iterations; i++)
            {
                //Graph.PrintPheromoneMatrix();
                Solutions.Clear();
                ResetAnts();

                for (int j = 0; j < Parameters.AntsCount; j++)
                {
                    Solution solution = OneAntSolution(Ants[j]);
                    //Console.WriteLine();
                    //   solution.PrintSolution();
                    //  Console.WriteLine();
                    Solutions.Add(solution);

                    if (i == 1 && j == 0)
                    {
                        BestSolution = solution;
                    }
                }

                // Wyciagnij najlepsze rozwiazanie
               


                if (BestSolution.Rating > Solutions.Min(x => x.Rating))
                {
                    BestSolution = Solutions.MinBy(x => x.Rating)!;
                }

                if (i % 500== 0)
                {
                    Console.WriteLine();
                    foreach (var x in Solutions)
                    {
                        Console.Write($"{x.Rating} ");
                    }
                    Console.WriteLine();

                    //Graph.PrintPheromoneMatrix();
                    BestSolution.PrintSolution();
                }

                // Update Pheronome
                UpdatePheromone();
            }
        }

        public Solution OneAntSolution(Ant ant)
        {
            Solution solution = new Solution(Graph);


            while (ant.UnvisitedPoints.Count != 0)
            {
                ant.Reset();

                ant.PointPath.Add(ant.StartPoint);

                while (ant.UnvisitedPoints.Count != 0)
                {
                    //double[] probabilities = ant.UnvisitedPoints
                    //                            .Select(x => Math.Pow(Graph.GetEdge(ant.ActualPoint.Id, x.Id).Pheromone, Parameters.Alfa) *
                    //                             Math.Pow(1 / (Graph.GetEdge(ant.ActualPoint.Id, x.Id).Length), Parameters.Beta))
                    //                            .ToArray();

                    List<double> probabilitiesList = new List<double>();    

                    foreach (var point in ant.UnvisitedPoints)
                    {
                        var edge = Graph.GetEdge(ant.ActualPoint.Id, point.Id);
                        double pheromone = edge.Pheromone;
                        double distance = edge.Length;

                        double pheromoneComponent = Math.Pow(pheromone, Parameters.Alfa);
                        double distanceComponent = Math.Pow(1.0 / distance, Parameters.Beta);
                        double probability = pheromoneComponent * distanceComponent;

                       // Console.WriteLine($"To point {point.Id} → pheromone: {pheromone}, distance: {distance}, pher^a: {pheromoneComponent}, (1/d)^b: {distanceComponent}, prob: {probability}");

                        probabilitiesList.Add(probability);
                    }

                    double sum = probabilitiesList.Sum(x => x);
                    double[] probabilities = probabilitiesList.Select(x => x / sum).ToArray();
                    double checksum = probabilities.Sum(x => x);

                    Point choosenPoint = SelectRandomByProbability(ant.UnvisitedPoints, probabilities);

                    Edge choosenEdge = Graph.GetEdge(ant.ActualPoint.Id, choosenPoint.Id);
                    ant.Capacity += choosenPoint.Demand;

                    if (ant.Capacity <= ant.CapacityLimit)
                    {
                        ant.Path.Add(choosenEdge);
                        ant.PointPath.Add(choosenPoint);
                        ant.ActualPoint = choosenPoint;
                        ant.VisitedPoints.Add(choosenPoint);
                        ant.UnvisitedPoints.Remove(choosenPoint);
                        ant.PathLength += choosenEdge.Length;
                    }
                    else
                    {
                        break;
                    }
                }

                Edge returnEdge = Graph.GetEdge(ant.ActualPoint.Id, ant.StartPoint.Id);
                ant.Path.Add(returnEdge);
                ant.PointPath.Add(ant.StartPoint);
                ant.PathLength += returnEdge.Length;
                solution.Paths.Add(ant.CopyPath());
            }

            solution.RateSolution();

            return solution;
        }

        public void CreateAnts()
        {
            for (int i = 0; i < Parameters.AntsCount; i++)
            {
                Ant ant = new Ant(Graph, Parameters.CapacityLimit, Parameters.LengthLimit, Graph.StartPoint);
                Ants.Add(ant);
            }
        }

        public void ResetAnts()
        {
            for (int i = 0; i < Parameters.AntsCount; i++)
            {
                Ants[i].ResetTotal();
            }
        }

        public void UpdatePheromone()
        {
            foreach (var edgeEntry in Graph.Edges)
            {
                var edge = edgeEntry.Value;
                Graph.EvaporatePheromone(edge.Start.Id, edge.End.Id, (1 - Parameters.EvaporationRate));
            }

            foreach (var solution in Solutions)
            {
                double depositAmount = Parameters.Q / solution.Rating;

                foreach (var path in solution.Paths)
                {
                    for (int i = 0; i < path.Count - 1; i++)
                    {
                        Point current = path[i];
                        Point next = path[i + 1];

                        var edge = Graph.GetEdge(current.Id, next.Id);
                        Graph.DepositPheromone(edge.Start.Id, edge.End.Id, depositAmount);
                    }
                }
            }

            // (Elitist Ant System)
            var eliteSolutions = Solutions.OrderBy(s => s.Rating).Take(Parameters.EliteCount);

            foreach (var eliteSolution in eliteSolutions)
            {
                double eliteDeposit = (Parameters.Q / eliteSolution.Rating)*Parameters.EliteBoost;

                foreach (var path in eliteSolution.Paths)
                {
                    for (int i = 0; i < path.Count - 1; i++)
                    {
                        Point current = path[i];
                        Point next = path[i + 1];

                        var edge = Graph.GetEdge(current.Id, next.Id);
                        Graph.DepositPheromone(edge.Start.Id, edge.End.Id, eliteDeposit);
                    }
                }
            }
        }

        public static T SelectRandomByProbability<T>(List<T> items, double[] probabilities)
        {
            if (items.Count != probabilities.Length)
                throw new ArgumentException("Items and probabilities must have the same length.");

            double randomValue = _random.NextDouble();
            double cumulative = 0.0;

            for (int i = 0; i < items.Count; i++)
            {
                cumulative += probabilities[i];
                if (randomValue <= cumulative)
                    return items[i];
            }

            // fallback – w razie błędu numerycznego zwróć ostatni
            return items.Last();
        }
    }
}