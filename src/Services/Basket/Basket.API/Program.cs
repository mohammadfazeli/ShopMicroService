using Basket.API.GrpsServices;
using Basket.API.Repositories.Inerface;
using Basket.API.Repositories.Repository;
using Discount.Grpc.Protos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddStackExchangeRedisCache(option =>
{
    option.Configuration = builder.Configuration.GetValue<string>("ConnectionStrings:DataBaseConnection");
    option.ConfigurationOptions = new StackExchange.Redis.ConfigurationOptions()
    {
        AbortOnConnectFail = false,
        EndPoints = { { "172.17.0.1", 6379 }, { "172.17.0.2", 6379 }, { "localhost", 6379 } },
    };
});


builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();

builder.Services.AddGrpcClient<DiscountServiceProto.DiscountServiceProtoClient>(options =>
{
    options.Address = new Uri(builder.Configuration.GetValue<string>("GrpcSetting:DiscountUrl"));
});
builder.Services.AddScoped<DiscountGrpsService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();