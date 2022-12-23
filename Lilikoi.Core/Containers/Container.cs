//       ========================
//       Lilikoi.Core::Container.cs
//       Distributed under the MIT License.
//
// ->    Created: 22.12.2022
// ->    Bumped: 22.12.2022
//
// ->    Purpose:
//
//
//       ========================
#region

using System;
using System.Collections.Generic;
using System.Reflection;

using Lilikoi.Core.Attributes.Builders;

#endregion

namespace Lilikoi.Core.Containers;

/// <summary>
///     A container consists of three things:
///     1.  The container class, which is the "execution environment" containing
///     injected parameters and all sub-functions
///     2.	The entry point, which defines the
///     3.	Argument and return value.
///     This class wraps those things into a common metadata structure for Lilikoi analysis.
/// </summary>
public class Container
{
	internal Type ContainerType { get; private set; }

	internal MethodInfo EntryPoint { get; set; }

	internal List<MkInjectionBuilderAttribute> Injections { get; set; }

	internal List<MkWrapBuilderAttribute> Wraps { get; set; }
}
