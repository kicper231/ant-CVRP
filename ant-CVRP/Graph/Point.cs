namespace GraphRepresentation
{
    public struct Point
    {
        public double X;
        public double Y;
        public double Demand;
        public int Id;

        public Point(double x, double y, double demand, int id)
        {
            X = x;
            Y = y;
            Demand = demand;
            Id = id;
        }

        public double DistanceTo(Point anotherPoint)
        {
            return Math.Sqrt( (anotherPoint.X - X)* (anotherPoint.X - X) + (anotherPoint.Y - Y)* (anotherPoint.Y - Y));

        }
    }
}