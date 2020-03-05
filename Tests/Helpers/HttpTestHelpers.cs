using System;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Tests.Helpers
{
    public static class HttpTestHelpers
    {
        public static async Task<T> DeserializeAsync<T>(this HttpResponseMessage message, JsonSerializerSettings settings = null)
        {
            var content = await message.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content, settings);
        }

        public static async Task<HttpResponseMessage> EnsureSuccess(this Task<HttpResponseMessage> responseTask,
            HttpStatusCode expectedCode = HttpStatusCode.OK)
        {
            var resp = await responseTask;
            if (resp.StatusCode != expectedCode)
            {
                var error = await resp.Content.ReadAsStringAsync();
                throw new AggregateException($"Statuscode {resp.StatusCode} != {expectedCode}: {error}");
            }

            return resp;
        }
    }
}