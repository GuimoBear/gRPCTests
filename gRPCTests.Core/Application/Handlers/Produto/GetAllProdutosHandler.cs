using gRPCTests.Core.Application.Requests;
using gRPCTests.Core.Domain.Entities;
using gRPCTests.Core.Domain.Services;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace gRPCTests.Core.Application.Handlers
{
    public class GetAllProdutosHandler : IRequestHandler<GetAllProdutosRequest, IEnumerable<Produto>>
    {
        private readonly ProdutoService service;
        public GetAllProdutosHandler(ProdutoService service)
            => this.service = service;

        public async Task<IEnumerable<Produto>> Handle(GetAllProdutosRequest request, CancellationToken cancellationToken)
            => await service.All();
    }
}
