# Stackeer
![Build](https://github.com/JacobMillward/Stackeer/workflows/Build/badge.svg)

A basic architectural experiment in message processing. May not amount to anything.

## Concept
Data is represented as a Message (`IMessage`). Handlers (`MessageHandler<T>`) are registered for a specific Message type.
You add one more more messages to the stack, and then start processing. Whilst the queue is not empty, we pop the top message,
retrieve the handler for it, and pass it to the Handler for processing. The handler can return 0 or more new Messages that will
then get pushed onto the stack in order. Processing is complete when the stack is empty.

Eventually, I would like to be able to provide general purpose processors, for example a `JsonMessage<T>` that can be processed by
a handler, to return something like `ParsedJsonMessage<T>`.
