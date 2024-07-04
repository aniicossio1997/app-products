using app_products.Models;
using app_products.ViewModels;
using AutoMapper;

namespace app_products.MappingServices
{
    public class ProductViewModelMappingService : Profile
    {
        public ProductViewModelMappingService()
        {
            CreateMap<Product, ProductViewModel>()
                      .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("dd/MM/yyyy")))
                      .ReverseMap(); 
        }
    }
}
