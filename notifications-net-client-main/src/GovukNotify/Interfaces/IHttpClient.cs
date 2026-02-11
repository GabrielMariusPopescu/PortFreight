using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Notify.Interfaces;

public interface IHttpClient : IDisposable
{
    Uri BaseAddress { get; set; }
    Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);

    void SetClientBaseAddress();

    void AddContentHeader(string header);

    void AddUserAgent(string userAgent);
}