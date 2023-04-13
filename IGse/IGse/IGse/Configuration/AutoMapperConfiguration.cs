using AutoMapper;
using IGse.Core.Dtos;
using IGse.Core.Entities;
using IGse.ViewModels;

namespace IGse.Api.Configuration
{
    public class AutoMapperConfiguration : Profile
    {
        internal AutoMapperConfiguration()
        {
            // Vm to entity
            CreateMap<CustomerVm, Customers>();
            CreateMap<CustomerUpdateVm, Customers>();
            CreateMap<SetPriceVm, SetPrice>();
            CreateMap<ReadingsVm, Bill>();
            // Entity to dto
        }
    }
}