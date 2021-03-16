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
            // Create a auto map from [Command] to [CommandReadDto]
            CreateMap<Command, CommandReadDto>();
        }
    }
}