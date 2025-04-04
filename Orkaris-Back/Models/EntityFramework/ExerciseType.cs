using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orkaris_Back.Models.EntityFramework
{
    [Table("t_j_exercice_type_ext")]
    public class ExerciceType
    {
        [Required, Column("exe_id")]
        public Guid ExerciseId { get; set; }
        [Required, Column("tpe_id")]
        public Guid TypeId { get; set; }

        // Navigation properties
        [ForeignKey("ExerciseId"), InverseProperty("ExerciceTypeType")]
        public virtual Exercise? TypeExerciceType { get; set; }
        [ForeignKey("TypeId"), InverseProperty("ExerciceTypeExercise")]
        public virtual Type? ExerciceExerciseType { get; set; }
    }
}