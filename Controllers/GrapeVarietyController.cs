using ChampagneApi.Models;
using ChampagneApi.Models.GrapeVariety;
using ChampagneApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChampagneApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrapeVarietyController : ControllerBase
    {
        private readonly IGrapeVarietyService _grapeVarietyService;

        public GrapeVarietyController(IGrapeVarietyService grapeVarietyService)
        {
            _grapeVarietyService = grapeVarietyService;
        }



        [HttpGet("GetAllGrapeVarieties")]
        public async Task<IActionResult> GetAllSizes()
        {
            try
            {
                var response = await _grapeVarietyService.GetAllGrapeVarieties();
                return Ok(response);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetGrapeVarietyById/{id}")]
        public async Task<IActionResult> GetGrapeVarietyById(int id)
        {
            try
            {
                var response = await _grapeVarietyService.GetGrapeVarietyById(id);
                return Ok(response);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddGrapeVariety")]
        public async Task<IActionResult> AddGrapeVariety([FromBody] AddGrapeVarietyModel addGrapeVarietyModel)
        {
            try
            {
                var response = await _grapeVarietyService.AddGrapeVariety(addGrapeVarietyModel);
                return Ok(response);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateGrapeVariety")]
        public async Task<IActionResult> UpdateGrapeVariety([FromBody] UpdateGrapeVarietyModel updateSizeModel)
        {
            try
            {
                if (updateSizeModel.Id > 0)
                {
                    var response = await _grapeVarietyService.UpdateGrapeVariety(updateSizeModel);
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

        [HttpDelete("DeleteGrapeVariety/{id}")]
        public async Task<IActionResult> DeleteGrapeVariety(int id)
        {
            try
            {
                if (id > 0)
                {
                    var response = await _grapeVarietyService.DeleteGrapeVariety(id);
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

        //[HttpDelete("DeleteGrapeVariety")]
        //public async Task<IActionResult> DeleteGrapeVariety([FromBody] DeleteGrapeVarietyModel deleteSizeModel)
        //{
        //    try
        //    {
        //        if (deleteSizeModel.Id > 0)
        //        {
        //            var response = await _grapeVarietyService.DeleteGrapeVariety(deleteSizeModel);
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
