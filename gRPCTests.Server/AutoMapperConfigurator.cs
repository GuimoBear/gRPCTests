using AutoMapper;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace gRPCTests.Server
{
    public class AutoMapperConfigurator
    {
        public static MapperConfiguration MapperConfiguration()
        {
            return new MapperConfiguration(x =>
            {
                x.CreateMap<Guid, ByteString>()
                 .ConstructUsing(guid => 
                    ByteString.CopyFrom(guid.ToByteArray()));
                x.CreateMap<Guid?, ByteString>()
                 .ConstructUsing(guid => 
                    !guid.HasValue ? ByteString.CopyFrom(Guid.Empty.ToByteArray()) : ByteString.CopyFrom(guid.Value.ToByteArray()));
                x.CreateMap<ByteString, Guid>()
                 .ConstructUsing(bytes =>
                    bytes.Equals(ByteString.Empty) ? Guid.Empty : new Guid(bytes.ToArray()));
                x.CreateMap<ByteString, Guid?>()
                 .ConstructUsing(bytes => 
                    new Guid(bytes.ToArray()).Equals(Guid.Empty) ? (Guid?)null : new Guid(bytes.ToArray()));

                x.CreateMap<DateTime, Timestamp>()
                 .ConstructUsing(dt => 
                    Timestamp.FromDateTime(dt.ToUniversalTime()));
                x.CreateMap<DateTime?, Timestamp>()
                 .ConstructUsing(dt => 
                    !dt.HasValue ? default : Timestamp.FromDateTime(dt.Value.ToUniversalTime()));
                x.CreateMap<Timestamp, DateTime>()
                 .ConstructUsing(ts => 
                    ts.ToDateTime().ToLocalTime());
                x.CreateMap<Timestamp, DateTime?>()
                 .ConstructUsing(ts => 
                    ts == default ? (DateTime?)null : ts.ToDateTime().ToLocalTime());

                x.CreateMap<Proto.Services.Produto, Core.Domain.Entities.Produto>();
                x.CreateMap<Core.Domain.Entities.Produto, Proto.Services.Produto>();

                x.CreateMap<IEnumerable<Core.Domain.Entities.Produto>, Proto.Services.Produtos>()
                    .ConstructUsing((produtos, rc) =>
                    {
                        var protoProdutos = new Proto.Services.Produtos();
                        protoProdutos.Produtos_.AddRange(rc.Mapper.Map<IEnumerable<Proto.Services.Produto>>(produtos));
                        return protoProdutos;
                    });
            });
        }
    }
}
