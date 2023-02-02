# Containers

Containers consist of the following components:

1. A **mount**, which is supplied at compile time, and is constant across invocations of the container.
2. An **input**, which can change from invocation to invocation
3. An **entry point**, a method to be called
4. An **output**, the return type of the entry point.

> **Note**: The output (entry point return) cannot be `void`. This functionality may be added in the future.


```cs
public class Host
{
    [Factory]
    public Interface Injection;
    
    public Output EntryPoint(Input input, [ParameterInjection] Interface parameterInjection)
    {
        return new Output();
    }
}
```

## Mounts

A "mount" is a type-keyed dictionary which is passed to all invocations of the same container instance.
A mount can be used for configuration, injecting values, or for storing factories to be used by attributes.

It is **highly recommended** (and may soon become a requirement) that all input types derive from the `Mount` type
so that users and libraries have ways to hurl opaque data around the entry point of a container.

> **Idea**: If you use parameter injection, you really don't need any other input type than `Mount`.
> By using parameter attributes, you can bundle all of your input into a mount and retrieve it for the entry point invocation.



## Steps

1. Injection attributes present on host
3. Wrap before execution (Possible halt)
3. Parameter injection
4. Entry point
5. Wrap after execution
7. Dejection of host
