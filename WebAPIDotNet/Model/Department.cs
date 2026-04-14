namespace WebAPIDotNet.Model
{
    public class Department
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string? ManagerName { get; set; }

        public List<Product>? Prods { get; set; }
    }
}
