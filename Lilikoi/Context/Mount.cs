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
using Lilikoi.Collection;

namespace Lilikoi.Context;

public class Mount
{
	private TypeDictionary dictionary = new TypeDictionary();

	public Mount()
	{

	}

	public Mount(Mount other)
	{
		dictionary = other.dictionary;
	}

	public virtual void Store<T>(T value)
		where T : class
	{
		dictionary.Set(value);
	}

	public virtual T? Get<T>()
		where T : class

	{
		return dictionary.Get<T>();
	}

	public virtual bool Has<T>() => dictionary.Has<T>();
}
