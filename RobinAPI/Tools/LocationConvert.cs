namespace RobinAPI.Tools
{
    public class LocationConvert
    {
        // Assumes that the location coordinants are in a (X, Y) format
        public string GetX(string location)
        {
            var xCoordinant = location.Replace("(", "").Replace(")", "").Replace(" ", "").Split(',')[0];

            return xCoordinant;
        }

        public string GetY(string location)
        {
            var yCoordinant = location.Replace("(", "").Replace(")", "").Replace(" ", "").Split(',')[1];

            return yCoordinant;
        }
    }
}