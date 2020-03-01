using System.Collections.Immutable;
using Stackeer.Test.Messages;

namespace Stackeer.Test.Handlers
{
    public class NoopHandler : MessageHandler<DummyMessage>
    {
        public override ImmutableArray<IMessage> Process(DummyMessage input)
        {
            return ImmutableArray.Create<IMessage>();
        }
    }
}