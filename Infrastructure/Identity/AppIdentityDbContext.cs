using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity
{
    // ovdje moramo specificirati appusera ako želimo da nam se prikažu njegovi podaci
    //u AppIdentityDbContextSeed ubacuješ podatke i lozinku kao i u startup u mvc 5
        public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        {
        }
        //we need to add this, otherwise we will have issues with appuser primary key
        protected override void OnModelCreating(ModelBuilder builder) 
        {
            base.OnModelCreating(builder);
        }
    }
}