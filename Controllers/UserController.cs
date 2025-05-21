using AutoMapper;
using EcoLudico.DTOS;
using EcoLudicoAPI.DTOS;
using EcoLudicoAPI.Enums;
using EcoLudicoAPI.MappingProfiles;
using EcoLudicoAPI.Models;
using EcoLudicoAPI.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcoLudicoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public UserController(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register([FromBody] UserRegisterDTO userRegisterDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (userRegisterDTO.Type == UserType.Professor)
            {
                if (userRegisterDTO.School == null)
                    return BadRequest("Professores devem cadastrar uma escola.");

                var school = _mapper.Map<School>(userRegisterDTO.School);
                var createdSchool = _uof.SchoolRepository.Create(school);

                var user = _mapper.Map<User>(userRegisterDTO);
                user.SchoolId = createdSchool.SchoolId;
                user.Password = userRegisterDTO.Password;

                var newUser = _uof.UserRepository.Create(user);
                await _uof.CommitAsync();

                var newUserDTO = _mapper.Map<UserDTO>(newUser);
                return CreatedAtAction(nameof(GetById), new { id = newUser.UserId }, newUserDTO);
            }


            if (userRegisterDTO.Type == UserType.Doador && userRegisterDTO.School != null)
                return BadRequest("Doadores não podem cadastrar escola.");

            var userDoador = _mapper.Map<User>(userRegisterDTO);
            userDoador.Password = userRegisterDTO.Password;

            var createdUserDoador = _uof.UserRepository.Create(userDoador);
            await _uof.CommitAsync();

            var doadorDTO = _mapper.Map<UserDTO>(createdUserDoador);
            return CreatedAtAction(nameof(GetById), new { id = createdUserDoador.UserId }, doadorDTO);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            if (loginRequestDTO is null)
            {
                return BadRequest("O login não pode ser realizado com dados nulos.");
            }

            var verifyUser = await _uof.UserRepository.GetUserByEmailAsync(loginRequestDTO.Email);

            if (verifyUser == null)
            {
                return Unauthorized("Usuário não encontrado, faça o seu cadastro para realizar o login.");
            }

            if (verifyUser.Password != loginRequestDTO.Password)
            {
                return Unauthorized("Senha incorreta.");
            }

            var userDTO = _mapper.Map<UserDTO>(verifyUser);

            return Ok(userDTO);
        }

        // ---------------------------------------------------------------------------------------------------------------
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAll()
        {
            var users = await _uof.UserRepository.GetAllUsersWithDetailsAsync();

            var userDTOs = _mapper.Map<IEnumerable<UserDTO>>(users);

            return Ok(userDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetById(int id)
        {
            var user = await _uof.UserRepository.GetByIdAsync(id);

            if (user == null)
                return NotFound($"Usuário com ID {id} não encontrado.");

            var userDTO = _mapper.Map<UserDTO>(user);

            return Ok(userDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserUpdateDTO userUpdateDTO)
        {
            var user = await _uof.UserRepository.GetByIdAsync(id);

            if (user == null)
                return NotFound($"O usuário de id = {id} não existe !");

            _mapper.Map(userUpdateDTO, user); 
            _uof.UserRepository.Update(user);
            await _uof.CommitAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _uof.UserRepository.GetByIdAsync(id);

            if (user == null)
                return NotFound($"Usuário com ID {id} não encontrado.");

            _uof.UserRepository.Delete(user);
            await _uof.CommitAsync();


            return NoContent();
        }

        // ---------------------------------------------------------------------------------------------------------------

        [HttpGet("{id}/favorite-schools")]
        public async Task<IActionResult> GetFavoriteSchools(int id)
        {
            var user = await _uof.UserRepository.GetByIdWithFavoriteSchoolsAsync(id);
            if (user == null) return NotFound("Usuário não encontrado.");

            var favoriteSchools = user.FavoriteSchools
                .Select(school => new FavoriteSchoolResponseDTO
                {
                    SchoolId = school.SchoolId,
                    Name = school.Name,
                    OperatingHours = school.OperatingHours,
                    Contact = school.Contact,
                    Address = new AddressDTO
                    {
                        Street = school.Address.Street,
                        Number = school.Address.Number,
                        City = school.Address.City,
                        State = school.Address.State,
                        Complement = school.Address.Complement,
                        Latitude = school.Address.Latitude,
                        Longitude = school.Address.Longitude,
                    }
                }).ToList();

            return Ok(favoriteSchools);
        }

        [HttpPost("{id}/favorite-schools/{idSchool}")]
        public async Task<IActionResult> AddFavoriteSchools(int id, int idSchool)
        {
            var user = await _uof.UserRepository.GetByIdAsync(id);
            if (user == null) return NotFound("Usuário não encontrado");

            var school = await _uof.SchoolRepository.GetSchoolsByIdsAsync(idSchool);
            if (school == null) return NotFound("Escola não encontrada");

            if (user.FavoriteSchools.Any(s => s.SchoolId == idSchool))
                return BadRequest("Escola já está nos favoritos");

            user.FavoriteSchools.Add(school); 
            _uof.UserRepository.Update(user);
            await _uof.CommitAsync();

            return NoContent();
        }

        [HttpDelete("{id}/favorite-schools/{schoolId}")]
        public async Task<IActionResult> RemoveFavoriteSchool(int id, int schoolId)
        {
            var user = await _uof.UserRepository.GetByIdWithFavoriteSchoolsAsync(id);
            if (user == null) return NotFound("Usuário não encontrado.");

            var school = user.FavoriteSchools.FirstOrDefault(school => school.SchoolId == schoolId);
            if (school == null) return NotFound("Escola não encontrada nos favoritos.");

            user.FavoriteSchools.Remove(school);

            _uof.UserRepository.Update(user);
            await _uof.CommitAsync();

            return NoContent(); 
        }

        // ---------------------------------------------------------------------------------------------------------------

        [HttpGet("{id}/favorite-projects")]
        public async Task<IActionResult> GetFavoriteProjects(int id)
        {
            var user = await _uof.UserRepository.GetByIdWithFavoriteProjectsAsync(id);
            if (user == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            if (user.FavoriteProjects == null || !user.FavoriteProjects.Any())
            {
                return Ok(new List<FavoriteProjectResponseDTO>()); 
            }

            var favoriteProjectsDTO = user.FavoriteProjects
                .Where(fp => fp.Projeto != null) 
                .Select(fp => new FavoriteProjectResponseDTO
                {
                    ProjectId = fp.Projeto.ProjectId,
                    Name = fp.Projeto.Name,
                    Description = fp.Projeto.Description,
                    ImageUrls = fp.Projeto.ImageUrls != null ? fp.Projeto.ImageUrls.Select(img => img.Url).ToList() : new List<string>(),
                    AgeRange = fp.Projeto.AgeRange.ToString() 
                })
                .ToList();

            return Ok(favoriteProjectsDTO);
        }

        [HttpPost("{id}/favorite-projects/{idProject}")]
        public async Task<IActionResult> AddFavoriteProjects(int id, int idProject)
        {
            var user = await _uof.UserRepository.GetByIdWithFavoriteProjectsAsync(id); 
            if (user == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            var project = await _uof.ProjectRepository.GetByIdAsync(idProject);
            if (project == null)
            {
                return NotFound("Projeto não encontrado.");
            }

            if (user.FavoriteProjects != null && user.FavoriteProjects.Any(fp => fp.ProjectId == idProject))
            {
                return Conflict("Projeto já está nos favoritos."); 
            }

            if (user.FavoriteProjects == null)
            {
                user.FavoriteProjects = new List<FavoriteProject>();
            }

            user.FavoriteProjects.Add(new FavoriteProject
            {
                UserId = user.UserId,
                ProjectId = project.ProjectId
            });

            _uof.UserRepository.Update(user);
            await _uof.CommitAsync();

            return NoContent(); 
        }

        [HttpDelete("{id}/favorite-projects/{projectId}")]
        public async Task<IActionResult> RemoveFavoriteProject(int id, int projectId)
        {
            var user = await _uof.UserRepository.GetByIdWithFavoriteProjectsAsync(id);
            if (user == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            if (user.FavoriteProjects == null)
            {
                return NotFound("Projeto não está na lista de favoritos do usuário."); 
            }

            var favoriteProjectToRemove = user.FavoriteProjects
                .FirstOrDefault(fp => fp.ProjectId == projectId);

            if (favoriteProjectToRemove == null)
            {
                return NotFound("Projeto não está na lista de favoritos do usuário.");
            }

            user.FavoriteProjects.Remove(favoriteProjectToRemove);

            _uof.UserRepository.Update(user);
            await _uof.CommitAsync();

            return NoContent();
        }

        // ---------------------------------------------------------------------------------------------------------------

        [HttpGet("{id}/comments")]
        public async Task<IActionResult> GetUserComments(int id)
        {
            var user = await _uof.UserRepository.GetByIdAsync(id);
            if (user == null)
                return NotFound("Usuário não encontrado.");

            var comments = user.MadeComments; 

            var commentDtos = _mapper.Map<List<CommentResponseDTO>>(comments);

            return Ok(commentDtos);
        }

        [HttpPost("{id}/comments/{projectId}")]
        public async Task<IActionResult> AddCommentToAProject(int id, int projectId, [FromBody] CommentCreateDTO commentDto)
        {
            var user = await _uof.UserRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            var project = await _uof.ProjectRepository.GetByIdAsync(projectId);
            if (project == null)
            {
                return NotFound("Projeto não encontrado.");
            }

            var comment = _mapper.Map<Comment>(commentDto);
            comment.UserId = id;
            comment.ProjectId = projectId;
            comment.CreationDate = DateTime.UtcNow;

            _uof.CommentRepository.Create(comment);
            await _uof.CommitAsync();

            var createdCommentWithUser = await _uof.CommentRepository.GetByIdAsync(comment.CommentId);
            if (createdCommentWithUser == null)
            {
                return StatusCode(500, "Erro ao recuperar o comentário recém-criado. Tente buscar o projeto novamente para ver o comentário.");
            }

            var responseDto = _mapper.Map<CommentResponseDTO>(createdCommentWithUser);

            return Ok(responseDto);
        }

        [HttpPut("{id}/comments/{commentId}")]
        public async Task<IActionResult> UpdateComment(int id, int commentId, [FromBody] CommentUpdateDTO commentDto)
        {
            if (commentId != commentDto.CommentId)
            {
                return BadRequest("ID do comentário na URL não corresponde ao ID no corpo da requisição.");
            }

            var user = await _uof.UserRepository.GetByIdAsync(id); 
            if (user == null)
            {
                return NotFound("Usuário que tenta editar não encontrado.");
            }

            var commentToUpdate = await _uof.CommentRepository.GetByIdAsync(commentId);
            if (commentToUpdate == null)
            {
                return NotFound("Comentário não encontrado.");
            }

            if (commentToUpdate.UserId != id)
            {
                return Forbid("Você não tem permissão para editar este comentário. Somente o autor do comentário pode editá-lo.");
            }

            _mapper.Map(commentDto, commentToUpdate); 
            _uof.CommentRepository.Update(commentToUpdate);
            await _uof.CommitAsync();

            return NoContent();
        }

        [HttpDelete("{id}/comments/{commentId}")]
        public async Task<IActionResult> DeleteComment(int id, int commentId)
        {
            var user = await _uof.UserRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound("Usuário que tenta excluir não encontrado.");
            }

            var commentToDelete = await _uof.CommentRepository.GetCommentByIdWithProjectAndSchoolInfoAsync(commentId);
            if (commentToDelete == null)
            {
                return NotFound("Comentário não encontrado.");
            }

            if (commentToDelete.Project == null)
            {
                return StatusCode(500, "Erro interno: Projeto associado ao comentário não foi carregado. Verifique o método GetCommentByIdWithProjectAndSchoolInfoAsync.");
            }

            int? projectOwnerUserId = null;
            if (commentToDelete.Project.School != null && commentToDelete.Project.School.Teachers != null && commentToDelete.Project.School.Teachers.Any())
            {
                projectOwnerUserId = commentToDelete.Project.School.Teachers
                                            .FirstOrDefault(t => t.Type == UserType.Professor)?.UserId;
            }

            if (commentToDelete.UserId == id || projectOwnerUserId == id)
            {
                _uof.CommentRepository.Delete(commentToDelete);
                await _uof.CommitAsync();
                return NoContent();
            }
            else
            {
                return Forbid("Você não tem permissão para excluir este comentário. Somente o autor do comentário ou o dono do projeto podem excluí-lo.");
            }
        }
    }
}
