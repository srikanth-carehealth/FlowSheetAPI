namespace FlowSheetAPI.Model
{
    public class SpecialityConditionTypeViewModel
    {
        public Guid SpecialityConditionTypeId { get; set; }
        public string ConditionName { get; set; }
        public string SpecilityConditionCode { get; set; }
        public bool IsActive { get; set; }
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public Guid SpecialityTypeId { get; set; }
    }
}
