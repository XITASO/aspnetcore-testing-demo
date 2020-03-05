using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.DTO;
using WebApp.Model;
using System.Linq;
using System.Collections.Generic;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly MyDbContext db;

        public ProjectsController(MyDbContext db)
        {
            this.db = db;
        }

        [HttpGet("{id}")]
        public async Task<ProjectDto> GetById(long id)
        {
            var result = await db.Projects.FindAsync(id);
            return new ProjectDto
            {
                Id = result.Id,
                Start = result.Start,
                End = result.End,
                Title = result.Title,
                Description = result.Description,
                MaxParticipants = result.MaxParticipants
            };
        }

        [HttpPost()]
        public async Task<ActionResult> CreateDto([FromBody] ProjectCreateDto dto) {
            var project = new Project {
                Description = dto.Description,
                End = dto.End,
                MaxParticipants = dto.MaxParticipants,
                Start = dto.Start,
                Title = dto.Title
            };

            var persistedProject = await db.Projects.AddAsync(project);
            await db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), persistedProject.Entity.Id);
        }

        [HttpPut("{projectId}")]
        public async Task<ActionResult> AddParticipant([FromRoute] long projectId, [FromBody] long participantId) {
            var project = await db.Projects.FindAsync(projectId);
            var participant = await db.Participants.FindAsync(participantId);
            if (project == null || participant == null) {
                return NotFound();
            }
            project.Add(participant);
            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("{id}/participants")]
        public  async Task<ActionResult<IEnumerable<ParticipantDto>>> GetProjectParticipantsByProjectName(long id) {
            var project = await db.Projects.FindAsync(id);
            if (project == null) {
                return NotFound();
            }
            return Ok(project.Participants
            .Select(p => new ParticipantDto {
                Name = p.Name,
                Birthday = p.Birthday,
                Email = p.Email,
                Id = p.Id,
                Phone = p.Phone
            }));
        }
    }
}
