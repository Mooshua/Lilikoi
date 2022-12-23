# Lilikoi
A C# framework for frameworks

> **Warning**: Lilikoi is in active, early development and should not be used unless you accept the risk that everything may change or break.

### Status

| Feature | Status |
| ------- | ------ |
| Property Injection | ✔️ |
| Parameter Injection | 🏗️ |
| Hooks ("Wraps") | 🏗️ |
| Contexts ("Mounts") | ⏳ |
| Builder Attributes | ⏳ |
| Configuration | ⏳ |
| NuGet Release | ⏳ |

### What is it?

Lilikoi is a *framework for frameworks*. 
It defines a common ground for dependency injection and pre/post behavior.
This allows programmers to write framework agnostic code that runs anywhere Lilikoi runs.

Lilikoi consists of "Containers", which contain a class ("Host") and a method defined on that class (the "Entry Point").
Using C# reflection APIs, a method is created which injects values into an instance of the host class and then executes the entry point.

The possibilities of a framework using Lilikoi are endless. 

```cs
public class SampleInjectionAttribute : MkTypedInjectionAttribute<SampleClass>
{
	public override SampleClass Inject()
	{
		return new SampleClass();
	}
}
```
```cs
class Program
{
    [SampleInjection]
    public SampleClass InjectedClass { get; set; }
  
    //  Entry point
    public Task Entry()
    {
        InjectedClass.DoSomething();
    }
}
```

### Performance

Lilikoi is designed with performance in mind, so that projects of any scale can benefit from it's paradigms.

In order to maximize performance and prevent diving into .NET reflection, Lilikoi uses **compiled expression trees**,
which behave just like a normal method.

Expression trees are no golden ticket to performanceville, 
but proper runtime code generation makes Lilikoi's overhead as low as **40ns** 
(yes, nanoseconds) per injection.

### What could finished Lilikoi look like?

Lilikoi could be used in any framework which heavily uses event-based programming,
such as games (or game mods!), discord bots, HTTP servers (ASP.NET), 
RPC servers, automation tools, and more. 

A finished version of Lilikoi could look like this from the standpoint of an application developer:

```cs
[Controller("/users")]
public class ApiController
{
    /// DbContext is instantiated by a generic new() injector attribute
    /// EF Core will take care of the rest.
    [New]
    public ApiContext Db { get; set; }
    
    /// You can imagine anything to go here--Because anything can!
    /// Lilikoi should be customizable to your heart's content.
    /// No more arbitrary framework restrictions!
    [ApiService]
    public UserService Users { get; set; }

    /// POSTAttribute is a "builder" attribute
    /// with a method (similar to Inject()) that describes how to build the container to Lilikoi.
    /// A HTTP server could use this to gather a list of routes and Lilikoi containers associated with them!
    [POST("/message")]
    /// BodyAttribute is a "wrap" attribute
    /// which defines methods to be executed before or after the entry point (in this case, parsing the body)
    [Body(BodyType.Json)]
    /// You can define your own application code here to authenticate and authorize the users.
    [Authenticate(UserType.Commenter)]
    /// FromBodyAttribute is a "parameter inject" attribute
    /// which accepts a context ("mount") from the wraps and builder attributes to provide additional parameters and abstractions.
    public Response PostMessage([JsonBody] PostMessageRequest request)
    {
        var success = Users.NewMessage(request.ToMessage());
        
        if (success)
          return Response.Success();
          
        return Response.Error();
    }
}
```
