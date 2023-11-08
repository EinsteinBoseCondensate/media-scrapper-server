using Common.Models;
using UserVideos.Domain;

namespace UserVideos.Application.Read.SingleUserVideo;
public class ReadUserVideoResponse : BaseResponse
{
    public UserVideo? Item { get; set; }
}
