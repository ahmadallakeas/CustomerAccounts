﻿using Application.DataTransfer;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Configurations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<CustomerForRegistrationDto, Customer>()
                .ForMember(c => c.Surname, opt => opt.MapFrom(cg => cg.LastName));
            CreateMap<Customer, CustomerDto>()
                .ForMember(cd => cd.LastName, opt =>
                opt.MapFrom(c => c.Surname));
            CreateMap<CustomerDto, Customer>()
    .ForMember(cd => cd.Surname, opt =>
    opt.MapFrom(c => c.LastName));

        }
    }
}
