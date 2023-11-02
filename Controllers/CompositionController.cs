using ChampagneApi.Data;
using ChampagneApi.Models.Composition;
using ChampagneApi.Models;
using ChampagneApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChampagneApi.Models.Price;

namespace ChampagneApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompositionController : ControllerBase
    {
        private readonly ICompositionService _compositionService;

        public CompositionController(ICompositionService compositionService)
        {
            _compositionService = compositionService;
        }



        [HttpGet("GetAllCompositions")]
        public async Task<IActionResult> GetAllCompositions()
        {
            try
            {
                var response = await _compositionService.GetAllCompositions();
                return Ok(response);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetCompositionById/{id}")]
        public async Task<IActionResult> GetCompositionById(int id)
        {
            try
            {
                var response = await _compositionService.GetCompositionById(id);
                return Ok(response);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetChampagneInfoByCompositionId/{compositionId}")]
        public async Task<IActionResult> GetChampagneInfoByCompositionId(int compositionId)
        {
            try
            {
                var response = await _compositionService.GetChampagneInfoByCompositionId(compositionId);
                return Ok(response);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetGrapeVarietyInfoByCompositionId/{compositionId}")]
        public async Task<IActionResult> GetGrapeVarietyInfoByCompositionId(int compositionId)
        {
            try
            {
                var response = await _compositionService.GetGrapeVarietyInfoByCompositionId(compositionId);
                return Ok(response);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddComposition")]
        public async Task<IActionResult> AddComposition([FromBody] AddCompositionModel compositionModel)
        {
            try
            {
                var response = await _compositionService.AddComposition(compositionModel);
                return Ok(response);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateComposition")]
        public async Task<IActionResult> UpdateComposition([FromBody] UpdateCompositionModel updateCompositionModel)
        {
            try
            {
                if (updateCompositionModel.Id > 0)
                {
                    var response = await _compositionService.UpdateComposition(updateCompositionModel);
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
        public async Task<IActionResult> DeleteSize(int id)
        {
            try
            {
                if (id > 0)
                {
                    var response = await _compositionService.DeleteComposition(id);
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

    }
}
