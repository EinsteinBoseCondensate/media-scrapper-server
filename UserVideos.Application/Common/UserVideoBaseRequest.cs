using Common.Models;

namespace UserVideos.Application.Common;
public abstract class UserVideoBaseRequest : UserIdBasedRequest
{
    public Guid Id { get; set; }
}
