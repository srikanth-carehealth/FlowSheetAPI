//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Runtime.Serialization;

//namespace FlowSheetAPI.DomainModel
//{
//    public class BaseEntity
//    {
//        //[NotMapped]
//        //public Guid Id { get; set; }

//        [Required]
//        [DataMember]
//        [Column("created_by")]
//        public string CreatedBy { get; set; }

//        [Required]
//        [DataMember]
//        [Column("updated_by")]
//        public string UpdatedBy { get; set; }

//        [Required]
//        [DataMember]
//        [Column("created_date")]
//        public DateTime CreatedDate { get; set; }

//        [Required]
//        [DataMember]
//        [Column("updated_date")]
//        public DateTime UpdatedDate { get; set; }

//        [Required]
//        [DataMember]
//        [Timestamp]
//        public virtual byte[] RowVersion { get; set; }
//    }
//}
