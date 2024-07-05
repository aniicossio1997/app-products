using app_products.Models;
using app_products.ViewModels;
using AutoMapper;

namespace app_products.MappingServices
{
    public class UserViewModelMappingService : Profile
    {
        public UserViewModelMappingService()
        {
            CreateMap<UserViewModel, User>().ReverseMap();
        }
    }
}
