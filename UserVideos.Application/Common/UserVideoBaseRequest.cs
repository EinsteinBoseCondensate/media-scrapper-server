namespace UserVideos.Application.Common;
public abstract class UserVideoBaseRequest : UserBasedUserVideoBaseRequest
{
    public Guid Id { get; set; }
}
public abstract class UserBasedUserVideoBaseRequest
{
    public string? UserId { get; set; }
}
