using System.Collections.Generic;
using Commander.Data;
using Commander.Models;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers
{
    /*
        If you're building an API with frontend, then inherit from [Controller]

        If you're building an API without View, then inherit from [ControllerBase]
    */

    [Route("api/commands")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommanderRepo _repository;

        public CommandsController(ICommanderRepo repository) // This is called dependency injection. Must first define in startup class
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Command>> GetAllCommands()
        {
            var commandItems = _repository.GetAllCommands();
            if (commandItems == null)
            {
                return NotFound();
            }
            return Ok(commandItems);
        }

        [HttpGet("{id}")]
        public ActionResult<Command> GetCommand(int id)
        {
            var commandItem = _repository.GetCommandById(id);

            return Ok(commandItem);
        }

    }
}