using FlowSheetAPI.DataTransferObjects;
using FlowSheetAPI.DomainModel;

namespace FlowSheetAPI.Services.Interfaces
{
    public interface IEhrUserService
    {
        public Task<Doctor?> GetDoctorByUserName(string ehrUserName);
        public Task<Patient?> GetPatientByUserName(string ehrUserName);
        public Response UpsertDoctor(Doctor doctor);
        public Response UpsertPatient(Patient patient);
    }
}
