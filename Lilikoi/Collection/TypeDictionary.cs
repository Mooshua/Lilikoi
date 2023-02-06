//       ========================
//       Lilikoi::TypeDictionary.cs
//       (c) 2023. Distributed under the MIT License
// 
// ->    Created: 22.12.2022
// ->    Bumped: 06.02.2023
//       ========================
#region

#endregion

namespace Lilikoi.Collection;

public class TypeDictionary
{
	private Dictionary<Type, object> _underlying;

	private Padlock mutable;

	public TypeDictionary(Dictionary<Type, object> underlying, Padlock mutable)
	{
		_underlying = underlying;
		this.mutable = mutable;
	}

	public TypeDictionary()
	{
		_underlying = new Dictionary<Type, object>();
		mutable = new Padlock();
	}

	public TValue? Get<TValue>()
		where TValue : class
	{
		if (!_underlying.ContainsKey(typeof(TValue)))
			return null;

		return _underlying[typeof(TValue)] as TValue;
	}

	public bool Has<TValue>()
	{
		return _underlying.ContainsKey(typeof(TValue));
	}

	public void Set<TValue>(TValue obj)
	{
		if (mutable.IsLocked())
			throw new Exception("Locked.");

		_underlying[typeof(TValue)] = obj;
	}

	public void Lock(out Padlock.Key key)
	{
		mutable.Lock(out key);
	}

	public void Unlock(Padlock.Key key)
	{
		mutable.Unlock(key);
	}
}