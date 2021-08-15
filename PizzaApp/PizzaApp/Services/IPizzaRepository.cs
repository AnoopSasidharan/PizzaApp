using PizzaApp.Entity;
using PizzaApp.Parameters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaApp.Services
{
    public interface IPizzaRepository
    {
        IEnumerable<Pizza> GetPizzas(InputParameters parameters);
        Pizza GetPizzaById(int id);
        void AddPizza(Pizza pizza);
        void RemovePizza(Pizza pizza);
        Task<bool> SaveAsync();
    }
}