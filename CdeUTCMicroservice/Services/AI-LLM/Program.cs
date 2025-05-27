using AI_LLM.Data;
using AI_LLM.Repository;
using AI_LLM.Service;
using AI_LLM.Setting;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AILLMDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AILLMDBContext")));
builder.Services.Configure<GeminiApiSetting>(builder.Configuration.GetSection("GeminiApi"));
builder.Services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IGeminiService, GeminiService>();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers(); // Bắt buộc phải gọi để ánh xạ các controller

app.UseHttpsRedirection();

app.Run();