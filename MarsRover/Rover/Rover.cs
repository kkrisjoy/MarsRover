using MarsRover.Common;
using MarsRover.Mover;
using MarsRover.Parser;
using System;

namespace MarsRover.Rover
{
    public class Rover : IRover
    {
        private ICommandParser _commandParser;
        private IMoverFactory _moverFactory;
        public Rover(ICommandParser commandParser, IMoverFactory moverFactory)
        {
            _commandParser = commandParser;
            _moverFactory = moverFactory;
        }

        public Point Position { get; set; }
        public Direction Direction { get; set; }

        public bool IsPlaced { get; private set; }
        public string Id { get; set; }

        public void Move(string commands)
        {
            if (!IsPlaced)
                throw new Exception("Rover is not placed yet. Please use SetPosition to place it first to move.");

            var commandList = _commandParser.Parse(commands);
            try
            {
                foreach(var command in commandList)
                {
                    var mover = _moverFactory.GetMover(command.Movement);
                    mover.Move(this, command.Places);
                }
            }
            catch
            {
                //Log Exception
                throw new Exception("Error in moving rover.");
            }
            
        }

        public void SetPosition(string id, int x, int y, char direction)
        {
            try
            {
                Id = id;
                Position = new Point(x, y);
                Direction = (Direction)direction;
                IsPlaced = true;
            }
            catch
            {
                //Log Exception
                throw new Exception("Error in placing the rover - please check the input is in the form of: Id X Y DirectionCharacter.");
            }
            return;
        }

        public string ReportCurrentPosition()
        {
            return $"{Id} {Position.x} {Position.y} {Direction}";
        }

    }
}
