using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orkaris_Back.Models.EntityFramework
{
    [Table("t_e_session_ses")]
    public class Session
    {
        [Key, Column("ses_id")]
        public Guid Id { get; set; }

        [Required, StringLength(100), Column("ses_name")]
        public string Name { get; set; } = string.Empty;

        [Required, Column("usr_id")]
        public Guid UserId { get; set; }

        [Required, Column("wrk_id")]
        public Guid WorkoutId { get; set; }

        [Required, Column("ses_created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        [ForeignKey("UserId"), InverseProperty(nameof(User.SessionUser))]
        public virtual User? UserSession { get; set; }
        [ForeignKey("WorkoutId"), InverseProperty(nameof(Workout.SessionWorkout))]
        public virtual Workout? WorkoutSession { get; set; }

        [InverseProperty(nameof(SessionExercise.SessionSessionExercise))]
        public virtual ICollection<SessionExercise> SessionExerciseSession { get; set; } = new List<SessionExercise>();

        [InverseProperty(nameof(SessionPerformance.SessionSessionPerformance))]
        public virtual ICollection<SessionPerformance> SessionPerformanceSession { get; set; } = new List<SessionPerformance>();
    }
}