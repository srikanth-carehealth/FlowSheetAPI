using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace FlowSheetAPI.DomainModel
{
    [Serializable]
    [Table("SpecialityConditionType")]
    public class SpecialityConditionType
    {
        [Key]
        [Required]
        [DataMember]
        [Column("speciality_condition_type_id")]
        public Guid SpecialityConditionTypeId { get; set; }

        [Column("condition_name")]
        [DataMember]
        [Required]
        public string ConditionName { get; set; }

        [Column("created_by")]
        [DataMember]
        public string CreatedBy { get; set; }

        [Column("updated_by")]
        public string UpdatedBy { get; set; }

        [Column("created_date")]
        [DataMember]
        public DateTime CreatedDate { get; set; }

        [Column("updated_date")]
        [DataMember]
        public DateTime UpdatedDate { get; set; }

        [Column("row_version", TypeName = "bytea")]
        [DataMember]
        public byte[] RowVersion { get; set; }

        [ForeignKey("speciality_id")]
        [DataMember]
        [Required]
        public SpecialityType SpecialityType { get; set; }
    }
}
