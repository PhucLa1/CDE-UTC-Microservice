using Project.API;
using Project.Application;
using Project.Infrastructure;
using User.Grpc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
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
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseApiServices();
app.MapControllers();



app.Run();


