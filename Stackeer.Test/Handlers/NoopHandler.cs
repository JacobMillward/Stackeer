﻿using System;
using System.Collections.Immutable;
using Stackeer.Test.Messages;

namespace Stackeer.Test.Handlers
{
    public class NoopHandler<T> : MessageHandler<T> where T : IMessage
    {
        public Action<T> Callback { get; set; } = _ => { };

        public NoopHandler()
        {
        }

        public NoopHandler(Action<T> callback)
        {
            Callback = callback;
        }

        public override ImmutableArray<IMessage>? Process(T input)
        {
            Callback(input);

            return null;
        }
    }
}