using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orkaris_Back.Models.EntityFramework
{
    [Table("t_j_exercise_category_exc")]
    public class ExerciseCategory
    {
        [Required, Column("exr_id")]
        public Guid ExerciseId { get; set; }
        [Required, Column("cat_id")]
        public Guid CategoryId { get; set; }

        // Navigation properties
        [ForeignKey("ExerciseId"), InverseProperty(nameof(Exercise.ExerciseCategoryExercise))]
        public virtual Exercise? ExerciseExerciseCategory { get; set; }
        [ForeignKey("CategoryId"), InverseProperty(nameof(Category.ExerciseCategoryCategory))]
        public virtual Category? CategoryExerciseCategory { get; set; }
    }
}