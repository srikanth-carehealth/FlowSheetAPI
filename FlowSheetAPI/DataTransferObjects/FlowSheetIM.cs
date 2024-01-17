namespace FlowSheetAPI.DataTransferObjects
{
    public class FlowSheetIM
    {
        public Guid FlowsheetId { get; set; }
        public string EhrDoctorUserName { get; set; }
        public int EhrPatientId { get; set; }
        public string? Note {  get; set; }
        public Guid ApproverId { get; set; }
        public Guid SpecialityTypeId { get; set; }
        public Guid SpecialityConditionTypeId { get; set; }
    }
}
