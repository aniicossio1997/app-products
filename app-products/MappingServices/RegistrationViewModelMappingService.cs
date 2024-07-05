using app_products.Models;
using app_products.ViewModels;
using AutoMapper;

namespace app_products.MappingServices
{
    public class RegistrationViewModelMappingService : Profile
    {
        public RegistrationViewModelMappingService()
        {
            CreateMap<RegistrationViewModel, User>();
        }
    }
}
