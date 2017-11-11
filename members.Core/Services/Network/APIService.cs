using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using members.Core.Models;
using members.Core.Models.DTOs;
using members.Core.Services.Proxy;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Oneview.Connect;
using Refit;

namespace members.Core.Services.Network
{
    public class APIService : IAPIService
    {
        /// <summary>
        /// The members service.
        /// </summary>
        private readonly Lazy<IMembersProxy> _membersService;
        public IMembersProxy MembersService
        {
            get { return _membersService.Value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:members.Core.Services.Network.APIService"/> class.
        /// </summary>
        /// <remarks>
        /// Pass the token to the HTTP handler
        /// Create the Refit services needed for our calls
        /// </remarks>
        public APIService()
        {
            var authenticatedHandler = new AuthenticatedHttpClientHandler("eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJzYW5kYm9" +
                                                                          "4Lmdsb2ZveC5jb20iLCJleHAiOjE1MTExOTIyNzYsIm" +
                                                                          "lhdCI6MTUwODUxMzg3NiwiaXNzIjoic2FuZGJveC5nbG9" +
                                                                          "mb3guY29tIiwibmJmIjoxNTA4NTEzODc2LCJ1c2VyIjp7I" +
                                                                          "l9pZCI6IjU5MTFhYzlhMTYzZDk2M2EwMjAwMDAwMCIsIm5" +
                                                                          "hbWVzcGFjZSI6InRoZXdvZGZhY3RvcnkiLCJicmFuY2hfa" +
                                                                          "WQiOiI1NmNkYzAxNTVjNDZiYjE3NmJiOTI1ODIiLCJmaX" +
                                                                          "JzdF9uYW1lIjoiQ3VjdW1iZXIiLCJsYXN0X25hbWUiOiJ" +
                                                                          "BZG1pbiIsInR5cGUiOiJBRE1JTiIsImlzU3VwZXJBZG1p" +
                                                                          "biI6ZmFsc2V9fQ.cVEXvLx0xhkXHLn_XbQj-8iU3bG3Vsn" +
                                                                          "4vZbtQlD60PE");
            var httpClient = new HttpClient(authenticatedHandler) { BaseAddress = new Uri("https://sandbox.glofox.com/2.0") };

            // Only instantiate when needed
            _membersService = new Lazy<IMembersProxy>(() => RestService.For<IMembersProxy>(httpClient));
        }

        /// <summary>
        /// Gets the members.
        /// </summary>
        /// <returns>The members.</returns>
        /// <param name="email">Email.</param>
        /// <param name="firstName">First name.</param>
        /// <param name="surname">Surname.</param>
        public async Task<ResponseWrapper<List<Member>>> GetMembers(int page, string email = null, 
                                                   string firstName = null, string surname = null)
        {
            // Not used here
            var cancellationToken = new CancellationTokenSource();

            // Make call for data
            var response = await MembersService.GetAll(cancellationToken.Token, page);
            // Read response
            var json = await response.Content.ReadAsStringAsync();
            // Process json
            var jsonObject = JObject.Parse(json);
            var data = jsonObject.GetValue("data");
            var moreContent = Convert.ToBoolean(jsonObject.GetValue("has_more").ToString());

            // Convert to objects
            var list = JsonConvert.DeserializeObject<List<MemberDTO>>(data.ToString());
            // Convert from transfer object to usable model object
            var convert = Mapper.Map<List<Member>>(list);

            return new ResponseWrapper<List<Member>>(convert, moreContent);
        }
    }
}