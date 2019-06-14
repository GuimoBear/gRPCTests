using gRPCTests.Core.Domain.Entities;
using MediatR;

namespace gRPCTests.Core.Application.Requests
{
    public class AddProdutoRequest : IRequest<Produto>
    {
        public Produto Produto { get; }
        public AddProdutoRequest(Produto produto)
            => Produto = produto;
    }
}
