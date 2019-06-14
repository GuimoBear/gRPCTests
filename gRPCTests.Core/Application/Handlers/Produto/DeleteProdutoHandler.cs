using gRPCTests.Core.Application.Requests;
using gRPCTests.Core.Domain.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace gRPCTests.Core.Application.Handlers
{
    public class DeleteProdutoHandler : IRequestHandler<DeleteProdutoRequest>
    {
        private readonly ProdutoService service;
        public DeleteProdutoHandler(ProdutoService service)
            => this.service = service;

        public async Task<Unit> Handle(DeleteProdutoRequest request, CancellationToken cancellationToken)
        {
            await service.Delete(request.Produto);
            return new Unit();
        }
    }
}
