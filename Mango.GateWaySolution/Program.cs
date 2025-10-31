using Mango.GateWaySolution.Extensions;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.AddAppAuthentication();
builder.Configuration.AddJsonFile("ocelot.json", optional : false, reloadOnChange:true);
builder.Services.AddOcelot(builder.Configuration);


var app = builder.Build();

app.MapGet("/", () => "Hello World!");

// Only run Ocelot for non-root requests
app.MapWhen(ctx => ctx.Request.Path != "/", subApp =>
{
    subApp.UseOcelot().GetAwaiter().GetResult();
});

app.Run();