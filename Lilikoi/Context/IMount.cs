//       ========================
//       Lilikoi::IMount.cs
//       (c) 2023. Distributed under the MIT License
//
// ->    Created: 13.08.2023
// ->    Bumped: 13.08.2023
//       ========================
namespace Lilikoi.Context;

public interface IMount
{
	/// <summary>
	/// Store a single object in this mount.
	/// The location where it is stored depends on the
	/// generic parameter passed
	/// </summary>
	/// <param name="value"></param>
	/// <typeparam name="T"></typeparam>
	void Store<T>(T value)
		where T : class;

	/// <summary>
	/// Get a single object, based on the generic parameter
	/// provided.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	T? Get<T>()
		where T : class;

	/// <summary>
	/// Get a subclass of a generic specified
	/// by the passed type.
	/// </summary>
	/// <param name="super"></param>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	T? Super<T>(Type super)
		where T : class;

	/// <summary>
	/// Check if an object exists in the mount
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	bool Has<T>();

	/// <summary>
	/// Check if an object exists in the mount
	/// </summary>
	/// <param name="t"></param>
	/// <returns></returns>
	bool Has(Type t);
}
