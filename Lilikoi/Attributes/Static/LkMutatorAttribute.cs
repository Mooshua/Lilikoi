//       ========================
//       Lilikoi.Core::LkMutatorAttribute.cs
//
// ->    Created: 31.01.2023
// ->    Bumped: 31.01.2023
//
// ->    Purpose:
//
//
//       ========================

using Lilikoi.Compiler.Public;

namespace Lilikoi.Attributes.Static;

[AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
public abstract class LkMutatorAttribute : Attribute
{
	public abstract void Mutate(LilikoiMutator mutator);
}
