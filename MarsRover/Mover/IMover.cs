using MarsRover.Rover;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.Mover
{
    public interface IMover
    {
        void Move(IRover rover, int places);
    }
}
