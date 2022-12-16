var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();

var app = builder.Build();

app.UseFileServer();
app.UseRouting();
app.UseAuthentication();

app.MapGet("/", () => "Hello World!");

app.Run();
