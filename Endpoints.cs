namespace HttpClientTestProject;

public sealed class Endpoints: IEndpointDefinition
{
    public void DefineEndpoints(WebApplication app)
    {
        RouteGroupBuilder group = app.MapGroup("/some-data");

        group.MapPost("/post", HandlePost);

    }

    private async Task<IResult> HandlePost(SomeData data, PlaceholderService service)
    {
        IResult result = await service.PostSomeData(data);

        return result;
    }
}
