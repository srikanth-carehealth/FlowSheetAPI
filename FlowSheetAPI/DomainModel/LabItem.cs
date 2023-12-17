using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace FlowSheetAPI.DomainModel
{
    [Serializable]
    [Table("LabItem")]
    public class LabItem
    {
        [Key]
        [Required]
        [DataMember]
        [Column("lab_item_id")]
        public Guid LabItemId { get; set; }

        [Required]
        [DataMember]
        [Column("lab_item_name")]
        public string LabItemName { get; set; }

        [Required]
        [Column("created_by")]
        public string CreatedBy { get; set; }

        [Required]
        [Column("updated_by")]
        public string UpdatedBy { get; set; }

        [Required]
        [Column("created_date")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [Column("updated_date")]
        public DateTime UpdatedDate { get; set; }

        [Column("row_version", TypeName = "bytea")]
        public byte[] RowVersion { get; set; }
    }
}
