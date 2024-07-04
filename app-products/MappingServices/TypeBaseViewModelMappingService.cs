using app_products.Models;
using app_products.ViewModels.ClassesBase;
using AutoMapper;
using Microsoft.VisualBasic.FileIO;
using System.Diagnostics.Metrics;

namespace app_products.MappingServices
{
    public class TypeBaseViewModelMappingService : Profile
    {
        public TypeBaseViewModelMappingService()
        {
            CreateMap<Category, TypeBaseViewModel>();

        }
    }
}
