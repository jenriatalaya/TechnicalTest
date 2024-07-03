using TechnicalTest.Api.Infrastracture.Middlewares;
using TechnicalTest.Infrastructure;
using TechnicalTest.Application;
using TechnicalTest.Api.Infrastracture.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationLayer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddInfrastructureLayer(builder.Configuration);

builder
    .Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
            )
        };
    });

builder.Services.AddControllers();
builder.Services.AddSwaggerWithVersioning();
builder.Services.AddAnyCors();

var app = builder.Build();



app.UseAnyCors();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSwaggerWithVersioning();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseMiddleware<OrganizationResolverMiddleware>();
app.MapControllers();

app.Run();
