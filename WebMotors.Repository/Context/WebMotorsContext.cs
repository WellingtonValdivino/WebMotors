using Microsoft.EntityFrameworkCore;
using WebMotors.Domain;

namespace WebMotors.Repository.Context
{
    public class WebMotorsContext : DbContext
    {
        public WebMotorsContext(DbContextOptions<WebMotorsContext> options) : base (options) { }

        public DbSet<Anuncio> Anuncios { get; set; }        
    }
}