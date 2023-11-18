using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace learn.Data
{
    public class DefaultAuthDbContext : IdentityDbContext
    {
        public DefaultAuthDbContext(DbContextOptions<DefaultAuthDbContext> options)
            : base(options) { }
    }
}
