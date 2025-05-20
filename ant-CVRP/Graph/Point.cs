namespace GraphRepresentation
{
    public struct Point
    {
        public double X;
        public double Y;
        public int Demand;
        public int Id;

        public Point(double x, double y, int demand, int id)
        {
            X = x;
            Y = y;
            Demand = demand;
            Id = id;
        }

        public int DistanceTo(Point anotherPoint)
        {
            double xd = anotherPoint.X - X;
            double yd = anotherPoint.Y - Y;
            double distance = Math.Sqrt(xd * xd + yd * yd);
            return (int)Math.Round(distance, MidpointRounding.AwayFromZero); // nint
        }
    }
}