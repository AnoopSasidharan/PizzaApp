using PizzaApp.Entity;
using System.Collections.Generic;

namespace PizzaApp.Services
{
    public interface IPizzaRepository
    {
        IEnumerable<Pizza> GetPizzas();
        Pizza GetPizzaById(int id);
        void AddPizza(Pizza pizza);
        void RemovePizza(Pizza pizza);
        bool Save();
    }
}