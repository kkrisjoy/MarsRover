using Autofac;
using MarsRover.Rover;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace MarsRover
{
    class Program
    {
        private static Dictionary<string, IRover> _rovers;
        static void Main(string[] args)
        {
            _rovers = new Dictionary<string, IRover>();
            var containerBuilder = createBuilder();

            using (var container = containerBuilder.Build())
            {
                RunDemoRover(container);
                RunDemoRover2(container);

                Console.WriteLine("Do you want to add more rovers: Y/N ?");
                var input = Console.ReadLine();
                if (!new string[] { "Y", "y" }.Contains(input))
                {
                    Environment.Exit(0);
                }
                else
                {
                    RunMoreRovers(container);
                }
            }
        }

        private static void RunDemoRover(IContainer container)
        {
            Console.WriteLine("");
            Console.WriteLine("Demo Rover 1");
            Console.WriteLine("Enter SetPosition in the form of: Id X Y Direction");
            Console.WriteLine("Set Command Sequence in the form of: <L/R><SpacesToMove>");
            Console.WriteLine("**********************************************************");
            Console.WriteLine("");

            var rover = container.Resolve<IRover>();
            Console.WriteLine("rover1 10 10 N");
            rover.SetPosition("rover1", 10, 10, 'N');
            _rovers.Add(rover.Id, rover);

            var commandSequence = "R1R3L2L1";
            Console.WriteLine(commandSequence);
            rover.Move(commandSequence);

            var currentPosition = rover.ReportCurrentPosition();
            Console.WriteLine(currentPosition);

            Console.WriteLine("**********************************************************");
        }

        private static void RunDemoRover2(IContainer container)
        {
            Console.WriteLine("Demo Rover 2");
            Console.WriteLine("");

            var rover = container.Resolve<IRover>();
            Console.WriteLine("rover2 20 20 N");
            rover.SetPosition("rover2", 20, 20, 'N');
            _rovers.Add(rover.Id, rover);

            var commandSequence = "L1L3L2L1R0";
            Console.WriteLine(commandSequence);
            rover.Move(commandSequence);

            var currentPosition = rover.ReportCurrentPosition();
            Console.WriteLine(currentPosition);

            Console.WriteLine("**********************************************************");
        }

        private static void RunMoreRovers(IContainer container)
        {
            while (true)
            {
                Console.WriteLine("**********************************************************");
                Console.WriteLine("Enter SetPosition in the form of: Id X Y Direction");
                var setPositionCommand = Console.ReadLine();
                var rover = container.Resolve<IRover>();
                var setPositionCommands = setPositionCommand.Split(' ');
                rover.SetPosition(setPositionCommands[0], int.Parse(setPositionCommands[1]), int.Parse(setPositionCommands[2]), char.Parse(setPositionCommands[3]));
                _rovers.Add(rover.Id, rover);
                Console.WriteLine("Set Command Sequence in the form of: <L/R><SpacesToMove>");
                var moveCommand = Console.ReadLine();
                rover.Move(moveCommand);
                
                PrintAllRoverPositions();

                Console.WriteLine("Do you want to add more rovers: Y/N ?");
                var input = Console.ReadLine();
                if (!new string[] { "Y", "y" }.Contains(input))
                    Environment.Exit(0);
            }
        }

        private static void PrintAllRoverPositions()
        {
            Console.WriteLine("**********************************************************");
            Console.WriteLine("***************ALL Rover Positions************************");
            foreach (var rover in _rovers)
                Console.WriteLine(rover.Value.ReportCurrentPosition());
            Console.WriteLine("**********************************************************");
        }

        private static ContainerBuilder createBuilder()
        {
            var assembly = Assembly.GetExecutingAssembly();

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces();
            return builder;
        }
    }
}
