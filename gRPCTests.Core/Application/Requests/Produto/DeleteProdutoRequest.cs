using gRPCTests.Core.Domain.Entities;
using MediatR;

namespace gRPCTests.Core.Application.Requests
{
    public class DeleteProdutoRequest : IRequest
    {
        public Produto Produto { get; }

        public DeleteProdutoRequest(Produto produto)
            => Produto = produto;
    }
}
