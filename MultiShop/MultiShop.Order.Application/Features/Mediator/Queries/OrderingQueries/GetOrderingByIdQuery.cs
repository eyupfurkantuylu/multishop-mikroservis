using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.Mediator.Queries.OrderingQueries;
public class GetOrderingByIdQuery : IRequest<GetOrderingByIdQuery>
{
    public int Id { get; set; }

    public GetOrderingByIdQuery(int id)
    {
        Id = id;
    }
}
