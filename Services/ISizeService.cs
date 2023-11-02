using ChampagneApi.Models;
using ChampagneApi.Models.Size;

namespace ChampagneApi.Services
{
    public interface ISizeService
    {
        Task<MainResponse> GetAllSizes();
        Task<MainResponse> GetSizeById(int id);
        Task<MainResponse> GetAllChampagnesBySizeId(int sizeId);
        Task<MainResponse> AddSize(AddSizeModel addSizeModel);
        Task<MainResponse> UpdateSize(UpdateSizeModel updateSizeModel);
        Task<MainResponse> DeleteSizeWithPrices(int sizeId);
        Task<MainResponse> DeleteSizeIfNoPrices(int sizeId);
        //Task<MainResponse> DeleteSize(DeleteSizeModel deleteSizeModel);
    }
}
