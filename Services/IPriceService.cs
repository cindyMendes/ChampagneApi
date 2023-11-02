using ChampagneApi.Models;
using ChampagneApi.Models.Price;
using ChampagneApi.Models.Size;

namespace ChampagneApi.Services
{
    public interface IPriceService
    {
        Task<MainResponse> GetAllPrices();
        Task<MainResponse> GetPriceById(int id);
        Task<MainResponse> GetChampagneInfoByPriceId(int priceId);
        Task<MainResponse> GetSizeInfoByPriceId(int priceId);
        Task<MainResponse> AddPrice(AddPriceModel addPriceModel);
        Task<MainResponse> UpdatePrice(UpdatePriceModel updatePriceModel);
        Task<MainResponse> DeletePrice(int id);
        //Task<MainResponse> DeletePrice(DeletePriceModel deletePriceModel);
    }
}
