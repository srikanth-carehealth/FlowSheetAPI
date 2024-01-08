using FlowSheetAPI.DomainModel;

namespace FlowSheetAPI.DataTransferObjects
{
    public class FlowSheetIM
    {
        public string EhrDoctorUserName { get; set; }

        public int EhrPatientId { get; set; }

        public string? Note {  get; set; }

        public Approver? Approver { get; set; }

        public Guid SpecialityTypeId { get; set; }

        public Guid SpecialityConditionTypeId { get; set; }

    }
}
