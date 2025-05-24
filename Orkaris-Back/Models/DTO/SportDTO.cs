using System;

namespace Orkaris_Back.Models.DTO;

public class SportDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
public class PostSportDTO
{
    public string Name { get; set; } = string.Empty;
}