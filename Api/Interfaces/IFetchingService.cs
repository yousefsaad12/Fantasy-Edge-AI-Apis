
namespace Api.Interfaces
{
    public interface IFetchingService
    {
        public Task <ICollection<TeamsJsonForm>> FetchDataAsync(int _currentWeek);
        public Task<ICollection<PlayerStatAndPerJson>> FetchPerformAsync(int _currentWeek);
    }
}