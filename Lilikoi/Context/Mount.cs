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

public class Mount : IMount
{
	private TypeDictionary dictionary = new();

	public Mount()
	{
	}

	public Mount(Mount other)
	{
		dictionary = other.dictionary;
	}

	/// <inheritdoc />
	public virtual void Store<T>(T value)
		where T : class
		=> dictionary.Set(value);

	
	/// <inheritdoc />
	public virtual void Remove<T>(T value)
		where T : class
		=> dictionary.Remove(value);
	
	
	/// <inheritdoc />
	public virtual void Remove(Type type)
		=> dictionary.Remove(type);

	
	/// <inheritdoc />
	public virtual T? Get<T>()
		where T : class
		=> dictionary.Get<T>();


	/// <inheritdoc />
	public virtual T? Super<T>(Type super) where T : class
		=> dictionary.Super<T>(super);

	/// <inheritdoc />
	public virtual bool Has<T>()
		=> dictionary.Has<T>();

	/// <inheritdoc />
	public virtual bool Has(Type t)
		=> dictionary.Has(t);

}
