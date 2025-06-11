using System;

namespace Orkaris_Back.Models.DTO
{
    public class MuscleExerciceSTO
    {
        public Guid MuscleId { get; set; }
        public string MuscleName { get; set; } = string.Empty;
        public int NbRep { get; set; }
        public int NbSet { get; set; }
        public int Valeur { get; set; }
    }
}
