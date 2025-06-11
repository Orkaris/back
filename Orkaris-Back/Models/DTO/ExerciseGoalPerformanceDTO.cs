using System;

namespace Orkaris_Back.Models.DTO;

public class ExerciseGoalPerformanceDTO
{
    public Guid Id { get; set; }
    public int Reps { get; set; }
    public int Sets { get; set; }
    public float Weight { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Guid ExerciseGoalId { get; set; }
}
public class PostExerciseGoalPerformanceDTO
{
    public int Reps { get; set; }
    public int Sets { get; set; }
    public float Weight { get; set; }

    public Guid ExerciseGoalId { get; set; }
}
