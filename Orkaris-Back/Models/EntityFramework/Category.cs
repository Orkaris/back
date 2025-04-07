using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orkaris_Back.Models.EntityFramework
{
    [Table("t_e_category_cat")]
    public class Category
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("cat_id")]
        public Guid Id { get; set; }

        [Required, MaxLength(100), Column("cat_name")]
        public string Name { get; set; } = string.Empty;

        [ForeignKey("Sport"), Required, Column("spo_id")]
        public Guid SportId { get; set; }
        [Column("cat_created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        [ForeignKey("SportId"), InverseProperty(nameof(Sport.CategorySport))]
        public virtual Sport? SportCategory { get; set; }
        [InverseProperty(nameof(ExerciseCategory.CategoryExerciseCategory))]
        public virtual ICollection<ExerciseCategory> ExerciseCategoryCategory { get; set; } = new List<ExerciseCategory>();

    }
}