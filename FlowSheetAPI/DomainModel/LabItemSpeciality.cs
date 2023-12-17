using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace FlowSheetAPI.DomainModel
{
    [Serializable]
    [Table("LabItemSpeciality")]
    public class LabItemSpeciality
    {
        [Key]
        [Required]
        [DataMember]
        [Column("lab_item_speciality_id")]
        public Guid LabItemSpecialityId { get; set; }

        [Required]
        [DataMember]
        [Column("is_active")]
        public bool IsActive { get; set; }

        [Required]
        [DataMember]
        [Column("client_id")]
        public string ClientId { get; set; }

        [Required]
        [DataMember]
        [Column("client_name")]
        public string ClientName { get; set; }

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

        [Required]
        [DataMember]
        [Column("row_version", TypeName = "bytea")]
        public byte[] RowVersion { get; set; }

        [Required]
        [ForeignKey("lab_item_id")]
        public LabItem LabItem { get; set; }

        [Required]
        [ForeignKey("specialityType_id")]
        public SpecialityType SpecialityType { get; set; }

        [Required]
        [ForeignKey("speciality_contition_type_id")]
        public SpecialityConditionType SpecialityConditionType { get; set; }
    }
}
