using System;

namespace Orkaris_Back.Models.DTO;

public class ExerciseGoalDTO
{
    public Guid Id { get; set; }
    public int Reps { get; set; }
    public int Sets { get; set; }
    public float Weight { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Guid ExerciseId { get; set; }

}
public class PostExerciseGoalDTO
{
    public int Reps { get; set; }
    public int Sets { get; set; }
    public float Weight { get; set; }
    public Guid ExerciseId { get; set; }
}

public class PutExerciseGoalDTO
{
    public Guid Id { get; set; }
    public Guid ExerciseId { get; set; }
    public int Reps { get; set; }
    public int Sets { get; set; }
    public float Weight { get; set; }

}

public class ExerciseGoalExerciseDTO : ExerciseGoalDTO
{
    public ExerciseDTO? ExerciseExerciseGoal { get; set; }

}