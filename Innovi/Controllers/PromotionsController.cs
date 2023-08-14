using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Innovi.Data;
using Innovi.Entities;
using Innovi.Services.Interfaces;
using Innovi.Services.Repository;
using Innovi.Models;

namespace Innovi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionsController : ControllerBase
    {
        private readonly IPromotionRepository _promotionRepository;
        public PromotionsController(IPromotionRepository promotionRepository)
        {
            this._promotionRepository = promotionRepository;
        }
        // GET: api/Promotions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PromotionDto>>> GetPromotions()
        {
            try
            {
                var promotions = await _promotionRepository.GetAllAsync();
                if (promotions == null)
                {
                    return NotFound("No Promotions found.");
                }
                return Ok(promotions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        // GET: api/Promotions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PromotionDto>> GetPromotion(int id)
        {
            try
            {
                var promotion = await _promotionRepository.GetByIdAsync(id);
                if (promotion == null)
                {
                    return NotFound("No Promotions found.");
                }
                return Ok(promotion);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            };
        }
        // GET: api/Promotions/CategoryId
        [HttpGet("{CategoryId}/GetByCategoryIdAsync")]
        public async Task<ActionResult<IEnumerable<PromotionDto>>> GetByCategoryIdAsync(int CategoryId)
        {
            try
            {
                var promotions = await _promotionRepository.GetByCategoryIdAsync(CategoryId);
                if (promotions == null)
                {
                    return NotFound("No Promotions found.");
                }
                return Ok(promotions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        // GET: api/Promotions/TEST2
        [HttpGet("{MerchantId}/GetByMerchantIdAsync")]
        public async Task<ActionResult<IEnumerable<PromotionDto>>> GetByMerchantIdAsync(int MerchantId)
        {
            try
            {
                var promotions = await _promotionRepository.GetByMerchantIdAsync(MerchantId);
                if (promotions == null)
                {
                    return NotFound("No Promotions found.");
                }
                return Ok(promotions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        // POST: api/Promotions/Pagination
        [HttpPost]
        [Route("Pagination")]
        public async Task<ActionResult<IEnumerable<PromotionDto>>> GetWithPagination(PromotionFilterDto filter)
        {
            try
            {
                var filteredPromotions = await _promotionRepository.GetWithPagination(filter);
                if (filteredPromotions == null)
                {
                    return NotFound("No Promotions found.");
                }
                return Ok(filteredPromotions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }

        }

        //TODO

    }
}
