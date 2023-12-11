using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace FlowSheetAPI.DomainModel
{
    [Serializable]
    [Table("FlowsheetApprovalHistory")]
    public class FlowsheetApprovalHistory
    {
        [Key]
        [Required]
        [DataMember]
        [Column("flowsheetApprovalHistory_id")]
        public Guid FlowsheetApprovalHistoryId { get; set; }

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
        [ForeignKey("flowsheet_id")]
        public Flowsheet Flowsheet { get; set; }

        [Required]
        [ForeignKey("flowsheetApprover_id")]
        public FlowsheetApprover FlowsheetApprover { get; set; }
    }
}
