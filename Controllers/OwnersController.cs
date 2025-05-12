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
        IOwnerRepository OwnerRepository;
        public OwnersController(IOwnerRepository OwnerRepository)
        {
            this.OwnerRepository = OwnerRepository;
        }

        [HttpGet]
        public IActionResult getall()
        {
            List<Owners> newOwner = OwnerRepository.GetAll();
            ;

            return Ok(newOwner);
        }
        [HttpGet("id")]

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
                //if (newOwnerdto == null)
                //{
                //    return BadRequest("Product data is null");
                //}

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
    }
}
        //public IActionResult update(Owners newOwner)
        //{


        //}

            
    


