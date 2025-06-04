using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orkaris_Back.Models.EntityFramework
{
    [Table("t_e_exercise_muscle_link")]
    public class ExerciseMuscleLink
    {
        [Column("exr_id")]
        public Guid ExerciseId { get; set; }

        [Column("mus_id")]
        public Guid MuscleId { get; set; }
    }
}
