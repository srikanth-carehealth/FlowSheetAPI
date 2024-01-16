using FlowSheetAPI.DomainModel;

namespace FlowSheetAPI.DataTransferObjects
{
    public class FlowSheetWrapper
    {
        public SpecialityType SpecialityType { get; set; }
        public SpecialityConditionType SpecialityConditionType { get; set; }
        public IEnumerable<FlowSheetColumns>? FlowsheetColumns { get; set; }
        public IEnumerable<FlowSheetVM> Flowsheets { get; set; }
        public IEnumerable<Approver> Approver { get; set; }
    }
}
