using System;
using Orkaris_Back.Models.EntityFramework;

namespace Orkaris_Back.Models.DTO;

public class SessionDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public Guid WorkoutId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<SessionExerciseExerciseDTO> SessionExerciseSession { get; set; } = new List<SessionExerciseExerciseDTO>();
}

public class PostSessionDTO
{
    public string Name { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public Guid WorkoutId { get; set; }

}
public class PostSession2DTO
{
    public string Name { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public Guid WorkoutId { get; set; }
    public ICollection<PostExerciseGoalDTO> SessionExerciseSession { get; set; } = new List<PostExerciseGoalDTO>();
}

public class PutSessionDTO
{
    public string Name { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public ICollection<PutExerciseGoalDTO> SessionExerciseSession { get; set; } = new List<PutExerciseGoalDTO>();
}


public class SessionWorkoutDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public Guid WorkoutId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
