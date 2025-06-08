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
    public class OwnersController : ControllerBase
    {
        private readonly IOwnerRepository OwnerRepository;
        public OwnersController(IOwnerRepository OwnerRepository)
        {
            this.OwnerRepository = OwnerRepository;
        }

        [HttpGet]
        public IActionResult Getall()
        {
            List<Owners> newOwner = OwnerRepository.GetAll();


            return Ok(newOwner);
        }
        [HttpGet("{id}")]

        public IActionResult getbyid(int id)
        {

            Owners owners = OwnerRepository.GetById(id);
            Ownersdto ownersdto = new Ownersdto();

            ownersdto.fName = owners.Firstname;
            ownersdto.lname = owners.Lastname;
            return Ok(ownersdto);

        }
        [HttpPost]
        public IActionResult Add(Ownersdto newOwnerdto)
        {
            if (ModelState.IsValid == true)
            {

                Ownersdto newOwner = new Ownersdto();
                if (newOwnerdto == null)
                {
                    return BadRequest("Product data is null");
                }

                Owners newowner = new Owners
                {

                    Firstname = newOwnerdto.fName,
                    Lastname = newOwnerdto.lname,
                    Email = newOwnerdto.Emaildto
                };

                OwnerRepository.Insert(newowner);

                return Ok("saved");
            }
            return BadRequest(ModelState);

        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, Ownersdto ownersdto)
        {
            if (ModelState.IsValid == true)
            {
                Owners existingOwner = OwnerRepository.GetById(id);
                if (existingOwner == null)
                {
                    return NotFound($"Owner with ID {id} not found.");
                }

                existingOwner.Firstname = ownersdto.fName;
                existingOwner.Lastname = ownersdto.lname;
                existingOwner.Email = ownersdto.Emaildto;

                OwnerRepository.Edit(id, existingOwner);

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
                var owner = OwnerRepository.GetById(id);
                if (owner == null)
                {
                    return NotFound(new
                    {
                        Message = $"Owner with ID {id} not found",
                        StatusCode = 404
                    });
                }


                OwnerRepository.Delete(id);

                return Ok(new
                {
                    Message = $"Owner with ID {id} deleted successfully",
                    StatusCode = 200
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "An error occurred while deleting the owner",
                    Error = ex.Message,
                    StatusCode = 500
                });
            }
        }
    }
}






