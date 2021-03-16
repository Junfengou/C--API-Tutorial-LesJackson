using System.Collections.Generic;
using AutoMapper;
using Commander.Data;
using Commander.Dtos;
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

        public readonly IMapper _mapper;

        public CommandsController(ICommanderRepo repository, IMapper mapper) // This is called dependency injection. Must first define in startup class
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
        {
            var commandItems = _repository.GetAllCommands();

            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
        }

        [HttpGet("{id}")]
        public ActionResult<CommandReadDto> GetCommand(int id)
        {
            var commandItem = _repository.GetCommandById(id);

            if (commandItem != null)
            {
                return Ok(_mapper.Map<CommandReadDto>(commandItem));
            }

            return NotFound();
        }

    }
}