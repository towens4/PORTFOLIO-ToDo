using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDo.Models.DataContexts;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddMvc();
builder.Services.AddDbContext<ToDoDataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ToDoConnectionString")));
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ToDoDataContext>();

var app = builder.Build();

app.UseFileServer();
app.UseRouting();
app.UseAuthentication();

app.MapGet("/", () => "Hello World!");

app.Run();
