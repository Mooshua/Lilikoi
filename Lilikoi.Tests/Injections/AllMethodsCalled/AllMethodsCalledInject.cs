//       ========================
//       Lilikoi.Tests::AllMethodsCalledInject.cs
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

using System.Runtime.CompilerServices;

#endregion

namespace Lilikoi.Tests.Injections.AllMethodsCalled;

public class AllMethodsCalledInject
{
	[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
	public bool IsNotNull()
	{
		return true;
	}
}