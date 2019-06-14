using gRPCTests.Core.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace gRPCTests.Core.Application.Requests
{
    public class GetAllProdutosRequest : IRequest<IEnumerable<Produto>>
    {
    }
}
