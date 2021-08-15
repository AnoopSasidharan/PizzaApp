using PizzaApp.Data;
using PizzaApp.Entity;
using PizzaApp.Parameters;
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

        public void AddPizza(Pizza pizza)
        {
            _pizzaDbContext.Pizzas.Add(pizza);
        }

        public Pizza GetPizzaById(int id)
        {
            return _pizzaDbContext.Pizzas.Find(id);
        }

        public IEnumerable<Pizza> GetPizzas(InputParameters parameters)
        {
            var pizzas = _pizzaDbContext.Pizzas as IQueryable<Pizza>;

            if(!string.IsNullOrWhiteSpace(parameters.SearchBy))
            {
                pizzas = pizzas.Where(p => p.Title.StartsWith(parameters.SearchBy));
            }

            

            return pizzas.ToList();
        }

        public void RemovePizza(Pizza pizza)
        {
           this._pizzaDbContext.Pizzas.Remove(pizza);
        }

        public async Task<bool> SaveAsync()
        {
            return await _pizzaDbContext.SaveChangesAsync() > 0;
        }
    }
}
