namespace RentAMovie.Services
{
	public class CustomMiddleware
	{
		private readonly RequestDelegate next;

		public CustomMiddleware(RequestDelegate next)
		{
			this.next = next;
		}

		public async Task InvokeAsync(HttpContext httpContext)
		{
			if (httpContext.Request.Query.ContainsKey("SomeQuery"))
			{
                await next(httpContext);
            }
            else
			{
			    await httpContext.Response.WriteAsync("No query, access denied.");
			}
		}
	}
}
