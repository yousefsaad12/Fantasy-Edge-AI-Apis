
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
        [Route("playersdata/train")]

        public async Task<IActionResult> GetPlayersData()
        {
            var players = await _playerService.GetPlayersAsync().ConfigureAwait(false);

            var DataToTrain = players.SelectMany(p => p.MapToPlayerDataForTrain()).ToList();

            return Ok(DataToTrain);
        }

        [HttpGet]
        [Route("GetplayerbyName")]

        public async Task<IActionResult> GetPlayersData([FromBody] PlayerSearchReq playerSearchReq)
        {
            var player = await _playerService.GetPlayerbyName(playerSearchReq.firstName, playerSearchReq.secondName).ConfigureAwait(false);

            Console.WriteLine(player);

            if(player is null) BadRequest("Player with this name is not found");

            return Ok(player);
        }
    }
}