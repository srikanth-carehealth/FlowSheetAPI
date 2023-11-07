using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowSheetAPI.DomainModel.Endocrinology
{
    public class Endocrinology : BaseEntity
    {
        [Required]
        [Key]
        public Guid EndocrinologyId { get; set; }

        [Required]
        [Column("patient_id")]
        public string PatientId { get; set; }
        public DateTime Date { get; set; }
        public string? Medication { get; set; }
        public string? Dose { get; set; }
        public float? A1C { get; set; }
        public string? Recommendation { get; set; }

        [Column("initial_date")]
        public DateTime? InitialDate { get; set; }

        [Column("is_locked")]
        public bool IsLocked { get; set; }

        [Column("locked_by")]
        public string? LockedBy { get; set; }

        [Column("locked_date")]
        public DateTime? LockedDate { get; set;}
    }
}
