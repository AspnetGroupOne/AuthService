using AuthServiceProvider;
using AuthServiceProvider.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options => { options.AddPolicy("AllowAll", x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()); });

builder.Services.AddScoped<IAuthService, AuthService>();

var accountServiceProviderUri = builder.Configuration["AccountServiceProvider"]!; 
//builder.Services.AddGrpcClient<AccountGrpcService.AccountGrpcServiceClient>(x =>
//{
//    x.Address = new Uri(accountServiceProviderUri);
//})
//    .ConfigurePrimaryHttpMessageHandler(() =>
//    {
//        return new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler());
//    });


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
