using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIDotNet.Model;

namespace WebAPIDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _context.Products.ToList();

            return Ok(products);
        
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        { 
            var ProductById =_context.Products.FirstOrDefault(p => p.Id == id);

            return Ok(ProductById);
        }

        [HttpPost]
        public IActionResult CreateProduct(Product product)
        { 
            _context.Products.Add(product);
            _context.SaveChanges();
            return CreatedAtAction("GetById",new { id = product.Id },product);
        
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateProduct(int id, Product product)
        {
            var productInDb = _context.Products.FirstOrDefault(p => p.Id == id);

            if (productInDb != null)
            {
                productInDb.Name = product.Name;
                productInDb.Price = product.Price;
                productInDb.Description = product.Description;
                productInDb.quentity = product.quentity;
                productInDb.DepartmentId = product.DepartmentId;
                _context.SaveChanges();
                return NoContent();

            }
            else
            {

                return NotFound();
            }


        }


        [HttpDelete]
        public IActionResult DeleteProduct(int id)
        { 
            var ProductInDb =_context.Products.FirstOrDefault(d=>d.Id == id);
            if (ProductInDb != null)
            {
                _context.Remove(ProductInDb);
                _context.SaveChanges();
                return NoContent();

            }
            else { 
            
            return NotFound();
            }
        
        }

    }
}
