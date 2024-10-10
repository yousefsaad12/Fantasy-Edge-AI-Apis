




namespace Api.Interfaces
{
    public interface IAuthServices
    {
        public Task<IdentityResult> Register(UserRequest user);
        public Task<string>Login (string Email, string Password);

        public Task<bool> CheckExist(string email);

    }
}