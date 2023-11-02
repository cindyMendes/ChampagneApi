using ChampagneApi.Context;
using ChampagneApi.Data;
using ChampagneApi.Models;
using ChampagneApi.Models.Champagne;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net.NetworkInformation;

namespace ChampagneApi.Services
{
    public class ChampagneService : IChampagneService
    {
        private readonly ChampagneDbContext _dbContext;

        public ChampagneService(ChampagneDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<MainResponse> GetAllChampagnes()
        {
            try
            {
                List<Champagne> champagnes = await _dbContext.Champagnes.ToListAsync();

                if (champagnes.Count == 0)
                {
                    return new MainResponse { Message = "No data found" };
                }
                else
                {
                    return new MainResponse { Content = champagnes, Message = "Champagnes retrieved successfully" };
                }
            }
            catch (Exception ex)
            {
                return new MainResponse { IsSuccess = false, Message = $"Error: {ex.Message}" };
            }
        }

        public async Task<MainResponse> GetChampagneById(int id)
        {   
            try
            {
                var champagne = await _dbContext.Champagnes.Where(c => c.Id == id).FirstOrDefaultAsync();

                if (champagne != null)
                {
                    return new MainResponse { Content = champagne, Message = "Champagne retrieved successfully" };
                }
                else
                {
                    return new MainResponse { IsSuccess = false, Message = "No champagne with this Id" };
                }
            }
            catch (Exception ex)
            {
                return new MainResponse { IsSuccess = false, Message = $"Error: {ex.Message}" };
            }
        }

        public async Task<MainResponse> GetChampagneByName(string name)
        {
            try
            {
                var champagne = await _dbContext.Champagnes.Where(c => c.Name == name).FirstOrDefaultAsync();

                if (champagne != null)
                {
                    return new MainResponse { Content = champagne, Message = "Champagne retrieved successfully" };
                }
                else
                {
                    return new MainResponse { IsSuccess = false, Message = "No champagne with this name" };
                }
            }
            catch (Exception ex)
            {
                return new MainResponse { IsSuccess = false, Message = $"Error: {ex.Message}" };
            }
        }

        public async Task<MainResponse> GetSizesByChampagneId(int champagneId)
        {
            try
            {
                // Query the Price entity and filter by ChampagneId
                var sizes = await _dbContext.Prices
                    .Where(p => p.ChampagneId == champagneId)
                    .Select(p => p.Size)
                    .ToListAsync();

                if (sizes.Count > 0)
                {
                    return new MainResponse { Content = sizes, Message = "All sizes for this champagne retrieved successfully" };
                }
                else
                {
                    return new MainResponse { IsSuccess = false, Message = "No sizes found with this champagne Id" };
                }
            }
            catch (Exception ex)
            {
                return new MainResponse { IsSuccess = false, Message = $"Error: {ex.Message}" };
            }
        }

        public async Task<MainResponse> GetGrapeVarietiesByChampagneId(int champagneId)
        {
            try
            {
                // Query the Composition entity and filter by ChampagneId
                var compositions = await _dbContext.Compositions
                    .Where(c => c.ChampagneId == champagneId)
                    .Select(c => c.GrapeVariety)
                    .ToListAsync();

                if (compositions.Count > 0)
                {
                    return new MainResponse { Content = compositions, Message = "All grape varieties for this champagne retrieved successfully" };
                }
                else
                {
                    return new MainResponse { IsSuccess = false, Message = "No grape varieties found with this champagne Id" };
                }
            }
            catch (Exception ex)
            {
                return new MainResponse { IsSuccess = false, Message = $"Error: {ex.Message}" };
            }
        }

        public async Task<MainResponse> GetChampagnesByAlcoholLevel(float threshold, string sign)
        {
            try
            {
                var champagnes = new List<Champagne>();

                switch (sign)
                {
                    case "<":
                        champagnes = await _dbContext.Champagnes
                        .Where(champagne => champagne.AlcoholLevel < threshold)
                        .ToListAsync();
                        break;

                    case "<=":
                        champagnes = await _dbContext.Champagnes
                        .Where(champagne => champagne.AlcoholLevel <= threshold)
                        .ToListAsync();
                        break;

                    case ">":
                        champagnes = await _dbContext.Champagnes
                            .Where(champagne => champagne.AlcoholLevel > threshold)
                            .ToListAsync();
                        break;

                    case ">=":
                        champagnes = await _dbContext.Champagnes
                            .Where(champagne => champagne.AlcoholLevel >= threshold)
                            .ToListAsync();
                        break;

                    default:
                        champagnes = await _dbContext.Champagnes
                            .Where(champagne => champagne.AlcoholLevel == threshold)
                            .ToListAsync();
                        break;
                }

                if (champagnes.Count > 0)
                {
                    switch (sign)
                    {
                        case "<":
                            return new MainResponse { Content = champagnes, Message = "Champagnes with alcohol level less than the threshold retrieved successfully" };

                        case "<=":
                            return new MainResponse { Content = champagnes, Message = "Champagnes with alcohol level less than or equal to the threshold retrieved successfully" };

                        case ">":
                            return new MainResponse { Content = champagnes, Message = "Champagnes with alcohol level greater than the threshold retrieved successfully" };

                        case ">=":
                            return new MainResponse { Content = champagnes, Message = "Champagnes with alcohol level greater than or equal to the threshold retrieved successfully" };

                        default:
                            return new MainResponse { Content = champagnes, Message = "Champagnes with alcohol level equal to the threshold retrieved successfully" };
                    }
                }
                else
                {
                    return new MainResponse { IsSuccess = false, Message = "No champagnes found" };
                }
            }
            catch (Exception ex)
            {
                return new MainResponse { IsSuccess = false, Message = $"Error: {ex.Message}" };
            }
        }


        public async Task<MainResponse> AddChampagne(AddChampagneModel addChampagneModel)
        {
            try
            {
                var champagneExists = await _dbContext.Champagnes.AnyAsync(c => c.Name.ToLower() == addChampagneModel.Name.ToLower());

                if (champagneExists)
                {
                    return new MainResponse { IsSuccess = false, Message = "Champagne already exists with this name" };
                }
                else
                {
                    await _dbContext.AddAsync(new Champagne
                    {
                        Name = addChampagneModel.Name,
                        Description = addChampagneModel.Description,
                        AlcoholLevel = addChampagneModel.AlcoholLevel,
                        BottlingDate = addChampagneModel.BottlingDate,
                        Aging = CalculateAging(addChampagneModel.BottlingDate),
                        //SizeId = addChampagneModel.SizeId,
                    });

                    await _dbContext.SaveChangesAsync();

                    return new MainResponse { Message = "Champagne added successfully" };
                }
            }
            catch (Exception ex)
            {
                return new MainResponse { IsSuccess = false, Message = $"Error: {ex.Message}" };
            }
        }

        public async Task<MainResponse> UpdateChampagne(UpdateChampagneModel updateChampagneModel)
        {
            try
            {
                var existingSize = await _dbContext.Champagnes.Where(c => c.Id == updateChampagneModel.Id).FirstOrDefaultAsync();

                if (existingSize != null)
                {
                    existingSize.Name = updateChampagneModel.Name;
                    existingSize.Description = updateChampagneModel.Description;
                    existingSize.AlcoholLevel = updateChampagneModel.AlcoholLevel;
                    existingSize.BottlingDate = updateChampagneModel.BottlingDate;
                    existingSize.Aging = CalculateAging(updateChampagneModel.BottlingDate);
                    //existingSize.SizeId = updateChampagneModel.SizeId;
                    await _dbContext.SaveChangesAsync();

                    return new MainResponse { Message = "Champagne updated successfully" };
                }
                else
                {
                    return new MainResponse { IsSuccess = false, Message = "Champagne not found with this Id" };
                }
            }
            catch (Exception ex)
            {
                return new MainResponse { IsSuccess = false, Message = $"Error: {ex.Message}" };
            }
        }

        public async Task<MainResponse> DeleteChampagneWithPricesAndCompositions(int champagneId)
        { // Deletes Champagne and Prices and Composition associated 
            try
            {
                // Find all prices associated with the specified ChampagneId
                var pricesToDelete = await _dbContext.Prices
                    .Where(p => p.ChampagneId == champagneId)
                    .ToListAsync();

                // Find all compositions associated with the specified ChampagneId
                var compositionsToDelete = await _dbContext.Compositions
                    .Where(p => p.ChampagneId == champagneId)
                    .ToListAsync();

                if (pricesToDelete.Count > 0 && compositionsToDelete.Count > 0)
                {
                    // Delete each price
                    foreach (var price in pricesToDelete)
                    {
                        _dbContext.Prices.Remove(price);
                    }

                    // Delete each composition
                    foreach (var composition in compositionsToDelete)
                    {
                        _dbContext.Compositions.Remove(composition);
                    }

                    // Delete the champagne
                    var champagneToDelete = await _dbContext.Champagnes.FindAsync(champagneId);
                    if (champagneToDelete != null)
                    {
                        _dbContext.Champagnes.Remove(champagneToDelete);
                    }

                    // Save changes to the database
                    await _dbContext.SaveChangesAsync();

                    return new MainResponse { Message = "Champagne and associated prices and compositions deleted successfully" };
                }
                else
                {
                    // If no prices and no compositions are associated with the champagne, just delete the champagne
                    var champagneToDelete = await _dbContext.Champagnes.FindAsync(champagneId);
                    if (champagneToDelete != null)
                    {
                        _dbContext.Champagnes.Remove(champagneToDelete);
                        await _dbContext.SaveChangesAsync();
                        return new MainResponse { Message = "Champagne deleted successfully" };
                    }
                    else
                    {
                        return new MainResponse { IsSuccess = false, Message = "No champagne with this Id" };
                    }
                }
            }
            catch (Exception ex)
            {
                return new MainResponse { IsSuccess = false, Message = $"Error: {ex.Message}" };
            }
        }

        public async Task<MainResponse> DeleteChampagneIfNoPricesAndCompositions(int champagneId)
        {
            try
            {
                // Check if the champagne is used in the price table
                bool isChampagneUsedInPrice = _dbContext.Prices.Any(p => p.ChampagneId == champagneId);

                // Check if the champagne is used in the composition table
                bool isChampagneUsedInComposition = _dbContext.Compositions.Any(p => p.ChampagneId == champagneId);

                if (isChampagneUsedInPrice)
                {
                    return new MainResponse { IsSuccess = false, Message = "Champagne is used in Price and cannot be deleted" };
                }

                if (isChampagneUsedInComposition)
                {
                    return new MainResponse { IsSuccess = false, Message = "Champagne is used in Composition and cannot be deleted" };
                }

                var existingChampagne = await _dbContext.Champagnes.Where(c => c.Id == champagneId).FirstOrDefaultAsync();

                if (existingChampagne != null)
                {
                    _dbContext.Remove(existingChampagne);
                    await _dbContext.SaveChangesAsync();

                    return new MainResponse { Message = "Champagne deleted successfully" };
                }
                else
                {
                    return new MainResponse { IsSuccess = false, Message = "Champagne not found with this Id" };
                }
            }
            catch (Exception ex)
            {
                return new MainResponse { IsSuccess = false, Message = $"Error: {ex.Message}" };
            }
        }

        //public async Task<MainResponse> DeleteChampagne(DeleteChampagneModel deleteChampagneModel)
        //{
        //    try
        //    {
        //        var existingSize = await _dbContext.Champagnes.Where(c => c.Id == deleteChampagneModel.Id).FirstOrDefaultAsync();

        //        if (existingSize != null)
        //        {
        //            _dbContext.Remove(existingSize);
        //            await _dbContext.SaveChangesAsync();

        //            return new MainResponse { Message = "Champagne deleted successfully" };
        //        }
        //        else
        //        {
        //            return new MainResponse { IsSuccess = false, Message = "Champagne not found with this Id" };
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new MainResponse { IsSuccess = false, Message = $"Error: {ex.Message}" };
        //    }
        //}




        public string CalculateAging(DateTime bottlingDate)
        {
            TimeSpan agingPeriod = DateTime.Now - bottlingDate;

            // Calculate years and months assuming an average of 30.44 days per month
            int years = (int)(agingPeriod.Days / 365.25);
            int months = (int)((agingPeriod.Days % 365.25) / 30.44);

            Console.WriteLine($"Years: {years}, Months: {months}");

            //return agingPeriod.Days + " days"; 
            return $"Days: {agingPeriod.Days}, Months: {months}, Years: {years}";
        }


        
    }
}
