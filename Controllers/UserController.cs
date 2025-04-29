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

            // Criação do usuário doador
            var userDoador = _mapper.Map<User>(userRegisterDTO);
            userDoador.Password = userRegisterDTO.Password;

            var createdUserDoador = _uof.UserRepository.Create(userDoador);
            await _uof.CommitAsync();

            var doadorDTO = _mapper.Map<UserDTO>(createdUserDoador);
            return CreatedAtAction(nameof(GetById), new { id = createdUserDoador.UserId }, doadorDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBasic(int id, [FromBody] UserUpdateDTO userUpdateDTO)
        {
            var user = await _uof.UserRepository.GetByIdAsync(id);
            if (user == null) return NotFound($"O usuário de id = {id} não existe !");

            _mapper.Map(userUpdateDTO, user); // Atualiza os dados com base no DTO
            _uof.UserRepository.Update(user);
            await _uof.CommitAsync();

            return NoContent();
        }

        [HttpPost("{idUser}/favorite-schools/{idSchool}")]
        public async Task<IActionResult> UpdateFavoriteSchools(int idUser, int idSchool)
        {
            var user = await _uof.UserRepository.GetByIdAsync(idUser);
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
