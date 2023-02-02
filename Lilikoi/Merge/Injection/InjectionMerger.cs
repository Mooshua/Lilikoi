//       ========================
//       Lilikoi::InjectionMerger.cs
//
// ->    Created: 01.02.2023
// ->    Bumped: 01.02.2023
//
// ->    Purpose:
//
//
//       ========================
using System.Runtime.CompilerServices;

using Lilikoi.Attributes;
using Lilikoi.Context;

namespace Lilikoi.Merge.Injection;

public class InjectionMerger
{
	private List<LkInjectionAttribute> attributes = new();
	private Dictionary<Type, Func<Mount, object>> merged = new();

	/// <summary>
	/// Add this initializer to this merged injection
	/// </summary>
	/// <param name="initializer"></param>
	/// <typeparam name="TAs"></typeparam>
	/// <returns></returns>
	/// <exception cref="InvalidOperationException"></exception>
	public InjectionMerger And<TAs>(Func<Mount, TAs> initializer)
		where TAs : class
	{
		if (merged.ContainsKey(typeof(TAs)))
			throw new InvalidOperationException($"Already contains injection handler for {typeof(TAs).Name}");

		merged.Add(typeof(TAs), initializer as Func<Mount, object> );

		return this;
	}

	/// <summary>
	/// Add this class to this merged injection
	/// </summary>
	/// <typeparam name="TAs"></typeparam>
	/// <returns></returns>
	public InjectionMerger And<TAs>()
		where TAs : class, new()
		=> And<TAs>(mount => new TAs());

	public InjectionMerger And(LkInjectionAttribute attribute)
	{
		attributes.Add(attribute);

		return this;
	}

	public MergedInjectionImpostor Impostor()
	{
		return new MergedInjectionImpostor(this);
	}

	internal bool Has<TAs>(Mount mount)
		where TAs: class
	{
		foreach (var attribute in attributes)
		{
			if (attribute.IsInjectable<TAs>(mount))
				return true;
		}

		return merged.ContainsKey(typeof(TAs));
	}

	internal TAs? Inject<TAs>(Mount mount)
		where TAs: class
	{
		foreach (var attribute in attributes)
		{
			if (attribute.IsInjectable<TAs>(mount))
				return attribute.Inject<TAs>(mount);
		}

		if (merged.TryGetValue(typeof(TAs), out var func))
		{
			return func(mount) as TAs;
		}

		return null;
	}

	internal void Deject<TAs>(Mount mount, TAs value)
		where TAs: class
	{
		foreach (var attribute in attributes)
		{
			if (attribute.IsInjectable<TAs>(mount))
			{
				attribute.Deject(mount, value);
				return;
			}
		}
	}
}
