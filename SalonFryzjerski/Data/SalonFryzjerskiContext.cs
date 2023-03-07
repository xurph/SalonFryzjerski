using Microsoft.EntityFrameworkCore;

namespace SalonFryzjerski.Data
{
    public class SalonFryzjerskiContext : DbContext
    {
        public SalonFryzjerskiContext()
        {

        }

        public SalonFryzjerskiContext(DbContextOptions<SalonFryzjerskiContext> options)
           : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<Rodzaj> Rodzaje { get; set; }  
        public virtual DbSet<Wizyta> Wizyty { get; set; }
    }
}
