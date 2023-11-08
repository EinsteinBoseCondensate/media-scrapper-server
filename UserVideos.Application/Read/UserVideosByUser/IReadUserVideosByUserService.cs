using Common.Contracts;
using UserVideos.Application.Read.UserVideos;

namespace UserVideos.Application.Read.UserVideosByUser;
public interface IReadUserVideosByUserService : IOperationHandler<ReadUserVideosByUserRequest, ReadUserVideosByUserResponse>
{
}
