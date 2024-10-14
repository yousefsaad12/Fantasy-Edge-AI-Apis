




namespace Api.Interfaces
{
    public interface IAuthServices
    {
        public Task<IdentityResult> Register(UserRequest user);

        public Task<User?>Login(LoginReq loginReq, CancellationToken  cancellationToken);
        public Task<bool> CheckExist(string email);

    }
}