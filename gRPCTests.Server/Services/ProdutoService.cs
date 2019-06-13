using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace gRPCTests.Server.Services
{
    public class ProdutoService : ProdutoHandler.ProdutoHandlerBase
    {
        private static ICollection<Produto> produtos = new List<Produto>();

        public override Task<Produto> Add(Produto produto, ServerCallContext context)
        {
            produto.Id = new UUID();
            produto.Id.Data = Google.Protobuf.ByteString.CopyFrom(Guid.NewGuid().ToByteArray());
            produtos.Add(produto);
            return Task.FromResult(produto);
        }

        public override Task<Produtos> GetAll(Empty request, ServerCallContext context)
        {
            var prods = new Produtos();
            prods.Produtos_.AddRange(produtos);
            return Task.FromResult(prods);
        }

        public override Task<Produto> GetById(UUID id, ServerCallContext context)
        {
            return Task.FromResult(produtos.FirstOrDefault(p => p.Id.Equals(id)));
        }

        public override Task<Empty> Update(Produto produto, ServerCallContext context)
        {
            var produtoInDatabase = produtos.FirstOrDefault(p => p.Id.Equals(produto.Id));
            if(!(produtoInDatabase is null))
            {
                produtoInDatabase.Descricao = produto.Descricao;
                produtoInDatabase.PrecoCompra = produto.PrecoCompra;
                produtoInDatabase.PrecoVenda = produto.PrecoVenda;
                produtoInDatabase.Validade = produto.Validade;
            }
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> Delete(Produto produto, ServerCallContext context)
        {
            var produtoInDatabase = produtos.FirstOrDefault(p => p.Id.Equals(produto.Id));
            if (!(produtoInDatabase is null))
                produtos.Remove(produtoInDatabase);
            return Task.FromResult(new Empty());
        }
    }
}
