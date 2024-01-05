using FlowSheetAPI.DomainModel;

namespace FlowSheetAPI.Model
{
    public class LabItemSpecialityViewModel
    {
        public Guid LabItemSpecialityId { get; set; }
        public bool IsActive { get; set; }
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public Guid LabItemId { get; set; }
        public Guid SpecialityTypeId { get; set; }
        public Guid SpecialityConditionTypeId { get; set; }
        public SpecialityType SpecialityType { get; set; }
        public LabItem LabItem { get; set; }
        public SpecialityConditionType SpecialityConditionType { get; set; }
    }

}
