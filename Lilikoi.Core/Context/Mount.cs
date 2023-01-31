//       ========================
//       Lilikoi.Core::InvocationContext.cs
//       Distributed under the MIT License.
//
// ->    Created: 22.12.2022
// ->    Bumped: 22.12.2022
//
// ->    Purpose:
//
//
//       ========================
using Lilikoi.Core.Collection;

namespace Lilikoi.Core.Context;

public class Mount
{
	private TypeDictionary Dictionary = new TypeDictionary();

	public virtual void Store<T>(T value)
		where T : class
	{
		Dictionary.Set(value);
	}

	public virtual T? Get<T>()
		where T : class

	{
		return Dictionary.Get<T>();
	}

	public virtual bool Has<T>() => Dictionary.Has<T>();
}
