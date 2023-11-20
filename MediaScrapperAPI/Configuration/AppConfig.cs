namespace MediaScrapper.Configuration;

public class AppConfig
{
    public bool UseLegitSslCertificate { get; set; }
    public Auth0Settings? Auth0SpaSettings { get; set; }
    public Auth0M2MSettings? Auth0M2MSettings { get; set; }
    public MongoDbSettings? MongoDbSettings { get; set; }
}
