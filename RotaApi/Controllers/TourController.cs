using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rota.Core.Interfaces;
using Rota.Entities.DTOs;

namespace Rota.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ITourService _tourService;

        public TourController(ITourService tourService, IWebHostEnvironment hostEnvironment)
        {
            _tourService = tourService;
            _hostEnvironment = hostEnvironment;
        }

        // GET api/tour
        [HttpGet]
        public async Task<IEnumerable<TourDto>> GetAll()
        {
            return await _tourService.GetAllAsync();
        }

        // GET api/tour/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TourDto>> GetById(int id)
        {
            var tour = await _tourService.GetByIdAsync(id);
            if (tour == null)
                return NotFound($"Tour with id {id} not found.");

            return tour;
        }

        // POST api/tour
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<TourDto>> Create([FromForm] TourDto dto, IFormFile formFile)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;

            if (formFile != null && formFile.Length > 0)
            {
                string fileName = Guid.NewGuid().ToString();
                var uploadRoot = Path.Combine(wwwRootPath, "img", "tours");

                // Klasör yoksa oluştur
                Directory.CreateDirectory(uploadRoot); // bu zaten var mı diye kontrol eder

                var extension = Path.GetExtension(formFile.FileName);
                var newFileName = fileName + extension;

                // Eski resmi sil
                if (!string.IsNullOrEmpty(dto.ImageUrl))
                {
                    var oldPicPath = Path.Combine(wwwRootPath, dto.ImageUrl.TrimStart('\\', '/'));
                    if (System.IO.File.Exists(oldPicPath))
                    {
                        System.IO.File.Delete(oldPicPath);
                    }
                }

                var newFilePath = Path.Combine(uploadRoot, newFileName);
                using (var fileStream = new FileStream(newFilePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(fileStream);
                }

                // Veritabanına kaydedilecek yol
                dto.ImageUrl = Path.Combine("img", "tours", newFileName).Replace("\\", "/");
            }


            await _tourService.CreateAsync(dto);
            return Ok(dto);
        }


        // PUT api/tour/5
        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<TourDto>> Update(int id, [FromBody] TourDto dto, IFormFile formFile)
        {
            dto.Id = id;
            var existing = await _tourService.GetByIdAsync(id);

            if (existing == null)
                return NotFound($"Tour with id {id} not found.");

            string wwwRootPath = _hostEnvironment.WebRootPath;

            if (formFile != null && formFile.Length > 0)
            {
                string fileName = Guid.NewGuid().ToString();
                string extension = Path.GetExtension(formFile.FileName);
                string uploadRoot = Path.Combine(wwwRootPath, "img", "tours");

                Directory.CreateDirectory(uploadRoot); // klasör varsa zaten bir şey yapmaz

                // Eski görsel varsa sil
                if (!string.IsNullOrEmpty(dto.ImageUrl))
                {
                    var oldImagePath = Path.Combine(wwwRootPath, dto.ImageUrl.TrimStart('\\', '/'));
                    if (System.IO.File.Exists(oldImagePath))
                        System.IO.File.Delete(oldImagePath);
                }

                string newFilePath = Path.Combine(uploadRoot, fileName + extension);
                using (var stream = new FileStream(newFilePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }

                dto.ImageUrl = Path.Combine("img", "tours", fileName + extension).Replace("\\", "/");
            }


         

            await _tourService.UpdateAsync(dto);
            return dto;
        }

        // DELETE api/tour/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existing = await _tourService.GetByIdAsync(id);
            if (existing == null)
                return NotFound($"Tour with id {id} not found.");

            await _tourService.DeleteAsync(id);
            return NoContent();
        }

        // GET api/tour/5/details
        [HttpGet("{id}/details")]
        public async Task<ActionResult<TourDetailsDto>> GetDetails(int id)
        {
            var tourDetails = await _tourService.GetTourDetailsAsync(id);
            if (tourDetails == null)
                return NotFound($"Details for tour with id {id} not found.");

            return tourDetails;
        }


        [HttpGet("popular")]
        public async Task<ActionResult<TourDto>> GetPopularTours()
        {
            var result = await _tourService.GetPopularToursAsync();
            return Ok(result);
        }
    }
}
