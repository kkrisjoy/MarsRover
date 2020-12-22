using MarsRover.Common;

namespace MarsRover.Rover
{
    public interface IRover
    {
        string Id { get; set; }
        Point Position { get; set; }
        Direction Direction { get; set; }
        void SetPosition(string id, int x, int y, char direction);
        void Move(string commands);
        bool IsPlaced { get; }
        string ReportCurrentPosition();
    }
}
