using TmyFlower.Service;
using TmyFlower.Service.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 添加跨域策略
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("CorsPolicy", opt => opt.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().WithExposedHeaders("X-Pagination"));
//});

// 注册AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperConfigs));

// 注册Service层服务
builder.Services.AddTransient<IFlowerService, FlowerService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ICustomJWTService, CustomJWTService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

// 使用跨域策略
//app.UseCors("CorsPolicy");

app.Run();
