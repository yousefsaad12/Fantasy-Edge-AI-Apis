
namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerController : ControllerBase
    {   

        private readonly IPlayerServices _playerService;
        private readonly ICacheServices _cacheServices;
        private readonly ILogger <PlayerController> _logger;
        public PlayerController(IPlayerServices playerService, ICacheServices cacheServices,ILogger <PlayerController> logger)
        {   
            _playerService = playerService;
            _logger = logger;
            _cacheServices = cacheServices;
        }
        
        [HttpGet]
        [Route("names")]

        public async Task<IActionResult> GetPlayersNames()
        {   
            var response = _cacheServices.GetData< IEnumerable<PlayerSearchResponse>>("PlayerNames");

            if(response is not null && response.Count() > 0) return Ok(response);

            IEnumerable<PlayerSearchResponse> playerSearchResponses = await _playerService.GetPlayerNames().ConfigureAwait(false);

            if(playerSearchResponses is null) return NotFound(new { message = "Players not fetched." });

            _cacheServices.SetData("PlayerNames", playerSearchResponses);

            return Ok(playerSearchResponses);
        }

        [HttpGet]
        [Route("GetplayerbyName")]

        public async Task<IActionResult> GetPlayersData([FromBody] PlayerSearchReq playerSearchReq)
        {
            var player = await _playerService.GetPlayerbyName(playerSearchReq.firstName, playerSearchReq.secondName).ConfigureAwait(false);

            if(player is null) return BadRequest("Player with this name is not found");

            return Ok(player);
        }

        [HttpPost]
        [Route("predict")]

        public async Task<IActionResult> GetPlayerPrediction([FromBody] PlayerNameRequest playerPredictionReq)
        {
            PlayerPredictionsResponse ? playerPredictions = await _playerService.GetPredictionFromModel(playerPredictionReq).ConfigureAwait(false);

            if (playerPredictions == null) return BadRequest("Player with this name is not found");

            return Ok(playerPredictions);
        }

    }
}