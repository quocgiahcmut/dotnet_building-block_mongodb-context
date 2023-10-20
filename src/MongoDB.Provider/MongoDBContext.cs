namespace MongoDB.Provider;

public class MongoDBContext
{
    private readonly IMongoDatabase _database = null;

    public MongoDBContext(IOptions<MongoDBSetting> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        if (client != null)
        {
            _database = client.GetDatabase(settings.Value.Database);
        }
    }
    
    public IMongoDatabase Database 
    { 
        get { return _database; } 
    }

    public IMongoCollection<TModel> Collection<TModel>()
    {
        return _database.GetCollection<TModel>(typeof(TModel).Name.ToLower());
    }

    public IMongoCollection<TModel> Collection<TModel>(string name)
    {
        return _database.GetCollection<TModel>(name);
    }
}
