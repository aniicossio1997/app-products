using app_products.Models;
using app_products.ViewModels;
using AutoMapper;

namespace app_products.MappingServices
{
    public class CategoryViewModelMappingService : Profile
    {
        public CategoryViewModelMappingService()
        {
            CreateMap<Category, CategoryViewModel>();
        }
    }

}
