using FlowSheetAPI.DataTransferObjects;
using FlowSheetAPI.DomainModel;

namespace FlowSheetAPI.Services.Interfaces
{
    public interface IFlowsheetService
    {
        public Task<IEnumerable<Flowsheet>> GetAllAsync();
        public Task<Flowsheet?> GetByIdAsync(Guid id);
        public Task<IEnumerable<Flowsheet?>> GetByPatient(string ehrUserName);
        public Task<IEnumerable<Flowsheet?>> GetByDoctorAndPatient(string ehrDoctorUserName, string ehrPatientUserName);
        public Response Upsert(Flowsheet? flowsheet);
        public Task<IEnumerable<Flowsheet?>> GetByDoctor(string ehrUserName);
    }
}