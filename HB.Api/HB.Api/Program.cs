using HB.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddCors();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<HBContext>();
builder.Services.AddScoped<IWeatherRespository, WeatherRepository>();
builder.Services.AddScoped<ICalendarRespository, CalendarRepository>();
builder.Services.AddScoped<IPhotoRespository, PhotoRepository>();
builder.Services.AddScoped<ISettingsRespository, SettingsRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();
// }

app.UseHttpsRedirection();

app.UseCors(builder => builder
    .AllowAnyHeader()
    .AllowAnyMethod()
    .SetIsOriginAllowed((host) => true)
    .AllowCredentials()
);

app.UseAuthorization();

app.MapControllers();

app.Run();
