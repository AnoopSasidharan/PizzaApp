using Microsoft.EntityFrameworkCore;
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
            if(pizza==null)
            {
                throw new ArgumentNullException(nameof(AddPizza));
            }
           _pizzaDbContext.Pizzas.Add(pizza);
        }

        public async Task<Pizza> GetPizzaByIdAsync(int id)
        {
            return await _pizzaDbContext.Pizzas.FindAsync(id);
        }

        public async Task<IEnumerable<Pizza>> GetPizzasAsync(InputParameters parameters)
        {
            var pizzas = _pizzaDbContext.Pizzas as IQueryable<Pizza>;

            if(!string.IsNullOrWhiteSpace(parameters.SearchBy))
            {
                pizzas = pizzas.Where(p => p.Title.StartsWith(parameters.SearchBy));
            }


            return await pizzas.ToListAsync();
        }
        public async Task<IEnumerable<Pizza>> GetPizzasByIdsAsync(IEnumerable<int> ids)
        {
            return await _pizzaDbContext.Pizzas.Where(p => ids.Contains(p.Id)).ToListAsync();
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
