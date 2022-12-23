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

	public void Store<T>(T value)
		where T : class
	{
		Dictionary.Set(value);
	}

	public T? Get<T>()
		where T : class

	{
		return Dictionary.Get<T>();
	}
}
