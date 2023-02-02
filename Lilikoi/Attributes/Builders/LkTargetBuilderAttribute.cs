//       ========================
//       Lilikoi.Core::LkTargetBuilderAttribute.cs
//
// ->    Created: 31.01.2023
// ->    Bumped: 31.01.2023
//
// ->    Purpose:
//
//
//       ========================

using Lilikoi.Context;

namespace Lilikoi.Attributes.Builders;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public abstract class LkTargetBuilderAttribute : Attribute
{
	/// <summary>
	/// Create an instance of LkTargetAttribute that will be consumed by Lilikoi.
	/// </summary>
	/// <returns></returns>
	public abstract LkTargetAttribute Build(Mount mount);

	/// <summary>
	/// Declare if the built TargetAttribute will be able to accept a context with the type "TUserContext".
	/// </summary>
	/// <typeparam name="TUserContext"></typeparam>
	/// <returns></returns>
	public abstract bool IsTargetable<TUserContext>()
		where TUserContext: Mount;
}
