using Microsoft.AspNetCore.Mvc;
using UnitOfWork.Domain;
using UnitOfWork.Infrastructure;
using UnitOfWork.Repository;

namespace UnitOfWork.Demo.Controllers
{
    [Route("test")]
    public class TestController : ControllerBase
    {
        private readonly IBaseRepository<Student> _studentRepository;
        private readonly IBaseRepository<Teacher> _teacherRepository;

        public TestController(IBaseRepository<Student> studentRepository,
            IBaseRepository<Teacher> teacherRepository)
        {
            this._studentRepository = studentRepository;
            this._teacherRepository = teacherRepository;
        }

        [HttpGet, UnitOfWork]
        public async Task<IActionResult> AddTestAsync()
        {
            await _studentRepository.InsertAsync(new Student
            {
                Name = "Hello",
                Age = 22
            });
             
            await _teacherRepository.InsertAsync(new Teacher
            {
                Name = "World",
                Age = 35,
                Subject = 1
            });

            return Ok("Ok");
        }
    }
}
