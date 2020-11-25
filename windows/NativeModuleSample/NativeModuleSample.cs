using System;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Web.Http;

using Microsoft.ReactNative.Managed;

namespace NativeModuleSample
{
    [ReactModule]
    public sealed class NativeModuleSample
    {
        static async Task GetHttpResponseAsync(string uri, ReactPromise<JSValue> promise)
        {
            // Create an HttpClient object
            var httpClient = new HttpClient();

            // Send the GET request asynchronously
            var httpResponseMessage = await httpClient.GetAsync(new Uri(uri));

            var statusCode = httpResponseMessage.StatusCode;
            var content = await httpResponseMessage.Content.ReadAsStringAsync();

            // Build result object
            var resultObject = new JSValueObject();

            resultObject["statusCode"] = (int)statusCode;
            resultObject["content"] = content;

            promise.Resolve(resultObject);
        }

        [ReactMethod]
        public void GetHttpResponse(string uri, ReactPromise<JSValue> promise)
        {
            var task = GetHttpResponseAsync(uri, promise);
            task.AsAsyncAction().Completed = (action, status) =>
            {
                if (status == AsyncStatus.Error)
                {
                    var error = new ReactError();
                    error.Exception = action.ErrorCode;
                    promise.Reject(error);
                }
            };
        }
    }
}
