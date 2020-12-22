using FluentAssertions;
using MarsRover.Common;
using MarsRover.Mover;
using MarsRover.Parser;
using MarsRover.Rover;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.Tests.Rover
{
    [TestFixture]
    public class RoverTests
    {
        IRover _sut;
        Mock<ICommandParser> _mockCommandParser;
        Mock<IMoverFactory> _moverFactory;

        private List<CommandDetail> _commandList;

        [SetUp]
        public void SetUp()
        {
            _mockCommandParser = new Mock<ICommandParser>();
            _moverFactory = new Mock<IMoverFactory>();

            _commandList = GetCommandList();
            _mockCommandParser.Setup(x => x.Parse(It.IsAny<string>())).Returns(_commandList);

            _moverFactory.Setup(x => x.GetMover(Movement.Right)).Returns(new RightMover());
            _moverFactory.Setup(x => x.GetMover(Movement.Left)).Returns(new LeftMover());
        }

        private List<CommandDetail> GetCommandList()
        {
            return new List<CommandDetail>()
            {
                new CommandDetail('R', 2),
                new CommandDetail('R', 3),
                new CommandDetail('L', 4)
            };
        }

        [Test]
        public void Given_New_Rover_When_Initialised_Then_Ensure_Its_Not_Placed()
        {
            _sut = new MarsRover.Rover.Rover(_mockCommandParser.Object, _moverFactory.Object);
       
            _sut.IsPlaced.Should().Be(false);
        }

        [Test]
        public void Given_New_Rover_When_Set_Then_Ensure_Its_Placed()
        {
            _sut = new MarsRover.Rover.Rover(_mockCommandParser.Object, _moverFactory.Object);
            _sut.SetPosition("R1", 10, 10, 'N');

            _sut.IsPlaced.Should().Be(true);
        }

        [TestCase(10, 10, 'N', Direction.East, 16, 7)]
        [TestCase(10, 10, 'W', Direction.North, 13, 16)]
        [TestCase(10, 10, 'S', Direction.West, 4, 13)]
        [TestCase(10, 10, 'E', Direction.South, 7, 4)]
        public void Given_RoverIsPlaced_When_Command_Passed_Sets_Rover_Expected_Direction_Expected_Position(int beginX, int beginY, char beginDirection, 
            Direction expectedEndDirection, int expectedEndX, int expectedEndY)
        {
            SetupRover("R1", beginX, beginY, beginDirection);
            _sut.Move("R2R3L4");

            _sut.Direction.Should().Be(expectedEndDirection);
            _sut.Position.Should().Be(new Point(expectedEndX, expectedEndY));
        }

        private void SetupRover(string id, int x, int y, char direction)
        {
            _sut = new MarsRover.Rover.Rover(_mockCommandParser.Object, _moverFactory.Object);
            _sut.SetPosition(id, x, y, direction);
        }

    }
}
