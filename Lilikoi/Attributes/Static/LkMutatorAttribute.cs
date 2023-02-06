//       ========================
//       Lilikoi::LkMutatorAttribute.cs
//       (c) 2023. Distributed under the MIT License
// 
// ->    Created: 31.01.2023
// ->    Bumped: 06.02.2023
//       ========================
#region

using Lilikoi.Compiler.Public;

#endregion

namespace Lilikoi.Attributes.Static;

[AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
public abstract class LkMutatorAttribute : Attribute
{
	public abstract void Mutate(LilikoiMutator mutator);
}