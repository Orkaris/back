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
        [Column("exr_created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        [InverseProperty("SessionExerciceExercise")]
        public virtual ICollection<SessionExercise> ExerciseSessionExercice { get; set; } = new List<SessionExercise>();
    }
}