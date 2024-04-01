using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IUserService _userService;

        public TaskController(ITaskService taskService, IUserService userService)
        {
            _taskService = taskService;
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<Domain.Entities.Task>> CreateTask(Domain.Entities.Task task)
        {
            var createdUser = await _userService.GetUserAsync(task.CreatedBy);
            if (createdUser == null)
            {
                return BadRequest(new { error = "Username already exists" });
            }

            var createdTask = await _taskService.CreateTaskAsync(task);
            return CreatedAtAction(nameof(GetTaskById), new { id = createdTask.Id }, createdTask);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(Guid id, Domain.Entities.Task task)
        {
            var createdUser = await _userService.GetUserAsync(task.CreatedBy);
            if (createdUser == null)
            {
                return BadRequest(new { error = "Username already exists" });
            }
            
            if (id != task.Id)
            {
                return BadRequest();
            }

            await _taskService.UpdateTaskAsync(task);

            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Domain.Entities.Task>>> GetAllTasks()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            return Ok(tasks);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Task>>> GetAllTasksByUser(Guid userId)
        {
            var tasks = await _taskService.GetAllTasksByUserAsync(userId);
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Domain.Entities.Task>> GetTaskById(Guid id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            return task;
        }
    }

}
