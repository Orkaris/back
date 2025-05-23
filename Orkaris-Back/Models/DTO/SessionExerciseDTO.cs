using System;
using Orkaris_Back.Models.EntityFramework;

namespace Orkaris_Back.Models.DTO;

public class SessionExerciseDTO
{
    public Guid SessionId { get; set; }
    public Guid ExerciseId { get; set; }
    public Session? SessionSessionExercise { get; set; }
    public ExerciseGoal? ExerciseGoalSessionExercise { get; set; }

}

public class PostSessionExerciseDTO
{
    public Guid SessionId { get; set; }
    public Guid ExerciseId { get; set; }
}

