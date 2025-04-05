using Ants;
using GraphRepresentation;
using System.Diagnostics;

namespace Solver
{
    public class Solver
    {
        public Parameters Parameters { get; set; }
        private Ant GlobalBestAnt { get; set; }

        private List<Ant> Ants { get; set; }
        private List<double> Results { get; set; }
        private Graph Graph { get; set; }
        private Stopwatch Stopwatch { get; set; }

        private List<int> Solutions { get; set; }

        public Solver()
        {
        }

        public void Solve()
        {
            // Create Ants
            CreateAnts(Parameters.AntsCount);
        }

        public void OneAntSolution(Ant ant)
        {
        }

        public void CreateAnts(int antsCount)
        {
            for (int i = 0; i < antsCount; i++)
            {
                Ant ant = new Ant();
                Ants.Add(ant);
            }
        }

        public void RateSolution()
        {
        }

        public void UpdateFeronome()
        {
        }
    }
}