using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Order.Application.Features.CQRS.Commands.OrderDetailCommands;
using MultiShop.Order.Application.Features.CQRS.Handlers.OrderDeteailHandlers;
using MultiShop.Order.Application.Features.CQRS.Queries.OrderDetailQueries;

namespace MultiShop.Order.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class OrderDetailsController : ControllerBase
{
    private readonly GetOrderDetailByIdQueryHandler _getOrderDetailByIdQueryHandler;
    private readonly GetOrderDetailQueryHandler _getOrderDetailQueryHandler;
    private readonly CreateOrderDetailCommandHandler _createOrderDetailCommandHandler;
    private readonly RemoveOrderDetailCommandHandler _removeOrderDetailCommandHandler;
    private readonly UpdateOrderDetailCommandHandler _updateOrderDetailCommandHandler;

    public OrderDetailsController(UpdateOrderDetailCommandHandler updateOrderDetailCommandHandler, RemoveOrderDetailCommandHandler removeOrderDetailCommandHandler, CreateOrderDetailCommandHandler createOrderDetailCommandHandler, GetOrderDetailQueryHandler getOrderDetailQueryHandler, GetOrderDetailByIdQueryHandler getOrderDetailByIdQueryHandler)
    {
        _updateOrderDetailCommandHandler = updateOrderDetailCommandHandler;
        _removeOrderDetailCommandHandler = removeOrderDetailCommandHandler;
        _createOrderDetailCommandHandler = createOrderDetailCommandHandler;
        _getOrderDetailQueryHandler = getOrderDetailQueryHandler;
        _getOrderDetailByIdQueryHandler = getOrderDetailByIdQueryHandler;
    }

    [HttpGet]
    public async Task<IActionResult> OrderDetailList()
    {
        var result = await _getOrderDetailQueryHandler.Handle();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderDetailById(int id)
    {
        var result = await _getOrderDetailByIdQueryHandler.Handle(new GetOrderDetailByIdQuery(id));
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrderDetail(CreateOrderDetailCommand command)
    {
        await _createOrderDetailCommandHandler.Handle(command);
        return Ok("Sipariş detayı başarıyla eklendi");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateOrderDetail(UpdateOrderDetailCommand command)
    {
        await _updateOrderDetailCommandHandler.Handle(command);
        return Ok("Sipariş detayı başarıyla güncellendi");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveOrderDetail(int id)
    {
        await _removeOrderDetailCommandHandler.Handle(new RemoveOrderDetailCommand(id));
        return Ok("Sipariş detayı başarıyla silindi");
    }

}
