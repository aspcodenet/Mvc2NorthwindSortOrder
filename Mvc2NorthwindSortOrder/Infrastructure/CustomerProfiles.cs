using System.Globalization;
using AutoMapper;
using Mvc2NorthwindSortOrder.Models;
using Mvc2NorthwindSortOrder.ViewModels;

namespace Mvc2NorthwindSortOrder.Infrastructure
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            //CreateMap<Customers, CustomerListViewModel.CustomerViewModel>();

            //CreateMap<CustomerEditViewModel, Customers>();

            CreateMap<Customers, CustomerEditViewModel>()
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.CompanyName))
                .ForMember(dest=>dest.HasDog, opt=>opt.MapFrom(src=>src.DogLover))
                .ForMember(dest => dest.ContactNameCapitalLetters,
                    opt => opt.MapFrom(src => src.ContactName.ToUpper()));


            CreateMap<CustomerEditViewModel, Customers>()
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company))
                .ForMember(dest => dest.DogLover, opt => opt.MapFrom(src => src.HasDog))
                .ForMember(dest=>dest.Orders, opt=>opt.Ignore())
                .ForMember(dest => dest.CustomerCustomerDemo, opt => opt.Ignore())
                .ForMember(dest => dest.ContactName,
                    opt => opt.MapFrom(src => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(src.ContactNameCapitalLetters)));

        }
    }
}