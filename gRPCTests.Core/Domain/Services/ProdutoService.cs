using gRPCTests.Core.Domain.Entities;
using gRPCTests.Core.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace gRPCTests.Core.Domain.Services
{
    public class ProdutoService
    {
        private class ProdutoProxy : Produto
        {
            public new bool Enabled { get => base.Enabled; set => base.Enabled = value; }
            public new DateTime CreatedAt { get => base.CreatedAt; set => base.CreatedAt = value; }
            public new DateTime? ModifiedAt { get => base.ModifiedAt; set => base.ModifiedAt = value; }

            public ProdutoProxy(Produto produto)
                : base(produto.Id, produto.Enabled, produto.CreatedAt, produto.ModifiedAt, produto.Descricao, produto.PrecoVenda, produto.PrecoCompra, produto.Validade)
            {

            }
        }

        private readonly ProdutoRepository repo;

        public ProdutoService(ProdutoRepository repo)
            => this.repo = repo;

        public async Task Add(Produto produto)
        {
            var produtoProxy = new ProdutoProxy(produto);
            produtoProxy.CreatedAt = DateTime.Now;
            await repo.Add(produtoProxy, true, true);
            produto.Id = produtoProxy.Id;
        }

        public async Task<IEnumerable<Produto>> All()
            => await repo.All(false);

        public async Task<Produto> ById(Guid id)
            => await repo.ById(id);

        public async Task Update(Produto produto)
        {
            var produtoNoDatabase = await ById(produto.Id);
            if (!(produtoNoDatabase is null))
            {
                var produtoProxy = new ProdutoProxy(produto);
                produtoProxy.CreatedAt = produtoNoDatabase.CreatedAt;
                produtoProxy.ModifiedAt = DateTime.Now;
                await repo.Update(produtoProxy);
            }
        }

        public async Task Delete(Produto produto)
            => await repo.Delete(produto);
    }
}
