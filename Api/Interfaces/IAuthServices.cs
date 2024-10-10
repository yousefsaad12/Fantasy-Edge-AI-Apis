
using Api.Models.UserModel;

namespace Api.Interfaces
{
    public interface IAuthServices
    {
        public Task<string> Register(User user);
        public Task<string>Login (string Email, string Password);

    }
}