using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.DTO;
using WebApp.Model;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParticipantsController : ControllerBase
    {
        private readonly MyDbContext db;

        public ParticipantsController(MyDbContext db)
        {
            this.db = db;
        }

        [HttpPost]
        public async Task<ActionResult> Add(ParticipantCreateDto dto)
        {
            var participant = new Participant {
                Name = dto.Name,
                Address = new Address {
                    City = dto.Address.City,
                    Country = dto.Address.Country,
                    HouseNumber = dto.Address.HouseNumber,
                    PostCode =  dto.Address.PostCode,
                    Street = dto.Address.Street
                },
                Birthday = dto.Birthday,
                Email = dto.Email,
                Phone = dto.Phone
            };
            var addedParticipant = await db.Participants.AddAsync(participant);
            await db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), addedParticipant.Entity.Id);
        }

        [HttpGet("{id}")]
        public async Task<ParticipantDto> GetById(long id) {
            var result = await db.Participants.FindAsync(id);
            return new ParticipantDto {
                Name = result.Name,
                Birthday = result.Birthday,
                Email = result.Email,
                Id = result.Id,
                Phone = result.Phone
            };
        }
    }
}
