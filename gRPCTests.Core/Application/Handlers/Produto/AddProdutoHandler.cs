using gRPCTests.Core.Application.Requests;
using gRPCTests.Core.Domain.Entities;
using gRPCTests.Core.Domain.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace gRPCTests.Core.Application.Handlers
{
    public class AddProdutoHandler : IRequestHandler<AddProdutoRequest, Produto>
    {
        private readonly ProdutoService service;
        public AddProdutoHandler(ProdutoService service)
            => this.service = service;

        public async Task<Produto> Handle(AddProdutoRequest request, CancellationToken cancellationToken)
        {
            await service.Add(request.Produto);
            return request.Produto;
        }
    }
}
