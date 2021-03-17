using System;
using System.Collections.Generic;
using System.Linq;
using Commander.Models;

namespace Commander.Data
{
    public class SqlCommanderRepo : ICommanderRepo
    {
        private readonly CommanderContext _context;

        public SqlCommanderRepo(CommanderContext context)
        {
            _context = context; // Dependency injection to utilize Context
        }

        // ------------------------------------------------>
        public void CreateCommand(Command cmd)
        {
            if (cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }

            _context.Commands.Add(cmd);
        }

        // ------------------------------------------------>

        public void DeleteCommand(Command cmd)
        {
            if (cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }

            _context.Commands.Remove(cmd);
        }

        // ------------------------------------------------>
        public IEnumerable<Command> GetAllCommands()
        {
            return _context.Commands.ToList();
        }

        // ------------------------------------------------>
        public Command GetCommandById(int id)
        {
            return _context.Commands.FirstOrDefault(item => item.Id == id); // Run a [dotnet build] just to make sure there is no errors
        }

        // ------------------------------------------------>
        public bool SaveChanges()
        {
            // When a change occur, the database won't change unless this method is called
            return (_context.SaveChanges() >= 0);
        }

        // ------------------------------------------------>
        public void UpdateCommand(Command cmd)
        {
            // Nothing need to be done here since it's already taken care of by DbContext?
        }
    }
}