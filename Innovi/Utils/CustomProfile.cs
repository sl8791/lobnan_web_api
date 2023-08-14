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

            CreateMap<Promotion, PromotionDto>();
            CreateMap<PromotionDto, Promotion>();

            CreateMap<SwipeBanner, SwipeBannerDto>();
            CreateMap<SwipeBannerDto, SwipeBanner>();

            CreateMap<Merchant, MerchantDto>();
            CreateMap<MerchantDto, Merchant>();

            CreateMap<City, CityDto>();
            CreateMap<CityDto, City>();

            CreateMap<Governorate, GovernorateDto>();
            CreateMap<GovernorateDto, Governorate>();

            CreateMap<Branch, BranchDto>();
            CreateMap<BranchDto, Branch>();

            CreateMap<ProductImage, ProductImageDto>();
            CreateMap<ProductImageDto, ProductImage>();

            CreateMap<ProductQuantity, ProductQuantityDto>();
            CreateMap<ProductQuantityDto, ProductQuantity>();

        }
    }
}
