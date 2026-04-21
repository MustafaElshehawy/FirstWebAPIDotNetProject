using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebAPIDotNet.Model
{
    public class ApplicationDbContext :IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> myContext):base(myContext)
        {
            

        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Product> Products { get; set; }


    }
}
