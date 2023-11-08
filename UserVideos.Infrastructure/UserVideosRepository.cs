using Common.Extensions;
using Common.Implementations;
using MongoDB.Driver;
using UserVideos.Domain;
using UserVideos.Domain.Interfaces;

namespace UserVideos.Infrastructure;
public class UserVideosRepository : MongoRepository<UserVideo>, IUserVideoRepository
{
    public UserVideosRepository(IMongoDatabase database) : base(database, typeof(UserVideosRepository).SafeName()) { }
}
