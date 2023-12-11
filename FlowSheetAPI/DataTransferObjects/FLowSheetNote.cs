namespace FlowSheetAPI.DataTransferObjects
{
    public class FLowSheetNote
    {
        public Guid SpecialityId { get; set; }

        public string? SpecialityName {  get; set; }

        public Array? Columns { get; set; }

        public IEnumerable<Dictionary<string, string>>? Data {  get; set; }
    }
}
