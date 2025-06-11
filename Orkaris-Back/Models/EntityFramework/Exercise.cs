using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Orkaris_Back.Models.EntityFramework
{
    [Table("t_e_Exercise_exr")]
    public class Exercise
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("exr_id")]
        public Guid Id { get; set; }
        [Required, MaxLength(100), Column("exr_name")]
        public string Name { get; set; } = string.Empty;
        [MaxLength(400), Column("exr_description")]
        public string Description { get; set; } = string.Empty;
        [Column("exr_created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        [InverseProperty(nameof(ExerciseGoal.ExerciseExerciseGoal))]
        public virtual ICollection<ExerciseGoal> ExerciseGoalExercice { get; set; } = new List<ExerciseGoal>();

        [InverseProperty(nameof(ExerciseCategory.ExerciseExerciseCategory))]
        public virtual ICollection<ExerciseCategory> ExerciseCategoryExercise { get; set; } = new List<ExerciseCategory>();

        [InverseProperty(nameof(ExerciseMuscleLink.ExerciseExerciseMuscle))]
        public virtual ICollection<ExerciseMuscleLink> ExerciseMuscleExercise { get; set; } = new List<ExerciseMuscleLink>();

        // public ICollection<Muscle> Muscles { get; set; } = new List<Muscle>();
    }
}