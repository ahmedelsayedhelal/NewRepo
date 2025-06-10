using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sakan_project.models;
using Sakan_project.Repository;
using Sakan_project.DTO;

namespace Sakan_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentController : ControllerBase
    {
        private readonly IApartmentReposatory ApartmentRepository;
        public ApartmentController(IApartmentReposatory ApartmentRepository)
        {
            this.ApartmentRepository = ApartmentRepository;
        }

        [HttpGet]
        public IActionResult Getall()
        {
            List<Apartments> apartments = ApartmentRepository.GetAll();


            return Ok(apartments);
        }
        [HttpGet("{id}")]

        public IActionResult getbyid(int id)
        {

            Apartments apartments = ApartmentRepository.GetById(id);
            ApartmentDto apartmentDto = new ApartmentDto();

            apartmentDto.Locationdto = apartments.Location;
            apartmentDto.PricePerMonthdto = apartments.PricePerMonth;
            apartmentDto.Descriptiondto = apartments.Description;
            apartmentDto.Titledto = apartments.Title;
            apartmentDto.ImageUrldto = apartments.ImageUrl;
            return Ok(apartmentDto);

        }
        [HttpPost]
        public IActionResult Add(ApartmentDto apartmentdto)
        {
            if (ModelState.IsValid == true)
            {

                //ApartmentDto apartmentdto = new ApartmentDto();
                if (apartmentdto == null)
                {
                    return BadRequest("Product data is null");
                }

                Apartments new_apartment = new Apartments
                {

                    Location = apartmentdto.Locationdto,
                    ImageUrl = apartmentdto.ImageUrldto,
                    Description = apartmentdto.Descriptiondto,
                    Title = apartmentdto.Titledto,
                    PricePerMonth = apartmentdto.PricePerMonthdto,
                    OwnerId = apartmentdto.Owner_id

                };

                ApartmentRepository.Insert(new_apartment);

                return Ok("saved");
            }
            return BadRequest(ModelState);

        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, ApartmentDto apartmentDto)
        {
            if (ModelState.IsValid == true)
            {
                Apartments existing_apartment = ApartmentRepository.GetById(id);
                if (existing_apartment == null)
                {
                    return NotFound($"Owner with ID {id} not found.");
                }

                existing_apartment.Description = apartmentDto.Descriptiondto;
                existing_apartment.ImageUrl = apartmentDto.ImageUrldto;
                existing_apartment.Title = apartmentDto.Titledto;
                existing_apartment.PricePerMonth = apartmentDto.PricePerMonthdto;
                existing_apartment.Location = apartmentDto.Locationdto;

                ApartmentRepository.Edit(id, existing_apartment);

                return Ok("Updated successfully");
            }
            return BadRequest(ModelState);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                // Check if owner exists
                var apartments = ApartmentRepository.GetById(id);
                if (apartments == null)
                {
                    return NotFound(new
                    {
                        Message = $"apartment with ID {id} not found",
                        StatusCode = 404
                    });
                }


                ApartmentRepository.Delete(id);

                return Ok(new
                {
                    Message = $"apartment with ID {id} deleted successfully",
                    StatusCode = 200
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "An error occurred while deleting the apartment",
                    Error = ex.Message,
                    StatusCode = 500
                });
            }
        }
        [HttpGet("search")]
        public IActionResult SearchByName([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Name parameter is required");
            }

            var apartments = ApartmentRepository.GetAll()
                .Where(a => a.Title.Contains(name, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return Ok(apartments);
        }
        [HttpGet("filter")]
        public IActionResult FilterByPrice([FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice)
        {
            var apartments = ApartmentRepository.GetAll().AsQueryable();

            if (minPrice.HasValue)
            {
                apartments = apartments.Where(a => a.PricePerMonth >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                apartments = apartments.Where(a => a.PricePerMonth <= maxPrice.Value);
            }

            return Ok(apartments.ToList());
        }


    }
}






