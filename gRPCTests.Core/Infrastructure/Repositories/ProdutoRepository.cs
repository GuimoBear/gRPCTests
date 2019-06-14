using gRPCTests.Core.Domain.Entities;
using gRPCTests.Core.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gRPCTests.Core.Infrastructure.Repositories
{
    public class ProdutoRepository
    {
        private readonly CadastroContext context;
        public ProdutoRepository(CadastroContext context)
        {
            this.context = context;
        }

        public async Task Add(Produto produto, bool saveChanges = true, bool removeTrackingAfterAdd = true)
        {
            var entry = await context.Produtos.AddAsync(produto);
            if(saveChanges)
                await context.SaveChangesAsync();
            if (removeTrackingAfterAdd)
                entry.State = EntityState.Detached;
        }

        public Task<IEnumerable<Produto>> All(bool tracking = false)
        {
            if (tracking)
                return Task.FromResult(context.Produtos.AsEnumerable());
            return Task.FromResult(context.Produtos.AsNoTracking().AsEnumerable());
        }

        public async Task<Produto> ById(Guid id, bool tracking = false)
        {
            if (tracking)
                return await context.Produtos.FindAsync(id);
            return await context.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.Id.Equals(id));
        }

        public async Task Update(Produto produto, bool saveChanges = true, bool removeTrackingAfterUpdate = true)
        {
            var entry = context.Produtos.Update(produto);
            if (saveChanges)
                await context.SaveChangesAsync();
            if (removeTrackingAfterUpdate)
                entry.State = EntityState.Detached;
        }

        public async Task Delete(Produto produto, bool saveChanges = true, bool removeTrackingAfterDelete = true)
        {
            var entry = context.Produtos.Remove(produto);
            if (saveChanges)
                await context.SaveChangesAsync();
            if (removeTrackingAfterDelete)
                entry.State = EntityState.Detached;
        }
    }
}
