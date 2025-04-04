using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orkaris_Back.Models.EntityFramework
{
    [Table("t_e_sport_spo")]
    public class Sport
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("spo_id")]
        public Guid Id { get; set; }

        [Required, Column("spo_name"), MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        // Navigation property
        [InverseProperty(nameof(Type.SportType))]
        public virtual ICollection<Type> TypeSport { get; set; } = new List<Type>();       
    }
}