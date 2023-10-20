using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Provider;
using WebApiTest.Domain.Models;

namespace WebApiTest.Infrastructure;

public class TestDBContext : MongoDBContext
{
    public TestDBContext(IOptions<MongoDBSetting> settings) : base(settings)
    { }

    public IMongoCollection<Product> Producs => Collection<Product>();
}
