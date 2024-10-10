
namespace Api.Services
{
    public class AuthServices : IAuthServices
    {   
        private readonly UserManager<User> _userManager;
        private readonly SignInManager <User> _signInManager;
        public AuthServices(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> CheckExist(string email)
        {
            return await _userManager.FindByEmailAsync(email).ConfigureAwait(false) is not null;
        }

        public Task<string> Login(string Email, string Password)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> Register(UserRequest userRequest)
        {
            User user = userRequest.ToUser();

            return await _userManager.CreateAsync(user, userRequest.passWord).ConfigureAwait(false);
        }
    }
}