
namespace Api.Interfaces
{
    public interface IFetchingService
    {
        public Task <ICollection<TeamsJsonForm>> FetchDataAsync(string url);
    }
}