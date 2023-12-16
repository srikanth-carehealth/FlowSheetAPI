namespace FlowSheetAPI.DataTransferObjects
{
    public class FlowSheetNote
    {
        public string? SpecialityId { get; set; }

        public string? SpecialityName {  get; set; }

        public int TotalApprovalCount { get; set; }

        public ApproverDetails[] ApproverDetails { get; set; }

        public string[] Columns { get; set; }

        public IEnumerable<Dictionary<string, string>>? Data {  get; set; }
    }

    public class ApproverDetails
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string SpecialityName { get; set; }
    }
}
