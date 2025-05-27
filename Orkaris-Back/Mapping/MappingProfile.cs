using System;
using Orkaris_Back.Models.DTO;
using Orkaris_Back.Models.EntityFramework;

namespace Orkaris_Back.Mapping;

public class MappingProfile : AutoMapper.Profile
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

        CreateMap<Session, SessionDTO>();
        CreateMap<Session, PostSessionDTO>();
        CreateMap<PostSessionDTO, Session>();
        CreateMap<PostSession2DTO, PostSessionDTO>();
        CreateMap<PostSession2DTO, Session>();
        CreateMap<Session, SessionWorkoutDTO>();

        CreateMap<SessionExercise, SessionExerciseDTO>();
        CreateMap<SessionExercise, SessionExerciseExerciseDTO>();
        CreateMap<PostSessionExerciseDTO, SessionExercise>();

        CreateMap<Exercise, ExerciseDTO>();
        CreateMap<PostExerciseDTO, Exercise>();

        CreateMap<ExerciseGoal, ExerciseGoalDTO>();
        CreateMap<ExerciseGoal, ExerciseGoalExerciseDTO>();
        CreateMap<PostExerciseGoalDTO, ExerciseGoal>();
        CreateMap<PostExerciseGoalDTO, SessionExercise>();

    }
}
