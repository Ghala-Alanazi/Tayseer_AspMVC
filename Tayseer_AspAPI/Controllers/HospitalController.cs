using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using Tayseer_AspMVC.Models;
using Tayseer_AspMVC.Repository.Base;

namespace Tayseer_AspAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IReposHospital _roposHospital;
        private readonly IRepository<Hospital> _repository;
        private readonly IWebHostEnvironment _env;

        public HospitalController(IUnitOfWork unitOfWork, IRepository<Hospital> repository, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _env = env;
        }


        [HttpGet("{id:int}")]
        public ActionResult<IEnumerable<Hospital>> GetAll(int id = 1)
        {
            var hospital = _unitOfWork.Hospitals.FindAll()
            .Select(c => new Hospital
            {
                Id = c.Id,
                uid = c.uid,
                Name = c.Name,
                Address = c.Address,
                Services = c.Services,
                Region = c.Region,
            }).Where(c => c.Id >= id)
                .ToList();

            return Ok(hospital); // يرجّع JSON
        }

        [HttpGet("GetAll")]
        public IActionResult GetAllHospital()
        {
            var hospitals = _unitOfWork.Hospitals.FindAll();
           
            return Ok(hospitals);
        }


        [HttpGet("GetById/{Id:int}")]
        public IActionResult GetById(int Id)
        {
            var cate = _repository.FindById(Id);
            if (cate == null)
            {
                return NotFound(new { Message = "لا توجد نتائج لهذا الرقم" });
            }

            return Ok(cate);
        }

        [HttpGet("GetByIdquery")]
        public IActionResult GetByIdquery([FromQuery] int Id)
        {
            var cate = _repository.FindById(Id);
            if (cate == null)
            {
                return NotFound(new { Message = "لا توجد نتائج لهذا الرقم" });
            }
            return Ok(cate);
        }



        [HttpGet("GetByUId/{uid}")]
        public IActionResult GetByUId(string uid)
        {
            var cate = _unitOfWork.roposHospital.FindByUIdHospital(uid);
            if (cate == null)
            {
                return NotFound(new { Message = "لا توجد نتائج لهذا الرقم" });
            }

            return Ok(cate);
        }

    }
}
