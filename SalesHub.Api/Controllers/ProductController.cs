using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SalesHub.Application.Product.Commands.Create;
using SalesHub.Application.Product.Commands.Delete;
using SalesHub.Application.Product.Commands.Update;
using SalesHub.Application.Product.Common;
using SalesHub.Application.Product.Queries.Get;
using SalesHub.Application.Services.Product.Common;
using SalesHub.Applications.Product.Common;
using SalesHub.Contracts.Product;

[ApiController]
[Route("product")]
public class ProductController : ControllerBase {

    private readonly ISender _sender;

    public ProductController(ISender sender) 
    {
        _sender = sender;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateProduct(CreateProductRequest request) {

        var command = new CreateProductCommand(request.Name, request.Description, request.SKU);
        ErrorOr<CreateProductResult> result = await _sender.Send(command);

        return Ok(result);
    }

    [HttpGet("get")]
    public async Task<IActionResult> GetProduct(GetProductRequest request) {
        var query = new GetProductQuery(request.SKU);
        ErrorOr<GetProductResult> result = await _sender.Send(query);

        return Ok(result);
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateProduct(UpdateProductRequest request) {
        var command = new UpdateProductCommand(request.Id, request.Name, request.Description, request.SKU);
        ErrorOr<UpdateProductResult> result = await _sender.Send(command);

        return Ok(result);
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteProduct(DeleteProductRequest request) {
        var command = new DeleteProductCommand(request.Id);
        ErrorOr<DeleteProductResult> result = await _sender.Send(command);

        return Ok(result);
    }

}