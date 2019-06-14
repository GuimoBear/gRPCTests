using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using gRPCTests.Proto.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace gRPCTests.Client
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var empty = new Empty();

            Thread.Sleep(TimeSpan.FromSeconds(10));

            var channel = new Channel("localhost:50051", ChannelCredentials.Insecure, new[] {
                new ChannelOption(ChannelOptions.MaxReceiveMessageLength , 5*1024*1024),
                new ChannelOption(ChannelOptions.MaxSendMessageLength , 1*1024*1024)
            });

            var client = new ProdutoService.ProdutoServiceClient(channel);

            for (int i = 0; i < 8; i++)
            {
                var produto1 = Produto1();
                var produto2 = Produto2();

                await client.AddAsync(produto1);
                await client.AddAsync(produto2);
                await client.AddAsync(Produto3());
            }

            var produtos = await client.AllAsync(empty);

            var produto2Inserido = produtos.Produtos_[5];

            produto2Inserido.PrecoVenda = 3.15;

            await client.UpdateAsync(produto2Inserido);

            var novosProdutos = await client.AllAsync(empty);

            var produto1Inserido = produtos.Produtos_[8];

            await client.DeleteAsync(produto1Inserido);

            var novosProdutos2 = await client.AllAsync(empty);

            var produto3Inserido = produtos.Produtos_[6];

            var produto3 = await client.ByIdAsync(new IdProduto() { Id = produto3Inserido.Id });
        }

        private static Produto Produto1()
        {
            return new Produto()
            {
                Id = ByteString.Empty,
                Descricao = "Shampoo Loreal Paris 280g",
                DescricaoReduzida = "Shampoo Loreal Paris",
                PrecoCompra = 4.75,
                PrecoVenda = 8,
                Validade = Timestamp.FromDateTime(new DateTime(2019, 11, 21).ToUniversalTime())
            };
        }

        private static Produto Produto2()
        {
            return new Produto()
            {
                Id = ByteString.Empty,
                Descricao = "Sabonete Senador classic 90g",
                DescricaoReduzida = "Sab. Senador classic",
                PrecoCompra = 1.25,
                PrecoVenda = 2.85,
                Validade = Timestamp.FromDateTime(new DateTime(2020, 05, 10).ToUniversalTime())
            };
        }

        private static Produto Produto3()
        {
            return new Produto()
            {
                Id = ByteString.Empty,
                Descricao = "Creme dental Colgate Total 12 120g",
                DescricaoReduzida = "Creme d. Colgate Total 12",
                PrecoCompra = 7.40,
                PrecoVenda = 11.15,
                Validade = Timestamp.FromDateTime(new DateTime(2021, 7, 29).ToUniversalTime())
            };
        }
    }
}
