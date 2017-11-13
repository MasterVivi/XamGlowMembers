using System.Collections.Generic;
using System.Threading.Tasks;
using members.Core.Models;

namespace members.Core.Services.Network
{
    public interface IAPIService
    {
        Task<ResponseWrapper<List<Member>>> GetMembers(int page, string email = null, 
                                                       string firstName = null, string surname = null);
    }
}