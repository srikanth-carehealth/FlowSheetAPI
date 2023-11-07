using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowSheetAPI.DomainModel
{
    [Serializable]
    public class BaseEntity
    {
        [NotMapped]
        [Required]
        public Guid Id { get; set; }

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

        [Required]
        [Column("row_version")]
        public byte[] RowVersion { get; set; }
    }
}
