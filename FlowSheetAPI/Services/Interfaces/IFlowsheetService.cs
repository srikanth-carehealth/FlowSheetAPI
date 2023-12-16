using FlowSheetAPI.DataTransferObjects;
using FlowSheetAPI.DomainModel;

namespace FlowSheetAPI.Services.Interfaces
{
    public interface IFlowsheetService
    {
        public Task<FlowSheetWrapper> GetAllAsync();
        public Task<FlowSheetWrapper> GetByIdAsync(Guid id);
        public Task<FlowSheetWrapper> GetByPatient(string ehrUserName);
        public Task<FlowSheetWrapper> GetByDoctorAndPatient(string ehrDoctorUserName, string ehrPatientUserName);
        public Response Upsert(Flowsheet? flowsheet);
        public Task<FlowSheetWrapper> GetByDoctor(string ehrUserName);
        public Task<FlowSheetWrapper> GetBySpecialityAndPatient(string specialityType, string ehrPatientUserName);
    }
}