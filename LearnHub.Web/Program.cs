using LearnHub.Infrastructure;
using MediatR;
using LearnHub.Application;
using LearnHub.Application.Common;
using Learnhub.Domain.Enums;
using LearnHub.Infrastructure.Identity;
using Mapster;
using LearnHub.Application.Common.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddMediatR(typeof(Program));
builder.Services.AddApplicationServices();
MappingConfiguration.Config();

TypeAdapterConfig<User, UserViewModel>.NewConfig().Map(x => x.TypeName, y => y.Type.GetDisplayName());
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
