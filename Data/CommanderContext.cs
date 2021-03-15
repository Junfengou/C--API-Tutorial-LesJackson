using Commander.Models;
using Microsoft.EntityFrameworkCore;

/*
    Intuitively, a [DbContext] corresponds to your database 
    (or a collection of tables and views in your database) 
    whereas a [DbSet] corresponds to a table or view in your database. 
    So it makes perfect sense that you will get a combination of both!

    You will be using a [DbContext] object to get access to your tables and views
    (which will be represented by DbSet's) and you will be using your [DbSet]'s to get access, 
    create, update, delete and modify your table data.

*/


namespace Commander.Data
{
    // In order to create a migration, we must have a Context file like this defined
    public class CommanderContext : DbContext
    {
        public DbSet<Command> Commands { get; set; } // Call this to get back a list of commands

        public CommanderContext(DbContextOptions<CommanderContext> opt) : base(opt)
        {

        }


    }
}