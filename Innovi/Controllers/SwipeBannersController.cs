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
    public class SwipeBannersController : ControllerBase
    {
        private readonly ISwipeBannerRepository _swipeBannerRepository;
        public SwipeBannersController(ISwipeBannerRepository swipeBannerRepository)
        {
            this._swipeBannerRepository = swipeBannerRepository;
        }

        // GET: api/SwipeBanners
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SwipeBannerDto>>> GetSwipeBanners()
        {
            try
            {
                var swipeBanners = await _swipeBannerRepository.GetAllAsync();
                if (swipeBanners == null)
                {
                    return NotFound("No SwipeBanner found.");
                }
                return Ok(swipeBanners);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        // GET: api/SwipeBanners/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SwipeBannerDto>> GetSwipeBanner(int id)
        {
            try
            {
                var swipeBanner = await _swipeBannerRepository.GetByIdAsync(id);
                if (swipeBanner == null)
                {
                    return NotFound("No SwipeBanner found.");
                }
                return Ok(swipeBanner);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            };
        }
        // GET: api/SwipeBanners/CategoryId
        [HttpGet("{CategoryId}/GetByCategoryIdAsync")]
        public async Task<ActionResult<IEnumerable<SwipeBannerDto>>> GetByCategoryIdAsync(int CategoryId)
        {
            try
            {
                var swipeBanners = await _swipeBannerRepository.GetByCategoryIdAsync(CategoryId);
                if (swipeBanners == null)
                {
                    return NotFound("No Promotions found.");
                }
                return Ok(swipeBanners);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
