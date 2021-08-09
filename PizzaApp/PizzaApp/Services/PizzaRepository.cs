using PizzaApp.Data;
using PizzaApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaApp.Services
{
    public class PizzaRepository : IPizzaRepository
    {
        private readonly PizzaDbContext _pizzaDbContext;

        public PizzaRepository(PizzaDbContext pizzaDbContext)
        {
            this._pizzaDbContext = pizzaDbContext;
        }
        public IEnumerable<Pizza> GetPizzas()
        {
            return _pizzaDbContext.Pizzas.ToList();
        }
    }
}
