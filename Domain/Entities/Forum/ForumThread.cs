namespace Domain.Entities.Forum;

public class ForumThread : IEntity<string>
{
    public virtual string Id { get; set; }
    public string Title { get; set; }
    public IList<ForumPost> Posts { get; set; }
}