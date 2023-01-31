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

using System;

using Lilikoi.Core.Compiler.Public;

namespace Lilikoi.Core.Attributes.Static;

[AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
public abstract class LkMutatorAttribute : Attribute
{
	public abstract void Mutate(LilikoiMutator mutator);
}
