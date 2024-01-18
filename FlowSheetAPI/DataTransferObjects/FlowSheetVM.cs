using FlowSheetAPI.DomainModel;

namespace FlowSheetAPI.DataTransferObjects
{
    public class FlowSheetVM
    {
        public Guid FlowsheetId { get; set; }
        public FlowSheetNote flowsheetNote { get; set; }
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public SpecialityType SpecialityType { get; set; }
        //public SpecialityConditionType SpecialityConditionType { get; set; }
        public FlowsheetApprover? Approver { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
