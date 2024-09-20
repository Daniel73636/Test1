using Microsoft.EntityFrameworkCore;
using test1.Data;
using test1.InterFaces;
using test1.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddDbContext<BaseContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("Connect"),
        ServerVersion.Parse("8.0.20-mysql"),
        mysqlOptions => mysqlOptions.EnableRetryOnFailure()
    )
);

builder.Services.AddScoped<IUsers, UsersServices>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("Cords", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();
app.UseCors("Cords");
app.MapControllers();

app.Run();