
using System.Text.Json;

namespace BuildingWebApisWithAspNet.Middlewares
{
    public class SampleMiddleware : IMiddleware
    {
        public SampleMiddleware()
        {

        }
        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var text = JsonSerializer.Serialize(context.Request.Headers);
            Console.WriteLine(text);

            return next.Invoke(context);
        }
    }
}
