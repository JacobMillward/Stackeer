using System;
using System.Collections.Generic;
using FluentAssertions;
using Stackeer.Errors;
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

            processor.Invoking(x => x.RegisterHandler(new NoopHandler<DummyMessage>()))
                .Should().NotThrow();
        }

        [Fact]
        public void StackProcessor_CallsProcessorWithMessage()
        {
            var processor = new StackProcessor();

            var wasCalled = false;
            var handler = new NoopHandler<DummyMessage>
            {
                Callback = _ => wasCalled = true
            };

            processor.RegisterHandler(handler);
            processor.AddMessage(new DummyMessage());

            processor.ProcessMessages();

            wasCalled.Should().BeTrue("the message processor should have been called");
        }

        [Fact]
        public void StackProcessor_CallsSuccessiveProcessors()
        {
            var processor = new StackProcessor();

            var processorCallList = new List<string>();

            Action<T> AddToCallList<T>(object x) => new Action<T>(_ => processorCallList.Add(x.GetType().FullName));

            var processor1 = new PassthroughHandler<DummyMessage, DummyMessage2>();
            processor1.Callback = AddToCallList<DummyMessage>(processor1);

            var processor2 = new PassthroughHandler<DummyMessage2, DummyMessage3>();
            processor2.Callback = AddToCallList<DummyMessage2>(processor2);

            var processor3 = new NoopHandler<DummyMessage3>();
            processor3.Callback = AddToCallList<DummyMessage3>(processor3);

            processor.RegisterHandler(processor1);
            processor.RegisterHandler(processor2);
            processor.RegisterHandler(processor3);

            processor.AddMessage(new DummyMessage());
            processor.ProcessMessages();

            processorCallList.Should().ContainInOrder(
                processor1.GetType().FullName,
                processor2.GetType().FullName,
                processor3.GetType().FullName);
        }

        [Fact]
        public void StackProcessor_WhenProcessorThrows_ThrowsStackProcessorException()
        {
            var processor = new StackProcessor();
            var expectedInnerException = new Exception("Test Exception");

            processor.RegisterHandler(new NoopHandler<DummyMessage>(_ => throw expectedInnerException));

            processor.AddMessage(new DummyMessage());

            processor.Invoking(x => x.ProcessMessages())
                .Should().ThrowExactly<StackProcessorException>()
                .Where(ex => ex.InnerException == expectedInnerException);
        }
    }
}