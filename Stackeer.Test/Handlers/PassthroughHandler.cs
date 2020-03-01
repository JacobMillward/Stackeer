using System;
using System.Collections.Immutable;

namespace Stackeer.Test.Handlers
{
    public class PassthroughHandler<T, R> : MessageHandler<T> where T : IMessage where R : IMessage, new()
    {
        public Action<T> Callback { get; set; } = _ => { };

        public PassthroughHandler()
        {
        }

        public PassthroughHandler(Action<T> callback)
        {
            Callback = callback;
        }

        public override ImmutableArray<IMessage>? Process(T input)
        {
            Callback(input);
            return ImmutableArray.Create<IMessage>(new R());
        }
    }
}