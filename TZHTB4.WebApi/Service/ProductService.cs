using TZHTB4.WebApi.Database.AppDbContextModels;
using TZHTB4.WebApi.Dto;

namespace TZHTB4.WebApi.Service
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _db;

        public ProductService(AppDbContext db)
        {
            _db = db;
        }

        public ProductListResponeDto GetAllProductPaging(int pageNo, int pageSize)
        {
            if (pageNo <= 0)
            {
                return new ProductListResponeDto
                {
                    IsSuccess = false,
                    tblProducts = new List<TblProduct>(),
                    Message = "Invalid page number.",
                };
            }
            if (pageSize <= 0)
            {
                return new ProductListResponeDto
                {
                    IsSuccess = false,
                    tblProducts = new List<TblProduct>(),
                    Message = "Invalid page number.",
                };
            }
            var result = _db.TblProducts
                .OrderByDescending(x => x.ProductId)
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            if (result == null)
            {
                return new ProductListResponeDto
                {
                    IsSuccess = false,
                    tblProducts = new List<TblProduct>(),
                    Message = "No Data Found",
                };
            }
            var respone = new ProductListResponeDto
            {
                IsSuccess = true,
                tblProducts = result,
                Message = "Data Found"
            };

            return respone;

        }
        public ProductCreateResponseDto CreateProduct(ProductCreateRequestDto productCreateRequestDto)
        {
            var item = new TblProduct
            {
                ProductName = productCreateRequestDto.ProductName,
                Price = productCreateRequestDto.Price,
                Quantity = productCreateRequestDto.Quantity,
                CreatedDateTime = DateTime.Now


            };

            _db.Add(item);
            var result = _db.SaveChanges();
            string message = result > 0 ? "Saved Successful" : "Saved Fail";
            ProductCreateResponseDto respone = new ProductCreateResponseDto
            {
                IsSuccess = true,
                Message = message,
                ProductName = item.ProductName,
                Price = item.Price,
                Quantity = item.Quantity,
                CreatedDateTime = item.CreatedDateTime,

            };

            return respone;
        }
        public ProductUpdateResponseDto UpdateProduct(int id, ProductUpdateRequestDto productUpdateRequestDto)
        {
            var item = _db.TblProducts.FirstOrDefault(x => x.ProductId == id);
            if (item == null)
            {
                return new ProductUpdateResponseDto
                {
                    IsSuccess = false,
                    Message = "Product not found",

                };
            }
            item.ProductName = productUpdateRequestDto.ProductName;
            item.Price = productUpdateRequestDto.Price;
            item.Quantity = productUpdateRequestDto.Quantity;
            item.ModifiedDateTime = DateTime.Now;

            _db.Update(item);
            var result = _db.SaveChanges();
            string message = result > 0 ? "Updated Successful" : "Updated Failed";
            var response = new ProductUpdateResponseDto
            {
                IsSuccess = true,
                Message = message,
                ProductName = item.ProductName,
                Price = item.Price,
                Quantity = item.Quantity,
                ModifiedDateTime = item.ModifiedDateTime ?? default
            };


            return response;
        }

        public ProductByIdResponseDto GEtProductById(int id)
        {
            var productId = _db.TblProducts.FirstOrDefault(x => x.ProductId == id);
            if (productId == null)
            {
                return new ProductByIdResponseDto
                {
                    IsSuccess = false,
                    Message = "Product Not Found"

                };
            }
            var response = new ProductByIdResponseDto
            {
                IsSuccess = true,
                Message = "Product Found",
                ProductId = productId.ProductId,
                ProductName = productId.ProductName,
                Price = productId.Price,
                Quantity = productId.Quantity,

            };
            return response;

        }
    }
}
