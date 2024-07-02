using TechnicalTest.Api.Infrastracture.Middlewares;
using TechnicalTest.Infrastructure;
using TechnicalTest.Application;
using TechnicalTest.Api.Infrastracture.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationLayer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddInfrastructureLayer(builder.Configuration);
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
