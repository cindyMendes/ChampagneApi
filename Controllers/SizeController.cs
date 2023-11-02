using ChampagneApi.Models;
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
    public class SizeController : ControllerBase
    {
        private readonly ISizeService _sizeService;

        public SizeController(ISizeService sizeService)
        {
            _sizeService = sizeService;
        }



        [HttpGet("GetAllSizes")]
        public async Task<IActionResult> GetAllSizes()
        {
            try
            {
                var response = await _sizeService.GetAllSizes();
                return Ok(response);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetSizeById/{id}")]
        public async Task<IActionResult> GetSizeById(int id)
        {
            try
            {
                var response = await _sizeService.GetSizeById(id);
                return Ok(response);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllChampagnesBySizeId/{sizeId}")]
        public async Task<IActionResult> GetAllChampagnesBySizeId(int sizeId)
        {
            try
            {
                var response = await _sizeService.GetAllChampagnesBySizeId(sizeId);
                return Ok(response);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddSize")]
        public async Task<IActionResult> AddSize([FromBody] AddSizeModel sizeModel)
        {
            try
            {
                var response = await _sizeService.AddSize(sizeModel);
                return Ok(response);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateSize")]
        public async Task<IActionResult> UpdateSize([FromBody] UpdateSizeModel updateSizeModel)
        {
            try
            {
                if (updateSizeModel.Id > 0)
                {
                    var response = await _sizeService.UpdateSize(updateSizeModel);
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

        [HttpDelete("DeleteSizeWithPrices/{sizeId}")]
        public async Task<IActionResult> DeleteSizeWithPrices(int sizeId)
        {
            try
            {
                if (sizeId > 0)
                {
                    var response = await _sizeService.DeleteSizeWithPrices(sizeId);
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

        [HttpDelete("DeleteSizeIfNoPrices/{sizeId}")]
        public async Task<IActionResult> DeleteSizeIfNoPrices(int sizeId)
        {
            try
            {
                if (sizeId > 0)
                {
                    var response = await _sizeService.DeleteSizeIfNoPrices(sizeId);
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

        //[HttpDelete("DeleteSize")]
        //public async Task<IActionResult> DeleteSize([FromBody] DeleteSizeModel deleteSizeModel)
        //{
        //    try
        //    {
        //        if (deleteSizeModel.Id > 0)
        //        {
        //            var response = await _sizeService.DeleteSize(deleteSizeModel);
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
