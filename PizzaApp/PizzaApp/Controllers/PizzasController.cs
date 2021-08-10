using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PizzaApp.Entity;
using PizzaApp.Models;
using PizzaApp.Services;
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
        private readonly IPizzaRepository _pizzaRepository;
        private readonly IMapper _mapper;

        public PizzasController(IPizzaRepository pizzaRepository, IMapper mapper)
        {
            this._pizzaRepository = pizzaRepository;
            this._mapper = mapper;
        }
        [HttpGet()]
        public ActionResult<IEnumerable<Pizza>> GetPizzas()
        {
            var pizzas = _pizzaRepository.GetPizzas();
            return Ok(pizzas);
        }
        [HttpGet("{Id}",Name ="GetPizzaById")]
        public ActionResult<Pizza> GetPizzas(int Id)
        {
            var pizza = _pizzaRepository.GetPizzaById(Id);
            if(pizza==null)
            {
                return NotFound();
            }
            return Ok(pizza);
        }
        [HttpPost()]
        public ActionResult CreatePizza([FromBody] PizzaDto pizzaDto)
        {
            var pizza = _mapper.Map<Pizza>(pizzaDto);

            _pizzaRepository.AddPizza(pizza);
            _pizzaRepository.Save();
            return CreatedAtRoute("GetPizzaById", new { Id = pizza.Id }, pizza);
        }
        [HttpDelete("{Id}")]
        public ActionResult DeletePizza(int Id)
        {
            var pizza = _pizzaRepository.GetPizzaById(Id);
            if (pizza == null)
            {
                return BadRequest();
            }
            _pizzaRepository.RemovePizza(pizza);
            _pizzaRepository.Save();
            return NoContent();
        }
    }
}
