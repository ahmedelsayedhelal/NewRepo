using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sakan_project.DTO;
using Sakan_project.models;
using Sakan_project.Repository;

namespace Sakan_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollegesController : ControllerBase
    {
        
            private readonly ICollegeRepository CollegeRepository;
        public CollegesController(ICollegeRepository CollegeRepository)
        {
            this.CollegeRepository = CollegeRepository;
        }

        [HttpGet]
            public IActionResult GetAll()
            {
                List<Colleges> colleges = CollegeRepository.GetAll();


                return Ok(colleges);
            }
            [HttpGet("{id}")]

            public IActionResult getbyid(int id)
            {

                Colleges colleges = CollegeRepository.GetById(id);
                CollegesDto CollegesDto = new CollegesDto();

                CollegesDto.Name = colleges.Name;
                CollegesDto.Unversityid = colleges.Unversityid;

                return Ok(CollegesDto);

            }
            [HttpPost]
            public IActionResult Add(CollegesDto CollegesDto)
            {
                if (ModelState.IsValid == true)
                {

                    //ApartmentDto apartmentdto = new ApartmentDto();
                    if (CollegesDto == null)
                    {
                        return BadRequest("Product data is null");
                    }

                    Colleges new_apartment = new Colleges
                    {

                        Name = CollegesDto.Name,
                        Unversityid = CollegesDto.Unversityid,

                    };

                    CollegeRepository.Insert(new_apartment);

                    return Ok("saved");
                }
                return BadRequest(ModelState);

            }
            [HttpPut("{id}")]
            public IActionResult Update(int id, CollegesDto CollegesDto)
            {
                if (ModelState.IsValid == true)
                {
                    Colleges existing_colloge = CollegeRepository.GetById(id);
                    if (existing_colloge == null)
                    {
                        return NotFound($"Owner with ID {id} not found.");
                    }

                    existing_colloge.Name = CollegesDto.Name;
                    existing_colloge.Unversityid = CollegesDto.Unversityid;


                    CollegeRepository.Edit(id, existing_colloge);

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
                    var colleges = CollegeRepository.GetById(id);
                    if (colleges == null)
                    {
                        return NotFound(new
                        {
                            Message = $"colleges with ID {id} not found",
                            StatusCode = 404
                        });
                    }


                    CollegeRepository.Delete(id);

                    return Ok(new
                    {
                        Message = $"colleges with ID {id} deleted successfully",
                        StatusCode = 200
                    });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new
                    {
                        Message = "An error occurred while deleting the colleges",
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

                var colleges = CollegeRepository.GetAll()
                    .Where(a => a.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                return Ok(colleges);
            }
        }
    }

