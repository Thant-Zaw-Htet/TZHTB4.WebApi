using TZHTB4.WebApi.Database.AppDbContextModels;

namespace TZHTB4.WebApi.Dto
{
    public class ProductListResponeDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<TblProduct>  tblProducts { get; set; }
    }
}
