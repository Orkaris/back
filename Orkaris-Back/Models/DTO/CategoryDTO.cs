using System;

namespace Orkaris_Back.Models.DTO;

public class CategoryDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid SportId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
public class PostCategoryDTO
{
    public string Name { get; set; } = string.Empty;
    public Guid SportId { get; set; }
}
