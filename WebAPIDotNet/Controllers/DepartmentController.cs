using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebAPIDotNet.Model;

namespace WebAPIDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public DepartmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllDepartment()
        {
            var departments = _context.Departments.ToList();
            return Ok(departments);
        }


        [HttpGet]
        [Route("{id}")]
        public IActionResult GetDepartmentById(int id)
        {

            var departmentById = _context.Departments.FirstOrDefault(d => d.ID == id);
            return Ok(departmentById);

        }


        [HttpGet("{name:alpha}")]
        
        public IActionResult GetDepartmentByName(string name)
        {

            var departmentById = _context.Departments.FirstOrDefault(d => d.Name.ToLower() == name.ToLower());
            return Ok(departmentById);

        }

        [HttpPost]
        public IActionResult CreateDepartment(Department dept)
        {
            _context.Departments.Add(dept);
            _context.SaveChanges();
            return CreatedAtAction("GetDepartmentById", new { id =dept.ID },dept );//status code 201 created 
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateDepartment(int id ,Department department)
        {
            var deparmentInDb = _context.Departments.FirstOrDefault(d => d.ID == id);
            if (deparmentInDb != null)
            {
                deparmentInDb.Name = department.Name;
                deparmentInDb.ManagerName = department.ManagerName;
                _context.SaveChanges();
                return NoContent();

            }
            else 
            {
                return NotFound("Department Not Found");
            }
        }





    }
}
