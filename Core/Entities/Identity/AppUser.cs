using Microsoft.AspNetCore.Identity;

namespace Core.Entities.Identity
{
    // do identityusera možemo doći zato kaj smo u core.csproj project instalirali
    // Microsoft.Extensions.Identity.Stores
     public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public Address Address { get; set; }
    }
}