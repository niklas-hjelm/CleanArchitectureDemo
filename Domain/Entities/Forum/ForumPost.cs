namespace Domain.Entities.Forum;

public class ForumPost : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string Author { get; set; }
    public string Content { get; set; }
}