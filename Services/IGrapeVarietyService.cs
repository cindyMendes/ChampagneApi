using ChampagneApi.Models;
using ChampagneApi.Models.GrapeVariety;

namespace ChampagneApi.Services
{
    public interface IGrapeVarietyService
    {
        Task<MainResponse> GetAllGrapeVarieties();
        Task<MainResponse> GetGrapeVarietyById(int id);
        Task<MainResponse> AddGrapeVariety(AddGrapeVarietyModel addGrapeVarietyModel);
        Task<MainResponse> UpdateGrapeVariety(UpdateGrapeVarietyModel updateGrapeVarietyModel);
        Task<MainResponse> DeleteGrapeVariety(int id);
        //Task<MainResponse> DeleteGrapeVariety(DeleteGrapeVarietyModel deleteGrapeVarietyModel);
    }
}
