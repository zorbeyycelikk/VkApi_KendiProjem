using System.Text.Json;
using Serilog;

namespace VkApi.Middleware;

public class HeartBeatMiddleware
{

    private readonly RequestDelegate next;

    //Bir sonraki değeri tutmak için
    public HeartBeatMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        Log.Information("HeartBeat");
        if (context.Request.Path.StartsWithSegments("/hello"))
        {
            await context.Response.WriteAsync(JsonSerializer.Serialize("Hello from server!"));
            context.Response.StatusCode = 200;
            return;
        }
        //Bunu yazmazsak api'ye dönmez.Root yazılan api'ye gider.Bir nevi blocklanma durumu söz konusu olur
        await next.Invoke(context);
    }
}