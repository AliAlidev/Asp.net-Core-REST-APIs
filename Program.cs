using Catalog.Repositories;
using Catalog.Settings;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

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
builder.Services.AddSingleton<IMongoClient>(ServiceProvider =>
{
    /*
        to make life easyer we tell mongodb client/driver how to serilize a couple of types
        here when driver see any Guid, DateTimeOffset then it will serilize it as string in the database
    */
    BsonSerializer.RegisterSerializer(new GuidSerializer(MongoDB.Bson.BsonType.String));
    BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(MongoDB.Bson.BsonType.String));

    // GetSection(nameof(MongoDbSettings)) convert the returned object to IConfigurationSection
    // we do this by add .Get<MongoDbSettings>()
    var settigns = builder.Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
    // construct IMongoClient instance
    return new MongoClient(settigns.ConnectionString);
});
// builder.Services.AddSingleton<IInMemItesmRepository, InMemItesmRepository>();
// change the implemintaion of the interface to MongoDbRepository
builder.Services.AddSingleton<IInMemItesmRepository, MongoDbItemsRepository>();

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
