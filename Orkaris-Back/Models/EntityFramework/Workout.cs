using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Orkaris_Back.Models.EntityFramework
{
    [Table("t_e_Workout_wkt")]
    public class Workout
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("wkt_id")]
        public Guid Id { get; set; }
        [Required, MaxLength(100), Column("wkt_name")]
        public string Name { get; set; } = string.Empty;
        [ForeignKey("User"), Required, Column("usr_id")]
        public Guid UserId { get; set; }
        [Column("pfr_created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        [ForeignKey("UserId"), InverseProperty(nameof(User.WorkoutUser))]
        public virtual User? UserWorkout { get; set; }

        [InverseProperty(nameof(Session.WorkoutSession))]
        public virtual ICollection<Session> SessionWorkout { get; set; } = new List<Session>();
    }
}