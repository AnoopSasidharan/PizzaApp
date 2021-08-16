using PizzaApp.Entity;
using PizzaApp.Parameters;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaApp.Services
{
    public interface IPizzaRepository
    {
        Task<IEnumerable<Pizza>> GetPizzasAsync(InputParameters parameters);
        Task<Pizza> GetPizzaByIdAsync(int id);
        Task<IEnumerable<Pizza>> GetPizzasByIdsAsync(IEnumerable<int> ids);
        void AddPizza(Pizza pizza);
        void RemovePizza(Pizza pizza);
        Task<bool> SaveAsync();
    }
}