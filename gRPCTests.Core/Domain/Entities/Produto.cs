using System;
using System.Collections.Generic;
using System.Text;

namespace gRPCTests.Core.Domain.Entities
{
    public class Produto
    {
        public virtual Guid Id { get; set; }
        public virtual bool Enabled { get; protected set; }
        public virtual DateTime CreatedAt { get; protected set; }
        public virtual DateTime? ModifiedAt { get; protected set; }
        public string Descricao { get; private set; }
        public double PrecoVenda { get; private set; }
        public double PrecoCompra { get; private set; }
        public DateTime Validade { get; private set; }

        private Produto() { }

        public Produto(Guid id, bool enabled, DateTime createdAt, DateTime? modifiedAt, string descricao, double precoVenda, double precoCompra, DateTime validade)
        {
            Id = id;
            Enabled = enabled;
            CreatedAt = createdAt;
            ModifiedAt = modifiedAt;
            Descricao = descricao;
            PrecoVenda = precoVenda;
            PrecoCompra = precoCompra;
            Validade = validade;
        }
    }
}
