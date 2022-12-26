using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDo.Interfaces;
using ToDo.Models.DataContexts;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddMvc();
builder.Services.AddDbContext<ToDoDataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ToDoConnectionString")));
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ToDoDataContext>();
builder.Services.AddScoped<IToDoRepository, ToDoDataRespository>();

var app = builder.Build();

app.UseFileServer();
app.UseRouting();
app.UseAuthentication();

app.MapControllerRoute("default", "{Controller=Home}/{Action=Index}/{id?}");

//app.MapGet("/", () => "Hello World!");

app.Run();
