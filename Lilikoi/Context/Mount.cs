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

	/// <summary>
	/// Store a single object in this mount.
	/// The location where it is stored depends on the
	/// generic parameter passed
	/// </summary>
	/// <param name="value"></param>
	/// <typeparam name="T"></typeparam>
	public virtual void Store<T>(T value)
		where T : class
		=> dictionary.Set(value);


	/// <summary>
	/// Get a single object, based on the generic parameter
	/// provided.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	public virtual T? Get<T>()
		where T : class
		=> dictionary.Get<T>();


	/// <summary>
	/// Get a subclass of a generic specified
	/// by the passed type.
	/// </summary>
	/// <param name="super"></param>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	public virtual T? Super<T>(Type super) where T : class
		=> dictionary.Super<T>(super);

	/// <summary>
	/// Check if an object exists in the mount
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	public virtual bool Has<T>()
		=> dictionary.Has<T>();

	/// <summary>
	/// Check if an object exists in the mount
	/// </summary>
	/// <param name="t"></param>
	/// <returns></returns>
	public virtual bool Has(Type t)
		=> dictionary.Has(t);

}
