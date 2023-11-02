using ChampagneApi.Models;
using ChampagneApi.Models.Champagne;
using ChampagneApi.Models.GrapeVariety;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace ChampagneApi.Services
{
    public interface IChampagneService
    {
        Task<MainResponse> GetAllChampagnes();
        Task<MainResponse> GetChampagneById(int id);
        Task<MainResponse> GetChampagneByName(string name);
        Task<MainResponse> GetSizesByChampagneId(int champagneId);
        Task<MainResponse> GetGrapeVarietiesByChampagneId(int champagneId);
        Task<MainResponse> GetChampagnesByAlcoholLevel(float threshold, string sign);
        Task<MainResponse> AddChampagne(AddChampagneModel addChampagneModel);
        Task<MainResponse> UpdateChampagne(UpdateChampagneModel updateChampagneModel);
        Task<MainResponse> DeleteChampagneWithPricesAndCompositions(int champagneId);
        Task<MainResponse> DeleteChampagneIfNoPricesAndCompositions(int champagneId);
        //Task<MainResponse> DeleteChampagne(DeleteChampagneModel deleteChampagneModel);
    }
}
