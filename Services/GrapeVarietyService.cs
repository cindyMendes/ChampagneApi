using ChampagneApi.Context;
using ChampagneApi.Data;
using ChampagneApi.Models;
using ChampagneApi.Models.GrapeVariety;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace ChampagneApi.Services
{
    public class GrapeVarietyService : IGrapeVarietyService
    {
        private readonly ChampagneDbContext _dbContext;

        public GrapeVarietyService(ChampagneDbContext dbContext)
        {
            _dbContext = dbContext;
        }



        public async Task<MainResponse> GetAllGrapeVarieties()
        {
            try
            {
                List<GrapeVariety> grapeVarieties = await _dbContext.GrapeVarieties.ToListAsync();

                if (grapeVarieties.Count == 0)
                {
                    return new MainResponse { Message = "No data found" };
                }
                else
                {
                    return new MainResponse { Content = grapeVarieties, Message = "Grape Varieties retrieved successfully" };
                }
            }
            catch (Exception ex)
            {
                return new MainResponse { IsSuccess = false, Message = $"Error: {ex.Message}" };
            }
        }

        public async Task<MainResponse> GetGrapeVarietyById(int id)
        {
            try
            {
                var grapeVariety = await _dbContext.GrapeVarieties.Where(g => g.Id == id).FirstOrDefaultAsync();

                if (grapeVariety != null)
                {
                    return new MainResponse { Content = grapeVariety, Message = "Grape Variety retrieved successfully" };
                }
                else
                {
                    return new MainResponse { IsSuccess = false, Message = "No Grape Variety with this Id" };
                }
            }
            catch (Exception ex)
            {
                return new MainResponse { IsSuccess = false, Message = $"Error: {ex.Message}" };
            }
        }

        public async Task<MainResponse> AddGrapeVariety(AddGrapeVarietyModel addGrapeVarietyModel)
        {
            try
            {
                var grapeVarietyExists = await _dbContext.GrapeVarieties.AnyAsync(g => g.Name.ToLower() == addGrapeVarietyModel.Name.ToLower());

                if (grapeVarietyExists)
                {
                    return new MainResponse { IsSuccess = false, Message = "Grape Variety already exists with this name" };
                }
                else
                {
                    await _dbContext.AddAsync(new GrapeVariety
                    {
                        Name = addGrapeVarietyModel.Name,
                    });

                    await _dbContext.SaveChangesAsync();

                    return new MainResponse { Message = "Grape Variety added successfully" };
                }
            }
            catch (Exception ex)
            {
                return new MainResponse { IsSuccess = false, Message = $"Error: {ex.Message}" };
            }
        }

        public async Task<MainResponse> UpdateGrapeVariety(UpdateGrapeVarietyModel updateGrapeVarietyModel)
        {
            try
            {
                var existingSize = await _dbContext.GrapeVarieties.Where(g => g.Id == updateGrapeVarietyModel.Id).FirstOrDefaultAsync();

                if (existingSize != null)
                {
                    existingSize.Name = updateGrapeVarietyModel.Name;

                    await _dbContext.SaveChangesAsync();

                    return new MainResponse { Message = "Grape Variety updated successfully" };
                }
                else
                {
                    return new MainResponse { IsSuccess = false, Message = "Grape Variety not found with this Id" };
                }
            }
            catch (Exception ex)
            {
                return new MainResponse { IsSuccess = false, Message = $"Error: {ex.Message}" };
            }
        }

        public async Task<MainResponse> DeleteGrapeVariety(int id) 
        { // Deletes grape variety if it's not being used in composition
            try
            {
                // Check if the grape variety is used in the "composition" table
                bool isGrapeVrietyUsedInComposition = await _dbContext.Compositions.AnyAsync(c => c.GrapeVarietyId == id);

                if (isGrapeVrietyUsedInComposition)
                {
                    return new MainResponse { IsSuccess = false, Message = "Grape Variety is used in Composition and cannot be deleted" };
                }

                var existingGrapeVariety = await _dbContext.GrapeVarieties.Where(c => c.Id == id).FirstOrDefaultAsync();

                if (existingGrapeVariety != null)
                {
                    _dbContext.Remove(existingGrapeVariety);
                    await _dbContext.SaveChangesAsync();

                    return new MainResponse { Message = "Grape Variety deleted successfully" };
                }
                else
                {
                    return new MainResponse { IsSuccess = false, Message = "Grape Variety not found with this Id" };
                }
            }
            catch (Exception ex)
            {
                return new MainResponse { IsSuccess = false, Message = $"Error: {ex.Message}" };
            }
        }

        //public async Task<MainResponse> DeleteGrapeVariety(DeleteGrapeVarietyModel deleteGrapeVarietyModel)
        //{
        //    try
        //    {
        //        var existingSize = await _dbContext.GrapeVarieties.Where(g => g.Id == deleteGrapeVarietyModel.Id).FirstOrDefaultAsync();

        //        if (existingSize != null)
        //        {
        //            _dbContext.Remove(existingSize);
        //            await _dbContext.SaveChangesAsync();

        //            return new MainResponse { Message = "Grape Variety deleted successfully" };
        //        }
        //        else
        //        {
        //            return new MainResponse { IsSuccess = false, Message = "Grape Variety not found with this Id" };
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new MainResponse { IsSuccess = false, Message = $"Error: {ex.Message}" };
        //    }
        //}


    }
}
