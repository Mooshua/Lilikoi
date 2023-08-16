//       ========================
//       Lilikoi::Padlock.cs
//       (c) 2023. Distributed under the MIT License
// 
// ->    Created: 22.12.2022
// ->    Bumped: 06.02.2023
//       ========================
#region

#endregion

namespace Lilikoi.Collection;

/// <summary>
///     A simple class to represent an unlockable object.
///     !! NOT SAFE. !! Do not even think of using this with sensitive code.
/// </summary>
public class Padlock
{
	private Key currentKey;
	private bool isLocked = false;

	public Padlock()
	{
		currentKey = new Key(new object());
	}

	public bool IsLocked()
	{
		return isLocked;
	}

	public void Lock(out Key key)
	{
		if (isLocked)
			throw new Exception("Locked");

		key = currentKey;
		isLocked = true;
	}

	public void Unlock(Key key)
	{
		if (currentKey.Equals(key))
			isLocked = false;
	}

	public struct Key
	{
		private object value;

		public Key(object value)
		{
			this.value = value;
		}

		public bool Equals(Key other)
		{
			return value.Equals(other.value);
		}
	}
}