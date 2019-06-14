using gRPCTests.Core.Domain.Entities;
using MediatR;
using System;

namespace gRPCTests.Core.Application.Requests
{
    public class GetProdutoByIdRequest : IRequest<Produto>
    {
        public Guid Id { get; }
        public GetProdutoByIdRequest(Guid id)
            => Id = id;
    }
}
