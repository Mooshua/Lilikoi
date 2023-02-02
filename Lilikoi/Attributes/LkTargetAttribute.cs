//       ========================
//       Lilikoi.Core::LkTargetAttribute.cs
//
// ->    Created: 31.01.2023
// ->    Bumped: 31.01.2023
//
// ->    Purpose:
//
//
//       ========================
using Lilikoi.Attributes.Builders;
using Lilikoi.Compiler.Public;
using Lilikoi.Context;

namespace Lilikoi.Attributes;

public abstract class LkTargetAttribute : LkTargetBuilderAttribute
{

	public sealed override LkTargetAttribute Build(Mount mount)
	{
		return this.MemberwiseClone() as LkTargetAttribute;
	}

	/// <summary>
	/// Determine if this method should be included in the UserContext's scan.
	/// </summary>
	/// <param name="context"></param>
	/// <param name="mutator"></param>
	/// <typeparam name="TUserContext"></typeparam>
	/// <returns></returns>
	public virtual bool IsTargetedBy<TUserContext>(TUserContext context, LilikoiMutator mutator)
		where TUserContext: Mount
		=> true;

	/// <summary>
	/// Mutate the usercontext and lilikoi container.
	/// Only called if IsTargetedBy returns true.
	/// </summary>
	/// <param name="context"></param>
	/// <param name="mutator"></param>
	/// <typeparam name="TUserContext"></typeparam>
	public abstract void Target<TUserContext>(TUserContext context, LilikoiMutator mutator)
		where TUserContext: Mount;

}
