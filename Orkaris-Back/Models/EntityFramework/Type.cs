using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orkaris_Back.Models.EntityFramework
{
    [Table("t_e_type_tpe")]
    public class Type
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("tpe_id")]
        public Guid Id { get; set; }

        [Required, MaxLength(100), Column("tpe_name")]
        public string Name { get; set; } = string.Empty;

        [ForeignKey("Sport"), Required, Column("spo_id")]
        public int SportId { get; set; }
        [Column("tpe_created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        [ForeignKey("SportId"), InverseProperty("TypeSport")]
        public virtual Sport? SportType { get; set; }

    }
}