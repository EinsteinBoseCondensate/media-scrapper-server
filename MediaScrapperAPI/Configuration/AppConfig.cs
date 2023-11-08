namespace MediaScrapper.Configuration;

public class AppConfig
{
    public Auth0Settings? Auth0SpaSettings { get; set; }
    public MongoDbSettings? MongoDbSettings { get; set; }
}
