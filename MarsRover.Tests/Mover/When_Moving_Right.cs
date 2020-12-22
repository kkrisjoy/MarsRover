using FluentAssertions;
using MarsRover.Common;
using MarsRover.Mover;
using MarsRover.Parser;
using MarsRover.Rover;
using Moq;
using NUnit.Framework;

namespace MarsRover.Tests.Mover
{
    public class When_Moving_Right
    {
        IRover _rover;
        IMover _sut;
        Mock<ICommandParser> _mockCommandParser;
        Mock<IMoverFactory> _moverFactory;

        [SetUp]
        public void SetUp()
        {
            _sut = new RightMover();
            _mockCommandParser = new Mock<ICommandParser>();
            _moverFactory = new Mock<IMoverFactory>();
        }

        [Test]
        [TestCase(10, 10, 'N', 3, Direction.East, 13, 10)]
        [TestCase(20, 10, 'N', 5, Direction.East, 25, 10)]
        [TestCase(5, 10, 'N', 2, Direction.East, 7, 10)]
        [TestCase(0, 2, 'N', 2, Direction.East, 2, 2)]
        [TestCase(10, 10, 'W', 3, Direction.North, 10, 13)]
        [TestCase(20, 10, 'W', 5, Direction.North, 20, 15)]
        [TestCase(5, 10, 'W', 2, Direction.North, 5, 12)]
        [TestCase(0, 0, 'W', 2, Direction.North, 0, 2)]
        [TestCase(10, 10, 'S', 3, Direction.West, 7, 10)]
        [TestCase(20, 10, 'S', 5, Direction.West, 15, 10)]
        [TestCase(5, 10, 'S', 2, Direction.West, 3, 10)]
        [TestCase(2, 0, 'S', 2, Direction.West, 0, 0)]
        [TestCase(10, 10, 'E', 3, Direction.South, 10, 7)]
        [TestCase(20, 10, 'E', 5, Direction.South, 20, 5)]
        [TestCase(5, 10, 'E', 2, Direction.South, 5, 8)]
        [TestCase(0, 2, 'E', 2, Direction.South, 0, 0)]
        public void From_Given_Direction_Moves_Specified_Places_To_Expected_Direction(int beginX, int beginY, char beginDirection,
            int movePlaces, Direction expectedEndDirection, int expectedEndX, int expectedEndY)
        {
            SetupRover("R1", beginX, beginY, beginDirection);

            _sut.Move(_rover, movePlaces);
            _rover.Direction.Should().Be(expectedEndDirection);
            _rover.Position.Should().Be(new Point(expectedEndX, expectedEndY));
        }

        private void SetupRover(string id, int x, int y, char direction)
        {
            _rover = new MarsRover.Rover.Rover(_mockCommandParser.Object, _moverFactory.Object);
            _rover.SetPosition(id, x, y, direction);
        }

    }
}
