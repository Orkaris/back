using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orkaris_Back.Models.EntityFramework
{
    [Table("t_j_exercice_type_ext")]
    public class ExerciseType
    {
        [Required, Column("exe_id")]
        public Guid ExerciseId { get; set; }
        [Required, Column("tpe_id")]
        public Guid TypeId { get; set; }

        // Navigation properties
        [ForeignKey("ExerciseId"), InverseProperty(nameof(Exercise.ExerciseTypeExercise))]
        public virtual Exercise? ExerciseExerciseType { get; set; }
        [ForeignKey("TypeId"), InverseProperty(nameof(Type.ExerciseTypeType))]
        public virtual Type? TypeExerciseType { get; set; }
    }
}