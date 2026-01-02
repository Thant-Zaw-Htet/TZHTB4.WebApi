namespace TZHTB4.WebApi.Dto
{
    public class ProductCreateRequestDto
    {
  
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
 
    }

    public class ProductCreateResponseDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }

}
