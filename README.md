<center>
<img src="https://raw.githubusercontent.com/Mooshua/Lilikoi/main/Assets/LilikoiBox1080.png" width="256">
<h1>Lilikoi</h1>
</center>

| Main | [![main](https://github.com/Mooshua/Lilikoi/actions/workflows/tests.yml/badge.svg?branch=main)](https://github.com/Mooshua/Lilikoi/actions/workflows/tests.yml) |
|------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Dev  | [![dev](https://github.com/Mooshua/Lilikoi/actions/workflows/tests.yml/badge.svg?branch=dev)](https://github.com/Mooshua/Lilikoi/actions/workflows/tests.yml)   |

> [!WARNING]
> Lilikoi is in active, early development and should not be used unless you accept the
> risk that everything may change or break.

> [!NOTE]
> Lilikoi requires the .NET 7 SDK in order to be compiled, but can be used (and is tested
> on) on any .NET Standard 2.0 platform.

### Status

| Feature             | Status |
|---------------------|--------|
| Field Injection     | ✔️     |
| Parameter Injection | ️️✔️   |
| Hooks ("Wraps")     | ✔️     |
| Contexts ("Mounts") | ✔️     |
| Builder Attributes  | ✔️    |
| Configuration       | 🏗️    |
| Headless/Portable   | 🏗️    |
| Wildcards           | ✔️     |
| Ecosystem           | ⏳      |
| Async/Await         | ⏳      |
| Debugging           | ⏳      |
| NuGet Release       | ✔️    |

### Documentation

Documentation is a work in progress, but here's what we have so far:

- **[Overview](./Docs/overview.md)** -- *Read me first!*
- **[Mounts](./Docs/mounts.md)**
- **[Containers](./Docs/containers.md)**
- **[Attributes](./Docs/attributes.md)**

### What is it?

Lilikoi is a *framework for event handlers*.
It defines a common ground for dependency injection and pre/post behavior.
This allows programmers to write framework agnostic code that runs anywhere Lilikoi runs.

Lilikoi consists of "Containers", which contain a class ("Host") and a method defined on that
class (the "Entry Point").
Using C# reflection APIs, a method is created which injects values into an instance of the host
class and then executes the entry point.

```cs
//  An attribute which injects a new SampleClass into the container
public class SampleInjectionAttribute : LkTypedInjectionAttribute<SampleClass>
{
	public override SampleClass Inject()
	{
		return new SampleClass();
	}
}
```

```cs
//  The host of the container
class Host
{
    [SampleInjection]
    private SampleClass _injectedClass;
  
    //  The entry point of the container
    public Task Entry()
    {
        _injectedClass.DoSomething();
    }
}
```

### What is it good for?

Lilikoi acts as a glue layer between event producers and event handlers.

Lilikoi automates discovery of event handlers and creating an API surface
that an event producer can easily consume. Lilikoi also generates the API surface
expected by the handler at runtime--so both sides of the event handler can use the contracts
they expect.

Lilikoi is used in my project [BitMod](https://github.com/Mooshua/BitMod), where it
glues plugins together with event handlers:

```cs
public class WhitelistHooks
{
	[Config("whitelist")]
	private WhitelistFile _whitelist;

	[BitHook(Priority.LOW)]
	private async Task<Directive> ServerConnectionRequest(GameServerConnectingEventArgs ev)
	{
		foreach (IPAddress allowedConnection in _whitelist.Parse(_logger))
		{
			if (allowedConnection.GetAddressBytes().SequenceEqual(ev.IPAddress.GetAddressBytes()))
			    return Directive.Allow;
		}
		return Directive.Neutral;
	}
}
```

Other plugins and BitMod can then invoke these handlers with a gloriously simple API.
The `BitHook` type places a `RouterAssignments` object in the container during compilation,
which is consumed by BitMod to find containers that handle the same event and group them together.
All in all, it looks like this:

```cs
public override async Task<bool> OnGameServerConnecting(IPAddress arg)
    => _invoker.Hook(new GameServerConnectingEventArgs(arg), defaultValue: false);
```

Overall, Lilikoi can be used in almost all event-handler use cases to provide
consistent and understandable APIs to both the event producer and the consumer.

### Headless

Like injecting things but don't want a full framework?
Lilikoi's headless injectors build minimal `Action<T>`s which behave
similarly to the full framework, giving you full control over the when and where.

### Performance

Lilikoi is designed with performance in mind, so that projects of any scale can benefit from it's
paradigms.

In order to maximize performance and prevent diving into .NET reflection, Lilikoi uses **compiled
expression trees**,
which behave just like a normal method.

Expression trees are no golden ticket to performanceville,
but proper runtime code generation makes Lilikoi's overhead as low as **60ns** per injection

| Framework      | Task              | Speed    |
|:---------------|:------------------|----------|
| .NET CLR       | Inject            | 60 ns    |
| .NET CLR       | Inject *(Debug)*  | 325 ns   |
| .NET Framework | Inject            | 100 ns   |
| .NET CLR       | Compile           | 0.330 ms |
| .NET CLR       | Compile *(Debug)* | 0.015 ms |
| .NET Framework | Compile           | 0.460 ms |
