using TmyFlower.Service;
using TmyFlower.Service.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ��ӿ������
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("CorsPolicy", opt => opt.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().WithExposedHeaders("X-Pagination"));
//});

// ע��AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperConfigs));

// ע��Service�����
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

// ʹ�ÿ������
//app.UseCors("CorsPolicy");

app.Run();
