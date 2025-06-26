using System;
using System.Collections.Generic;

namespace Orkaris_Back.Models.DTO;

public class SessionPerformanceDTO
{
    public Guid Id { get; set; }
    public Guid SessionId { get; set; }
    public string? Feeling { get; set; }
    public DateTime Date { get; set; }
}

public class PostSessionPerformanceDTO
{
    public Guid SessionId { get; set; }
    public string? Feeling { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
}

public class SessionPerformanceDetailDTO
{
    public Guid Id { get; set; }
    public Guid SessionId { get; set; }
    public string? SessionName { get; set; }
    public string? Feeling { get; set; }
    public DateTime Date { get; set; }
    public List<ExerciseGoalPerformanceDetailDTO> ExerciseGoalPerformances { get; set; } = new List<ExerciseGoalPerformanceDetailDTO>();
}

public class ExerciseGoalPerformanceDetailDTO
{
    public Guid Id { get; set; }
    public int Reps { get; set; }
    public int Sets { get; set; }
    public float Weight { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid ExerciseGoalId { get; set; }
    public string? ExerciseName { get; set; }
    public string? ExerciseDescription { get; set; }
}