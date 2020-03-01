using FluentAssertions;
using Stackeer.Test.Handlers;
using Xunit;

namespace Stackeer.Test
{
    public class StackProcessorTests
    {
        [Fact]
        public void StackProcessor_CanRegisterHandler()
        {
            var processor = new StackProcessor();

            processor.Invoking(x => x.RegisterHandler(new NoopHandler()))
                .Should().NotThrow();
        }
    }
}