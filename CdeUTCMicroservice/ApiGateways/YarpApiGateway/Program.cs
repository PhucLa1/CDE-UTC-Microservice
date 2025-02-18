
using BuildingBlocks.Behaviors;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using System.Text;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
#region Tạo cổng proxy
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
#endregion


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


#region Authencation
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddCookie(x =>
                {
                    x.Cookie.Name = "token";
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["X-Access-Token"];
                            return Task.CompletedTask;
                        }
                    };
                });
#endregion


builder.Services.AddRateLimiter(rateLimiterOptions =>
{
    rateLimiterOptions.AddFixedWindowLimiter("fixed", options =>
    {
        options.Window = TimeSpan.FromSeconds(10);
        options.PermitLimit = 5;
    });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowOrigin");
app.UseMiddleware<JwtBehavior>();
app.UseAuthentication();
app.UseAuthorization();
app.MapReverseProxy(proxyPipeline =>
{
    proxyPipeline.Use(async (context, next) =>
    {
        // Thêm X-UserId vào header từ HttpContext.Items
        if (context.Items.ContainsKey("UserId"))
        {
            context.Request.Headers["X-UserId"] = context.Items["UserId"]!.ToString();
            context.Request.Headers["X-UserDateDisplay"] = context.Items["UserDateDisplay"]!.ToString();
            context.Request.Headers["X-UserTimeDisplay"] = context.Items["UserTimeDisplay"]!.ToString();
        }
        await next();
    });
    proxyPipeline.UseAuthentication();
    proxyPipeline.UseAuthorization();
});



app.UseHttpsRedirection();
app.UseRateLimiter();


app.Run();


