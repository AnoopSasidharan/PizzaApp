using AutoMapper;
using PizzaApp.Entity;
using PizzaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaApp.Profiles
{
    public class PizzaProfile:Profile
    {
        public PizzaProfile()
        {
            CreateMap<PizzaDto, Pizza>();
            CreateMap<PizzaUpdateDto, Pizza>();
            CreateMap<Pizza, PizzaUpdateDto>();
            CreateMap<Pizza, PizzaDto>();
            CreateMap<PizzaCreationDto, Pizza>();
        }
    }
}
