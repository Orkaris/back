using System;
using Orkaris_Back.Models.DTO;
using Orkaris_Back.Models.EntityFramework;

namespace Orkaris_Back.Mapping;

public class MappingProfile: AutoMapper.Profile
{
    public MappingProfile()
    {
        CreateMap<User, MinimalUserDTO>();
        CreateMap<User, InfoUserDTO>();
        CreateMap<User, LoginRequestDTO>();
        CreateMap<User, RegisterUserDTO>();
        CreateMap<User, PutUserDTO>();
        CreateMap<PutUserDTO, User>();


        CreateMap<Workout, WorkoutDTO>();
        CreateMap<Workout, PostWorkoutDTO>();
        CreateMap<PostWorkoutDTO, Workout>();
            
    }
}
