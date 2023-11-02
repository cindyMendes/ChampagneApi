using ChampagneApi.Context;
using ChampagneApi.Data;
using ChampagneApi.Models;
using ChampagneApi.Models.Composition;
using ChampagneApi.Models.Price;
using Microsoft.EntityFrameworkCore;

namespace ChampagneApi.Services
{
    public class CompositionService : ICompositionService
    {
        private readonly ChampagneDbContext _dbContext;

        public CompositionService(ChampagneDbContext dbContext)
        {
            _dbContext = dbContext;
        }



        public async Task<MainResponse> GetAllCompositions()
        {
            try
            {
                List<Composition> compositions = await _dbContext.Compositions.ToListAsync();

                if (compositions.Count == 0)
                {
                    return new MainResponse { Message = "No data found" };
                }
                else
                {
                    return new MainResponse { Content = compositions, Message = "Compositions retrieved successfully" };
                }
            }
            catch (Exception ex)
            {
                return new MainResponse { IsSuccess = false, Message = $"Error: {ex.Message}" };
            }
        }

        public async Task<MainResponse> GetCompositionById(int id)
        {
            try
            {
                var composition = await _dbContext.Compositions.Where(c => c.Id == id).FirstOrDefaultAsync();

                if (composition != null)
                {
                    return new MainResponse { Content = composition, Message = "Composition retrieved successfully" };
                }
                else
                {
                    return new MainResponse { IsSuccess = false, Message = "No Composition with this Id" };
                }
            }
            catch (Exception ex)
            {
                return new MainResponse { IsSuccess = false, Message = $"Error: {ex.Message}" };
            }
        }

        public async Task<MainResponse> GetChampagneInfoByCompositionId(int compositionId)
        {
            try
            {
                var composition = await _dbContext.Compositions
                    .Where(c => c.Id == compositionId)
                    .Select(c => c.Champagne) // Using the navigation property for Champagne
                    .FirstOrDefaultAsync();

                if (composition != null)
                {
                    return new MainResponse { Content = composition, Message = "Champagne information retrieved successfully" };
                }
                else
                {
                    return new MainResponse { IsSuccess = false, Message = "No Composition with this Id" };
                }
            }
            catch (Exception ex)
            {
                return new MainResponse { IsSuccess = false, Message = $"Error: {ex.Message}" };
            }
        }

        public async Task<MainResponse> GetGrapeVarietyInfoByCompositionId(int compositionId)
        {
            try
            {
                var composition = await _dbContext.Compositions
                    .Where(c => c.Id == compositionId)
                    .Select(c => c.GrapeVariety) // Using the navigation property for Champagne
                    .FirstOrDefaultAsync();

                if (composition != null)
                {
                    return new MainResponse { Content = composition, Message = "Grape Variety information retrieved successfully" };
                }
                else
                {
                    return new MainResponse { IsSuccess = false, Message = "No Composition with this Id" };
                }
            }
            catch (Exception ex)
            {
                return new MainResponse { IsSuccess = false, Message = $"Error: {ex.Message}" };
            }
        }

        public async Task<MainResponse> AddComposition(AddCompositionModel addCompositionModel)
        {
            try
            {
                var compositionExists = await _dbContext.Compositions.AnyAsync(p => p.ChampagneId == addCompositionModel.ChampagneId) &&
                                 await _dbContext.Compositions.AnyAsync(p => p.GrapeVarietyId == addCompositionModel.GrapeVarietyId);

                if (compositionExists)
                {
                    return new MainResponse { IsSuccess = false, Message = "Composition already exists with this champagne and grape variety" };
                }
                else
                {
                    await _dbContext.AddAsync(new Composition
                    {
                        ChampagneId = addCompositionModel.ChampagneId,
                        GrapeVarietyId = addCompositionModel.GrapeVarietyId,
                        Percentage = addCompositionModel.Percentage,
                    });
                    await _dbContext.SaveChangesAsync();

                    return new MainResponse { Message = "Composition added successfully" };
                }
            }
            catch (Exception ex)
            {
                var champagneExists = await _dbContext.Champagnes.AnyAsync(c => c.Id == addCompositionModel.ChampagneId);
                var grapeVarietyExists = await _dbContext.GrapeVarieties.AnyAsync(g => g.Id == addCompositionModel.GrapeVarietyId);

                if (!champagneExists)
                {
                    return new MainResponse { IsSuccess = false, Message = "Champagne does not exist" };
                }
                else if (!grapeVarietyExists)
                {
                    return new MainResponse { IsSuccess = false, Message = "Grape Variety does not exist" };
                }

                return new MainResponse { IsSuccess = false, Message = $"Error: {ex.Message}" };
            }
        }

        public async Task<MainResponse> UpdateComposition(UpdateCompositionModel updateCompositionModel)
        {
            try
            {
                var existingComposition = await _dbContext.Compositions.Where(c => c.Id == updateCompositionModel.Id).FirstOrDefaultAsync();

                if (existingComposition != null)
                {
                    existingComposition.ChampagneId = updateCompositionModel.ChampagneId;
                    existingComposition.GrapeVarietyId = updateCompositionModel.GrapeVarietyId;
                    existingComposition.Percentage = updateCompositionModel.Percentage;

                    await _dbContext.SaveChangesAsync();

                    return new MainResponse { Message = "Composition updated successfully" };
                }
                else
                {
                    return new MainResponse { IsSuccess = false, Message = "Composition not found with this Id" };
                }
            }
            catch (Exception ex)
            {
                var champagneExists = await _dbContext.Champagnes.AnyAsync(c => c.Id == updateCompositionModel.ChampagneId);
                var grapeVarietyExists = await _dbContext.GrapeVarieties.AnyAsync(g => g.Id == updateCompositionModel.GrapeVarietyId);
                
                if (!champagneExists)
                {
                    return new MainResponse { IsSuccess = false, Message = "Champagne does not exist" };
                }
                else if (!grapeVarietyExists)
                {
                    return new MainResponse { IsSuccess = false, Message = "Grape Variety does not exist" };
                }

                return new MainResponse { IsSuccess = false, Message = $"Error: {ex.Message}" };
            }
        }

        public async Task<MainResponse> DeleteComposition(int id)
        {
            try
            {
                var existingComposition = await _dbContext.Compositions.Where(c => c.Id == id).FirstOrDefaultAsync();

                if (existingComposition != null)
                {
                    _dbContext.Remove(existingComposition);
                    await _dbContext.SaveChangesAsync();

                    return new MainResponse { Message = "Composition deleted successfully" };
                }
                else
                {
                    return new MainResponse { IsSuccess = false, Message = "Composition not found with this Id" };
                }
            }
            catch (Exception ex)
            {
                return new MainResponse { IsSuccess = false, Message = $"Error: {ex.Message}" };
            }
        }



    }
}
