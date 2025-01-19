
namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ModelController : ControllerBase
    {   
        private readonly IModelServices _modelService;
        public ModelController(IModelServices modelServices)
        {
            _modelService = modelServices;
        }

        [HttpGet]
        [Route("retrain")]
        public async Task<IActionResult> RetrainModel()
        {
            ModelResponse modelResponse = await _modelService.RetrainModel().ConfigureAwait(false);

            return Ok(modelResponse);
        }
    }
}