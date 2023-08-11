using AutoMapper;
using Innovi.Entities;
using Innovi.Models;

namespace Innovi.Utils
{
    public class CustomProfile : Profile
    {
        public CustomProfile()
        {
          
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();

            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();


        }
    }
}
