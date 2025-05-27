using BuildingBlocks.Behaviors;
using Microsoft.AspNetCore.StaticFiles;
using Project.API;
using Project.Application;
using Project.Infrastructure;
using Project.Infrastructure.Hubs;
using User.Grpc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact", builder =>
    {
        builder.WithOrigins("http://localhost:3000") // Origin của React
               .AllowAnyMethod() // Bao gồm POST, OPTIONS, GET, etc.
               .AllowAnyHeader()
               .AllowCredentials(); // Cần cho WebSocket
    });
});
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ProjectIdFilter>();
});
#region CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder => builder
            .WithOrigins("http://127.0.0.1:5500", "http://localhost:3000") // Điền vào tên miền của dự án giao diện của bạn
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials() // Cho phép sử dụng credentials (cookies, xác thực)
    );
});
#endregion
//Add services to the container
builder.Services
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices(builder.Configuration);

builder.Services.AddGrpcClient<UserProtoService.UserProtoServiceClient>(options =>
{
    options.Address = new Uri("https://localhost:5050");
})
.ConfigureAdditionalHttpMessageHandlers((handlers, serviceProvider) =>
{
    var handler = new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    };
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //await app.IntialiseDatabaseAsync();
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseCors("AllowOrigin");
app.UseStaticFiles(new StaticFileOptions
{
    ContentTypeProvider = new FileExtensionContentTypeProvider
    {
        Mappings = { [".gltf"] = "model/gltf+json" }
    }
});
app.UseHttpsRedirection();
app.UseApiServices();
app.UseCors("AllowReact"); // Trước MapHub
app.MapHub<AnnotationHub>("/annotation-hub");
app.MapControllers();



app.Run();


