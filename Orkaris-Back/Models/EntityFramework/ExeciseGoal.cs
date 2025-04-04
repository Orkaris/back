using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Orkaris_Back.Models.EntityFramework
{
    [Table("t_e_exercise_goal_exg")]
    public class ExerciseGoal
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("exg_id")]
        public Guid Id { get; set; }
        [Required, MaxLength(100), Column("exg_name")]
        public string Name { get; set; } = string.Empty;
        
        [Column("exg_reps")]
        public int Reps { get; set; }
        [Column("exg_sets")]
        public int Sets { get; set; }
        [Column("exr_created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("Exercise"), Required, Column("exr_id")]
        public Guid ExerciseId { get; set; }


        // Navigation property
        [ForeignKey("ExerciseId"), InverseProperty("ExerciseGoalExercise")]
        public Exercise? ExerciseExerciseGoal { get; set; }
        [InverseProperty(nameof(SessionExercise.SessionExerciceGoalExercise))]
        public virtual ICollection<SessionExercise> ExerciseGoalSessionExercice { get; set; } = new List<SessionExercise>();

        [InverseProperty(nameof(ExerciseGoalPerfomance.ExerciseGoalExerciseGoalPerfomance))]
        public virtual ICollection<ExerciseGoalPerfomance> ExerciseGoalPerfomanceExerciseGoal { get; set; } = new List<ExerciseGoalPerfomance>();
    }
}