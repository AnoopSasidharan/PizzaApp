using Microsoft.EntityFrameworkCore;
using PizzaApp.Entity;

namespace PizzaApp.Data
{
    public class PizzaDbContext : DbContext
    {
        public PizzaDbContext()
        {

        }
        public PizzaDbContext(DbContextOptions<PizzaDbContext> options) : base(options)
        {

        }
        public DbSet<Pizza> Pizzas { get; set; }
    }
}
