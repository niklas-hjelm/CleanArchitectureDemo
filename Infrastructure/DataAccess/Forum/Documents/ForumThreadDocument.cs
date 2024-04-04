using Domain.Entities;
using Domain.Entities.Forum;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Infrastructure.DataAccess.Forum.Documents;

public class ForumThreadDocument : IEntity<string>
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public List<ForumPost> Posts { get; set; }
}