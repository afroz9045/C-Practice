using AutoMapper;
using EmployeeRecordBook.Core.Entities;
using EmployeeRecordBook.ViewModels;

namespace EmployeeRecordBook.Configurations
{
   internal class AutoMapperProfile : Profile
   {
      internal AutoMapperProfile()
      {
         CreateMap<EmployeeVm, Employee>();
      }
   }
}
