namespace chalk.IntegrationTests.Tests;

public class UserIntegrationTests(IntegrationTestFactory factory, ITestOutputHelper logger)
    : IntegrationTest(factory, logger)
{
    private readonly IntegrationTestFactory _factory = factory;

    [Fact]
    public void Test()
    {
    }
}