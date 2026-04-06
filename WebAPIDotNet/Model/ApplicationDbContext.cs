using Microsoft.EntityFrameworkCore;

namespace WebAPIDotNet.Model
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> myContext):base(myContext)
        {
            

        }

        public DbSet<Department> Departments { get; set; }


    }
}
