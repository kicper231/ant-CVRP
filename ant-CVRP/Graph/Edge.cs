namespace GraphRepresentation
{
    public class Edge
    {
        public Point Start { get; set; }
        public Point End { get; set; }
        public int Length { get; set; }
        public double Pheromone { get; set; }
        public double Weight { get; set; }

        public Edge()
        { }

        public Edge(Point start, Point end)
        {
            Start = start;
            End = end;
            Length = Start.DistanceTo(End);
            // Maybe to remove
            Weight = Length;
            Pheromone = 0.5;
        }
        public Edge(Point start, Point end, double pheromone)
        {
            Start = start;
            End = end;
            Length = Start.DistanceTo(End);
            // Maybe to remove
            Weight = Length;
            Pheromone = pheromone;
        }
    }
}