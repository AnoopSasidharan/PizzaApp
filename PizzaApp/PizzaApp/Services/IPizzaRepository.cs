using PizzaApp.Entity;
using System.Collections.Generic;

namespace PizzaApp.Services
{
    public interface IPizzaRepository
    {
        IEnumerable<Pizza> GetPizzas();
    }
}