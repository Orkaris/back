using System;

namespace Orkaris_Back.Models.DTO;

public class SessionPerformanceDTO
{
    public Guid Id { get; set; }
    public Guid SessionId { get; set; }
    public string? feeling { get; set; }
    public DateTime Date { get; set; }
}
public class PostSessionPerformanceDTO
{
    public Guid SessionId { get; set; }
    public string? feeling { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
}