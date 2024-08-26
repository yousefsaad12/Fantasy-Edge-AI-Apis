
namespace Api.Interfaces
{
    public interface IFetchingService
    {
        public Task<FantasyForm> FetchDataAsync(string url);
    }
}