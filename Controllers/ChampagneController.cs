using ChampagneApi.Models.Champagne;
using ChampagneApi.Models.Size;
using ChampagneApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChampagneApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChampagneController : ControllerBase
    {
        private readonly IChampagneService _champagneService;

        public ChampagneController(IChampagneService champagneService)
        {
            _champagneService = champagneService;
        }


        [HttpGet("GetAllChampagnes")]
        public async Task<IActionResult> GetAllChampagnes()
        {
            try
            {
                var response = await _champagneService.GetAllChampagnes();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet("GetChampagneById/{id}")]
        public async Task<IActionResult> GetChampagneById(int id)
        {
            try
            {
                var response = await _champagneService.GetChampagneById(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetChampagneByName/{name}")]
        public async Task<IActionResult> GetChampagneByName(string name)
        {
            try
            {
                var response = await _champagneService.GetChampagneByName(name);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetSizesByChampagneId/{champagneId}")]
        public async Task<IActionResult> GetSizesByChampagneId(int champagneId)
        {
            try
            {
                var response = await _champagneService.GetSizesByChampagneId(champagneId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetGrapeVarietiesByChampagneId/{champagneId}")]
        public async Task<IActionResult> GetGrapeVarietiesByChampagneId(int champagneId)
        {
            try
            {
                var response = await _champagneService.GetGrapeVarietiesByChampagneId(champagneId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetChampagnesByAlcoholLevel")]
        public async Task<IActionResult> GetChampagnesByAlcoholLevel(float threshold, string sign)
        {
            try
            {
                var response = await _champagneService.GetChampagnesByAlcoholLevel(threshold, sign);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddChampagne")]
        public async Task<IActionResult> AddChampagne([FromBody] AddChampagneModel addChampagneModel)
        {
            try
            {
                var response = await _champagneService.AddChampagne(addChampagneModel);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateChampagne")]
        public async Task<IActionResult> UpdateChampagne([FromBody] UpdateChampagneModel updateChampagneModel)
        {
            try
            {
                if (updateChampagneModel.Id > 0)
                {
                    var response = await _champagneService.UpdateChampagne(updateChampagneModel);
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

        [HttpDelete("DeleteChampagneWithPricesAndCompositions/{champagneId}")]
        public async Task<IActionResult> DeleteChampagneWithPricesAndCompositions(int champagneId)
        {
            try
            {
                if (champagneId > 0)
                {
                    var response = await _champagneService.DeleteChampagneWithPricesAndCompositions(champagneId);
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

        [HttpDelete("DeleteChampagneIfNoPricesAndCompositions/{champagneId}")]
        public async Task<IActionResult> DeleteChampagneIfNoPricesAndCompositions(int champagneId)
        {
            try
            {
                if (champagneId > 0)
                {
                    var response = await _champagneService.DeleteChampagneIfNoPricesAndCompositions(champagneId);
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

        //[HttpDelete("DeleteChampagne")]
        //public async Task<IActionResult> DeleteChampagne(DeleteChampagneModel deleteChampagneModel)
        //{
        //    try
        //    {
        //        if (deleteChampagneModel.Id > 0)
        //        {
        //            var response = await _champagneService.DeleteChampagne(deleteChampagneModel);
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
