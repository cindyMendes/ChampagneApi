using ChampagneApi.Models;
using ChampagneApi.Models.Price;
using ChampagneApi.Models.Size;
using ChampagneApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Runtime.InteropServices;

namespace ChampagneApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceController : ControllerBase
    {
        private readonly IPriceService _priceService;

        public PriceController(IPriceService priceService)
        {
            _priceService = priceService;
        }



        [HttpGet("GetAllPrices")]
        public async Task<IActionResult> GetAllPrices()
        {
            try
            {
                var response = await _priceService.GetAllPrices();
                return Ok(response);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetPriceById/{id}")]
        public async Task<IActionResult> GetPriceById(int id)
        {
            try
            {
                var response = await _priceService.GetPriceById(id);
                return Ok(response);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetChampagneInfoByPriceId/{priceId}")]
        public async Task<IActionResult> GetChampagneInfoByPriceId(int priceId)
        {
            try
            {
                var response = await _priceService.GetChampagneInfoByPriceId(priceId);
                return Ok(response);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetSizeInfoByPriceId/{priceId}")]
        public async Task<IActionResult> GetSizeInfoByPriceId(int priceId)
        {
            try
            {
                var response = await _priceService.GetSizeInfoByPriceId(priceId);
                return Ok(response);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddPrice")]
        public async Task<IActionResult> AddPrice([FromBody] AddPriceModel priceModel)
        {
            try
            {
                var response = await _priceService.AddPrice(priceModel);
                return Ok(response);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdatePrice")]
        public async Task<IActionResult> UpdateSize([FromBody] UpdatePriceModel updatePriceModel)
        {
            try
            {
                if (updatePriceModel.Id > 0)
                {
                    var response = await _priceService.UpdatePrice(updatePriceModel);
                    return Ok(response); 
                }
                else
                {
                    return BadRequest("Please enter a correct Id");
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeletePrice/{id}")]
        public async Task<IActionResult> DeletePrice(int id)
        {
            try
            {
                if (id > 0)
                {
                    var response = await _priceService.DeletePrice(id);
                    return Ok(response);
                }
                else
                {
                    return BadRequest("Please enter a correct Id");
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        //[HttpDelete("DeletePrice")]
        //public async Task<IActionResult> DeletePrice([FromBody] DeletePriceModel deletePriceModel)
        //{
        //    try
        //    {
        //        if (deletePriceModel.Id > 0)
        //        {
        //            var response = await _priceService.DeletePrice(deletePriceModel);
        //            return Ok(response);
        //        }
        //        else
        //        {
        //            return BadRequest("Please enter a correct Id");
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        return BadRequest(ex.Message);
        //    }
        //}




    }
}
