
namespace Api.Interfaces
{
    public interface ICacheServices
    {
        T?GetData<T> (string key);
        void SetData<T>(string key, T data);
    }
}