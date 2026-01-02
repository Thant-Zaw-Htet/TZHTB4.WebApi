namespace TZHTB4.WebApi.Dto
{
    public class ProductByIdResponseDto
    {
        public bool  IsSuccess { get; set; }
        public string Message { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
