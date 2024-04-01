using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITaskService
    {
        Task<Domain.Entities.Task> CreateTaskAsync(Domain.Entities.Task task);
        Task<Domain.Entities.Task> UpdateTaskAsync(Domain.Entities.Task task);
        Task<IEnumerable<Domain.Entities.Task>> GetAllTasksAsync();
        Task<IEnumerable<Domain.Entities.Task>> GetAllTasksByUserAsync(Guid userId);
        Task<Domain.Entities.Task> GetTaskByIdAsync(Guid id);
    }

}
