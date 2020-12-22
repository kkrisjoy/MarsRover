using MarsRover.Common;
using MarsRover.Rover;

namespace MarsRover.Mover
{
    public class RightMover : IMover
    {
        public void Move(IRover rover, int places)
        {
            switch (rover.Direction)
            {
                case Direction.North:
                    rover.Direction = Direction.East;
                    rover.MoveEast(places);
                    break;
                case Direction.East:
                    rover.Direction = Direction.South;
                    rover.MoveSouth(places);
                    break;
                case Direction.South:
                    rover.Direction = Direction.West;
                    rover.MoveWest(places);
                    break;
                case Direction.West:
                    rover.Direction = Direction.North;
                    rover.MoveNorth(places);
                    break;
            }
        }          
            
    }
}