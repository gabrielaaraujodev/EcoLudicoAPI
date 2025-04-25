using AutoMapper;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAll()
        {
            var users = await _uof.UserRepository.GetAllAsync(u => u.Address);

            var userDTOs = _mapper.Map<IEnumerable<UserDTO>>(users);

            return Ok(userDTOs);
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetById(int id)
        {
            var user = await _uof.UserRepository.GetByIdAsync(id, include => include.Address);

            if (user == null)
                return NotFound($"Usuário com ID {id} não encontrado.");

            var userDTO = _mapper.Map<UserDTO>(user);

            return Ok(userDTO);
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

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register([FromBody] UserRegisterDTO userRegisterDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (userRegisterDTO.Type == UserType.Professor)
            {
                if (userRegisterDTO.SchoolId == null)
                    return BadRequest("Professores devem estar vinculados a uma escola.");
            }
            else
            {
                userRegisterDTO.SchoolId = null; 
            }


            var existingUser = await _uof.UserRepository.GetUserByEmailAsync(userRegisterDTO.Email);

            if (existingUser != null)
                return Conflict("Este e-mail já está em uso.");

            var user = _mapper.Map<User>(userRegisterDTO);

            user.Password = userRegisterDTO.Password;

            var newUser = _uof.UserRepository.Create(user);
            await _uof.CommitAsync();
            
            var newUserDTO = _mapper.Map<UserDTO>(newUser);

            return CreatedAtAction(nameof(GetById), new { id = newUser.UserId }, newUserDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBasic(int id, [FromBody] UserUpdateDTO userUpdateDTO)
        {
            if (id != userUpdateDTO.UserId)
                return BadRequest("Os IDs não coincidem !");

            var user = await _uof.UserRepository.GetByIdAsync(id);
            if (user == null) return NotFound();

            _mapper.Map(userUpdateDTO, user);
            _uof.UserRepository.Update(user);
            await _uof.CommitAsync();

            return NoContent();
        }

        [HttpPut("{id}/favorite-schools")]
        public async Task<IActionResult> UpdateFavoriteSchools(int id, [FromBody] List<int> schoolIds)
        {
            var user = await _uof.UserRepository.GetByIdWithFavoriteSchoolsAsync(id);
            if (user == null) return NotFound();

            var schools = await _uof.SchoolRepository.GetSchoolsByIdsAsync(schoolIds);

            user.FavoriteSchools = schools;
            _uof.UserRepository.Update(user);
            await _uof.CommitAsync();

            return NoContent();
        }

        [HttpPut("{id}/favorite-projects")]
        public async Task<IActionResult> UpdateFavoriteProjects(int id, [FromBody] List<int> projectIds)
        {
            var user = await _uof.UserRepository.GetByIdWithFavoriteProjectsAsync(id);
            if (user == null) return NotFound("Usuário não encontrado.");

            var projects = await _uof.FavoriteProjectRepository.GetByIdsAsync(projectIds);

            user.FavoriteProjects = projects
                .Select(project => new FavoriteProject
                {
                    UserId = user.UserId,
                    ProjectId = project.ProjectId
                })
                .ToList();

            _uof.UserRepository.Update(user);
            await _uof.CommitAsync();

            return NoContent();
        }

        [HttpPut("{id}/comments")]
        public async Task<IActionResult> UpdateUserComments(int id, [FromBody] List<CommentDTO> updatedComments)
        {
            var user = await _uof.CommentRepository.GetByIdWithCommentsAsync(id);
            if (user == null) return NotFound("Usuário não encontrado.");

            foreach (var commentDto in updatedComments)
            {
                var comment = user.MadeComments.FirstOrDefault(c => c.CommentId == commentDto.CommentId);
                if (comment != null)
                {
                    comment.Content = commentDto.Content;
                }
            }

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
    }
}
