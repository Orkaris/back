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

        // Navigation properties
        [ForeignKey("SessionId"), InverseProperty("SessionSessionPerformance")]
        public Session? SessionSessionPerformance { get; set; }

    }
}