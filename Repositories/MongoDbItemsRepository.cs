using Catalog.Entities;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using MongoDB.Bson;
using MongoDB.Driver;

/*
    add reference to MongoDb by nuget package
    > dotnet add package MongoDb.Driver
*/
namespace Catalog.Repositories
{
    public class MongoDbItemsRepository : IInMemItesmRepository
    {
        /*
            add dependency injection for MongoDb Client like: IMongoClient mongoClient
            MongoDb use collections to assosiate all the entites together
            define variable of type IMongoCollection
            define MongoDb database name
            define collection name
            add reference to MongoDb database inside constructor
            add reference to collection
        */

        /*
            where is MongoDb database?
            we can add MongoDb database by two methods:
            -   install MongoDb from the MongoDb installer
            -   run database as a part of docker continer
        */

        /*
            Docker Package:
            it is a standalone package of software that includes every thing to run an applciation.
            this application in our case is MongoDb database.
            when we exeucte this docker image it consists what we called docker container
            docker container is a runtime instance of a docker image
            docker container will run in docker engine
            to import docker engine to our box we need to insatll docker engine from offical website
            after install docker engine we can import any public containers inside docker like MongoDb
            when finish installing do this steps:
            from terminal run command:
            > docker run -d --rm --name mongo -p 27017:27017 -v mongodbdata:/data/db mongo
            -d mean we don't want to attach to the proccess
            --rm to destroy the container after proccess finish
            --name the name of the image
            -p open a port number
                right 27017 it is the mongoDb port
                left 27017 it is the external port we can change it
            -v volumn used to store the data added to mongoDb when used the docker container
                if we don't add volumn then all data will be lose when we start again docker
                we link our local path mongodbdata to the default mongoDb path in docker /data/db
                mongo is the name of th image
            
            after add docker image we need to point to this image so we need to write a little bit of
            configuration to appsetting.json:
            -   "MongoDbBetting": {
                "host": "localhost",
                "post": "27017"
            }
            in order to read this config in our service the best way is to define a class 
            create Settings/MongoDbSettings.cs
            we add this config as properties in the new class
            calculate connection string from properity like this we define read only property
            public string ConnectionString {get { return $"mongodb://{}:{}" }}
            after that we want to register MongoDb Client in service provider
        */

        private const string databaseName = "catalog";
        private const string collectionName = "items";

        // define filter property and initilize it, after that we have refernece to filter object
        private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;

        private readonly IMongoCollection<Item> itemsCollection;

        public MongoDbItemsRepository(IMongoClient mongoClient)
        {
            // add reference to mongoDb database
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            // add reference to collection
            itemsCollection = database.GetCollection<Item>(collectionName);
        }

        public void CreateItem(Item item)
        {
            itemsCollection.InsertOne(item);
        }

        public void DeleteItem(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.id, id);
            itemsCollection.DeleteOne(filter);
        }

        public Item GetItem(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.id, id);
            return itemsCollection.Find(filter).SingleOrDefault();
        }

        public IEnumerable<Item> GetItems()
        {
            return itemsCollection.Find(new BsonDocument()).ToList();
        }

        public void UpdateItem(Item item)
        {
            var filter = filterBuilder.Eq(existencItem => existencItem.id, item.id);
            itemsCollection.ReplaceOne(filter, item);
        }
    }
}