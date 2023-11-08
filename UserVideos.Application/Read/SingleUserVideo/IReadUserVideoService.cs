using Common.Contracts;

namespace UserVideos.Application.Read.SingleUserVideo;
public interface IReadUserVideoService : IOperationHandler<ReadUserVideoRequest, ReadUserVideoResponse>
{
}
