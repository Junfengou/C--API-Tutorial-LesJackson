using System.Collections.Generic;
using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
using Microsoft.AspNetCore.JsonPatch;
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

        // ------------------------------------------------------------>

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
        {
            var commandItems = _repository.GetAllCommands();

            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
        }

        // ------------------------------------------------------------>

        [HttpGet("{id}", Name = "GetCommandById")]
        public ActionResult<CommandReadDto> GetCommandById(int id)
        {
            var commandItem = _repository.GetCommandById(id);

            if (commandItem != null)
            {
                return Ok(_mapper.Map<CommandReadDto>(commandItem));
            }

            return NotFound();
        }

        // ------------------------------------------------------------>

        // Once the data is created, it will return the exact data that's created in postman
        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto)
        {
            // Map to <Command> from (commandCreateDto) : Basically saying whatever data come in, we want to map/mirror it to the Command class to make sure all data coming in meets requirements
            var commandModel = _mapper.Map<Command>(commandCreateDto);
            _repository.CreateCommand(commandModel);
            _repository.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDto>(commandModel); // By mapping it this way, we can show only the fields that specified in CommandReadDto

            // The purpose of this line os return is to get the URI right after the data is created.
            /*
                Reference: 
                    GetCommandById refer to the get individual data method
                    commandReadDto.Id is the id of this newly created data
                    commandReadDto refer to how the data is formatted upon return
            */
            return CreatedAtRoute(nameof(GetCommandById), new { Id = commandReadDto.Id }, commandReadDto);

            // return Ok(commandReadDto);
        }

        // ------------------------------------------------------------>
        // api/commands/{id}

        // Just a side note to self. When executing a put request. Even if you just want to change one field, you have to have the entire object in before performing any kind of change
        [HttpPut("{id}")]
        public ActionResult<CommandReadDto> UpdateCommand(int id, CommandCreateDto commandUpdateDto)
        {
            var commandModelFromRepo = _repository.GetCommandById(id); // check if the item we're looking for exist 
            if (commandModelFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(commandUpdateDto, commandModelFromRepo);

            // Even tho UpdateCommand has no implementation of any sort, it is still good convention to call the method that's defined in the interface
            _repository.UpdateCommand(commandModelFromRepo);

            _repository.SaveChanges();

            return NoContent(); // 204 request return

        }

        // ------------------------------------------------------------>
        // PATCH api/commands/{id}
        /*Patch request will require several Nuget packages
            Microsoft.AspNetCore.JsonPatch
            dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson
        */

        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
            var commandModelFromRepo = _repository.GetCommandById(id);
            if (commandModelFromRepo == null)
            {
                return NotFound();
            }

            var commandToPatch = _mapper.Map<CommandUpdateDto>(commandModelFromRepo);
            patchDoc.ApplyTo(commandToPatch, ModelState); // ModelState ensures all the validations are valid

            if (!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(commandToPatch, commandModelFromRepo);

            _repository.UpdateCommand(commandModelFromRepo);

            _repository.SaveChanges();

            return NoContent(); // 204 request return
        }

        // ------------------------------------------------------------>
        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var commandModelFromRepo = _repository.GetCommandById(id);
            if (commandModelFromRepo == null)
            {
                return NotFound();
            }

            _repository.DeleteCommand(commandModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

    }
}