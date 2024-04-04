using Domain.Entities.Forum;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Infrastructure.DataAccess.Forum.Documents;

public class ForumThreadDocument : ForumThread
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public override string Id { get; set; }
}