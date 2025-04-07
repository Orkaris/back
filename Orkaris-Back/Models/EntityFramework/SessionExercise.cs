using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Orkaris_Back.Models.EntityFramework
{
    [Table("t_j_session_exercise_goal_seg")]
    public class SessionExercise
    {
        [Required, Column("ses_id")]
        public Guid SessionId { get; set; }
        [Required, Column("exg_id")]
        public Guid ExerciseId { get; set; }

        // Navigation property
        [ForeignKey("SessionId"), InverseProperty(nameof(Session.SessionExerciseSession))]
        public Session? SessionSessionExercise { get; set; }

        [ForeignKey("ExerciseId"), InverseProperty(nameof(ExerciseGoal.SessionExerciseExerciseGoal))]
        public ExerciseGoal? ExerciseGoalSessionExercise { get; set; }
        
    }
}