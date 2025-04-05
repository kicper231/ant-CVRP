using System;
using GraphRepresentation;

namespace Program
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Graph graph = CVRPDataParser.Parse("DataSets/E-n22-k4.txt");

            //graph.PrintDemands();
            //Console.WriteLine();
            //graph.PrintDistanceMatrix();
            //Console.WriteLine();
            //graph.PrintPheromoneMatrix();
        }
    }
}