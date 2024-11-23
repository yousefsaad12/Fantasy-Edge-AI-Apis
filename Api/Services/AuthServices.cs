

namespace Api.Services
{
    public class AuthServices : IAuthServices
    {   
        private readonly UserManager<User> _userManager;
        private readonly SignInManager <User> _signInManager;

        private readonly IUnitOfWork _unitOfWork;
        public AuthServices(UserManager<User> userManager, SignInManager<User> signInManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CheckExist(string email)
        {
            return await _userManager.FindByEmailAsync(email).ConfigureAwait(false) is not null;
        }


        public async Task<User> Login(LoginReq loginReq, CancellationToken  cancellationToken)
        {
            User user = await _unitOfWork.Users.GetByEmail(loginReq.email, cancellationToken).ConfigureAwait(false);

            if (user is null) return null;

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginReq.passWord, false).ConfigureAwait(false);

            if(!result.Succeeded) return null;

            return user;
        }

        public async Task<IdentityResult> Register(UserRequest userRequest)
        {
            User user = userRequest.ToUser();

            return await _userManager.CreateAsync(user, userRequest.password).ConfigureAwait(false);
        }
    }
}