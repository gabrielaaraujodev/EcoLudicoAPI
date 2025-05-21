using AutoMapper;
using EcoLudicoAPI.DTOS;
using EcoLudicoAPI.Enums;
using EcoLudicoAPI.Models;
using EcoLudicoAPI.Repositories.SpecificRepositories;
using EcoLudicoAPI.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcoLudicoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public ProjectController(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDTO>>> GetAllProjects()
        {
            var projects = await _uof.ProjectRepository.GetAllAsync(p => p.Comments);
            if (projects == null || !projects.Any())
                return NotFound("Nenhum projeto encontrado.");

            var projectDTO = _mapper.Map<IEnumerable<ProjectDTO>>(projects);
            return Ok(projectDTO);
        }

        //[HttpGet("{id}")]
        //public async Task<ActionResult<ProjectDTO>> GetProjectById(int id)
        //{
        //    var project = await _uof.ProjectRepository.GetByIdAsync(id);
        //    if (project == null)
        //        return NotFound("Projeto não encontrado.");

        //    var projectDTO = _mapper.Map<ProjectDTO>(project);
        //    return Ok(projectDTO);
        //}

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDTO>> GetProjectById(int id)
        {
            var project = await _uof.ProjectRepository.GetByIdAsync(id);
            if (project == null)
            {
                return NotFound("Projeto não encontrado.");
            }

            var projectDTO = _mapper.Map<ProjectDTO>(project);

            if (project.School != null && project.School.Teachers.Any())
            {
                projectDTO.SchoolOwnerUserId = project.School.Teachers
                                                .FirstOrDefault(t => t.Type == UserType.Professor)?.UserId;
            }
            else
            {
                projectDTO.SchoolOwnerUserId = null; 
            }

            if (project.Comments != null && project.Comments.Any())
            {
                projectDTO.Comments = _mapper.Map<List<CommentResponseDTO>>(project.Comments);
            }
            else
            {
                projectDTO.Comments = new List<CommentResponseDTO>(); 
            }

            return Ok(projectDTO);
        }


        [HttpGet("user/projects")]
        public async Task<IActionResult> GetProjectsByUserId(int userId)
        {
            var user = await _uof.UserRepository.GetByIdAsync(userId);

            if (user == null)
                return NotFound("Usuário não encontrado.");

            if (user.Type != UserType.Professor)
                return BadRequest("Usuário não é um professor.");

            if (user.SchoolId == null)
                return BadRequest("Professor não está vinculado a nenhuma escola.");

            var projects = await _uof.ProjectRepository
                .GetProjectsBySchoolIdAsync(user.SchoolId.Value);

            return Ok(projects);
        }


        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] ProjectCreateDTO dto, [FromQuery] int userId)
        {
            var user = await _uof.UserRepository.GetByIdAsync(userId);

            if (user == null || user.Type != UserType.Professor)
                return BadRequest("Apenas professores podem criar projetos ou professor não encontrado.");

            if (user.SchoolId == 0)
                return BadRequest("Professor não possui escola vinculada.");

            var project = _mapper.Map<Project>(dto);
            project.SchoolId = user.SchoolId.Value;

            _uof.ProjectRepository.Create(project);
            await _uof.CommitAsync();

            var result = _mapper.Map<ProjectDTO>(project);
            return CreatedAtAction(nameof(GetProjectById), new { id = result.ProjectId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, [FromQuery] int userId, [FromBody] ProjectUpdateDTO projectUpdateDTO)
        {
            var user = await _uof.UserRepository.GetByIdAsync(userId);
            if (user == null || user.Type != UserType.Professor)
                return Unauthorized("Usuário não autorizado.");

            if (user.SchoolId == 0)
                return BadRequest("Professor não possui escola associada.");

            var project = await _uof.ProjectRepository.GetByIdAsync(id);
            if (project == null)
                return NotFound("Projeto não encontrado.");

            if (project.SchoolId != user.SchoolId)
                return Forbid("Este projeto não pertence à sua escola.");

            _mapper.Map(projectUpdateDTO, project); 

            _uof.ProjectRepository.Update(project);
            await _uof.CommitAsync();

            var updatedProjectDTO = _mapper.Map<ProjectDTO>(project);

            updatedProjectDTO.SchoolOwnerUserId = userId; 
                                                         
            return Ok(updatedProjectDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id, [FromQuery] int userId)
        {
            var project = await _uof.ProjectRepository.GetByIdAsync(id);
            if (project == null)
                return NotFound("Projeto não encontrado.");

            var user = await _uof.UserRepository.GetByIdAsync(userId);
            if (user == null || user.Type != UserType.Professor)
                return NotFound("Somente professores podem excluir projetos ou professor não encontrado.");

            _uof.ProjectRepository.Delete(project);
            await _uof.CommitAsync();

            return NoContent();
        }

        [HttpGet("age-range/{range}")]
        public async Task<ActionResult<IEnumerable<ProjectDTO>>> GetProjectsByAgeRange(AgeRange range)
        {
            var projects = await _uof.ProjectRepository.GetProjectsByAgeRangeAsync(range);
            if (projects == null || !projects.Any())
                return NotFound("Nenhum projeto encontrado para a faixa etária especificada.");

            var projectDtos = _mapper.Map<IEnumerable<ProjectDTO>>(projects);
            return Ok(projectDtos);
        }
    }
}