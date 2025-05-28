using AutoMapper;
using EcoLudicoAPI.DTOS;
using EcoLudicoAPI.Enums;
using EcoLudicoAPI.Models;
using EcoLudicoAPI.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcoLudicoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public SchoolController(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<SchoolDTO>> GetAllSchools()
        {
            var schools = await _uof.SchoolRepository.GetAllWithDetailsAsync();

            var usersWithSchools = await _uof.UserRepository.GetAllAsync(); 
            var userSchoolMap = usersWithSchools
                .Where(u => u.SchoolId != null)
                .ToDictionary(u => u.SchoolId.Value, u => u.UserId);

            var schoolsDTO = schools.Select(school =>
            {
                var dto = _mapper.Map<SchoolDTO>(school);
                if (userSchoolMap.TryGetValue(school.SchoolId, out int ownerId))
                {
                    dto.OwnerUserId = ownerId;
                }
                return dto;
            });

            return Ok(schoolsDTO);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SchoolDTO>> GetSchoolById(int id)
        {
            var school = await _uof.SchoolRepository.GetSchoolsByIdsAsync(id);
            if (school == null)
                return NotFound("Escola não encontrada.");

            var schoolDTO = _mapper.Map<SchoolDTO>(school);
            return Ok(schoolDTO);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSchool(int id, [FromBody] SchoolUpdateDTO schoolUpdateDTO)
        {
            var school = await _uof.SchoolRepository.GetSchoolsByIdsAsync(id);
            if (school == null)
                return NotFound();

            _mapper.Map(schoolUpdateDTO, school);

            if (school.Address != null && schoolUpdateDTO.Address != null)
            {
                _mapper.Map(schoolUpdateDTO.Address, school.Address);
            }

            await _uof.CommitAsync();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchool(int id)
        {
            var school = await _uof.SchoolRepository.GetSchoolsByIdsAsync(id);
            if (school == null)
                return NotFound();

            _uof.SchoolRepository.Delete(school);
            await _uof.CommitAsync();

            return NoContent();  
        }


    }
}
