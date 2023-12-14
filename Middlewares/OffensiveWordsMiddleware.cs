using System.Text;

namespace DotnetSteps.Middlewares
{
    public class OffensiveWordsMiddleware
    {
        private readonly RequestDelegate _next;
        public OffensiveWordsMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            var hasAuthenticationHeader = context.Request.Headers.ContainsKey("Authorization");

            if (context.Request.Method.Equals("PUT") || context.Request.Method.Equals("POST") && !hasAuthenticationHeader)
            {
                string requestBody;
                using (StreamReader reader = new StreamReader(context.Request.Body, Encoding.UTF8, true, 1024, true))
                {
                    requestBody = await reader.ReadToEndAsync();
                }
                if (ContainsSwearWords(requestBody))
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    context.Response.ContentType = "application/json";
                    var errorMessage = "{\"message\": \"The request contains inappropriate content.\"}";
                    await context.Response.WriteAsync(errorMessage, Encoding.UTF8);
                    return;
                }
                context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(requestBody));
            }
            await _next(context);
        }
        private bool ContainsSwearWords(string text)
        {
            //third library de var bunun için
            return text.Contains("küfür", StringComparison.OrdinalIgnoreCase);
        }
    }
}