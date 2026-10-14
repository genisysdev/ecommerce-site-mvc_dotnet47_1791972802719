using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Ecommerce.Web.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        static ApplicationDbContext()
        {
            // The AspNet* tables are provisioned by 04_create_identity_tables.sql in an
            // existing database, so disable Code-First initialization/model checks.
            Database.SetInitializer<ApplicationDbContext>(null);
        }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
