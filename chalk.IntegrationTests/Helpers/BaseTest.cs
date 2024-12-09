namespace chalk.IntegrationTests.Helpers;

public class BaseTest(WebApplicationFactory<Program> factory, ITestOutputHelper logger)
    : IClassFixture<WebApplicationFactory<Program>>
{
    protected ITestOutputHelper Logger { get; } = logger;
    protected HttpClient HttpClient { get; } = factory.CreateClient();
}