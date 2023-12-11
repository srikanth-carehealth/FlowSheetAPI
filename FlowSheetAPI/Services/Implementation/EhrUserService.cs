using FlowSheetAPI.DomainModel;
using FlowSheetAPI.Repository.Interfaces;
using FlowSheetAPI.Services.Interfaces;
using Response = FlowSheetAPI.DataTransferObjects.Response;

namespace FlowSheetAPI.Services.Implementation
{
    public class EhrUserService : IEhrUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<EhrUserService> _logger;

        public EhrUserService(IUnitOfWork unitOfWork, 
                            ILogger<EhrUserService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public Task<Doctor?> GetDoctorByUserName(string ehrUserName)
        {
            return Task.FromResult(_unitOfWork.RegisterRepository<Doctor>().Where(d => d.EhrUserName == ehrUserName).Result.FirstOrDefault());
        }

        public Response UpsertDoctor(Doctor doctor)
        {
            var response = new Response();
            try
            {
                // Begin a transaction.
                _unitOfWork.BeginTransaction();

                // Add the entity to the database.
                _unitOfWork.RegisterRepository<Doctor>().UpsertAsync(doctor);

                // Perform other operations that may be part of the same transaction...

                // Save the changes to the database.
                _unitOfWork.SaveChanges();

                // Commit the transaction.
                _unitOfWork.CommitTransaction();
                response.Success = true;
                response.Message = "Data successfully saved.";
                _logger.LogInformation("Doctor data successfully saved. " + doctor.DoctorId);
            }
            catch (Exception ex)
            {
                // Rollback the transaction.
                _unitOfWork.RollbackTransaction();
                response.Success = false;
                response.Message = ex.Message;
                _logger.LogError("Error occurred while saving doctor data. " + ex);
            }

            return response;
        }

        public Task<Patient?> GetPatientByUserName(string ehrUserName)
        {
            return Task.Run(() => _unitOfWork.RegisterRepository<Patient>().Where(d => d.EhrUserName == ehrUserName).Result.FirstOrDefault());
        }

        public Response UpsertPatient(Patient patient)
        {
            var response = new Response();
            try
            {
                // Begin a transaction.
                _unitOfWork.BeginTransaction();

                // Add the entity to the database.
                _unitOfWork.RegisterRepository<Patient>().UpsertAsync(patient);

                // Perform other operations that may be part of the same transaction...

                // Save the changes to the database.
                _unitOfWork.SaveChanges();

                // Commit the transaction.
                _unitOfWork.CommitTransaction();
                response.Success = true;
                response.Message = "Data successfully saved.";
            }
            catch (Exception ex)
            {
                // Rollback the transaction.
                _unitOfWork.RollbackTransaction();
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
