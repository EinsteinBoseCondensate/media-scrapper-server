namespace MediaScrapper.Configuration;

public class MongoDbSettings
{
    public string UserVideosDatabaseName { get; init; } = "UserVideosDatabase";
    public string? ConnectionString  { get; init; }
}
