using Application;
using Application.Interfaces.IRepository;
using Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MongoInfrastructure;
using MongoInfrastructure.Repository;
using Presentation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddPresenationServices();
var database = builder.Configuration.GetSection("Database").Value;
if (database == "Mongo")
{
    builder.Services.AddMongoInfrastructureServices(builder.Configuration);
}
else
{
    builder.Services.AddInfrastrcutureServices(builder.Configuration);
}
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddControllers(options =>
{
    options.SuppressAsyncSuffixInActionNames = false
    ; options.ReturnHttpNotAcceptable = true;
}).AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore

).AddCustomCSVFormatter()
.AddXmlDataContractSerializerFormatters();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSwagger();
app.UseSwaggerUI(c =>
c.SwaggerEndpoint("/swagger/v1/swagger.json", "CustomersAccounts V1"));
app.UseHttpLogging();
app.ConfigureExceptionHandler();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("AllowAll");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
