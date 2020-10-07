using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using BlazorWithMongo.Shared;

namespace BlazorWithMongo.Server.Service
{
    public class UserService
    {
        private IMongoCollection<User> _users;
        public UserService(IConfiguration config)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("pratice");
            _users = database.GetCollection<User>("user");
        }

        public List<User> GetUsers() => _users.Find(User => true).ToList();
        public User GetUser(string id) => _users.Find(user => user.Id == id).FirstOrDefault();
        public User PostUser(User user)
        {
            _users.InsertOne(user);
            return user;
        }
        public User PutUser(string id, User newUser)
        {
            _users.ReplaceOne(user => user.Id == id, newUser);
            return newUser;
        }

        public User DeleteUser(string id)
        {
            var oldUser = _users.Find(user => user.Id == id).FirstOrDefault();
            _users.DeleteOne(user => user.Id == id);
            return oldUser;
        }
    }
}