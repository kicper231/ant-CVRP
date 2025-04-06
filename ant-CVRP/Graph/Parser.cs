using System.Globalization;

namespace GraphRepresentation
{
    public static class CVRPDataParser
    {
        public static Graph Parse(string filePath)
        {
            var points = new List<Point>();
            int size = 0;
            var coordinates = new Dictionary<int, (double X, double Y)>();
            var demands = new Dictionary<int, int>();
            var capacityLimit = 0;

            bool readingCoords = false;
            bool readingDemands = false;

            foreach (var line in File.ReadLines(filePath))
            {
                string trimmed = line.Trim();

                if (trimmed.StartsWith("DIMENSION"))
                {
                    trimmed = trimmed.Split(" : ")[1];
                    int.TryParse(trimmed, NumberStyles.Any, CultureInfo.InvariantCulture, out size);
                }

                if (trimmed.StartsWith("CAPACITY"))
                {
                    trimmed = trimmed.Split(" : ")[1];
                    int.TryParse(trimmed, NumberStyles.Any, CultureInfo.InvariantCulture, out capacityLimit);
                }

                if (trimmed.StartsWith("NODE_COORD_SECTION"))
                {
                    readingCoords = true;
                    continue;
                }

                if (trimmed.StartsWith("DEMAND_SECTION"))
                {
                    readingCoords = true;
                    readingDemands = true;
                    continue;
                }

                //if (trimmed.StartsWith("DEPOT_SECTION") || trimmed.StartsWith("EOF"))
                //{
                //    readingDemands = false;
                //    break;
                //}

                if (readingCoords)
                {
                    var parts = trimmed.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length >= 3 &&
                        int.TryParse(parts[0], out int id) &&
                        double.TryParse(parts[1], NumberStyles.Any, CultureInfo.InvariantCulture, out double x) &&
                        double.TryParse(parts[2], NumberStyles.Any, CultureInfo.InvariantCulture, out double y))
                    {
                        coordinates[id] = (x, y);
                    }
                }

                if (readingDemands)
                {
                    var parts = trimmed.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length >= 2 &&
                        int.TryParse(parts[0], out int id) &&
                        int.TryParse(parts[1], NumberStyles.Any, CultureInfo.InvariantCulture, out int demand))
                    {
                        demands[id] = demand;
                    }
                }
            }

            // Łączenie danych
            foreach (var cord in coordinates)
            {
                int id = cord.Key;
                (double x, double y) = cord.Value;
                int demand = demands.ContainsKey(id) ? demands[id] : 0;

                points.Add(new Point(x, y, demand, id));
            }

            // Tworzenie grafu
            Graph graph = new Graph(points, capacityLimit);
            return graph;
        }
    }
}