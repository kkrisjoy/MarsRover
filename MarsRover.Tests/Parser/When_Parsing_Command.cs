using FluentAssertions;
using MarsRover.Parser;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.Tests.Parser
{
    [TestFixture]
    public class When_Parsing_Command
    {
        ICommandParser _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new CommandParser();
        }

        [Test]
        public void Valid_Command_Returns_Parsed_CommandList()
        {
            var input = "R1R2R3L1L2";
            var output = _sut.Parse(input);

            output.Should().BeEquivalentTo(new List<CommandDetail>() { new CommandDetail('R', 1),
            new CommandDetail('R', 2),
            new CommandDetail('R', 3),
            new CommandDetail('L', 1),
            new CommandDetail('L', 2)});
        }

        [Test]
        public void Command_With_NoMovementSpaces_Returns_CommandList_DefaultedTo0()
        {
            var input = "R1R2L";
            var output = _sut.Parse(input);

            output.Should().BeEquivalentTo(new List<CommandDetail>(){
                new CommandDetail('R', 1),
                new CommandDetail('R', 2),
                new CommandDetail('L', 0)});
        }

        [Test]
        public void Command_With_InvalidMovementSpaces_Returns_CommandList_DefaultedTo0()
        {
            var input = "RKR2L";
            var output = _sut.Parse(input);

            output.Should().BeEquivalentTo(new List<CommandDetail>(){
                new CommandDetail('R', 0),
                new CommandDetail('R', 2),
                new CommandDetail('L', 0)});
        }
    }
}
