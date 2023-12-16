using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace FlowSheetAPI.DomainModel
{
    [Serializable]
    [Table("FlowsheetHistory")]
    public class FlowsheetHistory
    {
        [Key]
        [Required]
        [Column("flowsheetHistory_id")]
        public Guid FlowsheetHistoryId { get; set; }

        [Required]
        [Column("flowsheet_note")]
        public string? FlowsheetNote { get; set; }

        [Column("is_locked")]
        public bool IsLocked { get; set; }

        [Column("locked_by")]
        public string? LockedBy { get; set; }

        [Column("locked_date")]
        public DateTime? LockedDate { get; set; }

        [Column("row_version", TypeName = "bytea")]
        public byte[] RowVersion { get; set; }

        [Required]
        [ForeignKey("specialityType_id")]
        public SpecialityType SpecialityType { get; set; }

        [Required]
        [ForeignKey("speciality_condition_type_id")]
        public SpecialityConditionType? SpecialityConditionType { get; set; }

        [Required]
        [ForeignKey("flowsheet_id")]
        public Flowsheet? Flowsheet { get; set; }

        [Required]
        [ForeignKey("doctor_id")]
        public Doctor Doctor { get; set; }

        [Required]
        [ForeignKey("patient_id")]
        public Patient Patient { get; set; }

        [Required]
        [DataMember]
        [Column("created_by")]
        public string CreatedBy { get; set; }

        [Required]
        [DataMember]
        [Column("updated_by")]
        public string UpdatedBy { get; set; }

        [Required]
        [DataMember]
        [Column("created_date")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [DataMember]
        [Column("updated_date")]
        public DateTime UpdatedDate { get; set; }
    }
}
