using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Orkaris_Back.Models.EntityFramework
{
    [Table("t_e_user_usr")]
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("usr_id")]
        public Guid Id { get; set; }

        [Required, MaxLength(100), Column("usr_name")]
        public string Name { get; set; } = string.Empty;

        [Required, EmailAddress, Column("usr_email")]
        public string Email { get; set; } = string.Empty;

        [Required, Column("usr_password")]
        public string Password { get; set; } = string.Empty;

        [Required, MaxLength(50), Column("usr_gender")]
        public string Gender { get; set; } = string.Empty;

        [Required, Range(0, 300), Column("usr_height")]
        public int Height { get; set; }

        [Required, Range(0, 300), Column("usr_weight")]
        public int Weight { get; set; }
        [Column("usr_birth_date")]
        public DateOnly? BirthDate { get; set; } = null;

        [Required, Range(1, 3), Column("usr_profile_type")]
        public int ProfileType { get; set; }

        [Required, Column("usr_is_verified")]
        public bool IsVerified { get; set; } = false;

        [Column("usr_created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property

        [InverseProperty(nameof(Workout.UserWorkout))]
        public virtual ICollection<Workout> WorkoutUser { get; set; } = new List<Workout>();

        [InverseProperty(nameof(Session.UserSession))]
        public virtual ICollection<Session> SessionUser { get; set; } = new List<Session>();

        [InverseProperty(nameof(EmailConfirmationToken.UserEmail))]
        public virtual ICollection<EmailConfirmationToken> EmailUser { get; set; } = new List<EmailConfirmationToken>();
    }
}