using AuthServiceProvider;
using AuthServiceProvider.Services;
using Grpc.Net.Client.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddCors(options => { options.AddPolicy("AllowAll", x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()); });

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddGrpcClient<AccountGrpcService.AccountGrpcServiceClient>(x =>
{
    x.Address = new Uri(builder.Configuration["Providers:AccountServiceProvider"]!);
})
    .ConfigurePrimaryHttpMessageHandler(() =>
    {
        return new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler());
    });


var app = builder.Build();
app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI(x =>
{
    x.SwaggerEndpoint("/swagger/v1/swagger.json", "Ventixe AuthServiceProvider API");
    x.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
