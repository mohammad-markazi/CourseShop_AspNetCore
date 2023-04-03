using LearnHub.Infrastructure;
using MediatR;
using LearnHub.Application;
using LearnHub.Application.Common;
using Learnhub.Domain.Enums;
using LearnHub.Infrastructure.Identity;
using Mapster;
using LearnHub.Application.Common.Interfaces;
using Dapper;
using Learnhub.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;
using LearnHub.Application.Course.Query;
using Newtonsoft.Json;
using System.Data;
using LearnHub.Infrastructure.Persistence;
using Dapper.FluentMap;
using LearnHub.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddScoped<IFileUploader, FileUploader>();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddMediatR(typeof(Program));
builder.Services.AddApplicationServices();
MappingConfiguration.Config();
//FluentMapper.Initialize(config =>
//{
//    // Configure entities explicitly.
//    config.AddConvention<TypePrefixConvention>()
//        .ForEntity<CourseViewModel>();

//});

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
