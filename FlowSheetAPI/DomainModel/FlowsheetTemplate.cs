using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace FlowSheetAPI.DomainModel
{
    [Serializable]
    [Table("FlowsheetTemplate")]
    public class FlowsheetTemplate
    {
        [Key]
        [Required]
        [Column("flowsheetTemplate_id")]
        public Guid FlowsheetTemplateId { get; set; }

        [Required]
        [Column("column_name")]
        public string? ColumnName { get; set; }

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
        [ForeignKey("specialityType_id")]
        public SpecialityType SpecialityType { get; set; }
    }
}
