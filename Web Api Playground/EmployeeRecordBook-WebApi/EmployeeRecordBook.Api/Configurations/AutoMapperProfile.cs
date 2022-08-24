using AutoMapper;
using EmployeeRecordBook.Core.Entities;
using EmployeeRecordBook.ViewModels;

namespace EmployeeRecordBook.Api.Configurations
{
   internal class AutoMapperProfile : Profile
   {
      internal AutoMapperProfile()
      {
         CreateMap<EmployeeVm, Employee>();
      }
   }
}
