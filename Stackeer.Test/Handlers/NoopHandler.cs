using System;
using System.Collections.Immutable;
using Stackeer.Test.Messages;

namespace Stackeer.Test.Handlers
{
    public class NoopHandler : MessageHandler<DummyMessage>
    {
        public Action<DummyMessage> Callback { get; set; } = _ => { };

        public override ImmutableArray<IMessage> Process(DummyMessage input)
        {
            Callback(input);

            return ImmutableArray.Create<IMessage>();
        }
    }
}