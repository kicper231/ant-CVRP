using ant_CVRP.Solver;
using Ants;
using GraphRepresentation;
using System.Diagnostics;

namespace AntSolver
{
    public class Solver
    {
        public Parameters Parameters { get; set; }
        private Ant GlobalBestAnt { get; set; }

        private List<Ant> Ants { get; set; }
        private List<double> Results { get; set; }
        private Graph Graph { get; set; }
        private Stopwatch Stopwatch { get; set; }

        private List<Solution> Solutions { get; set; }

        public Solver(Graph graph)
        {
            Graph = graph;
            Solutions = new List<Solution>();
            Parameters = new Parameters();
            Parameters.CapacityLimit = graph.CapacityLimit;
        }

        public Solver(Graph graph, Parameters parameters)
        {
            Graph = graph;
            Solutions = new List<Solution>();
            Parameters = parameters;
            Parameters.CapacityLimit = graph.CapacityLimit;

        }

        public void Solve()
        {
            // Create Ants
            CreateAnts();
            Solution solution = OneAntSolution(Ants[0]);
            Solutions.Add(solution);

            //for (int i = 1; i <= Parameters.Iterations; i++)
            //{
            //    Solutions.Clear();
            //    ResetAnts();

            //    for (int j = 0; j < Parameters.AntsCount; j++)
            //    {
            //Solution solution = OneAntSolution(Ants[j]);
            //Solutions.Add(solution);
            //    }

            //    // Wyciagnij najlepsze rozwiazanie

            //    // Update Pheronome
            //    UpdatePheromone();
            //}
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
                                                .Select(x => Graph.Edges[(ant.ActualPoint.Id, x.Id)].Pheromone * Parameters.Alfa +
                                                 Math.Pow(1 / (Graph.Edges[(ant.ActualPoint.Id, x.Id)].Length), Parameters.Beta))
                                                .ToArray();

                    double sum = probabilities.Sum(x => x);
                    probabilities = probabilities.Select(x => x / sum).ToArray();

                    Point choosenPoint = SelectRandomByProbability(ant.UnvisitedPoints, probabilities);

                    Edge choosenEdge = Graph.GetEdge(ant.ActualPoint.Id, choosenPoint.Id);

                    ant.Capacity += choosenPoint.Demand;

                    if (ant.Capacity <= ant.CapacityLimit)
                    {
                        ant.Path.Add(choosenEdge);
                        ant.VisitedPoints.Add(choosenPoint);
                        ant.UnvisitedPoints.Remove(choosenPoint);
                        ant.PathLength += choosenEdge.Length;
                    }
                    else
                    {
                        break;
                    }
                }
                solution.Paths.Add(ant.Path);
            }

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

        public void RateSolution()
        {
        }

        public void UpdatePheromone()
        {
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