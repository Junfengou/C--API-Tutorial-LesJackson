using System.Collections.Generic;
using Commander.Models;

namespace Commander.Data
{
    /*
        To define a interface method, follow this convention

            [returnType] [methodName](int whatever);
    */

    public interface ICommanderRepo
    {
        IEnumerable<Command> GetAllCommands(); // get a list of commands

        Command GetCommandById(int id); // get a single command
    }
}