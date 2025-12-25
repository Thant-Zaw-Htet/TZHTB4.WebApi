using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TZHTB4.WebApi.Database.AppDbContextModels;

namespace TZHTB4.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly AppDbContext _db;
    public ProductController()
    {
        _db = new AppDbContext();
    }
    [HttpGet]
    public IActionResult GetAllProduct()
    {
        var result = _db.TblProducts.ToList();
        return Ok(result);
    }
    [HttpGet("{pageNo}/{pageSize}")]
    public IActionResult GeProductPaging(int pageNo , int pageSize)
    {
        var result = _db.TblProducts.OrderByDescending(x => x.ProductId).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
        var respone = new ProductGetListResponseDto
        {
            IsSuccess = true,
            Message = "Data found",
            ProductDtos = result

        };
        return Ok(respone);
    }
    [HttpGet("{id}")]
    public IActionResult GetAllProductById(int id)
    {
        var result = _db.TblProducts.Where(x => x.ProductId == id).FirstOrDefault();
        if (result == null)
        {
            return NotFound(new { Message = "No Data Found" });
        }
        return Ok(result);
    }

    [HttpPost]
    public IActionResult CreateProduct(ProductCreateRequestDto requestDto)
    {
        TblProduct tbl = new TblProduct
        {
            ProductName = requestDto.ProductName,
            Price = requestDto.Price,
            Quantity = requestDto.Quantity,
            CreatedDateTime = DateTime.Now

        };
        _db.Add(tbl);
        var result = _db.SaveChanges();
        var message = result > 0 ? "Saved Successfully." : "Saved Fail";

        var response = new ProductCreateResponseDto
        {
            IsSuccess = true,
            Message = message
        };

        return Ok(response);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateProduct(int id, ProductUpdateRequestDto requestDto)
    {
        
        var updateProduct = _db.TblProducts.Where(x => x.ProductId == id).FirstOrDefault();
        if(updateProduct == null)
        {
            return NotFound(new {Message = "No data found."});
        }
        updateProduct.ProductName = requestDto.ProductName;
        updateProduct.Price = requestDto.Price;
        updateProduct.Quantity = requestDto.Quantity;
        updateProduct.ModifiedDateTime = DateTime.Now;
        _db.Update(updateProduct);
        var result = _db.SaveChanges();
        var message = result > 0 ? "Updated Successfully." : "Updated Failed";
        var response = new ProductUpdateResponseDto
        {
            IsSuccess = true,
            Message = message
            
        };

        return Ok(response);
    }

    [HttpPatch("{id}")]
    public IActionResult PatchProduct(int id, ProductPatchRequestDto requestDto)
    {
        var patchProduct = _db.TblProducts.Where(x => x.ProductId == id).FirstOrDefault();
        if (patchProduct == null)
        {
            return NotFound(new { Message = "No data found." });
        }
        if(patchProduct.ProductName != null)
        {
            patchProduct.ProductName = requestDto.ProductName;
        }
        if(patchProduct.Price != null && patchProduct.Price > 0)
        {
            patchProduct.Price = Convert.ToDecimal(requestDto.Price);
        }
        if(patchProduct.Quantity != null && patchProduct.Quantity > 0)
        {
            patchProduct.Quantity = Convert.ToInt32(requestDto.Quantity);
        }
        patchProduct.ModifiedDateTime = DateTime.Now;
        var result = _db.SaveChanges();
        var message = result > 0 ? "Patch Successfully." : "Patch Failed";
        var response = new ProductPatchResponseDto
        {
            IsSuccess = true,
            Message = message

        };

        return Ok(response);
    }

    [HttpDelete("{id} ")]
    public IActionResult DeleteProduct(int id)
    {
        var deleteProduct = _db.TblProducts.FirstOrDefault(x => x.ProductId == id);
        if(deleteProduct == null)
        {
            return NotFound(new { Message = "No data found." });
        }
        deleteProduct.IsDelete = true;
        _db.Update(deleteProduct);
        var result = _db.SaveChanges();
        var message = result > 0 ? "Delete Successfully" : "Delete Failed";
        var response = new ProductDeleteResponeDto
        {
            IsSuccess = true,
            Message = message
        };

        return Ok(response);
    }
}

public class ProductGetListResponseDto
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public List<TblProduct> ProductDtos { get; set; }
}


public class ProductCreateRequestDto
{
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
public class ProductUpdateRequestDto : ProductCreateRequestDto
{

}
public class ProductCreateResponseDto
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
}

public class ProductUpdateResponseDto
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
}

public class ProductPatchRequestDto
{

    public string? ProductName { get; set; }
    public decimal? Price { get; set; }
    public int? Quantity { get; set; }
}


public class ProductPatchResponseDto
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
}

public class ProductDeleteResponeDto
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
}


