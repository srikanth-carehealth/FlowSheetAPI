using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace FlowSheetAPI.DomainModel
{
    [Table("Flowsheet")]
    public class Flowsheet
    {
        [Key]
        [Required]
        [DataMember]
        [Column("flowsheet_id")]
        public Guid FlowsheetId { get; set; }

        [Required]
        [DataMember]
        [Column("flowsheet_note")]
        public string? flowsheetNote { get; set; }

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

        [Column("row_version", TypeName = "bytea")]
        public byte[] RowVersion { get; set; }

        [Required]
        [DataMember]
        [ForeignKey("patient_id")]
        public Patient Patient { get; set; }

        [Required]
        [ForeignKey("doctor_id")]
        public Doctor Doctor { get; set; }

        [Required]
        [ForeignKey("specialityType_id")]
        public SpecialityType SpecialityType { get; set; }
    }
}
