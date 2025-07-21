namespace ApiSale
{
    public class CookieToBearerMiddleware
    {
        private readonly RequestDelegate _next;

        public CookieToBearerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Check if the cookie exists
            if (context.Request.Cookies.TryGetValue("AuthToken", out var token))
            {
                // Add the token as a Bearer token in the Authorization header
                context.Request.Headers["Authorization"] = $"Bearer {token}";
            }

            // Call the next middleware in the pipeline
            await _next(context);
        }
    }
}
