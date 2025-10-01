using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands;
using MultiShop.Order.Application.Features.CQRS.Handlers.AddressHandlers;
using MultiShop.Order.Application.Features.CQRS.Queries.AddressQueries;

namespace MultiShop.Order.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AddressesController : ControllerBase
{
    private readonly GetAddressByIdQueryHandler _getAddressByIdQueryHandler;
    private readonly GetAddressQueryHandler _getAddressQueryHandler;
    private readonly CreateAddressCommandHandler _createAddressCommandHandler;
    private readonly UpdateAddressCommandHandler _updateAddressCommandHandler;
    private readonly RemoveAddressCommandHandler _removeAddressCommandHandler;

    public AddressesController(GetAddressByIdQueryHandler getAddressByIdQueryHandler, GetAddressQueryHandler getAddressQueryHandler, CreateAddressCommandHandler createAddressCommandHandler, RemoveAddressCommandHandler removeAddressCommandHandler, UpdateAddressCommandHandler updateAddressCommandHandler)
    {
        _getAddressByIdQueryHandler = getAddressByIdQueryHandler;
        _getAddressQueryHandler = getAddressQueryHandler;
        _createAddressCommandHandler = createAddressCommandHandler;
        _removeAddressCommandHandler = removeAddressCommandHandler;
        _updateAddressCommandHandler = updateAddressCommandHandler;
    }

    [HttpGet]
    public async Task<IActionResult> AddressList()
    {
        var result = await _getAddressQueryHandler.Handle();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> AddressById(int id)
    {
        var result = await _getAddressByIdQueryHandler.Handle(new GetAddressByIdQuery(id));
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAdress(CreateAddressCommand command)
    {
        _createAddressCommandHandler.Handle(command);
        return Ok("Adres bilgisi başarıyla eklendi");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAddress(UpdateAddressCommand command)
    {
        await _updateAddressCommandHandler.Handle(command);
        return Ok("Adres bilgisi başarıyla güncellendi");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveAddress(int id)
    {
        await _removeAddressCommandHandler.Handle(new RemoveAddressCommand(id));
        return Ok("Adres bilgisi başarıyla silindi");
    }

}
