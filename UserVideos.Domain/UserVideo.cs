using Common.Contracts;

namespace UserVideos.Domain;
public class UserVideo : IEntity
{
    public string? Url { get; set; }
    public string? Title { get; set; }
    public string? ThumbnailUrl { get; set; }
    public string? DurationFormatted { get; set; }
    public string? ChannelTitle { get; set; }
    public string? UserId { get; set; }
    public string? VideoId { get; set; }
    public Guid Id { get; set; }
}
