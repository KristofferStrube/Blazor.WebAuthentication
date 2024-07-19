using KristofferStrube.Blazor.WebAuthentication.API;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(o => o.AddPolicy("default",
    builder =>
        builder.WithOrigins("https://localhost:7203",
            "https://kristofferstrube.github.io")
        .AllowAnyMethod()
        .AllowAnyHeader()
    ));

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    _ = app
        .UseSwagger()
        .UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("default");

app.MapWebAuthenticationAPI();

app.Run();