using MarsRover.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.Mover
{
    public class MoverFactory : IMoverFactory
    {
        public IMover GetMover(Movement turn)
        {
            switch(turn)
            {
                case Movement.Left:
                    return new LeftMover();                   
                case Movement.Right:
                    return new RightMover();                    
            }

            //Log invalid movement specified in the command.
            throw new Exception("Incorrect Movement specified.");
        }
    }
}
