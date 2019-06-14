using gRPCTests.Core.Application.Requests;
using gRPCTests.Core.Domain.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace gRPCTests.Core.Application.Handlers
{
    public class UpdateProdutoHandler : IRequestHandler<UpdateProdutoRequest>
    {
        private readonly ProdutoService service;
        public UpdateProdutoHandler(ProdutoService service)
            => this.service = service;
        public async Task<Unit> Handle(UpdateProdutoRequest request, CancellationToken cancellationToken)
        {
            await service.Update(request.Produto);
            return new Unit();
        }
    }
}
