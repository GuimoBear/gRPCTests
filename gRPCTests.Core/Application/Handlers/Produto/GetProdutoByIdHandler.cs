using gRPCTests.Core.Application.Requests;
using gRPCTests.Core.Domain.Entities;
using gRPCTests.Core.Domain.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace gRPCTests.Core.Application.Handlers
{
    public class GetProdutoByIdHandler : IRequestHandler<GetProdutoByIdRequest, Produto>
    {
        private readonly ProdutoService service;
        public GetProdutoByIdHandler(ProdutoService service)
            => this.service = service;

        public async Task<Produto> Handle(GetProdutoByIdRequest request, CancellationToken cancellationToken)
            => await service.ById(request.Id);
    }
}
