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

        [Required, MinLength(6), Column("usr_password")]
        public string Password { get; set; } = string.Empty;

        [Required, MaxLength(50), Column("usr_gender")]
        public string Gender { get; set; } = string.Empty;

        [Required, Range(0, 300), Column("usr_height")]
        public int Height { get; set; }

        [Required, Range(0, 300), Column("usr_weight")]
        public int Weight { get; set; }
        [Required, Column("usr_birth_date")]
        public DateTime BirthDate { get; set; }

        [Required, Range(1, 3), Column("usr_profile_type")]
        public int ProfileType { get; set; }

        [Column("usr_created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property

        [InverseProperty(nameof(Program.UserProgram))]
        public virtual ICollection<Program> ProgramUser { get; set; } = new List<Program>();

        [InverseProperty(nameof(Session.SessionUser))]
        public virtual ICollection<Session> UserSession { get; set; } = new List<Session>();
    }
}