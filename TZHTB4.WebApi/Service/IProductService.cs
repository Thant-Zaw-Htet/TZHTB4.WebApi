using TZHTB4.WebApi.Dto;

namespace TZHTB4.WebApi.Service
{
    public interface IProductService
    {
        ProductCreateResponseDto CreateProduct(ProductCreateRequestDto productCreateRequestDto);
        ProductListResponeDto GetAllProductPaging(int pageNo, int pageSize);
        ProductByIdResponseDto GEtProductById(int id);
        ProductUpdateResponseDto UpdateProduct(int id, ProductUpdateRequestDto productUpdateRequestDto);
    }
}