namespace chalk.IntegrationTests;

[CollectionDefinition("IntegrationTests", DisableParallelization = true)]
public class DatabaseCollection : ICollectionFixture<IntegrationTestFactory>;

[Collection("IntegrationTests")]
public class IntegrationTest(IntegrationTestFactory factory, ITestOutputHelper logger)
    : IClassFixture<IntegrationTestFactory>
{
    protected ITestOutputHelper Logger { get; } = logger;

    protected HttpClient HttpClient { get; } = factory.CreateClient(new WebApplicationFactoryClientOptions
    {
        HandleCookies = true,
        BaseAddress = new Uri("http://localhost:8080")
    });
}