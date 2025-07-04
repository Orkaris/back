using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orkaris_Back.Models.EntityFramework
{
    [Table("t_e_session_performance_spe")]
    public class SessionPerformance
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("spe_id")]
        public Guid Id { get; set; }

        [Required, Column("ses_id")]
        public Guid SessionId { get; set; }

        [Column("spe_feeling")]
        public string? feeling { get; set; }

        [Required, Column("spe_date")]
        public DateTime Date { get; set; }

        [Required, Column("ses_duration")]
        public TimeSpan Duration { get; set; } = TimeSpan.Zero;

        // Navigation properties
        [ForeignKey("SessionId"), InverseProperty(nameof(Session.SessionPerformanceSession))]
        public Session? SessionSessionPerformance { get; set; }

    }
}