using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;

using BlazorTrimmingIssue.Shared;

namespace BlazorTrimmingIssue.Server
{
    public class TestContract
        : ITestContract
    {
        public Task<ScalarResponse> GetScalarResponseAsync()
            => Task.FromResult(new ScalarResponse(1, "Value 1"));

        public Task<VectorResponse> GetVectorResponseAsync()
        {
            return Task.FromResult(new VectorResponse(
                ImmutableArray.Create(
                    new ScalarResponse(2, "Value 2"),
                    new ScalarResponse(3, "Value 3"),
                    new ScalarResponse(4, "Value 4"))));
        }

        public Task<VectorResponse2> GetVectorResponse2Async()
        {
            return Task.FromResult(new VectorResponse2(
                new[]
                {
                    new ScalarResponse(5, "Value 5"),
                    new ScalarResponse(6, "Value 6"),
                    new ScalarResponse(7, "Value 7")
                }));
        }

        public async IAsyncEnumerable<ScalarResponse> GetStreamingResponse()
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            yield return new ScalarResponse(8, "Value 8");
            await Task.Delay(TimeSpan.FromSeconds(1));
            yield return new ScalarResponse(9, "Value 9");
            await Task.Delay(TimeSpan.FromSeconds(1));
            yield return new ScalarResponse(10, "Value 10");
            await Task.Delay(TimeSpan.FromSeconds(1));
        }
    }
}
