using UserVideos.Domain;

namespace UserVideos.Application.Create;
public static class CreateUserVideoExtensions
{
    public static UserVideo ToUserVideo(this CreateUserVideoRequest request)
        => new()
        {
            ChannelTitle = request.ChannelTitle,
            DurationFormatted = request.DurationFormatted,
            Id = request.Id,
            ThumbnailUrl = request.ThumbnailUrl,
            Title = request.Title,
            Url = request.Url,
            UserId = request.UserId,
            VideoId = request.VideoId
        };
}
