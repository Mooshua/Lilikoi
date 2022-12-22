//       ========================
//       Miliko.API::Padlock.cs
//       Distributed under the MIT License.
//
// ->    Created: 05.12.2022
// ->    Bumped: 05.12.2022
//
// ->    Purpose:
//
//
//       ========================
using System;

namespace Miliko.API.Collection;

/// <summary>
/// A simple class to represent an unlockable object.
/// !! NOT SAFE. !! Do not even think of using this with sensitive code.
/// </summary>
public class Padlock
{
	public struct Key
	{
		private object value;

		public Key(object value)
		{
			this.value = value;
		}

		public bool Equals(Key other) => value.Equals(other.value);
	}

	private Key currentKey;
	private bool isLocked;

	public Padlock()
	{
		this.currentKey = new Key(new object());
	}

	public bool IsLocked() => isLocked;

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
}
