using System.Security.Claims;
using DevVault.Application.Tasks.DTOs;
using DevVault.Domain.Entities;
using DevVault.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevVault.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TasksController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{projectId}")]
        public async Task<IActionResult> GetTasks(Guid projectId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            // Verify project ownership
            var project = await _context.Projects
                .FirstOrDefaultAsync(p => p.Id == projectId && p.OwnerId == Guid.Parse(userId));

            if (project == null) return NotFound("Project not found or not accessible.");

            var tasks = await _context.Tasks
                .Where(t => t.ProjectId == projectId)
                .Select(t => new TaskResponse
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    IsCompleted = t.IsCompleted,
                    CreatedAt = t.CreatedAt
                })
                .ToListAsync();

            return Ok(tasks);
        }

        [HttpPost("{projectId}")]
        public async Task<IActionResult> CreateTask(Guid projectId, [FromBody] TaskRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var project = await _context.Projects
                .FirstOrDefaultAsync(p => p.Id == projectId && p.OwnerId == Guid.Parse(userId));

            if (project == null) return NotFound("Project not found or not accessible.");

            var task = new TaskItem
            {
                Title = request.Title,
                Description = request.Description,
                ProjectId = projectId,
                CreatedAt = DateTime.UtcNow
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return Ok(new TaskResponse
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                IsCompleted = task.IsCompleted,
                CreatedAt = task.CreatedAt
            });
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateTask(Guid id, [FromBody] TaskItem updatedTask)
        {
            var existingTask = await _context.Tasks.FindAsync(id);
            if (existingTask == null)
                return NotFound();

            existingTask.Title = updatedTask.Title ?? existingTask.Title;
            existingTask.Description = updatedTask.Description ?? existingTask.Description;
            existingTask.IsCompleted = updatedTask.IsCompleted;

            await _context.SaveChangesAsync();
            return Ok(existingTask);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
                return NotFound();

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
