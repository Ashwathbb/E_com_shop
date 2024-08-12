using AutoMapper;
using Dept.DataAcess.Dto;
using Dept.DataAcess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Service
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<simpleDto, Department>()
                 .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.DepartmentId))
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.DepartmentName));

            CreateMap<EmployeeDto, Employee>()
                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DepartmentId))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName));

            CreateMap<CustomerDto, Customer>()
                .ForMember(dest => dest.DepartmentGuid, opt => opt.MapFrom(src => src.DepartmentGuid))
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerID))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName));
               
            CreateMap<AddressDto, Address>()
                .ForMember(dest => dest.AddressId, opt => opt.MapFrom(src => src.AddressID))
                .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.ZipCode))
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerID));

            CreateMap<OrderDto, Order>()
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderID))
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerID));

            CreateMap<SalesDto, Sale>()
                .ForMember(dest => dest.SaleId, opt => opt.MapFrom(src => src.SalesID))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderID));
        }
    }
}
