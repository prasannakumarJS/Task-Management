using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Interfaces
{
    public interface ITaskRepository
    {
        Task<Domain.Entities.Task> CreateAsync(Domain.Entities.Task task);
        Task<Domain.Entities.Task> UpdateAsync(Domain.Entities.Task task);
        Task<IEnumerable<Domain.Entities.Task>> GetAllAsync();
        Task<IEnumerable<Domain.Entities.Task>> GetAllByUserAsync(Guid userId);
        Task<Domain.Entities.Task> GetByIdAsync(Guid id);
    }

}
