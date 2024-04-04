using Application.Services;
using Domain.Entities.Forum;
using Infrastructure.DataAccess.Forum.Documents;

namespace Infrastructure.DataAccess.Forum;

public interface IForumRepository : IGenericRepository<ForumThreadDocument, string>
{
    Task AddPostToThread(string threadId, ForumPost post);
    Task DeletePostFromThread(string threadId, Guid postId);
    Task UpdatePostInThread(string threadId, ForumPost post);
}