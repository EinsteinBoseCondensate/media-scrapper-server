using UserVideos.Application.Common;

namespace UserVideos.Application.Create;
public class CreateUserVideoRequest : UserVideoBaseRequest
{
    public string? Url { get; set; }
    public string? Title { get; set; }
    public string? ThumbnailUrl { get; set; }
    public string? DurationFormatted { get; set; }
    public string? ChannelTitle { get; set; }
    public string? VideoId { get; set; }
}
