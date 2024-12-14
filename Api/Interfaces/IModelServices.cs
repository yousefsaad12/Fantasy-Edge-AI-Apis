
namespace Api.Interfaces
{
    public interface IModelServices
    {
        public Task<ModelResponse> RetrainModel();
    }
}