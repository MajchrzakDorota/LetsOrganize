using LetsOrganize.Models;

namespace LetsOrganize.Interfaces
{
    public interface IAccountService
    {
        Task<bool> RegisterUser(UserDto userDto);
        Task<string> GenerateJwtToken(LoginDto dto);
    }
}
