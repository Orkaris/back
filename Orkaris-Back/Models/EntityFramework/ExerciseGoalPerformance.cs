using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Orkaris_Back.Models.EntityFramework
{
    [Table("t_e_exercise_goal_performance_egp")]
    public class ExerciseGoalPerformance
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("egp_id")]
        public Guid Id { get; set; }
        
        [Column("egp_reps")]
        public int Reps { get; set; }
        [Column("egp_sets")]
        public int Sets { get; set; }
        [Column("egp_created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Column("egp_weight")]
        public float Weight { get; set; }
        [ForeignKey("ExerciseGoal"), Required, Column("exg_id")]
        public Guid ExerciseGoalId { get; set; }

        // Navigation property
        [ForeignKey("ExerciseGoalId"), InverseProperty(nameof(ExerciseGoal.ExerciseGoalPerformanceExerciseGoal))]
        public ExerciseGoal? ExerciseGoalExerciseGoalPerformance { get; set; }
       
    }
}