
namespace Api.Interfaces
{
    public interface IFetchingService
    {
        public Task<PlayerJsonForm> FetchDataAsync(string url);
    }
}