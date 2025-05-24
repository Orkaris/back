using System;
using Orkaris_Back.Models.EntityFramework;

namespace Orkaris_Back.Models.DTO;

public class ExerciseCategoryDTO
{
    public Guid ExerciseId { get; set; }
    public Guid CategoryId { get; set; }
    public virtual Exercise? ExerciseExerciseCategory { get; set; }
    public virtual Category? CategoryExerciseCategory { get; set; }
}
public class PostExerciseCategoryDTO
{
    public Guid ExerciseId { get; set; }
    public Guid CategoryId { get; set; }
}
