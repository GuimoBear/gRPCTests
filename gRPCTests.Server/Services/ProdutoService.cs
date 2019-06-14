using System;
using System.Threading.Tasks;
using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using gRPCTests.Core.Application.Requests;
using gRPCTests.Proto.Services;
using MediatR;

namespace gRPCTests.Server.Services
{
    public class ProdutoService : Proto.Services.ProdutoService.ProdutoServiceBase
    {
        private static readonly Empty empty = new Empty();

        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public ProdutoService(IMapper mapper, IMediator mediator)
        {
            this.mapper = mapper;
            this.mediator = mediator;
        }

        public override async Task<Produto> Add(Produto produto, ServerCallContext context)
        {
            try
            {
                var domainProduto = mapper.Map<Core.Domain.Entities.Produto>(produto);
                var prod = await mediator.Send(new AddProdutoRequest(domainProduto));
                return mapper.Map<Produto>(prod);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocorreu um erro ao adicionar o produto: {ex.Message}", ex);
            }
        }

        public override async Task<Produtos> All(Empty request, ServerCallContext context)
        {
            try
            { 
                var domainProdutos = await mediator.Send(new GetAllProdutosRequest());
                return mapper.Map<Produtos>(domainProdutos);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocorreu um erro ao listar todos os produtos: {ex.Message}", ex);
            }
        }

        public override async Task<Produto> ById(IdProduto idProduto, ServerCallContext context)
        {
            try
            {
                var id = mapper.Map<Guid>(idProduto.Id);
                var domainProduto = await mediator.Send(new GetProdutoByIdRequest(id));
                var protoProduto = mapper.Map<Produto>(domainProduto);
                return protoProduto;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocorreu um erro ao pegar o produto pelo id: {ex.Message}", ex);
            }
        }

        public override async Task<Empty> Update(Produto produto, ServerCallContext context)
        {
            try
            {
                var domainProduto = mapper.Map<Core.Domain.Entities.Produto>(produto);
                await mediator.Send(new UpdateProdutoRequest(domainProduto));
                return empty;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocorreu um erro ao atualizar o produto: {ex.Message}", ex);
            }
        }

        public override async Task<Empty> Delete(Produto produto, ServerCallContext context)
        {
            try
            {
                var domainProduto = mapper.Map<Core.Domain.Entities.Produto>(produto);
                await mediator.Send(new DeleteProdutoRequest(domainProduto));
                return empty;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocorreu um erro ao deletar o produto: {ex.Message}", ex);
            }
        }
    }
}
