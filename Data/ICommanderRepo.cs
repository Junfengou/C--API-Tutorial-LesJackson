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

        bool SaveChanges(); //

        IEnumerable<Command> GetAllCommands(); // get a list of commands

        Command GetCommandById(int id); // get a single command

        void CreateCommand(Command cmd); // create a command 

        void UpdateCommand(Command cmd); // I guess since the [Id] is a primary key, we don't need to pass it in? 

        void DeleteCommand(Command cmd); // delete a command
    }
}