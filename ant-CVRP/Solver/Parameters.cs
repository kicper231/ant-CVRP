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
        /// Probability of choosing best ant path instead of random roulette
        /// </summary>
        public double Q0 { get; set; }

        /// <summary>
        /// Initial pheromone level along each Edge
        /// </summary>
        public double T0 { get; set; }

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

        public double StartPheromone { get; set; }

        public Parameters(int alfa, int beta, double evaporationRate, double T0, double Q0, int antsCount, int iterations, int capacityLimit, double startPheromone)
        {
            this.Alfa = alfa;
            this.Beta = beta;
            this.EvaporationRate = EvaporationRate;
            this.T0 = T0;
            this.Q0 = Q0;
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
            Beta = 2;
            Alfa = 1;
            EvaporationRate = 0.1;
            Q0 = 0.9;
            AntsCount = 20;
            Iterations = 10000;
            T0 = 0.01;
        }

        public void Show()
        {
            Console.WriteLine("Alfa:" + Alfa);
            Console.WriteLine("Beta: " + Beta);
            Console.WriteLine("Global Evaporation Rate: " + EvaporationRate);
            Console.WriteLine("Q0: " + Q0);
            Console.WriteLine("AntCount: " + AntsCount);
            Console.WriteLine("Iterations: " + Iterations);
            Console.WriteLine("T0: " + T0);
            Console.WriteLine();
        }
    }
}