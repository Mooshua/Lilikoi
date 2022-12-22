//       ========================
//       Miliko.Tests::AllMethodsCalledInject.cs
//       Distributed under the MIT License.
//
// ->    Created: 19.12.2022
// ->    Bumped: 19.12.2022
//
// ->    Purpose:
//
//
//       ========================
using System.Runtime.CompilerServices;

namespace Miliko.Tests.Injections.AllMethodsCalled;

public class AllMethodsCalledInject
{

	[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
	public bool IsNotNull() => true;

}
