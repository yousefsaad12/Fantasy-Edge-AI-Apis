
namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerController : ControllerBase
    {   

        private readonly IPlayerServices _playerService;
        private readonly ILogger <PlayerController> _logger;
        public PlayerController(IPlayerServices playerService, ILogger <PlayerController> logger)
        {   
            _playerService = playerService;
            _logger = logger;
        }

        [HttpGet]
        [Route("playersData/train")]

        public async Task<IActionResult> GetPlayersData()
        {
            var players = await _playerService.GetPlayersAsync();

            var DataToTrain = players.Select(p => p.MapToPlayerDataForTrain());

            return Ok(DataToTrain);
        }
    }
}