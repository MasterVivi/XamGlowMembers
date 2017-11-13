using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using ModernHttpClient;
using MvvmCross.Platform.Platform;

namespace Oneview.Connect
{
	/// <summary>
	/// Authenticated http client handler passing basic string as auth token
	/// </summary>
	/// <remarks>
	/// Implementation of ModernHttpClient NativeMessageHandler.
	/// This allows us to have extra control around the calling of APIs
    /// and the returning of exceptions.
    /// Passing a string token for now, usually would pass a function that
    /// would perform a task of fectching a token
	/// </remarks>
	/// <see cref="https://github.com/paulcbetts/ModernHttpClient"/> 
	public class AuthenticatedHttpClientHandler : NativeMessageHandler
	{
        private readonly string _token;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Oneview.Connect.AuthenticatedHttpClientHandler"/> class.
		/// </summary>
		/// <param name="token">token.</param>
        public AuthenticatedHttpClientHandler(string token)
		{
            _token = token;
		}

		/// <summary>
		/// Sends the request async.
		/// </summary>
		/// <returns>The async response.</returns>
		/// <param name="request">Request.</param>
		/// <param name="cancellationToken">Cancellation token.</param>
		/// <remarks>Injects Auth header into request</remarks>
		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			MvxTrace.TaggedTrace(MvxTraceLevel.Diagnostic, "HttpClient", "Making request: " + request.RequestUri);
			MvxTrace.TaggedWarning("ConnectTrace.Network", string.Format("Request headers: '{0}'", request.Headers));

			return await ExecuteRequest(request, cancellationToken);
		}

		/// <summary>
		/// Executes the request.
		/// </summary>
		/// <returns>The response.</returns>
		/// <param name="request">Request.</param>
		/// <param name="cancellationToken">Cancellation token.</param>
		private async Task<HttpResponseMessage> ExecuteRequest(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			// See if the request has an authorize header
			var auth = request.Headers.Authorization;
			if (auth != null)
			{
                request.Headers.Authorization = new AuthenticationHeaderValue(auth.Scheme, _token);
			}

			HttpResponseMessage response;
			try
			{
				response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
			}
			catch (TaskCanceledException tce)
			{
				MvxTrace.TaggedWarning("HttpHandler", "Http Task Cancelled: " + request.RequestUri);
				throw new TimeoutException(tce.Message);
			}
			catch (Exception ex)
			{
				// unhandled exception
				throw new HttpRequestException(HttpStatusCode.InternalServerError.ToString(), ex);
			}

			// handle an unsuccessful call
			if (!response.IsSuccessStatusCode)
			{
				MvxTrace.TaggedWarning("***[ExecuteRequest]***", string.Format("Request URL:{0} - Status Code:{1}: {2}", request.RequestUri, response.StatusCode, response.ReasonPhrase));
				switch (response.StatusCode)
				{
					case HttpStatusCode.NotModified:
					case HttpStatusCode.BadRequest: // 400
					case HttpStatusCode.Unauthorized: // 401 - unauthenticated. request a new token
					case HttpStatusCode.Forbidden: // 403
					case HttpStatusCode.NotFound: // 404
					case HttpStatusCode.Conflict: // 409
					case HttpStatusCode.NotAcceptable: // 40
					case HttpStatusCode.RequestTimeout: // 408
					case HttpStatusCode.Gone: // 410 - api version out of date
					case HttpStatusCode.RequestedRangeNotSatisfiable: //416
					case HttpStatusCode.InternalServerError: //500
					case HttpStatusCode.BadGateway: //502
					case HttpStatusCode.ServiceUnavailable: //503
					default:
							 // no retry for other exceptions, let caller handle
						throw new HttpRequestException(response.StatusCode.ToString());
				}
			}

			return response;
		}
	}
}