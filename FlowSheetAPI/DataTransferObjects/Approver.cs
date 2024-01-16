namespace FlowSheetAPI.DataTransferObjects
{
    public class Approver
    {
        public Guid FlowsheetApproverId { get; set; }
        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string? LastName { get; set; }

        public string? Initial { get; set; }

        public string? Designation { get; set; }

        public string? Telephone { get; set; }

        public string? Fax { get; set; }

        public string? Address { get; set; }

        public string ClientId { get; set; }

        public string ClientName { get; set; }
    }
}
