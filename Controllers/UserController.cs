
namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {   
        private readonly IAuthServices _authServices;
        private readonly ITokenServices _tokenService;

        private readonly IUnitOfWork _unitOfWork;
        public UserController(IAuthServices authServices, ITokenServices tokenServices, IUnitOfWork unitOfWork)
        {
            _authServices = authServices;
            _tokenService = tokenServices;
            _unitOfWork = unitOfWork;
        }

        [Route("register")]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRequest userRequest)
        {
            try
            {
                    // Validate the incoming request
                if (!ModelState.IsValid)return BadRequest(new { Message = "Invalid data", Errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });
                
                if (await _authServices.CheckExist(userRequest.email).ConfigureAwait(false)) return BadRequest(new { Message = "This email is already in use" });
                    
                var results = await _authServices.Register(userRequest).ConfigureAwait(false);

                if (results.Succeeded)
                {
                        // Create and return the token
                    var token = _tokenService.CreateToken(userRequest.ToUser());
                    return Ok(new UserResponse
                    {
                            userName = userRequest.userName,
                            email = userRequest.email,
                            token = token
                    });
                }

                    
                return StatusCode(500, new
                {
                    Message = "Registration failed",
                    Errors = results.Errors
                });
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error during registration: {ex.Message}");

                return StatusCode(500, new { Message = "An unexpected error occurred", Details = ex.Message });
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginReq request, CancellationToken cancellationToken)
        {
            if(!ModelState.IsValid) return BadRequest(request);

            var response = await _authServices.Login(request, cancellationToken).ConfigureAwait(false);

            if(response is null) return Unauthorized("Invalid Email or Password");

            UserResponse userResponse = response.ToUserResponse();

            userResponse.token = _tokenService.CreateToken(response);
            userResponse.Message = "Login Successfull!";

            return Ok(userResponse);
        }


        [HttpGet]
        [Route("users")]
        public async Task<IActionResult> AllUsers()
        {

            var allUsers = await _unitOfWork.Users.GetAll().ConfigureAwait(false);

            return Ok(allUsers.Select(u => u.ToUserResponse()));
        }

    }
}