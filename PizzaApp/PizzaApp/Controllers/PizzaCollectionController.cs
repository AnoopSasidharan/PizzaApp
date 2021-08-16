using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using PizzaApp.Entity;
using PizzaApp.ModelBinders;
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
    public class PizzaCollectionController: ControllerBase
    {
        private readonly IPizzaRepository _pizzaRepository;
        private readonly IMapper _mapper;

        public PizzaCollectionController(IPizzaRepository pizzaRepository, IMapper mapper)
        {
            this._pizzaRepository = pizzaRepository;
            this._mapper = mapper;
        }
        
        [HttpGet("({Ids})", Name = "GetPizzaCollection")]
        public async Task<IEnumerable<Pizza>> GetPizzasCollection(
            [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<int> Ids)
        {
            var pizzas = await _pizzaRepository.GetPizzasByIdsAsync(Ids);
            return pizzas;
        }

        [HttpPost()]
        public async Task<ActionResult> CreatePizzaCollection([FromBody] IEnumerable<PizzaCreationDto> pizzasToCreate)
        {
            var pizzas = _mapper.Map<IEnumerable<Pizza>>(pizzasToCreate);
            foreach (var pizza in pizzas)
            {
                _pizzaRepository.AddPizza(pizza);
            }
            await _pizzaRepository.SaveAsync();

            return Ok();
        }
    }
}
