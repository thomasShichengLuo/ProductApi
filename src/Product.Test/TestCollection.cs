using Xunit;

namespace Framecad.Nexa.MyFramecad.Tests
{
    [CollectionDefinition(nameof(TestCollection))]
    public class TestCollection : ICollectionFixture<TestServerFixture>
    {

    }
}
