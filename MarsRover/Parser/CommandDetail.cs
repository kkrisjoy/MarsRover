using MarsRover.Common;

namespace MarsRover.Parser
{
    public struct CommandDetail
    {
        public Movement Movement { get; private set; }
        public int Places { get; private set; }
        public CommandDetail(char movement, int places)
        {
            Movement = (Movement)movement;
            Places = places;
        }
    }
}
