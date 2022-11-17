using Application.DataTransfer;
using AutoMapper;
using Domain.Entities;
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
            CreateMap<Transaction, TransactionDto>();
            CreateMap<Account, AccountDto>();
            CreateMap<Account, UserInfoDto>()
                .ForMember(u => u.FirstName,opt=>
                opt.MapFrom(c=>c.Customer.FirstName))
               .ForMember(u => u.Surname, opt =>
                opt.MapFrom(c => c.Customer.Surname));
        }
    }
}
