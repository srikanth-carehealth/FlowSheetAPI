namespace FlowSheetAPI.Model
{
    public class FlowsheetTemplateViewModel
    {
        public Guid FlowsheetTemplateId { get; set; }
        public string? ColumnName { get; set; }
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public bool IsActive { get; set; }
        public Guid SpecialityTypeId { get; set; }
        public Guid SpecialityConditionTypeId { get; set; }
    }
}
