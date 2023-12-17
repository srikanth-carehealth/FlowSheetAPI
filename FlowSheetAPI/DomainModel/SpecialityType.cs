using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FlowSheetAPI.DomainModel
{
    [Serializable]
    [Table("SpecialityType")]
    public class SpecialityType
    {
        [Key]
        [Required]
        [Column("specialityType_id")]
        public Guid SpecialityTypeId { get; set; }

        [Required]
        [Column("client_id")]
        public string ClientId { get; set; }

        [Required]
        [Column("client_name")]
        public string ClientName { get; set; }

        [Required]
        [Column("speciality_name")]
        public string SpecialityName { get; set; }

        [Column("speciality_code")]
        public string SpecialityCode { get; set; }

        [Required]
        [Column("is_active")]
        public bool IsActive { get; set; }

        [Column("total_approval_count")]
        public int TotalApprovalCount { get; set; }

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
