//       ========================
//       Lilikoi.Tests::AllMethodsCalledParameterAttribute.cs
//       Distributed under the MIT License.
//
// ->    Created: 22.12.2022
// ->    Bumped: 22.12.2022
//
// ->    Purpose:
//
//
//       ========================
using Lilikoi.Core.Attributes.Typed;
using Lilikoi.Core.Context;

namespace Lilikoi.Tests.Injections.AllMethodsCalled;

public class AllMethodsCalledParameterAttribute : LkTypedParameterAttribute<object, AllMethodsCalledTest.AllMethodsCalledCounter>
{
	public override object Inject(Mount context, AllMethodsCalledTest.AllMethodsCalledCounter input)
	{
		input.ParameterCalled = true;
		return new object();
	}
}
