//       ========================
//       Miliko.Core::MkContainer.cs
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
using System.Collections.Generic;
using System.Reflection;

using Miliko.API.Attributes.Builders;

namespace Miliko.Attributes.Containers;

/// <summary>
/// A container consists of three things:
///
/// 1.  The container class, which is the "execution environment" containing
///		injected parameters and all sub-functions
/// 2.	The entry point, which defines the
/// 3.	Argument and return value.
///
/// This class wraps those things into a common metadata structure for Miliko analysis.
/// </summary>
public class Container
{

	internal Type ContainerType { get; private set; }
	internal MethodInfo EntryPoint { get; set; }

	internal List<MkInjectionBuilderAttribute> Injections { get; set; }
	internal List<MkWrapBuilderAttribute> Wraps { get; set; }

}
