using Microsoft.AspNetCore.Mvc;
using Tayseer_AspMVC.Models;
using Tayseer_AspMVC.Repository;
using Tayseer_AspMVC.Repository.Base;

namespace Tayseer_AspAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CentersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Centers> _repository;
        private readonly IWebHostEnvironment _env;
        private readonly IRepoCenter _repoCenters;

        public CentersController(IUnitOfWork unitOfWork, IRepository<Centers> repository, IWebHostEnvironment env, IRepoCenter repoCenters)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _env = env;
            _repoCenters = repoCenters;
        }

       
        // ✅ Get All
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var centers = _unitOfWork.Centers.FindAll();
            return Ok(centers);
        }

        // ✅ Get by Id (route)
        [HttpGet("GetById/{id:int}")]
        public IActionResult GetById(int id)
        {
            var center = _repository.FindById(id);
            if (center == null)
                return NotFound(new { Message = "المركز غير موجود" });

            return Ok(center);
        }

        // ✅ Get by Id (query string)
        // مثال: /api/Centers/GetByIdquery?Id=3
        [HttpGet("GetByIdquery")]
        public IActionResult GetByIdquery([FromQuery] int Id)
        {
            var center = _repository.FindById(Id);
            if (center == null)
                return NotFound(new { Message = "المركز غير موجود" });

            return Ok(center);
        }

        // ✅ Get by Uid
        [HttpGet("GetByUid/{uid}")]
        public IActionResult GetByUid(string uid)
        {
            var center = _repoCenters.FindByUIdCenter(uid);
            if (center == null)
                return NotFound(new { Message = "لم يتم العثور على مركز بهذا المعرّف" });

            return Ok(center);
        }

      
    }
}
