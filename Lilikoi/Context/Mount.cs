//       ========================
//       Lilikoi::Mount.cs
//       (c) 2023. Distributed under the MIT License
// 
// ->    Created: 22.12.2022
// ->    Bumped: 06.02.2023
//       ========================
#region

using Lilikoi.Collection;

#endregion

namespace Lilikoi.Context;

public class Mount
{
	private TypeDictionary dictionary = new();

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

	public virtual bool Has<T>()
	{
		return dictionary.Has<T>();
	}
}