using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRover.Parser
{
    public class CommandParser : ICommandParser
    {
        public List<CommandDetail> Parse(string commands)
        {
            var result = new List<CommandDetail>();

            try
            {
                var commandListstring = GetCommandListString(commands);
                var commandList = commandListstring.Split('|');

                commandList.ToList().ForEach(command =>
                {
                    var movementChar = command.Take(1).First();
                    var places = int.TryParse(command.Substring(1), out int val) ? val : 0; //Default invalid characters to 0 
                    result.Add(new CommandDetail(movementChar, places));

                });
            }
            catch (Exception)
            {
                //Log Exception
                throw new Exception("Error in parsing command.");
            }

            return result;
        }

        private static string GetCommandListString(string commands)
        {
            var commandListstring = new StringBuilder(commands);
            commandListstring = commandListstring.Replace("L", "|L");
            commandListstring = commandListstring.Replace("R", "|R");
            return commandListstring.ToString().TrimStart(new char[] { '|' });
        }
    }
}
