using System.Collections.Immutable;

using ProtoBuf;

namespace BlazorTrimmingIssue.Shared
{
    [ProtoContract]
    public class VectorResponse
    {
        public VectorResponse(ImmutableArray<ScalarResponse> scalars)
            => Scalars = scalars;

        public VectorResponse()
            => Scalars = ImmutableArray<ScalarResponse>.Empty;

        [ProtoMember(1)]
        public ImmutableArray<ScalarResponse> Scalars { get; init; }
    }
}
