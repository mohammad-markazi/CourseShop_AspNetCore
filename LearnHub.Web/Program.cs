using LearnHub.Application.CourseCategory.Query;
using LearnHub.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using System.Reflection;
using LearnHub.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddMediatR(typeof(Program));
builder.Services.AddApplicationServices();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapAreaControllerRoute("default", "Administration", "{controller=Home}/{action=Index}/{id?}");
app.Run();
