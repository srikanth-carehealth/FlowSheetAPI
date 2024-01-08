using FlowSheetAPI.DataTransferObjects;
using FlowSheetAPI.DomainModel;

namespace FlowSheetAPI.Services.Interfaces
{
    public interface IFlowsheetService
    {
        public Task<FlowSheetWrapper> GetAllAsync();
        public Task<FlowSheetWrapper> GetByIdAsync(Guid id);
        public Task<FlowSheetWrapper> GetByPatient(int ehrPatientId);
        public Task<FlowSheetWrapper> GetByDoctorAndPatient(string ehrDoctorUserName, int ehrPatientId);
        public Response Upsert(Flowsheet? flowsheet);
        public Response InsertFlowSheet(FlowSheetIM? inputModel);
        public Task<FlowSheetWrapper> GetByDoctor(string ehrUserName);
        public Task<FlowSheetWrapper> GetBySpecialityConditionAndPatient(string conditionSpecialityType, int ehrPatientId);
    }
}
