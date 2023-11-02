using ChampagneApi.Models.Composition;
using ChampagneApi.Models;

namespace ChampagneApi.Services
{
    public interface ICompositionService
    {
        Task<MainResponse> GetAllCompositions();
        Task<MainResponse> GetCompositionById(int id);
        Task<MainResponse> GetChampagneInfoByCompositionId(int compositionId);
        Task<MainResponse> GetGrapeVarietyInfoByCompositionId(int compositionId);
        Task<MainResponse> AddComposition(AddCompositionModel addPriceModel);
        Task<MainResponse> UpdateComposition(UpdateCompositionModel updateCompositionModel);
        Task<MainResponse> DeleteComposition(int id);
    }
}
