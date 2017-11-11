using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Refit;

namespace members.Core.Services.Proxy
{
    [Headers("Authorization: Bearer", "Accept: application/json")]
    public interface IMembersProxy
    {
        [Get("/members")]
        Task<HttpResponseMessage> GetAll(CancellationToken token, int? page = null,
                                        string email = null, string first_name = null, string last_name = null);
    }
}