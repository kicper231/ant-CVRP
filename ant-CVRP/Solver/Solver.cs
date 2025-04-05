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
        private List<double> Results { get; set; }
        private Graph Graph { get; set; }
        private Stopwatch Stopwatch { get; set; }

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

            Parameters.CapacityLimit = graph.CapacityLimit;
        }

        public void Solve()
        {
            // Create Ants
            CreateAnts();
            //Solution solution = OneAntSolution(Ants[0]);
            //solution.PrintSolution();
            //Solutions.Add(solution);

            for (int i = 1; i <= Parameters.Iterations; i++)
            {
                Solutions.Clear();
                ResetAnts();

                for (int j = 0; j < Parameters.AntsCount; j++)
                {
                    Solution solution = OneAntSolution(Ants[j]);
                    //Console.WriteLine();
                 //   solution.PrintSolution();
                  //  Console.WriteLine();
                    Solutions.Add(solution);

                    if (i == 1 && j==0)
                    {
                        BestSolution = solution;
                    }
                }

                // Wyciagnij najlepsze rozwiazanie

                if(BestSolution.Rating > Solutions.Min(x=>x.Rating)) {
                    BestSolution = Solutions.MinBy(x => x.Rating);
                }
                // Update Pheronome
                UpdatePheromone();
            }
             
        }

        public Solution OneAntSolution(Ant ant)
        {
            Solution solution = new Solution();

            while (ant.UnvisitedPoints.Count != 0)
            {
                ant.Reset();
                while (ant.UnvisitedPoints.Count != 0)
                {
                    double[] probabilities = ant.UnvisitedPoints
                                                .Select(x => Math.Pow(Graph.GetEdge(ant.ActualPoint.Id, x.Id).Pheromone, Parameters.Alfa) *
                                                 Math.Pow(1 / (Graph.GetEdge(ant.ActualPoint.Id, x.Id).Length), Parameters.Beta))
                                                .ToArray();

                    double sum = probabilities.Sum(x => x);
                    probabilities = probabilities.Select(x => x / sum).ToArray();
                    double checksum = probabilities.Sum(x => x);

                    Point choosenPoint = SelectRandomByProbability(ant.UnvisitedPoints, probabilities);

                    Edge choosenEdge = Graph.GetEdge(ant.ActualPoint.Id, choosenPoint.Id);
                    choosenEdge.Direction = ant.ActualPoint.Id < choosenPoint.Id ? true : false;
                    ant.Capacity += choosenPoint.Demand;

                    if (ant.Capacity <= ant.CapacityLimit)
                    {
                        ant.Path.Add(choosenEdge);
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
            // Wyparowanie feromonu (tau_ij = (1 - rho) * tau_ij)
            foreach (var edgeEntry in Graph.Edges)
            {
                var edge = edgeEntry.Value;
                edge.Pheromone *= (1 - Parameters.Rho);
            }

            // Osadzanie feromonu przez wszystkie mrówki (Delta_tau = Q / Lk)
            foreach (var solution in Solutions)
            {
                double depositAmount = Parameters.Q / solution.Rating;

                foreach (var path in solution.Paths)
                {
                    foreach (var edge in path)
                    {
                        Graph.DepositPheromone(edge, depositAmount);
                    }
                }
            }

            // Wzmocnienie ścieżek przez wybrane najlepsze elitarne mrówki (Elitist Ant System)
            var eliteSolutions = Solutions.OrderBy(s => s.Rating).Take(Parameters.EliteCount);

            foreach (var eliteSolution in eliteSolutions)
            {
                double eliteDeposit = Parameters.Q / eliteSolution.Rating;

                foreach (var path in eliteSolution.Paths)
                {
                    foreach (var edge in path)
                    {
                        Graph.DepositPheromone(edge, eliteDeposit);
                    }
                }
            }
        }

        public static T SelectRandomByProbability<T>(List<T> items, double[] probabilities)
        {
            if (items.Count != probabilities.Length)
                throw new ArgumentException("Items and probabilities must have the same length.");

            double randomValue = new Random().NextDouble();
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