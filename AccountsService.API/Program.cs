using UsersService.API;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAccountsServiceServices(builder.Configuration);
builder.Services.AddControllers(options =>
{
    options.SuppressAsyncSuffixInActionNames = false
    ; options.ReturnHttpNotAcceptable = true;
}).AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore

)
.AddXmlDataContractSerializerFormatters();

var database = builder.Configuration.GetSection("Database").Value;
if (database == "Mongo")
{
    builder.Services.ConfigureMongo(builder.Configuration);
}
else
{
    builder.Services.ConfigureSql(builder.Configuration);
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseHsts();

}
app.UseSwagger();
app.UseSwaggerUI(c =>
c.SwaggerEndpoint("/swagger/v1/swagger.json", "Customers V1"));

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