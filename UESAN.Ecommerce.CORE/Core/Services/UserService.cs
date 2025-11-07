using UESAN.Ecommerce.CORE.Core.DTOs;
using UESAN.Ecommerce.CORE.Core.Entities;
using UESAN.Ecommerce.CORE.Core.Interfaces;

namespace UESAN.Ecommerce.CORE.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJWTService _jwtService;

        public UserService(IUserRepository userRepository, IJWTService jWTService)
        {
            _userRepository = userRepository;
            _jwtService = jWTService;
        }

        public async Task<UserDTO?> SignIn(string email, string password)
        {
            var user = await _userRepository.SignIn(email, password);
            var token = _jwtService.GenerateJWToken(user); // Aquí deberías generar el token JWT según tu lógica de autenticación
            if (user == null) return null;
            return new UserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName ?? string.Empty,
                LastName = user.LastName ?? string.Empty,
                Email = user.Email ?? string.Empty,
                Token = token
            };
        }

        public async Task<int> SignUp(UserCreateDTO userCreateDTO)
        {
            var user = new User
            {
                FirstName = userCreateDTO.FirstName,
                LastName = userCreateDTO.LastName,
                DateOfBirth = userCreateDTO.DateOfBirth,
                Country = userCreateDTO.Country,
                Address = userCreateDTO.Address,
                Email = userCreateDTO.Email,
                Password = userCreateDTO.Password,
                Type = userCreateDTO.Type
            };
            return await _userRepository.SignUp(user);
        }
    }
}
