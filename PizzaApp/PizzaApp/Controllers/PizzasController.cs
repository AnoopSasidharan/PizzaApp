using Microsoft.AspNetCore.Mvc;
using PizzaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PizzasController : ControllerBase
    {
        private static List<Pizza> _pizas = new()
        {
            new Pizza()
            {
                Id=1,
                Title="Cheese",
                Description="Cheese",
                Price=12.99
            },
            new Pizza()
            {
                Id = 2,
                Title = "Steak",
                Description = "Steak",
                Price = 9.99
            }
        };

        public PizzasController()
        {

        }
        [HttpGet()]
        public ActionResult<IEnumerable<Pizza>> GetPizzas()
        {
            var pizzas = _pizas;
            return Ok(pizzas);
        }
        [HttpGet("{Id}",Name ="GetPizzaById")]
        public ActionResult<Pizza> GetPizzas(int Id)
        {
            var pizza = _pizas.FirstOrDefault(p => p.Id == Id);
            if(pizza==null)
            {
                return NotFound();
            }
            return Ok(pizza);
        }
        [HttpPost()]
        public ActionResult CreatePizza([FromBody] Pizza pizza)
        {
            int maxId = _pizas.Max(p => p.Id);
            pizza.Id = maxId + 1;
            _pizas.Add(pizza);
            return CreatedAtRoute("GetPizzaById", new { Id = pizza.Id }, pizza);
        }
        [HttpDelete("{Id}")]
        public ActionResult DeletePizza(int Id)
        {
            var pizza = _pizas.FirstOrDefault(p => p.Id == Id);
            if (pizza == null)
            {
                return NotFound();
            }
            _pizas.Remove(pizza);
            return NoContent();
        }
    }
}
