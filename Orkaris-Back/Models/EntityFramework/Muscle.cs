using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orkaris_Back.Models.EntityFramework
{
    [Table("t_e_muscle_mus")]
    public class Muscle
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("mus_id")]
        public Guid Id { get; set; }

        [Required, Column("mus_name"), MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        public ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
    }
}