using ChampagneApi.Context;
using ChampagneApi.Data;
using ChampagneApi.Models;
using ChampagneApi.Models.Size;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace ChampagneApi.Services
{
    public class SizeService : ISizeService
    {
        private readonly ChampagneDbContext _dbContext;

        public SizeService(ChampagneDbContext dbContext)
        {
            _dbContext = dbContext;
        }



        public async Task<MainResponse> GetAllSizes()
        {
            try
            {
                List<Size> sizes = await _dbContext.Sizes.ToListAsync();

                if (sizes.Count == 0)
                {
                    return new MainResponse { Message = "No data found" };
                }
                else
                {
                    return new MainResponse { Content = sizes, Message = "Sizes retrieved successfully" };
                }
            }
            catch (Exception ex)
            {
                return new MainResponse { IsSuccess = false, Message = $"Error: {ex.Message}" };
            }
        }

        public async Task<MainResponse> GetSizeById(int id)
        {
            try
            {
                var size = await _dbContext.Sizes.Where(s => s.Id == id).FirstOrDefaultAsync();

                if (size != null)
                {
                    return new MainResponse { Content = size, Message = "Size retrieved successfully" };
                }
                else
                {
                    return new MainResponse { IsSuccess = false, Message = "No size with this Id" };
                }
            }
            catch (Exception ex)
            {
                return new MainResponse { IsSuccess = false, Message = $"Error: {ex.Message}" };
            }
        }

        public async Task<MainResponse> GetAllChampagnesBySizeId(int sizeId)
        {
            try
            {
                var champagnes = await _dbContext.Prices
                    .Where(price => price.SizeId == sizeId)
                    .Select(price => price.Champagne)
                    .ToListAsync();

                if (champagnes.Count > 0)
                {
                    return new MainResponse { Content = champagnes, Message = "All champagnes for this size retrieved successfully" };
                }
                else
                {
                    return new MainResponse { IsSuccess = false, Message = "No champagne found with this size Id" };
                }
            }
            catch (Exception ex)
            {
                return new MainResponse { IsSuccess = false, Message = $"Error: {ex.Message}" };
            }
        }

        public async Task<MainResponse> AddSize(AddSizeModel addSizeModel)
        {
            try
            {
                var sizeExists = await _dbContext.Sizes.AnyAsync(s => s.NameFR.ToLower() == addSizeModel.NameFR.ToLower()) ||
                                 await _dbContext.Sizes.AnyAsync(s => s.NameEN.ToLower() == addSizeModel.NameEN.ToLower());

                if (sizeExists)
                {
                    return new MainResponse { IsSuccess = false, Message = "Size already exists with this name" };
                }
                else
                {
                    await _dbContext.AddAsync(new Size
                    {
                        NameFR = addSizeModel.NameFR,
                        NameEN = addSizeModel.NameEN,
                        DescriptionFR = addSizeModel.DescriptionFR,
                        DescriptionEN = addSizeModel.DescriptionEN,
                    });

                    await _dbContext.SaveChangesAsync();

                    return new MainResponse { Message = "Size added successfully" };
                }
            }
            catch (Exception ex)
            {
                return new MainResponse { IsSuccess = false, Message = $"Error: {ex.Message}" };
            }
        }

        public async Task<MainResponse> UpdateSize(UpdateSizeModel updateSizeModel)
        {
            try
            {
                var existingSize = await _dbContext.Sizes.Where(s => s.Id == updateSizeModel.Id).FirstOrDefaultAsync();

                if (existingSize != null)
                {
                    existingSize.NameFR = updateSizeModel.NameFR;
                    existingSize.NameEN = updateSizeModel.NameEN;
                    existingSize.DescriptionFR = updateSizeModel.DescriptionFR;
                    existingSize.DescriptionEN = updateSizeModel.DescriptionEN;

                    await _dbContext.SaveChangesAsync();

                    return new MainResponse { Message = "Size updated successfully" };
                }
                else
                {
                    return new MainResponse { IsSuccess = false, Message = "Size not found with this Id" };
                }
            }
            catch (Exception ex)
            {
                return new MainResponse { IsSuccess = false, Message = $"Error: {ex.Message}" };
            }
        }

        public async Task<MainResponse> DeleteSize(DeleteSizeModel deleteSizeModel)
        {
            try
            {
                var existingSize = await _dbContext.Sizes.Where(s => s.Id == deleteSizeModel.Id).FirstOrDefaultAsync();

                if (existingSize != null)
                {
                    _dbContext.Remove(existingSize);
                    await _dbContext.SaveChangesAsync();

                    return new MainResponse { Message = "Record deleted" };
                }
                else
                {
                    return new MainResponse { IsSuccess = false, Message = "Size not found with this Id" };
                }
            }
            catch (Exception ex)
            {
                return new MainResponse { IsSuccess = false, Message = $"Error: {ex.Message}" };
            }
        }

        public async Task<MainResponse> DeleteSizeWithPrices(int sizeId)
        {
            try
            {
                // Find all prices associated with the specified SizeId
                var sizesToDelete = await _dbContext.Prices
                    .Where(p => p.SizeId == sizeId)
                    .ToListAsync();

                if (sizesToDelete.Count > 0)
                {
                    // Delete each price
                    foreach (var price in sizesToDelete)
                    {
                        _dbContext.Prices.Remove(price);
                    }

                    // Delete the size
                    var sizeToDelete = await _dbContext.Sizes.FindAsync(sizeId);
                    if (sizeToDelete != null)
                    {
                        _dbContext.Sizes.Remove(sizeToDelete);
                    }

                    // Save changes to the database
                    await _dbContext.SaveChangesAsync();

                    return new MainResponse { Message = "Size and associated prices deleted successfully" };
                }
                else
                {
                    // If no prices are associated with the size, just delete the champagne
                    var sizeToDelete = await _dbContext.Sizes.FindAsync(sizeId);
                    if (sizeToDelete != null)
                    {
                        _dbContext.Sizes.Remove(sizeToDelete);
                        await _dbContext.SaveChangesAsync();
                        return new MainResponse { Message = "Size deleted successfully" };
                    }
                    else
                    {
                        return new MainResponse { IsSuccess = false, Message = "No size with this id" };
                    }
                }
            }
            catch (Exception ex)
            {
                return new MainResponse { IsSuccess = false, Message = $"Error: {ex.Message}" };
            }
        }

        public async Task<MainResponse> DeleteSizeIfNoPrices(int sizeId)
        {
            try
            {
                // Check if the size is used in the "price" table
                bool isSizeUsedInPrice = _dbContext.Prices.Any(p => p.SizeId == sizeId);

                if (isSizeUsedInPrice)
                {
                    return new MainResponse { IsSuccess = false, Message = "Size is used in a price and cannot be deleted" };
                }

                var existingSize = await _dbContext.Sizes.Where(s => s.Id == sizeId).FirstOrDefaultAsync();

                if (existingSize != null)
                {
                    _dbContext.Remove(existingSize);
                    await _dbContext.SaveChangesAsync();

                    return new MainResponse { Message = "Record deleted" };
                }
                else
                {
                    return new MainResponse { IsSuccess = false, Message = "Size not found with this Id" };
                }
            }
            catch (Exception ex)
            {
                return new MainResponse { IsSuccess = false, Message = $"Error: {ex.Message}" };
            }
        }

        //public async Task<MainResponse> DeleteSize(DeleteSizeModel deleteSizeModel)
        //{
        //    try
        //    {
        //        var existingSize = await _dbContext.Sizes.Where(s => s.Id == deleteSizeModel.Id).FirstOrDefaultAsync();

        //        if (existingSize != null)
        //        {
        //            _dbContext.Remove(existingSize);
        //            await _dbContext.SaveChangesAsync();

        //            return new MainResponse { Message = "Record deleted" };
        //        }
        //        else
        //        {
        //            return new MainResponse { IsSuccess = false, Message = "Size not found with this Id" };
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new MainResponse { IsSuccess = false, Message = $"Error: {ex.Message}" };
        //    }
        //}


    }
}
