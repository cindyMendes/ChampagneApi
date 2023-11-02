using ChampagneApi.Context;
using ChampagneApi.Data;
using ChampagneApi.Models;
using ChampagneApi.Models.Composition;
using ChampagneApi.Models.Price;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace ChampagneApi.Services
{
    public class PriceService : IPriceService
    {
        private readonly ChampagneDbContext _dbContext;

        public PriceService(ChampagneDbContext dbContext)
        {
            _dbContext = dbContext;
        }



        public async Task<MainResponse> GetAllPrices()
        {
            try
            {
                List<Price> prices = await _dbContext.Prices.ToListAsync();

                if (prices.Count == 0)
                {
                    return new MainResponse { Message = "No data found" };
                }
                else
                {
                    return new MainResponse { Content = prices, Message = "Prices retrieved successfully" };
                }
            }
            catch (Exception ex)
            {
                return new MainResponse { IsSuccess = false, Message = $"Error: {ex.Message}" };
            }
        }

        public async Task<MainResponse> GetPriceById(int id)
        {
            try
            {
                var price = await _dbContext.Prices.Where(p => p.Id == id).FirstOrDefaultAsync(); 

                if (price != null)
                {
                    return new MainResponse { Content = price, Message = "Price retrieved successfully" };
                }
                else
                {
                    return new MainResponse { IsSuccess = false, Message = "No Price with this Id" };
                }
            }
            catch (Exception ex)
            {
                return new MainResponse { IsSuccess = false, Message = $"Error: {ex.Message}" };
            }
        }

        public async Task<MainResponse> GetChampagneInfoByPriceId(int priceId)
        {
            try
            {
                var price = await _dbContext.Prices
                    .Where(p => p.Id == priceId)
                    .Select(p => p.Champagne) // Using the navigation property for Champagne
                    .FirstOrDefaultAsync();

                if (price != null)
                {
                    return new MainResponse { Content = price, Message = "Champagne information retrieved successfully" };
                }
                else
                {
                    return new MainResponse { IsSuccess = false, Message = "No Price with this Id" };
                }
            }
            catch (Exception ex)
            {
                return new MainResponse { IsSuccess = false, Message = $"Error: {ex.Message}" };
            }
        }

        public async Task<MainResponse> GetSizeInfoByPriceId(int priceId)
        {
            try
            {
                var price = await _dbContext.Prices
                    .Where(p => p.Id == priceId)
                    .Select(p => p.Size) // Using the navigation property for Size
                    .FirstOrDefaultAsync();

                if (price != null)
                {
                    return new MainResponse { Content = price, Message = "Size information retrieved successfully" };
                }
                else
                {
                    return new MainResponse { IsSuccess = false, Message = "No Price with this Id" };
                }
            }
            catch (Exception ex)
            {
                return new MainResponse { IsSuccess = false, Message = $"Error: {ex.Message}" };
            }
        }

        public async Task<MainResponse> AddPrice(AddPriceModel addPriceModel)
        {
            try
            {
                var priceExists = await _dbContext.Prices.AnyAsync(p => p.ChampagneId == addPriceModel.ChampagneId) &&
                                      await _dbContext.Prices.AnyAsync(p => p.SizeId == addPriceModel.SizeId) &&
                                      await _dbContext.Prices.AnyAsync(p => p.Currency == addPriceModel.Currency);

                if (priceExists)
                {
                    return new MainResponse { IsSuccess = false, Message = "Price already exists with this champagne, size and currency" };
                }
                else
                {
                    await _dbContext.AddAsync(new Price
                    {
                        ChampagneId = addPriceModel.ChampagneId,
                        SizeId = addPriceModel.SizeId,
                        SellingPrice = addPriceModel.SellingPrice,
                        Currency = addPriceModel.Currency,
                    });

                    await _dbContext.SaveChangesAsync();

                    return new MainResponse { Message = "Price added successfully" };
                }
            }
            catch (Exception ex)
            {
                var champagneExists = await _dbContext.Champagnes.AnyAsync(c => c.Id == addPriceModel.ChampagneId);
                var sizeExists = await _dbContext.Sizes.AnyAsync(s => s.Id == addPriceModel.SizeId);

                if (!champagneExists)
                {
                    return new MainResponse { IsSuccess = false, Message = "Chamapagne does not exist" };
                }
                else if (!sizeExists)
                {
                    return new MainResponse { IsSuccess = false, Message = "Size does not exist" };
                }

                return new MainResponse { IsSuccess = false, Message = $"Error: {ex.Message}" };
            }
        }

        public async Task<MainResponse> UpdatePrice(UpdatePriceModel updatePriceModel)
        {
            try
            {
                var existingPrice = await _dbContext.Prices.Where(p => p.Id == updatePriceModel.Id).FirstOrDefaultAsync();

                if (existingPrice != null)
                {
                    existingPrice.ChampagneId = updatePriceModel.ChampagneId;
                    existingPrice.SizeId = updatePriceModel.SizeId;
                    existingPrice.SellingPrice = updatePriceModel.SellingPrice;
                    existingPrice.Currency = updatePriceModel.Currency;

                    await _dbContext.SaveChangesAsync();

                    return new MainResponse { Message = "Price updated successfully" };
                }
                else
                {
                    return new MainResponse { IsSuccess = false, Message = "Price not found with this Id" };
                }
            }
            catch (Exception ex)
            {
                var champagneExists = await _dbContext.Champagnes.AnyAsync(c => c.Id == updatePriceModel.ChampagneId);
                var sizeExists = await _dbContext.Sizes.AnyAsync(s => s.Id == updatePriceModel.SizeId);

                if (!champagneExists)
                {
                    return new MainResponse { IsSuccess = false, Message = "Chamapagne does not exist" };
                }
                else if (!sizeExists)
                {
                    return new MainResponse { IsSuccess = false, Message = "Size does not exist" };
                }

                return new MainResponse { IsSuccess = false, Message = $"Error: {ex.Message}" };
            }
        }

        public async Task<MainResponse> DeletePrice(int id)
        {
            try
            {
                var existingPrice = await _dbContext.Prices.Where(p => p.Id == id).FirstOrDefaultAsync();

                if (existingPrice != null)
                {
                    _dbContext.Remove(existingPrice);
                    await _dbContext.SaveChangesAsync();

                    return new MainResponse { Message = "Price deleted successfully" };
                }
                else
                {
                    return new MainResponse { IsSuccess = false, Message = "Price not found with this Id" };
                }
            }
            catch (Exception ex)
            {
                return new MainResponse { IsSuccess = false, Message = $"Error: {ex.Message}" };
            }
        }

        //public async Task<MainResponse> DeletePrice(DeletePriceModel deletePriceModel)
        //{
        //    try
        //    {
        //        var existingPrice = await _dbContext.Prices.Where(p => p.Id == deletePriceModel.Id).FirstOrDefaultAsync();

        //        if (existingPrice != null)
        //        {
        //            _dbContext.Remove(existingPrice);
        //            await _dbContext.SaveChangesAsync();

        //            return new MainResponse { Message = "Price deleted successfully" };
        //        }
        //        else
        //        {
        //            return new MainResponse { IsSuccess = false, Message = "Price not found with this Id" };
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new MainResponse { IsSuccess = false, Message = $"Error: {ex.Message}" };
        //    }
        //}



    }
}
