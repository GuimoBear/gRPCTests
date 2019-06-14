using gRPCTests.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace gRPCTests.Core.Infrastructure.Contexts
{
    public class CadastroContext : DbContext
    {
        public DbSet<Produto> Produtos { get; private set; }

        public CadastroContext()
        { }

        public CadastroContext(DbContextOptions<CadastroContext> options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseInMemoryDatabase("main");
        }
    }
}
