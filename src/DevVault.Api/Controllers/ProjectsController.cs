using DevVault.Domain.Entities;
using DevVault.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DevVault.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProjectsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProjectsController(AppDbContext context)
        {
            _context = context;
        }

        private Guid GetUserId()
        {
            return Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserProjects()
        {
            var userId = GetUserId();
            var projects = await _context.Projects
                .Where(p => p.OwnerId == userId)
                .ToListAsync();

            return Ok(projects);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized();

            var project = new Project
            {
                Name = request.Name,
                Description = request.Description,
                CreatedAt = DateTime.UtcNow,
                OwnerId = Guid.Parse(userId)
            };

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return Ok(project);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(Guid id, Project updated)
        {
            var userId = GetUserId();
            var project = await _context.Projects.FindAsync(id);

            if (project == null) return NotFound();
            if (project.OwnerId != userId) return Forbid();

            project.Name = updated.Name;
            project.Description = updated.Description;

            await _context.SaveChangesAsync();
            return Ok(project);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(Guid id)
        {
            var userId = GetUserId();
            var project = await _context.Projects.FindAsync(id);

            if (project == null) return NotFound();
            if (project.OwnerId != userId) return Forbid();

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}