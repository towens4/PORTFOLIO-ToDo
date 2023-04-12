using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Session;
using ToDo.Interfaces;
using ToDo.Models.DataContexts;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Logging.AddDebug();
builder.Logging.AddConsole();

builder.Services.AddMvc();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddDbContext<ToDoDataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ToDoConnectionString")),
    ServiceLifetime.Scoped);
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ToDoDataContext>();
builder.Services.AddScoped<IToDoRepository, ToDoDataRespository>();

var app = builder.Build();


app.UseDeveloperExceptionPage();

app.UseStaticFiles();

app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute("default", "{Controller=Assignment}/{Action=Index}/{id?}");

//app.MapGet("/", () => "Hello World!");

app.Run();
