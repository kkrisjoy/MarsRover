namespace MarsRover.Rover
{
    public static class RoverExtensions
    {
        public static void MoveWest(this IRover rover, int places)
        {
            rover.Position = new Point(rover.Position.x - places, rover.Position.y);
        }

        public static void MoveSouth(this IRover rover, int places)
        {
            rover.Position = new Point(rover.Position.x, rover.Position.y - places);
        }

        public static void MoveEast(this IRover rover, int places)
        {
            rover.Position = new Point(rover.Position.x + places, rover.Position.y);
        }

        public static void MoveNorth(this IRover rover, int places)
        {
            rover.Position = new Point(rover.Position.x, rover.Position.y + places);
        }
    }
}
