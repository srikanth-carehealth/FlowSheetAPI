namespace FlowSheetAPI.DataTransferObjects
{
    public class FlowSheetNote
    {
        public string? SpecialityId { get; set; }
        public string? SpecialityName {  get; set; }
        public string? SpecialityConditionTypeId { get; set; }
        public string? SpecialityConditionTypeName { get; set; }

        public IEnumerable<Dictionary<string, string>>? Data {  get; set; }
        public ApproverDetails[] ApproverDetails { get; set; }
        //public string[] Columns { get; set; }
        //public int TotalApprovalCount { get; set; }
    }

    public class ApproverDetails
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Initial { get; set; }
        public string Designation { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string Address { get; set; }
        //public string SpecialityName { get; set; }
    }
}
