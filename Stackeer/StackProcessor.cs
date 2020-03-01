using System;
using System.Collections.Generic;

namespace Stackeer
{
    public class StackProcessor
    {
        private readonly Stack<IMessage> MessageStack = new Stack<IMessage>();
        private readonly Dictionary<Type, IMessageHandler> RegisteredHandlers = new Dictionary<Type, IMessageHandler>();

        public void RegisterHandler<T>(MessageHandler<T> processor) where T : IMessage => RegisteredHandlers.Add(typeof(T), processor);

        public void AddMessage(IMessage msg) => MessageStack.Push(msg);

        public void ProcessMessages()
        {
            while (MessageStack.Count > 0)
            {
                var msg = MessageStack.Pop();

                var handler = GetProcessor(msg);

                var output = handler.Process(msg);
                if (output is null)
                {
                    continue;
                }

                foreach (var outputMsg in output)
                {
                    MessageStack.Push(outputMsg);
                }
            }
        }

        private IMessageHandler GetProcessor(IMessage msg) => RegisteredHandlers[msg.GetType()];
    }
}