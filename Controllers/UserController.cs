using AutoMapper;
using EcoLudicoAPI.DTOS;
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
    }
}
