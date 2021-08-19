using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PizzaApp.Entity;
using PizzaApp.Models;
using PizzaApp.Parameters;
using PizzaApp.Services;
using System.Collections.Generic;
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
        [ResponseCache(Duration = 40)]
        public async Task<ActionResult<IEnumerable<Pizza>>> GetPizzas([FromQuery] InputParameters parameters)
        {
            var pizzas = await _pizzaRepository.GetPizzasAsync(parameters);
            return Ok(pizzas);
        }
        [HttpGet("{Id}", Name = "GetPizzaById")]
        [ResponseCache(CacheProfileName = "120MinProfile")]
        public async Task<ActionResult<PizzaDto>> GetPizzas(int Id)
        {
            var pizza = await _pizzaRepository.GetPizzaByIdAsync(Id);
            if (pizza == null)
            {
                return NotFound();
            }
            var selectedPizza = _mapper.Map<PizzaDto>(pizza);
            selectedPizza.Links = CreateLinks(Id);

            return Ok(selectedPizza);
        }
        [HttpPost(Name = "CreatePizza")]
        public async Task<ActionResult> CreatePizza([FromBody] PizzaCreationDto pizzaDto)
        {
            var pizza = _mapper.Map<Pizza>(pizzaDto);

            _pizzaRepository.AddPizza(pizza);
            await _pizzaRepository.SaveAsync();
            return CreatedAtRoute("GetPizzaById", new { Id = pizza.Id }, pizza);
        }
        [HttpDelete("{Id}", Name = "DeletePizza")]
        public async Task<ActionResult> DeletePizza(int Id)
        {
            var pizza = await _pizzaRepository.GetPizzaByIdAsync(Id);
            if (pizza == null)
            {
                return BadRequest();
            }
            _pizzaRepository.RemovePizza(pizza);
            await _pizzaRepository.SaveAsync();
            return NoContent();
        }
        [HttpPatch("{Id}", Name = "PatchPizza")]
        public async Task<ActionResult> PatchPizza(int Id, [FromBody] JsonPatchDocument<PizzaUpdateDto> patchDocument)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var pizza = await _pizzaRepository.GetPizzaByIdAsync(Id);

            if (pizza == null)
            {
                return NotFound();
            }
            var pizzaToUpdate = _mapper.Map<PizzaUpdateDto>(pizza);
            patchDocument.ApplyTo(pizzaToUpdate, ModelState);

            if (!TryValidateModel(pizzaToUpdate))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(pizzaToUpdate, pizza);
            await _pizzaRepository.SaveAsync();
            return NoContent();
        }
        [HttpPut("{Id}", Name = "PutPizza")]
        public async Task<ActionResult> UpdatePizza(int Id, PizzaUpdateDto pizzaUpdate)
        {
            var pizza = await _pizzaRepository.GetPizzaByIdAsync(Id);
            if (pizza == null)
            {
                return NotFound();
            }

            _mapper.Map(pizzaUpdate, pizza);
            await _pizzaRepository.SaveAsync();
            return NoContent();
        }
        private IEnumerable<LinksDto> CreateLinks(int Id)
        {
            var links = new List<LinksDto>();
            links.Add(new LinksDto()
            {
                Rel = "Self",
                Method = "GET",
                Href = Url.Link("GetPizzaById", new { Id })
            });

            links.Add(new LinksDto()
            {
                Rel = "delete_pizza",
                Method = "DELETE",
                Href = Url.Link("DeletePizza", new { Id })
            });

            links.Add(new LinksDto()
            {
                Rel = "partial_update_pizza",
                Method = "PATCH",
                Href = Url.Link("PatchPizza", new { Id })
            });
            links.Add(new LinksDto()
            {
                Rel = "full_update_pizza",
                Method = "PUT",
                Href = Url.Link("PutPizza", new { Id })
            });

            return links;
        }

    }
}
