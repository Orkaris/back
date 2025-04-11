using System;

namespace Orkaris_Back.Models.DTO;

public class WorkoutDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Guid UserId { get; set; }
}
public class PostWorkoutDTO
{
    public string Name { get; set; } = string.Empty;
}
