namespace HttpClientTestProject;

public sealed class PlaceholderService
{
    private readonly HttpClient client;

    public PlaceholderService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<IResult> PostSomeData(SomeData data)
    {
        using HttpResponseMessage responseMessage = await client.PostAsJsonAsync<SomeData>("/posts", data);

        responseMessage.EnsureSuccessStatusCode();
        SomeData? someData = await responseMessage.Content.ReadFromJsonAsync<SomeData>();

        return Results.Ok(someData);
    }
}
