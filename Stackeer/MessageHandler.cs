﻿using System.Collections.Immutable;

namespace Stackeer
{
    public interface IMessageHandler
    {
        ImmutableArray<IMessage> Process(IMessage input);
    }

    public abstract class MessageHandler<T> : IMessageHandler where T : IMessage
    {
        public ImmutableArray<IMessage> Process(IMessage input) => Process((T)input);

        public abstract ImmutableArray<IMessage> Process(T input);
    }
}