using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace FlowSheetAPI.DomainModel
{
    [Serializable]
    [Table("Doctor")]
    public class Doctor
    {
        [Key]
        [Required]
        [DataMember]
        [Column("doctor_id")]
        public Guid DoctorId { get; set; }

        [Required]
        [DataMember]
        [Column("ehr_user_name")]
        public string? EhrUserName { get; set; }

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
    }
}
