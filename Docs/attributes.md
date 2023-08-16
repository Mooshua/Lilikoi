
# Attributes

Lilikoi uses attributes to statically describe the injection environment. 
Attributes are *highly* dynamic--You can use whatever APIs you wish to describe and build them.

The general structure of Lilikoi's built in attributes is:

1. A root attribute, called `Lk___BuilderAttribute`
2. A common attribute, called `Lk___Attribute`, inheriting from `Lk___BuilderAttribute`
3. A statically-typed attribute, called `LkTyped___Attribute`, inheriting from `Lk___Attribute`.

The `Lk___Builder` contains functions, such as:
- `Build()`, which returns a `Lk___Attribute` to be used by the injection runtime **(this is called once per injection!)**
- `Is____<T>()`, which returns a boolean describing whether or not the input/output types are known to this builder 
  and compatible with it's `Build()` output.
  This is called once per method compilation, so it's output should be deterministic or constant.

The `Lk___Attribute` contains generic functions which are expected to return or accept it's generics.
For example, `Inject` attributes have:
- `T Inject<T>()`, which returns the inputted type,
- `Deject<T>(T)`, which can be used for disposing the produced type.

`Lk___Attributes` inherit from `Lk___Builder`, but use a dummy build function which returns `this`. As such, **fields and properties are not safe to modify.**

This is why the `Is` methods are important--Once we have entered the injection routine, 
we perform no checks as we believe the `Is<T>` methods have statically validated the injection.

Finally, the `LkTyped___Attribute<T...>` wraps `Lk___Attribute` to form a familiar interface to most developers. 
They no longer have to worry about compatibility, but rather returning an instance of a single type. Yay!

Since it behaves similarly to `Lk___Attribute`, **fields and properties are not safe to modify.**

As an example, the typed `Inject<TInjectable>` attribute has a much cleaner interface:
- `TInject Inject()`
- `Deject(TInject)`

You are welcome to build your own versions of these, or, quite frankly, do whatever you want.
These attributes are intended to give maximum flexibility while sticking to a rigid type system.

### The Rules:
1. The `Build()` function on a `Builder` may be called at any time, even when not injecting, but must **always** return values that behave the same
2. The result of the `Build()` value must accept any generic parameters that receive a `true` response from the corresponding `Is<T>` call.
3. You should not use any writable/mutable properties or fields in your attribute unless you pass them as a result to your `Build()` function.

> **Note**: `Build()`'s result does not actually have to be constant--It is evaluated every injection invocation
> specifically so that temporary values may be reliably isolated.

**Invalid**:
```cs
class MyAttribute : Lk___Attribute
{
    int temporaryValue = 0;
    
    public override T ___<T>()
    {
        //  WARNING: We are writing to the global state!
        temporaryValue++;
    }
}
```

**Valid**:
```cs
class MyBuilderAttribute : Lk___BuilderAttribute
{
    public override Lk___Attribute Build()
    {
        return new MyAttribute();
    }
}

class MyAttribute : Lk___Attribute
{
    int temporaryValue = 0;
    public override T ___<T>()
    {
        //  SAFE: We are writing to a local state.
        temporaryValue++;
    }
}
```

## Field Injection

Field injection is the simplest form of injection to understand. 
They are universal and will work anywhere their return type is supported (and when the mount is set up correctly) as they are
not exposed to any container-specific types.
When a container is invoked, any fields with your attribute will have the following calls made:

- `Builder.Build()`, to retrieve a temporary instance of an `LkInjectionAttribute`
- `Injector.Inject<T>(Mount mount)`, to retrieve the value to be injected as a property,
- Executing the "Entry point"
- `Injector.Deject<T>(Mount mount, T value)`, to perform any (optional!) disposal logic.

### Example

```cs
class ToBeInjected
{
    [MyInjectable]
    public MyInterface MyImpl;
}
```
Results in the following calls:
- `MyInjectable.Build()`
- `BuildResult.Inject<MyInterface>(Mount mount)`
- `ToBeInjected.MyImpl = InjectResult`
- Execution...
- `BuildResult.Deject<MyInterface>(Mount mount, MyInterface InjectResult)`

## Parameter Injection

Parameter injection augments the input of a container's entry point by allowing parameters to be
sourced from the container's input and compile-time mount.

Parameters have two types:
1. **Attribute-defined**, where the presence of an attribute declares that attribute is responsible for it's sourcing, and
2. **Wildcard-defined**, where the presence of a specific type as a parameter declares a certain subroutine as responsible for it's sourcing.

Currently, Lilikoi only exposes attribute-defined parameter injection (and uses wildcard defined injection for the `input` type),
but wildcard functionality may be exposed to builder attributes.

Unlike standard property injection, **parameter injection is exposed to the input type of the container.** While ignoring this is possible,
you must ensure compatibility with the input you are attempting to use.

Parameter injection has the following steps:
- `Builder.Build()` is called
- `BuilderResult.Inject<TResult,TInput>(mount, input)` is called
- The `TResult` is placed as a parameter to the entry point.

### Example
```cs
void MethodToBeInjected([MyInject] string injectedString)
{
}
```
Results in the following calls:
- `MyInject.Build()`
- `BuildResult.Inject<string, ContainerInput>(containerInput)`
- `MethodToBeInjected(InjectResult)`

## Builders
> **Note**: This feature has not yet been created. This is purely speculative.

The presence of a builder attribute should do two things:
1. Act as an attribute which declares a method as an entry point when Lilikoi is managing the start of a program
2. When an entry point is compiled, the builder should oversee the compilation process (eg, injecting it's own wildcards or wraps)

## Wraps (Hooks)

Wraps allow you to execute before or after the entry point which modifies the input or output, or prevents the entry point
from running altogether.

> [!IMPORTANT]
> Wraps *do not* support property injection by default.
> 
> You can use `LilikoiInjector.Inject(mount, this)` and `LilikoiInjector.Deject(mount, this)` to emulate standard injection in the before and after methods, respectively.
> 
> (If you do this, it is **highly** recommended to call `Deject` at the end of your `After` function as the injections may rely on this to prevent leaks.)
