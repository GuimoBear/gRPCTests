using gRPCTests.Core.Domain.Entities;
using MediatR;

namespace gRPCTests.Core.Application.Requests
{
    public class UpdateProdutoRequest : IRequest
    {
        public Produto Produto { get; }

        public UpdateProdutoRequest(Produto produto)
            => Produto = produto;
    }
}
