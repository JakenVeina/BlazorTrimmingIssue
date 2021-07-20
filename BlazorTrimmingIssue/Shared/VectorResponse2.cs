using ProtoBuf;

namespace BlazorTrimmingIssue.Shared
{
    [ProtoContract]
    public class VectorResponse2
    {
        public VectorResponse2(ScalarResponse[] scalars)
            => Scalars = scalars;

        public VectorResponse2()
            => Scalars = null!;

        [ProtoMember(1)]
        public ScalarResponse[] Scalars { get; init; }
    }
}
