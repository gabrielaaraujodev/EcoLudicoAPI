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
        private readonly IWebHostEnvironment _env;

        public ProjectController(IUnitOfWork uof, IMapper mapper, IWebHostEnvironment env)
        {
            _uof = uof;
            _mapper = mapper;
            _env = env;
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

        [HttpPost("upload-project-picture")]
        public async Task<ActionResult<ProjectDTO>> CreateProjectWithImage([FromForm] ProjectCreateWithImageDTO dto, [FromQuery] int userId)
        {
            try
            {
                var user = await _uof.UserRepository.GetByIdAsync(userId);

                if (user == null || user.Type != UserType.Professor)
                    return BadRequest("Apenas professores podem criar projetos ou professor não encontrado.");

                if (user.SchoolId == null || user.SchoolId == 0)
                    return BadRequest("Professor não possui escola vinculada.");

                if (dto.File == null || dto.File.Length == 0)
                    return BadRequest("Nenhuma imagem enviada para o projeto.");

                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
                var fileExtension = Path.GetExtension(dto.File.FileName).ToLowerInvariant();
                if (!allowedExtensions.Contains(fileExtension))
                    return BadRequest("Tipo de arquivo inválido.");

                var uploadsFolder = Path.Combine(_env.WebRootPath, "ProjectPictures");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.File.CopyToAsync(fileStream);
                }

                var relativePath = $"/ProjectPictures/{uniqueFileName}";
                var project = _mapper.Map<Project>(dto);
                project.ImageUrls.Add(new ImageUrl { Url = relativePath });
                project.SchoolId = user.SchoolId.Value;

                _uof.ProjectRepository.Create(project);
                await _uof.CommitAsync();

                var result = _mapper.Map<ProjectDTO>(project);
                return CreatedAtAction(nameof(GetProjectById), new { id = result.ProjectId }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Erro interno no servidor", details = ex.Message });
            }
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

        [HttpPut("{id}/images")]
        public async Task<IActionResult> UpdateProjectImages(int id, [FromQuery] int userId, [FromForm] ProjectUpdateImagesDTO dto)
        {
            var user = await _uof.UserRepository.GetByIdAsync(userId);
            if (user == null || user.Type != UserType.Professor)
                return Unauthorized("Usuário não autorizado.");

            var project = await _uof.ProjectRepository.GetByIdAsync(id);
            if (project == null)
                return NotFound("Projeto não encontrado.");

            if (project.SchoolId != user.SchoolId)
                return Forbid("Este projeto não pertence à sua escola.");

            if (dto.KeepImageUrls != null)
            {
                var normalizedKeepImageUrls = dto.KeepImageUrls?
                    .Select(url => url.Replace("\\", "/").TrimStart('/'))
                    .ToList() ?? new List<string>();

                var toRemove = project.ImageUrls
                    .Where(img =>
                    {
                        var imgNormalized = img.Url.Replace("\\", "/").TrimStart('/');
                        return !normalizedKeepImageUrls.Any(k => string.Equals(k, imgNormalized, StringComparison.OrdinalIgnoreCase));
                    })
                    .ToList();


                foreach (var img in toRemove)
                {
                    var fullPath = Path.Combine(_env.WebRootPath, img.Url.TrimStart('/'));
                    if (System.IO.File.Exists(fullPath))
                        System.IO.File.Delete(fullPath);

                    project.ImageUrls.Remove(img);
                }
            }

            if (dto.NewFiles != null && dto.NewFiles.Any())
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
                var uploadsFolder = Path.Combine(_env.WebRootPath, "ProjectPictures");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                foreach (var file in dto.NewFiles)
                {
                    var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
                    if (!allowedExtensions.Contains(fileExtension))
                        return BadRequest("Tipo de arquivo inválido.");

                    var uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using var fileStream = new FileStream(filePath, FileMode.Create);
                    await file.CopyToAsync(fileStream);

                    var relativePath = $"/ProjectPictures/{uniqueFileName}";
                    project.ImageUrls.Add(new ImageUrl { Url = relativePath });
                }
            }

            _uof.ProjectRepository.Update(project);
            await _uof.CommitAsync();

            var result = _mapper.Map<ProjectDTO>(project);
            return Ok(result);
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