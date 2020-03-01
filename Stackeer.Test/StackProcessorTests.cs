using FluentAssertions;
using Stackeer.Test.Handlers;
using Stackeer.Test.Messages;
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

        [Fact]
        public void StackProcessor_CallsProcessorWithMessage()
        {
            var processor = new StackProcessor();

            var wasCalled = false;
            var handler = new NoopHandler
            {
                Callback = _ => wasCalled = true
            };

            processor.RegisterHandler(handler);
            processor.AddMessage(new DummyMessage());

            processor.ProcessMessages();

            wasCalled.Should().BeTrue("the message processor should have been called");
        }
    }
}