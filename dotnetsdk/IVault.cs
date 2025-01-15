using System.Threading.Tasks;
using xpanse.sdk.Models;

namespace xpanse.sdk
{
    public interface IVault
    {
        VaultData Create(NewVault newVault);
        Task<VaultData> CreateAsync(NewVault newVault);
        VaultData Delete(string vaultId);
        Task<VaultData> DeleteAsync(string vaultId);
        VaultDataWithPCI Single(string vaultId);
        Task<VaultDataWithPCI> SingleAsync(string vaultId);
    }
}