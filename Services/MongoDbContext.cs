using JLITE.API.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace JLITE.API.Services
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MongoDB") 
                ?? "mongodb+srv://Jlite:Jlite1234@cluster0.0hahy.mongodb.net/JLiteDB";
            var databaseName = configuration["MongoDB:DatabaseName"] ?? "JLiteDB";

            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<Product> Products => _database.GetCollection<Product>("Products");
        public IMongoCollection<Category> Categories => _database.GetCollection<Category>("Categories");
        public IMongoCollection<HeroBanner> HeroBanners => _database.GetCollection<HeroBanner>("HeroBanners");
        public IMongoCollection<ProjectItem> Projects => _database.GetCollection<ProjectItem>("Projects");
        public IMongoCollection<ClientLogo> Clients => _database.GetCollection<ClientLogo>("Clients");
        public IMongoCollection<ContactSubmission> ContactSubmissions => _database.GetCollection<ContactSubmission>("ContactSubmissions");
        public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
        public IMongoCollection<Order> Orders => _database.GetCollection<Order>("Orders");
        public IMongoCollection<ServiceRequest> ServiceRequests => _database.GetCollection<ServiceRequest>("ServiceRequests");
        public IMongoCollection<QuoteRequest> QuoteRequests => _database.GetCollection<QuoteRequest>("QuoteRequests");
        public IMongoCollection<PolicyContent> Policies => _database.GetCollection<PolicyContent>("Policies");
    }
}
