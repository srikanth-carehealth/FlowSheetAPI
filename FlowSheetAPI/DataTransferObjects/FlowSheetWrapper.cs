using FlowSheetAPI.DomainModel;

namespace FlowSheetAPI.DataTransferObjects
{
    public class FlowSheetWrapper
    {
        public SpecialityType SpecialityType { get; set; }

        public IEnumerable<FlowsheetTemplate>? FlowsheetColumns { get; set; }

        public IEnumerable<Flowsheet> Flowsheets { get; set; }
    }
}
