using Application.Interfaces;
using Persistance.Interfaces;

namespace Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<Domain.Entities.Task> CreateTaskAsync(Domain.Entities.Task task)
        {
            return await _taskRepository.CreateAsync(task);
        }

        public async Task<Domain.Entities.Task> UpdateTaskAsync(Domain.Entities.Task task)
        {
            return await _taskRepository.UpdateAsync(task);
        }

        public async Task<IEnumerable<Domain.Entities.Task>> GetAllTasksAsync()
        {
            return await _taskRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Domain.Entities.Task>> GetAllTasksByUserAsync(Guid userId)
        {
            return await _taskRepository.GetAllByUserAsync(userId);
        }

        public async Task<Domain.Entities.Task> GetTaskByIdAsync(Guid id)
        {
            return await _taskRepository.GetByIdAsync(id);
        }
    }

}
