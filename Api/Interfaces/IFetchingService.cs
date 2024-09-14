
namespace Api.Interfaces
{
    public interface IFetchingService
    {
        public Task <ICollection<TeamsJsonForm>> FetchDataAsync(string url);
        public Task<ICollection<PlayerStatAndPerJson>> FetchPerformAsync(string url);
    }
}