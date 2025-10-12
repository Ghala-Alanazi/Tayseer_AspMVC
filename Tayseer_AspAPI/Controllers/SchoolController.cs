using Microsoft.AspNetCore.Mvc;
using Tayseer_AspMVC.Models;
using Tayseer_AspMVC.Repository.Base;

namespace Tayseer_AspAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<School> _repository;
        private readonly IRepoSchool _repoSchool;
        private readonly IWebHostEnvironment _env;

        public SchoolController(IUnitOfWork unitOfWork, IRepository<School> repository, IWebHostEnvironment env, IRepoSchool repoSchool)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _env = env;
            _repoSchool = repoSchool;
        }

       

        [HttpGet("GetAll")]
        public IActionResult GetAllSchools()
        {
            var schools = _unitOfWork.Schools.FindAll();
            return Ok(schools);
        }


        [HttpGet("GetById/{id:int}")]
        public IActionResult GetById(int id)
        {
            var school = _repository.FindById(id);
            if (school == null)
                return NotFound(new { Message = "المدرسة غير موجودة" });

            return Ok(school);
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


        [HttpGet("GetByUid/{uid}")]
        public IActionResult GetByUid(string uid)
        {
            var school = _repoSchool.FindByUIdSchool(uid);
            if (school == null)
                return NotFound(new { Message = "المدرسة غير موجودة" });

            return Ok(school);
        }

       
    }
}
