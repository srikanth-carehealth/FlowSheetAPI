using FlowSheetAPI.Repository.Interfaces;
using FlowSheetAPI.Services.Interfaces;

namespace FlowSheetAPI.Services.Implementation
{
    public class LookupService : ILookupService
    {
        //    //private readonly ILookupRepository<CallType> _callTypeRepository;
        //    //private readonly ILookupRepository<PriorityType> _priorityTypeRepository;
        //    //private readonly ILookupRepository<TaskOptionType> _taskOptionTypeRepository;
        //    //private readonly ILookupRepository<TaskStatusType> _taskStatusTypeRepository;
        //    //private readonly ILookupRepository<TaskType> _taskTypeRepository;
        //    //private readonly ILookupRepository<SystemPhrases> _systemPhrasesRepository;

        //    public LookupService(ILookupRepository<CallType> callTypeRepository,
        //                            ILookupRepository<PriorityType> priorityTypeRepository,
        //                            ILookupRepository<TaskOptionType> taskOptionTypeRepository,
        //                            ILookupRepository<TaskStatusType> taskStatusTypeRepository,
        //                            ILookupRepository<TaskType> taskTypeRepository,
        //                            ILookupRepository<SystemPhrases> systemPhrasesRepository)
        //    {
        //        _callTypeRepository = callTypeRepository;
        //        _priorityTypeRepository = priorityTypeRepository;
        //        _taskOptionTypeRepository = taskOptionTypeRepository;
        //        _taskStatusTypeRepository = taskStatusTypeRepository;
        //        _taskTypeRepository = taskTypeRepository;
        //        _systemPhrasesRepository = systemPhrasesRepository;
        //    }

        //    public async Task<IEnumerable<TaskType?>> GetAllAsyncTaskTypes()
        //        => await _taskTypeRepository.GetAllAsync();

        //    public async Task<IEnumerable<TaskStatusType?>> GetAllAsyncTaskStatusTypes()
        //        => await _taskStatusTypeRepository.GetAllAsync();

        //    public async Task<IEnumerable<TaskOptionType?>> GetAllAsyncTaskOptionTypes()
        //        => await _taskOptionTypeRepository.GetAllAsync();

        //    public async Task<IEnumerable<PriorityType?>> GetAllAsyncPriorityTypes()
        //        => await _priorityTypeRepository.GetAllAsync();

        //    public async Task<IEnumerable<CallType?>> GetAllAsyncCallTypes()
        //        => await _callTypeRepository.GetAllAsync();

        //    public async Task<IEnumerable<SystemPhrases?>> GetAllAsyncSystemPhrases()
        //        => await _systemPhrasesRepository.GetAllAsync();

        //    public async Task<TaskType?> GetTaskTypeById(Guid taskTypeId)
        //        => await _taskTypeRepository.GetByIdAsync(taskTypeId);

        //    public async Task<TaskStatusType?> GetTaskStatusTypeById(Guid taskStatusTypeId)
        //        => await _taskStatusTypeRepository.GetByIdAsync(taskStatusTypeId);

        //    public async Task<TaskOptionType?> GetTaskOptionTypeById(Guid taskOptionTypeId)
        //        => await _taskOptionTypeRepository.GetByIdAsync(taskOptionTypeId);

        //    public async Task<PriorityType?> GetPriorityTypeById(Guid priorityTypeId)
        //        => await _priorityTypeRepository.GetByIdAsync(priorityTypeId);

        //    public async Task<CallType?> GetCallTypeById(Guid callTypeId)
        //        => await _callTypeRepository.GetByIdAsync(callTypeId);

        //    public async Task<SystemPhrases?> GetSystemPhrasesById(Guid systemPhrasesId)
        //        => await _systemPhrasesRepository.GetByIdAsync(systemPhrasesId);
        //}
    }
}
