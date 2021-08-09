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

        public void AddPizza(Pizza pizza)
        {
            _pizzaDbContext.Pizzas.Add(pizza);
        }

        public Pizza GetPizzaById(int id)
        {
            return _pizzaDbContext.Pizzas.Find(id);
        }

        public IEnumerable<Pizza> GetPizzas()
        {
            return _pizzaDbContext.Pizzas.ToList();
        }

        public void RemovePizza(Pizza pizza)
        {
            this._pizzaDbContext.Remove(pizza);
        }

        public bool Save()
        {
            return _pizzaDbContext.SaveChanges() > 0;
        }
    }
}
