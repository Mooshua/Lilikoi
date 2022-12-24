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
    public Interface Injection { get; set }
    
    public Output EntryPoint(Input input, [ParameterInjection] Interface parameterInjection)
    {
        return new Output();
    }
}
```
