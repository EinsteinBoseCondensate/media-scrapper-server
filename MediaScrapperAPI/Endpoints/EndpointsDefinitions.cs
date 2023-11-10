using MediaScrapper.Authentication;

namespace MediaScrapper.Endpoints;

public static class EndpointsDefinitions
{
    public static void MapAllEndpoints(this WebApplication app)
    {
        app.MapPost("/videos", Videos.GetAsync);

        app.MapPost("/user-videos/create", UserVideos.CreateAsync)
            .RequireAuthorization(DefaultAuthorizationPolicy.Name);

        app.MapPost("/user-videos/delete", UserVideos.DeleteAsync)
            .RequireAuthorization(DefaultAuthorizationPolicy.Name);

        app.MapGet("/user-videos/read-by-user", UserVideos.ReadByUserAsync)
            .RequireAuthorization(DefaultAuthorizationPolicy.Name);
    }
}
