using System.Threading.Tasks;
using xpanse.sdk.Models;

namespace xpanse.sdk
{
    public interface IProvider
    {
        Models.Provider Create(NewProvider newProvider);
        Task<Models.Provider> CreateAsync(NewProvider newProvider);
        Models.Provider Update(string providerId, UpdateProvider updateProvider);
        Task<Models.Provider> UpdateAsync(string providerId, UpdateProvider updateProvider);
        Models.Provider Delete(string providerId);
        Task<Models.Provider> DeleteAsync(string providerId);
    }
}
