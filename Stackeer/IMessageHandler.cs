using System.Collections.Immutable;

namespace Stackeer
{
    internal interface IMessageHandler
    {
        ImmutableArray<IMessage> Process(IMessage input);
    }
}