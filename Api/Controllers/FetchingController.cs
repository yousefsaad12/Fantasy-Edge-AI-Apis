
namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FetchingController : ControllerBase
    {   
        private readonly IFetchingService _fetchingService;
        public FetchingController(IFetchingService fetchingService)
        {
            _fetchingService = fetchingService;   
        }

        [HttpPost]
        [Route("fetch")]
        public async Task<IActionResult> FetchingData([FromBody] FetchingRequest fetchingRequest)
        {
            string response = await _fetchingService.FetchDataAsync(fetchingRequest.gameWeek);

            return Ok(response);
        }
    }
}