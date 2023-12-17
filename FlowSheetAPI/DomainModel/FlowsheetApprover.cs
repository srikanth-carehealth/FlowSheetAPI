using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace FlowSheetAPI.DomainModel
{
    [Serializable]
    [Table("FlowsheetApprover")]
    public class FlowsheetApprover
    {
        [Key]
        [Required]
        [DataMember]
        [Column("flowsheetApprover_id")]
        public Guid FlowsheetApproverId { get; set; }

        [Required]
        [Column("first_name")]
        public string? FirstName { get; set; }

        [Column("middle_name")]
        public string? MiddleName { get; set; }

        [Required]
        [Column("last_name")]
        public string? LastName { get; set; }

        [Required]
        [Column("initial")]
        public string? Initial { get; set; }

        [Required]
        [Column("designation")]
        public string? Designation { get; set; }

        [Column("telephone")]
        public string? Telephone { get; set; }

        [Column("fax")]
        public string? Fax { get; set; }

        [Column("Address")]
        public string? Address { get; set; }

        [Required]
        [Column("is_active")]
        public bool IsActive { get; set; }

        [Required]
        [Column("client_id")]
        public string ClientId { get; set; }

        [Required]
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
        [ForeignKey("specialityType_id")]
        public SpecialityType? SpecialityType { get; set; }

        [Required]
        [ForeignKey("speciality_condition_type_id")]
        public SpecialityConditionType? SpecialityConditionType { get; set; }
    }
}
