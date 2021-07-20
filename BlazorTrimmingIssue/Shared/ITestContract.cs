using System.Collections.Generic;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorTrimmingIssue.Shared
{
    [ServiceContract(Name = "Test")]
    public interface ITestContract
    {
        [OperationContract(Name = "GetScalarResponse")]
        Task<ScalarResponse> GetScalarResponseAsync();

        [OperationContract(Name = "GetVectorResponse")]
        Task<VectorResponse> GetVectorResponseAsync();

        [OperationContract(Name = "GetVectorResponse2")]
        Task<VectorResponse2> GetVectorResponse2Async();

        [OperationContract(Name = "GetStreamingResponse")]
        IAsyncEnumerable<ScalarResponse> GetStreamingResponse();
    }
}
