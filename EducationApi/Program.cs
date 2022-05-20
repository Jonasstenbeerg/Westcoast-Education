using EducationApi.Data;
using EducationApi.Helpers;
using EducationApi.Interfaces;
using EducationApi.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Context that uses SQLite
builder.Services.AddDbContext<EducationContext>(options => {
    options.UseSqlite(builder.Configuration.GetConnectionString("SQLite"));
});

//Context that uses SQL
// builder.Services.AddDbContext<EducationContext>(options => {
// options.UseSqlServer(builder.Configuration.GetConnectionString("SQL"));
// });

//AutoMapper instance for DI
builder.Services.AddScoped<ICourseRepository, CourseRepository>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

try
{
   await SeedDb.PopulateDb(app);
}
catch (Exception ex)
{
    using(var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var logger = services.GetRequiredService<ILogger<Program>>();

        logger.LogError(ex,"Något gick med att populera databasen");

    }
}

app.Run();
