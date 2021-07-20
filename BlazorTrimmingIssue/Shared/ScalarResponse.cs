using ProtoBuf;

namespace BlazorTrimmingIssue.Shared
{
    [ProtoContract]
    public class ScalarResponse
    {
        public ScalarResponse(
            int     id,
            string  value)
        {
            Id      = id;
            Value   = value;
        }

        private ScalarResponse()
            => Value = null!;

        [ProtoMember(1)]
        public int Id { get; init; }

        [ProtoMember(2)]
        public string Value { get; init; }
    }
}
