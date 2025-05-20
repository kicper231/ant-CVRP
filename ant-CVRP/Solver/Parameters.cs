namespace AntSolver
{
    public class Parameters
    {
        /// <summary>
        /// Importance of pheronome
        /// </summary>
        public int Alfa { get; set; }

        /// Relative importance of distance(default=2)
        public int Beta { get; set; }

        /// <summary>
        /// Evaporation rate of pheromone(0..1, default=0.1)
        /// </summary>
        public double EvaporationRate { get; set; }

        /// <summary>
        ///   Q / Rating parametr
        /// </summary>
        public double Q { get; set; }

        /// <summary>
        /// Define how many ants will be used
        /// </summary>
        public int AntsCount { get; set; }

        /// <summary>
        /// Number of iterations to perform (default=2500)
        /// </summary>
        public int Iterations { get; set; }

        /// <summary>
        /// Ant capacity limit
        /// </summary>
        public int CapacityLimit { get; set; }

        /// <summary>
        /// Ant solution length Limits
        /// </summary>
        public int LengthLimit { get; set; }

        /// <summary>
        /// Initial pheromone level along each Edge
        /// </summary>
        public double StartPheromone { get; set; }

        /// <summary>
        /// Elite ants number
        /// </summary>
        public int EliteCount { get; set; }

        public double MinPheronome { get; set; }
        
        public double EliteBoost { get; set; }

        public Parameters(int alfa, int beta, double evaporationRate, double T0, double Q0, int antsCount, int iterations, int capacityLimit, double startPheromone)
        {
            this.Alfa = alfa;
            this.Beta = beta;
            this.EvaporationRate = EvaporationRate;
            this.AntsCount = antsCount;
            this.Iterations = Iterations;
            this.CapacityLimit = capacityLimit;
            this.StartPheromone = startPheromone;
        }

        /// <summary>
        /// Default parameters
        /// </summary>
        public Parameters()
        {
            Beta = 4;
            Alfa = 1;
            EvaporationRate = 0.4;
            Q = 250;
            AntsCount = 60;
            CapacityLimit = 100;
            Iterations = 5000;
            StartPheromone = 1;
            EliteCount = 5;
            MinPheronome = 1;
            EliteBoost = 5;
        }

        public void Show()
        {
            Console.WriteLine("Alfa:" + Alfa);
            Console.WriteLine("Beta: " + Beta);
            Console.WriteLine("Global Evaporation Rate: " + EvaporationRate);
            Console.WriteLine("AntCount: " + AntsCount);
            Console.WriteLine("Iterations: " + Iterations);
            Console.WriteLine();
        }
    }
}