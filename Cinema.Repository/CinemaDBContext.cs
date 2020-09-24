
using Cinema.Domain;
using Cinema.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Repository
{
    public class CinemaDBContext : IdentityDbContext<UserModel, RoleModel, int>
    {
        public CinemaDBContext(DbContextOptions<CinemaDBContext> options)
            : base(options)
        {
        }

        public DbSet<FilmModel> Films { get; set; }
    }
}
