
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

        [HttpPost]
        [Route("predict")]

        public async Task<IActionResult> GetPlayerPrediction([FromBody] PlayerNameRequest playerPredictionReq)
        {
            PlayerPredictionsResponse ? playerPredictions = await _playerService.GetPrediction(playerPredictionReq).ConfigureAwait(false);

            if(playerPredictions is null) BadRequest("Player with this name is not found");

            return Ok(playerPredictions);
        }
    }
}