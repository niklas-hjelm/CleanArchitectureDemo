using Application.Services;
using Domain.Entities.Forum;
using Infrastructure.DataAccess.Forum.Documents;
using MongoDB.Driver;

namespace Infrastructure.DataAccess.Forum;

public class ForumRepository : IForumRepository
{
    private readonly IMongoCollection<ForumThreadDocument> _threadCollection;

    public ForumRepository()
    {
        var client = new MongoClient("mongodb://localhost:27017");
        var database = client.GetDatabase("forum");
        _threadCollection = database.GetCollection<ForumThreadDocument>("forumThreads", new MongoCollectionSettings() { AssignIdOnInsert = true });
    }

    public async Task<ForumThreadDocument> GetByIdAsync(string id)
    {
        var thread = await _threadCollection.Find(t => t.Id == id).FirstOrDefaultAsync();
        return thread;
    }

    public async Task<IEnumerable<ForumThreadDocument>> GetAllAsync()
    {
        var threads = await _threadCollection.Find(t => true).ToListAsync();
        return threads;
    }

    public async Task AddAsync(ForumThreadDocument entity)
    {
        await _threadCollection.InsertOneAsync(entity);
    }

    public async Task UpdateAsync(ForumThreadDocument entity)
    {
        await _threadCollection.ReplaceOneAsync(t => t.Id == entity.Id, entity);
    }

    public async Task DeleteAsync(string id)
    {
        await _threadCollection.DeleteOneAsync(t => t.Id == id);
    }

    public async Task AddPostToThread(string threadId, ForumPost post)
    {
        var filter = Builders<ForumThreadDocument>.Filter.Eq(t => t.Id, threadId);
        var update = Builders<ForumThreadDocument>.Update.Push(t => t.Posts, post);
        await _threadCollection.UpdateOneAsync(filter, update);
    }

    public async Task DeletePostFromThread(string threadId, Guid postId)
    {
        var filter = Builders<ForumThreadDocument>.Filter.Eq(t => t.Id, threadId);
        var update = Builders<ForumThreadDocument>.Update.PullFilter(t => t.Posts, Builders<ForumPost>.Filter.Eq(p => p.Id, postId));
        await _threadCollection.UpdateOneAsync(filter, update);
    }

    public async Task UpdatePostInThread(string threadId, ForumPost post)
    {
        var filter = Builders<ForumThreadDocument>.Filter.And(
                       Builders<ForumThreadDocument>.Filter.Eq(t => t.Id, threadId),
                                  Builders<ForumThreadDocument>.Filter.ElemMatch(t => t.Posts, Builders<ForumPost>.Filter.Eq(p => p.Id, post.Id))
                              );
        var update = Builders<ForumThreadDocument>.Update.Set(t => t.Posts[-1], post);
        await _threadCollection.UpdateOneAsync(filter, update);
    }
}