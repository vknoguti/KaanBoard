using Microsoft.IdentityModel.Tokens;

namespace KaanBoard.Services
{
    public interface ICacheService 
    {
        Task<TData> GetAsync<TData>(string key);

        Task<TData> SetAsync<TData>(string key, TData data, TimeSpan? expiration = null);
        Task<TData> RemoveAsync<TData>(string key);
    }
}
