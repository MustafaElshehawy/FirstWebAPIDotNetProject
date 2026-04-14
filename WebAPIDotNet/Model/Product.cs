using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIDotNet.Model
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public int quentity { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        
        public Department? Department { get; set; }


    }
}
