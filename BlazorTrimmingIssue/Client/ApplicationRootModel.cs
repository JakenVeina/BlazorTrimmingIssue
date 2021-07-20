using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

using BlazorTrimmingIssue.Shared;

namespace BlazorTrimmingIssue.Client
{
    public class ApplicationRootModel
    {
        public ApplicationRootModel(ITestContract testContract)
            => _testContract = testContract;

        public bool IsTesting { get; set; }

        public string ScalarResponseText { get; set; }
            = string.Empty;

        public string VectorResponseText { get; set; }
            = string.Empty;

        public string VectorResponse2Text { get; set; }
            = string.Empty;

        public string StreamingResponseText { get; set; }
            = string.Empty;

        public event Action? StateChanged;

        public Task TestScalarResponseAsync()
            => DoTestAsync(async () =>
            {
                var response = await _testContract.GetScalarResponseAsync();

                ScalarResponseText = JsonSerializer.Serialize(response);

                StateChanged?.Invoke();
            });

        public Task TestVectorResponseAsync()
            => DoTestAsync(async () =>
            {
                var response = await _testContract.GetVectorResponseAsync();

                VectorResponseText = JsonSerializer.Serialize(response);

                StateChanged?.Invoke();
            });

        public Task TestVectorResponse2Async()
            => DoTestAsync(async () =>
            {
                var response = await _testContract.GetVectorResponse2Async();

                VectorResponse2Text = JsonSerializer.Serialize(response);

                StateChanged?.Invoke();
            });

        public Task TestStreamingResponseAsync()
            => DoTestAsync(async () =>
            {
                StreamingResponseText = string.Empty;
                StateChanged?.Invoke();

                var responses = new List<ScalarResponse>();
                await foreach (var response in _testContract.GetStreamingResponse())
                {
                    responses.Add(response);

                    StreamingResponseText = JsonSerializer.Serialize(responses);

                    StateChanged?.Invoke();
                }
            });

        private async Task DoTestAsync(Func<Task> testAsync)
        {
            IsTesting = true;
            StateChanged?.Invoke();
            try
            {
                await testAsync();
            }
            finally
            {
                IsTesting = false;
                StateChanged?.Invoke();
            }
        }

        private readonly ITestContract _testContract;
    }
}
