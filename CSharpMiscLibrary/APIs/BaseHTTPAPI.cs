using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CSharpMiscLibrary.APIs
{
    /// <summary>
    /// Base class model of a HTTP client.
    /// </summary>
    public class BaseHTTPAPI
    {
        private HttpClient _httpClient = null;

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public BaseHTTPAPI()
        {
            GetHttpClient();
        }

        /// <summary>
        /// Instantiate a http client connection.
        /// </summary>
        /// <returns>HttpClient connection.</returns>
        private HttpClient GetHttpClient()
        {
            if (this._httpClient == null) this._httpClient = new HttpClient();
            return this._httpClient;
        }

        /// <summary>
        /// Set accepted request headers for a HTTP client connection.
        /// </summary>
        /// <param name="mediaType">Type of media accepted (e.g. application/json)</param>
        public void SetRequestHeadersAcceptRules(string mediaType)
        {
            GetHttpClient().DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(mediaType));
        }

        /// <summary>
        /// Set timeout for the HTTP client connection.
        /// </summary>
        /// <param name="timeout">Accepted timeout for the HTTP client connection in TimeSpan.</param>
        public void SetTimeout(TimeSpan timeout)
        {
            GetHttpClient().Timeout = timeout;
        }

        /// <summary>
        /// Call GET method from HTTP client.
        /// </summary>
        /// <param name="uri">URI to be consumed.</param>
        /// <param name="continueOnCapturedContext">Awaiter configuration status to continue or not on captured context</param>
        /// <returns>Async HttpResponseMessage with details of the request response</returns>
        protected async Task<HttpResponseMessage> GetMethod(string uri, bool continueOnCapturedContext = false)
        {
            return await GetHttpClient().GetAsync(uri).ConfigureAwait(continueOnCapturedContext);
        }

        /// <summary>
        /// Call POST method from HTTP client.
        /// </summary>
        /// <param name="uri">URI to be consumed.</param>
        /// <param name="content">Content to be sent.</param>
        /// <param name="continueOnCapturedContext">Awaiter configuration status to continue or not on captured context</param>
        /// <returns>Async HttpResponseMessage with details of the request response</returns>
        protected async Task<HttpResponseMessage> PostMethod(string uri, HttpContent content, bool continueOnCapturedContext = false)
        {
            return await GetHttpClient().PostAsync(uri, content).ConfigureAwait(continueOnCapturedContext);
        }

        /// <summary>
        /// Call PUT method from HTTP client.
        /// </summary>
        /// <param name="uri">URI to be consumed.</param>
        /// <param name="content">Content to be sent.</param>
        /// <param name="continueOnCapturedContext">Awaiter configuration status to continue or not on captured context</param>
        /// <returns>Async HttpResponseMessage with details of the request response</returns>
        protected async Task<HttpResponseMessage> PutMethod(string uri, HttpContent content, bool continueOnCapturedContext = false)
        {
            return await GetHttpClient().PutAsync(uri, content).ConfigureAwait(continueOnCapturedContext);
        }

        /// <summary>
        /// Call DELETE method from HTTP client.
        /// </summary>
        /// <param name="uri">URI to be consumed.</param>
        /// <param name="continueOnCapturedContext">Awaiter configuration status to continue or not on captured context</param>
        /// <returns>Async HttpResponseMessage with details of the request response</returns>
        protected async Task<HttpResponseMessage> DeleteMethod(string uri, bool continueOnCapturedContext = false)
        {
            return await GetHttpClient().DeleteAsync(uri).ConfigureAwait(continueOnCapturedContext);
        }
    }
}
