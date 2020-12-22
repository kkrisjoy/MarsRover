using MarsRover.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.Mover
{
    public interface IMoverFactory
    {
        IMover GetMover(Movement turn);
    }
}
