namespace TZHTB4.WebApi.Dto
{
    public class ProductUpdateRequestDto
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
    public class ProductUpdateResponseDto : ProductUpdateRequestDto
    { 
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public DateTime ModifiedDateTime { get; set; }
        
    }
}
