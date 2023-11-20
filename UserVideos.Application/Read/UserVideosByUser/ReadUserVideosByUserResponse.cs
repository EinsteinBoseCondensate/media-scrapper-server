using Common.Models;
using UserVideos.Domain;

namespace UserVideos.Application.Read.UserVideos;

public class ReadUserVideosByUserResponse : BaseResponse
{
    public List<UserVideo>? Items { get; set; }

}