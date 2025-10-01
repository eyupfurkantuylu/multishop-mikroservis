using MultiShop.Order.Application.Features.CQRS.Results.OrderDetailResults;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.OrderDeteailHandlers;
public class GetOrderDetailQueryHandler
{
    private readonly IRepository<OrderDetail> _repositroy;

    public GetOrderDetailQueryHandler(IRepository<OrderDetail> repositroy)
    {
        _repositroy = repositroy;
    }

    public async Task<List<GetOrderDetailByIdQueryResult>> Handle()
    {
        var values = await _repositroy.GetAllAsync();
        return values.Select(x => new GetOrderDetailByIdQueryResult
        {
            OrderDetailId = x.OrderDetailId,
            ProductId = x.ProductId,
            ProductName = x.ProductName,
            ProductPrice = x.ProductPrice,
            ProductAmount = x.ProductAmount,
            ProductTotalPrice = x.ProductTotalPrice,
            OrderingId = x.OrderingId
        }).ToList();
    }
}
