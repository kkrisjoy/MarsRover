using System.Collections.Generic;

namespace MarsRover.Parser
{
    public interface ICommandParser
    {
        List<CommandDetail> Parse(string commands);
    }
}
