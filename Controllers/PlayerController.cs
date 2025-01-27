
using Microsoft.AspNetCore.Authorization;

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
        [Route("data")]
        public async Task<IActionResult> GetPlayersData()
        {   
            //var response = _cacheServices.GetData< IEnumerable<PlayerDataForTrain>>("PlayerTrain");

            //if(response is not null && response.Count() > 0) return Ok(response);

            IEnumerable<Player> player = await _playerService.GetPlayersAsync().ConfigureAwait(false);
            IEnumerable<PlayerDataForTrain> playerInfo =  player.SelectMany(p => p.MapToPlayerDataForTrain());

            if(playerInfo is null) return NotFound(new { message = "Players not fetched." });

            //_cacheServices.SetData("PlayerTrain", playerInfo);

            return Ok(playerInfo);
        }

        [HttpGet]
        [Route("info")]
        public async Task<IActionResult> GetPlayersInfo()
        {   
            var response = _cacheServices.GetData< IEnumerable<PlayerInfo>>("PlayerInformation");

            if(response is not null && response.Count() > 0) return Ok(response);

            IEnumerable<Player> player = await _playerService.GetPlayersInfo().ConfigureAwait(false);
            IEnumerable<PlayerInfo> playerInfos =  player.Select(p => p.MapToPlayerInfo());

            if(playerInfos is null) return NotFound(new { message = "Players not fetched." });

            _cacheServices.SetData("PlayerInformation", playerInfos);

            return Ok(playerInfos);
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
        [Authorize]
        [Route("predict")]

        public async Task<IActionResult> GetPlayerPrediction([FromBody] PlayerNameRequest playerPredictionReq)
        {
            PlayerPredictionsResponse ? playerPredictions = await _playerService.GetPredictionFromModel(playerPredictionReq).ConfigureAwait(false);
            Console.WriteLine(playerPredictionReq);
            if (playerPredictions == null) return BadRequest("Player with this name is not found");

            return Ok(playerPredictions);
        }

    }
}