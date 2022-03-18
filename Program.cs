using Catalog.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/// <summary>
/// add singleton mean there is one copy of the instance of a type accross the life time of the service 
/// when we create it will be reusable whenever we want
/// first add interface after that the concret instance
/// this is how to register dependincy
/// </summary>
/// <returns></returns>
builder.Services.AddSingleton<IInMemItesmRepository, InMemItesmRepository>();

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

app.Run();
