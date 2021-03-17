using AutoMapper;
using Commander.Dtos;
using Commander.Models;

namespace Commander.Profiles
{
    // All the object mapping happens in here!

    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            // Create a auto map from [Source] to [Target]
            CreateMap<Command, CommandReadDto>();

            CreateMap<CommandCreateDto, Command>(); // This is for POST a new command

            CreateMap<CommandUpdateDto, Command>(); // This is for UPDATE command

            CreateMap<Command, CommandUpdateDto>(); // Patch command
        }
    }
}