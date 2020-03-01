using System;
using System.Collections.Generic;
using Stackeer.Errors;

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

                if (msg is Error error)
                {
                    if (error.ShouldContinue)
                    {
                        continue;
                    }

                    throw new StackProcessorException(error);
                }

                var handler = GetProcessor(msg);
                try
                {
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
                catch (Exception ex)
                {
                    MessageStack.Push(new Error(innerException: ex));
                }
            }
        }

        private IMessageHandler GetProcessor(IMessage msg) => RegisteredHandlers[msg.GetType()];
    }
}