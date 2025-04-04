using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Orkaris_Back.Models.EntityFramework
{
    [Table("t_e_Program_pgr")]
    public class Program
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("pfr_id")]
        public Guid Id { get; set; }
        [Required, MaxLength(100), Column("pfr_name")]
        public string Name { get; set; } = string.Empty;
        [ForeignKey("User"), Required, Column("usr_id")]
        public int UserId { get; set; }
        [Column("pfr_created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("UserId"), InverseProperty("ProgramUser")]
        public virtual User? UserProgram { get; set; }
    }
}