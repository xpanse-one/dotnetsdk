using xpanse.sdk.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace xpanse.sdk
{
    public interface ITransfer
    {
        List<TransferData> Create(NewTransferGroup newTransfer);
        Task<List<TransferData>> CreateAsync(NewTransferGroup newTransfer);
        TransferData Single(string transferId);
        Task<TransferData> SingleAsync(string transferId);
        TransferList Search(TransferSearch searchData);
        Task<TransferList> SearchAsync(TransferSearch searchData);
    }
}